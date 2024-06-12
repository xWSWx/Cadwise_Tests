using NUnit.Framework;
using System;
using System.Text;

namespace EmulatorATM.Tests
{
    [TestFixture]
    public class ATMutilsTests
    {
        [Test]
        public void GenerateRandomNumberString_ShouldReturnStringOfGivenLength()
        {
            // Arrange
            int length = 10;

            // Act
            string result = ATMutils.GenerateRandomNumberString(length);

            // Assert
            Assert.That(result.Length, Is.EqualTo(length));
        }

        [Test]
        public void GenerateRandomNumberString_ShouldReturnStringWithOnlyDigits()
        {
            // Arrange
            int length = 10;

            // Act
            string result = ATMutils.GenerateRandomNumberString(length);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.All.Matches<char>(char.IsDigit));
        }

        [Test]
        public void GenerateRandomNumberString_ShouldReturnDifferentStringsOnMultipleCalls()
        {
            // Arrange
            int length = 10;

            // Act
            string result1 = ATMutils.GenerateRandomNumberString(length);
            string result2 = ATMutils.GenerateRandomNumberString(length);

            // Assert
            Assert.That(result1, Is.Not.EqualTo(result2));
        }
    }

    public static class ATMutils
    {
        public static string GenerateRandomNumberString(int length)
        {
            Random random = new Random();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                sb.Append(random.Next(0, 10)); // Generates a random number between 0 and 9
            }

            return sb.ToString();
        }
    }
}
