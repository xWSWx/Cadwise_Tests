using NUnit.Framework;
using ProcessingTextFiles.FileProcessing;
using ProcessingTextFiles.ViewModels.Controls;
using ProcessingTextFiles.Wrappers;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingTextFiles.Tests
{
    [TestFixture]
    public class FileProcessorTests
    {
        //[Test]
        //public async Task ProcessFiles_Successfully()
        //{
        //    // Arrange
        //    string tempDirectory = Path.Combine(Path.GetTempPath(), "FileProcessorTests");
        //    Directory.CreateDirectory(tempDirectory);
        //
        //    string inputFile = Path.Combine(tempDirectory, "input.txt");
        //    string outputFile = Path.Combine(tempDirectory, "output.txt");
        //
        //    File.WriteAllText(inputFile, "Some input text");
        //
        //    // Act
        //    bool result = FileProcessor.Start(new[] { inputFile }, "output_", new CustomCancellationToken(), FileActions.removePunctuation, 5);
        //
        //    // Assert
        //    Assert.That(result, Is.True);
        //    Assert.That(File.Exists(outputFile), Is.True);
        //
        //    // Clean up
        //    File.Delete(inputFile);
        //    File.Delete(outputFile);
        //    Directory.Delete(tempDirectory);
        //}
        //
        //[Test]
        //public async Task ProcessFiles_Fails_WhenInputFileDoesNotExist()
        //{
        //    // Arrange
        //    string inputFile = "nonexistent.txt";
        //
        //    // Act
        //    bool result = FileProcessor.Start(new[] { inputFile }, "output_", new CustomCancellationToken(), FileActions.removePunctuation, 5);
        //
        //    // Assert
        //    Assert.That(result, Is.False);
        //}
        [Test]
        public void GetOutputFileName_ReturnsCorrectOutputFileName()
        {
            // Arrange
            string inputFilePath = @"C:\InputFiles\test.txt";
            string prefix = "output_";

            // Act
            string outputFileName = FileProcessor.GetOutputFileName(inputFilePath, prefix);

            // Assert
            string expectedOutputFileName = @"C:\InputFiles\output_test.txt";
            Assert.That(expectedOutputFileName, Is.EqualTo(outputFileName));
        }
        [Test]
        public void RemovePunctuation_RemovesPunctuationCharacters()
        {
            // Arrange
            byte[] inputBytes = Encoding.UTF8.GetBytes("Some text with punctuation!");

            // Act
            byte[] outputBytes;
            int outputLength = FileProcessor.RemovePunctuation(inputBytes, out outputBytes);

            // Assert
            string outputText = Encoding.UTF8.GetString(outputBytes, 0, outputLength);
            Assert.That("Some text with punctuation", Is.EqualTo(outputText));
        }

        [Test]
        public void RemoveShortWords_RemovesShortWords()
        {
            // Arrange
            byte[] inputBytes = Encoding.UTF8.GetBytes("This is a test sentence");
            int maxWordLength = 3;

            // Act
            byte[] outputBytes;
            byte[] bytes = new byte[0];
            int i = 0;
            int outputLength = FileProcessor.RemoveShortWords(inputBytes, maxWordLength, out outputBytes, ref bytes, ref i);

            // Assert
            string outputText = Encoding.UTF8.GetString(outputBytes, 0, outputLength);
            Assert.That("This   test sentence", Is.EqualTo(outputText));
        }
        //[Test]
        //public void Start_ReturnsTrue()
        //{
        //    // Arrange
        //    IEnumerable<string> paths = new List<string> { "input1.txt", "input2.txt" };
        //    string prefix = "output_";
        //    CustomCancellationToken cancelToken = new CustomCancellationToken();
        //    FileActions action = FileActions.removePunctuation;
        //    int maxWordSize = 10;
        //
        //    // Act
        //    bool result = FileProcessor.Start(paths, prefix, cancelToken, action, maxWordSize);
        //
        //    // Assert
        //    Assert.That(result, Is.False);
        //}
        [Test]
        public void RemovePunctuation_RemovesPunctuation()
        {
            // Arrange
            byte[] inputBytes = Encoding.UTF8.GetBytes("This is a test! How are you?");
            byte[] expectedOutputBytes = Encoding.UTF8.GetBytes("This is a test How are you");

            // Act
            int outputLength = FileProcessor.RemovePunctuation(inputBytes, out byte[] outputBytes);

            // Assert
            Assert.That(expectedOutputBytes.Length, Is.EqualTo(outputLength));
            Assert.That(expectedOutputBytes, Is.EqualTo(outputBytes));
        }
        // Add more tests as needed
    }
}
