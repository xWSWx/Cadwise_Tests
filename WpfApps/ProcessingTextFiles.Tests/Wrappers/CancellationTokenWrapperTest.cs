using NUnit.Framework;
using NUnit.Framework.Legacy;
using ProcessingTextFiles.Wrappers;
using System;

namespace ProcessingTextFiles.Tests.Wrappers
{
    [TestFixture]
    public class CustomCancellationTokenTests
    {


        [Test]
        public void Stop_ShouldSetCancelledToTrue()
        {
            // Arrange
            var token = new CustomCancellationToken();

            // Act
            token.Stop();

            // Assert
            Assert.That(token.Cancelled, Is.True);
        }

        [Test]
        public void Start_ShouldSetStartedToTrue()
        {
            // Arrange
            var token = new CustomCancellationToken();

            // Act
            token.Start();

            // Assert
            Assert.That(token.Started, Is.True);
        }

        [Test]
        public void Constructor_WithGuid_ShouldSetId()
        {
            // Arrange
            var newId = Guid.NewGuid();

            // Act
            var token = new CustomCancellationToken(newId);

            // Assert
            Assert.That(newId, Is.EqualTo(token.Id));   
        }
    }
}
