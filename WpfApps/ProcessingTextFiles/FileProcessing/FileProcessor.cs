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

        public static event GuidEventHandler OnCancelled;
        public static event GuidEventHandler OnStarted;

        static int bufferSize = 4096;
        public static bool Start(IEnumerable<string> pathes, string prefix, CustomCancellationToken cancelToken, IFileProcessingStrategy fileProcessingStrategy, int maxwordSize ) 
        {
            Task task = Task.Run(() => FileProcessor_TaskMethod(pathes, prefix, cancelToken, fileProcessingStrategy, maxwordSize));            
            return true; 
        }

        static async void FileProcessor_TaskMethod(IEnumerable<string> pathes, string prefix, CustomCancellationToken token, IFileProcessingStrategy fileProcessingStrategy, int maxWordSize)
        {
            OnStarted?.Invoke(null, new(token.Id));
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
                            token.Wait();

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

                            
                            
                            byte[] proccessedData;
                            int proccessedBytesCout = 0;

                            proccessedBytesCout = fileProcessingStrategy.Process(buffer, out proccessedData, ref unknowPart, ref unknowPartLength, maxWordSize);
                                                        
                            outputStream.Write(proccessedData, 0, proccessedBytesCout);
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
    }
}
