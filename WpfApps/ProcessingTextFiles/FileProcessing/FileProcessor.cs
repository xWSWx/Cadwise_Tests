using DynamicData;
using ProcessingTextFiles.ViewModels.Controls;
using ProcessingTextFiles.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static ProcessingTextFiles.CustomEvents.CustomEventHandlers;

namespace ProcessingTextFiles.FileProcessing
{
    public static class FileProcessor
    {
        //Закончили обработку файла
        public static event StringGuidEventHandler OnFileDone;
        //Закончили обработку всех файлов
        public static event GuidEventHandler OnDone;
        //Ошибка обработки файлов
        public static event StringGuidEventHandler OnError;
        
        public static event ProcessingHandler OnProgress;

        public static event GuidEventHandler OnPaused;
        public static event GuidEventHandler OnCancelled;
        static int bufferSize = 4096;
        public static bool Start(IEnumerable<string> pathes, string prefix, CustomCancellationToken cancelToken, FileActions action, int maxwordSize ) 
        {
            Task task = Task.Run(() => FileProcessor_TaskMethod(pathes, prefix, cancelToken, action, maxwordSize));            
            return true; 
        }

        static async void FileProcessor_TaskMethod(IEnumerable<string> pathes, string prefix, CustomCancellationToken token, FileActions action, int maxWordSize)
        {
            List<string> processedFiles = new List<string>();
            OnProgress?.Invoke(null, new (token.Id, 0));
            string outputFilePath = string.Empty;

            var TotalSize = 0L;
            var currentByteProgress = 0L;
            var ProgressPercent = 0;
            foreach(var inputFilePath in pathes) 
            {
                if (string.IsNullOrEmpty(inputFilePath))
                {
                    OnError(null, new (inputFilePath, token.Id));
                    continue;
                }

                if (!File.Exists(inputFilePath))
                {
                    OnError(null, new (inputFilePath, token.Id));
                    continue;
                }

                FileInfo fileInfo = new (inputFilePath);
                TotalSize += fileInfo.Length;
            }

            foreach (var inputFilePath in pathes) 
            {
                if (string.IsNullOrEmpty(inputFilePath))
                {
                    OnError(null, new (inputFilePath, token.Id));
                    continue;
                }

                if (!File.Exists(inputFilePath))
                {
                    OnError(null, new (inputFilePath, token.Id));
                    continue;
                }

                outputFilePath = GetOutputFileName(inputFilePath, prefix);

                try
                {
                    using (FileStream inputStream = new (inputFilePath, FileMode.Open, FileAccess.Read))
                    using (FileStream outputStream = new (outputFilePath, FileMode.Create, FileAccess.Write))
                    {
                        byte[] buffer = new byte[bufferSize];
                        int bytesRead;

                        byte[] unknowPart = new byte[maxWordSize];
                        int unknowPartLength=0;
                        while ((bytesRead = inputStream.Read(buffer, 0, bufferSize)) > 0)
                        {
                            if (token.Cancelled)
                            {
                                outputStream.Close();
                                try { File.Delete(outputFilePath); } catch { }
                                foreach (var a in processedFiles)
                                {
                                    try {File.Delete(a);}catch { }
                                }
                                OnCancelled?.Invoke(null, new(token.Id));
                                return;
                            }

                            if (token.Paused)
                            {
                                OnPaused?.Invoke(null, new(token.Id));
                                while (token.Paused)
                                {
                                    Thread.Sleep(1000);
                                }
                            }


                            byte[] proccessedData;
                            int proccessedBytesCout = 0;
                            switch (action)
                            {
                                case FileActions.removePunctuation:
                                    proccessedBytesCout = RemovePunctuation(buffer, out proccessedData);
                                    break;
                                case FileActions.RemoveWordsLessThan:
                                    proccessedBytesCout = RemoveShortWords(buffer, maxWordSize, out proccessedData, ref unknowPart, ref unknowPartLength);
                                    
                                    break;
                                default:
                                    proccessedData = buffer;
                                    proccessedBytesCout = bytesRead;
                                    break;
                            }


                            //outputStream.Write(buffer, 0, bytesRead);
                            outputStream.Write(proccessedData, 0, proccessedBytesCout);

                            //Thread.Sleep(bytesRead);

                            currentByteProgress += bytesRead;
                            ProgressPercent = (int)((currentByteProgress * 100) / TotalSize);
                            OnProgress?.Invoke(null, new (token.Id, ProgressPercent));
                        }
                    }
                    processedFiles.Add(inputFilePath);
                    OnFileDone?.Invoke(null, new (inputFilePath, token.Id));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                    OnError?.Invoke(null, new (inputFilePath, token.Id));
                    ////////////////
                    //// Кастыль на тему... чтобы не было OnDone елcи единственный был с ошибкой.
                    if (pathes.Count() == 1)
                        return;
                }

            }
            OnDone?.Invoke(null, new(token.Id));
        }

        public static string GetOutputFileName(string inputFilePath, string prefix)
        {
            try
            {
                string directory = Path.GetDirectoryName(inputFilePath);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(inputFilePath);
                string extension = Path.GetExtension(inputFilePath);
                return Path.Combine(directory, $"{prefix}{fileNameWithoutExtension}{extension}");
            }catch(Exception ex) { return string.Empty; }
        }
        public static int RemovePunctuation(byte[] indata, out byte[] outdata)
        {

            var chars = Encoding.UTF8.GetChars(indata);
            StringBuilder textAccumulator = new StringBuilder();
            outdata = new byte[indata.Length];            

            foreach(var c in chars)
            {                
                if (char.IsPunctuation(c))
                    continue;

                textAccumulator.Append(c);                
            }
            outdata = Encoding.UTF8.GetBytes(textAccumulator.ToString());            
                        
            return outdata.Length;
        }

        public static int RemoveShortWords(byte[] indata, int shouldLessWordLength, out byte[] outdata, ref byte[] unknownPart, ref int unknownPartLength)
        {
            var chars = Encoding.UTF8.GetChars(indata);
            var unknownPartChars = Encoding.UTF8.GetChars(unknownPart);
            
            // Initialize the accumulators
            StringBuilder wordAccumulator = new StringBuilder();
            StringBuilder textAccumulator = new StringBuilder();

            // Process the unknown part, if any
            foreach(var c in unknownPartChars)
            {
                if (c == '\0')
                    break;
                wordAccumulator.Append(c);                
            }


            
            bool isLimitReachedFirstTimeForCurrentWord = true;
            bool isLimitReached = false;
            foreach(var c in chars)
            {                                
                if (char.IsLetter(c))
                {
                    if (isLimitReached)
                    {
                        if (isLimitReachedFirstTimeForCurrentWord)
                        {
                            isLimitReachedFirstTimeForCurrentWord = false;
                            textAccumulator.Append(wordAccumulator);
                            wordAccumulator.Clear();
                        }
                        textAccumulator.Append(c);
                        continue;
                    }

                    wordAccumulator.Append(c);

                    if (wordAccumulator.Length < shouldLessWordLength) 
                    {
                        continue;
                    }

                    isLimitReachedFirstTimeForCurrentWord = true;
                    isLimitReached = true;                                                                               
                }
                else
                {
                    isLimitReachedFirstTimeForCurrentWord = false;
                    isLimitReached = false;
                    var isLittleWord = wordAccumulator.Length < shouldLessWordLength;
                    if (isLittleWord)
                    {
                        wordAccumulator.Clear();                           
                    }
                    textAccumulator.Append(c);
                }
            }
            unknownPartLength = wordAccumulator.Length;
            unknownPart = Encoding.Unicode.GetBytes(wordAccumulator.ToString());            
            outdata = Encoding.UTF8.GetBytes(textAccumulator.ToString());
            return outdata.Length;
        }
    }
}
