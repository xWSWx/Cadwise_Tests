using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingTextFiles.FileProcessing
{
    public interface IFileProcessingStrategy
    {
        int Process(byte[] inputData, out byte[] outputData, ref byte[] unknownPart, ref int unknownPartLength, int maxWordSize);
    }
    public class RemovePunctuationStrategy : IFileProcessingStrategy
    {
        public int Process(byte[] inputData, out byte[] outputData, ref byte[] unknownPart, ref int unknownPartLength, int maxWordSize)
        {
            var chars = Encoding.UTF8.GetChars(inputData);
            StringBuilder textAccumulator = new StringBuilder();
            outputData = new byte[inputData.Length];

            foreach (var c in chars)
            {
                if (!char.IsPunctuation(c) && c != '\0')
                {
                    textAccumulator.Append(c);
                }
            }

            outputData = Encoding.UTF8.GetBytes(textAccumulator.ToString());
            return outputData.Length;
        }
    }

    public class RemoveShortWordsStrategy : IFileProcessingStrategy
    {
        public int Process(byte[] inputData, out byte[] outputData, ref byte[] unknownPart, ref int unknownPartLength, int maxWordSize)
        {
            var chars = Encoding.UTF8.GetChars(inputData);
            var unknownPartChars = Encoding.UTF8.GetChars(unknownPart);

            StringBuilder wordAccumulator = new StringBuilder();
            StringBuilder textAccumulator = new StringBuilder();

            foreach (var c in unknownPartChars)
            {
                if (c == '\0')
                    break;
                wordAccumulator.Append(c);
            }

            bool isLimitReachedFirstTimeForCurrentWord = true;
            bool isLimitReached = false;
            foreach (var c in chars)
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

                    if (wordAccumulator.Length < maxWordSize)
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
                    var isLittleWord = wordAccumulator.Length < maxWordSize;
                    if (isLittleWord)
                    {
                        wordAccumulator.Clear();
                    }
                    textAccumulator.Append(c);
                }
            }
            unknownPartLength = wordAccumulator.Length;
            unknownPart = Encoding.Unicode.GetBytes(wordAccumulator.ToString());
            outputData = Encoding.UTF8.GetBytes(textAccumulator.ToString());
            return outputData.Length;
        }
    }
    public class CopyStrategy : IFileProcessingStrategy 
    {
        public int Process(byte[] inputData, out byte[] outputData, ref byte[] unknownPart, ref int unknownPartLength, int maxWordSize)
        {
            outputData = new byte[inputData.Length];
            inputData.CopyTo(outputData, 0);
            return inputData.Length;
        }
    }

}
