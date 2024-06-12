using NUnit.Framework;
using ProcessingTextFiles.FileProcessing;
using System.Text;

namespace ProcessingTextFiles.Tests
{
    [TestFixture]
    public class FileProcessingStrategyTests
    {
        [Test]
        public void RemovePunctuationStrategy_RemovesPunctuation()
        {
            // Arrange
            var strategy = new RemovePunctuationStrategy();
            var inputData = Encoding.UTF8.GetBytes("Hello, World!");
            byte[] unknownPart = new byte[0];
            int unknownPartLength = 0;
            int maxWordSize = 0;

            // Act
            int resultLength = strategy.Process(inputData, out byte[] outputData, ref unknownPart, ref unknownPartLength, maxWordSize);

            // Assert
            string resultString = Encoding.UTF8.GetString(outputData);
            Assert.That("Hello World", Is.EqualTo(resultString));
            Assert.That(11, Is.EqualTo(resultLength));
        }

        [Test]
        public void RemoveShortWordsStrategy_RemovesShortWords()
        {
            // Arrange
            var strategy = new RemoveShortWordsStrategy();
            var inputData = Encoding.UTF8.GetBytes("This is a test sentence.");
            byte[] unknownPart = new byte[0];
            int unknownPartLength = 0;
            int maxWordSize = 3;

            // Act
            int resultLength = strategy.Process(inputData, out byte[] outputData, ref unknownPart, ref unknownPartLength, maxWordSize);

            // Assert
            string resultString = Encoding.UTF8.GetString(outputData);
            Assert.That("This   test sentence.", Is.EqualTo(resultString));
            Assert.That(21, Is.EqualTo(resultLength));
        }

        [Test]
        public void CopyStrategy_CopiesInputData()
        {
            // Arrange
            var strategy = new CopyStrategy();
            var inputData = Encoding.UTF8.GetBytes("Hello, World!");
            byte[] unknownPart = new byte[0];
            int unknownPartLength = 0;
            int maxWordSize = 0;

            // Act
            int resultLength = strategy.Process(inputData, out byte[] outputData, ref unknownPart, ref unknownPartLength, maxWordSize);

            // Assert
            string resultString = Encoding.UTF8.GetString(outputData);
            Assert.That("Hello, World!", Is.EqualTo(resultString));
            Assert.That(13, Is.EqualTo(resultLength));
        }
    }
}
