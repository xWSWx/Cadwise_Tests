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
    }
}
