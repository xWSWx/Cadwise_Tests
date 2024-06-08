using System;
using System.IO;
using System.Text;

namespace LargeFileGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: LargeFileGenerator <file_path> <file_size_in_bytes>");
                return;
            }

            string filePath = args[0];
            long fileSizeInBytes;

            if (!long.TryParse(args[1], out fileSizeInBytes) || fileSizeInBytes <= 0)
            {
                Console.WriteLine("Invalid file size. Please provide a positive integer value for file size in bytes.");
                return;
            }

            GenerateLargeFile(filePath, fileSizeInBytes);
        }

        static void GenerateLargeFile(string filePath, long fileSizeInBytes)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string punctuation = " .,;:!?\"'()[]{}<>-";
            Random random = new Random();
            long bytesWritten = 0;

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
            {
                while (bytesWritten < fileSizeInBytes)
                {
                    int wordLength = random.Next(1, 21);
                    StringBuilder word = new StringBuilder();

                    // Generate a word with random characters
                    for (int i = 0; i < wordLength; i++)
                    {
                        word.Append(chars[random.Next(chars.Length)]);
                    }

                    // Append punctuation randomly
                    if (random.Next(0, 5) == 0)
                    {
                        word.Append(punctuation[random.Next(punctuation.Length)]);
                    }

                    // Append space at the end of the word
                    word.Append(' ');

                    string wordString = word.ToString();
                    writer.Write(wordString);
                    bytesWritten += Encoding.UTF8.GetByteCount(wordString);

                    // Check if we need to add a new line
                    if (random.Next(0, 10) == 0)
                    {
                        writer.Write(Environment.NewLine);
                        bytesWritten += Encoding.UTF8.GetByteCount(Environment.NewLine);
                    }
                }
            }

            Console.WriteLine($"File '{filePath}' generated with size {fileSizeInBytes / (1024 * 1024 * 1024)} GB.");
        }
    }
}
