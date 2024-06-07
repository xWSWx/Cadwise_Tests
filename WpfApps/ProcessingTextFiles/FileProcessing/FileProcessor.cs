using ProcessingTextFiles.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        static int bufferSize = 4096;
        public static bool Start(IEnumerable<string> pathes, CancellationTokenWrapper cancelToken) 
        {
            Task task = Task.Run(() => FileProcessor_TaskMethod(pathes, cancelToken), cancelToken.Token);            
            return false; 
        }

        static async void FileProcessor_TaskMethod(IEnumerable<string> pathes, CancellationTokenWrapper namedToken)
        {
            OnProgress?.Invoke(null, new ProcessingArgs(namedToken.Id, 0));
            string outputFilePath = String.Empty;

            var TotalSize = 0L;
            var currentByteProgress = 0L;
            var ProgressPercent = 0;
            foreach(var inputFilePath in pathes) 
            {
                if (string.IsNullOrEmpty(inputFilePath))
                {
                    OnError(null, new StringGuidEventArgs(inputFilePath, namedToken.Id));
                    continue;
                }

                if (!File.Exists(inputFilePath))
                {
                    OnError(null, new StringGuidEventArgs(inputFilePath, namedToken.Id));
                    continue;
                }

                FileInfo fileInfo = new FileInfo(inputFilePath);
                TotalSize += fileInfo.Length;
            }

            foreach (var inputFilePath in pathes) 
            {
                if (string.IsNullOrEmpty(inputFilePath))
                {
                    OnError(null, new StringGuidEventArgs(inputFilePath, namedToken.Id));
                    continue;
                }

                if (!File.Exists(inputFilePath))
                {
                    OnError(null, new StringGuidEventArgs(inputFilePath, namedToken.Id));
                    continue;
                }

                outputFilePath = GetOutputFileName(inputFilePath);

                try
                {
                    using (FileStream inputStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
                    using (FileStream outputStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                    {
                        byte[] buffer = new byte[bufferSize];
                        int bytesRead;

                        while ((bytesRead = inputStream.Read(buffer, 0, bufferSize)) > 0)
                        {
                            //ProcessData(buffer, bytesRead);

                            outputStream.Write(buffer, 0, bytesRead);

                            currentByteProgress += bytesRead;
                            ProgressPercent = (int)((currentByteProgress * 100) / TotalSize);
                            OnProgress?.Invoke(null, new ProcessingArgs(namedToken.Id, ProgressPercent));
                        }
                    }
                    OnFileDone?.Invoke(null, new StringGuidEventArgs(inputFilePath, namedToken.Id));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                    OnError(null, new StringGuidEventArgs(inputFilePath, namedToken.Id));
                }

            }

        }

        private static string GetOutputFileName(string inputFilePath)
        {
            try
            {
                string directory = Path.GetDirectoryName(inputFilePath);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(inputFilePath);
                string extension = Path.GetExtension(inputFilePath);
                return Path.Combine(directory, $"{fileNameWithoutExtension}.Edited-{Guid.NewGuid()}{extension}");
            }catch(Exception ex) { return string.Empty; }
        }
    }
}
