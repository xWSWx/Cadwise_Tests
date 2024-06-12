using NUnit.Framework;
using EmulatorATM.ViewModels.Screens;
using System;

namespace EmulatorATM.Tests
{
    [TestFixture]
    public class EnterPinViewModelTests
    {
        private EnterPinViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _viewModel = new EnterPinViewModel();
        }

        [Test]
        public void Constructor_ShouldInitializeWithDefaultPIN()
        {
            Assert.That(_viewModel.PIN, Is.EqualTo("1234"));
        }

        [Test]
        public void Clear_ShouldResetPIN()
        {
            // Arrange
            _viewModel.PIN = "5678";

            // Act
            _viewModel.Clear();

            // Assert
            Assert.That(_viewModel.PIN, Is.EqualTo(string.Empty));
        }

        [Test]
        public void AddNumber_ShouldAddNumberToPIN_WhenPINLengthIsLessThanFour()
        {
            // Arrange
            _viewModel.PIN = "12";

            // Act
            bool result = _viewModel.AddNumber('3');

            // Assert
            Assert.That(result, Is.True);
            Assert.That(_viewModel.PIN, Is.EqualTo("123"));
        }

        [Test]
        public void AddNumber_ShouldNotAddNumberToPIN_WhenPINLengthIsFour()
        {
            // Arrange
            _viewModel.PIN = "1234";

            // Act
            bool result = _viewModel.AddNumber('5');

            // Assert
            Assert.That(result, Is.False);
            Assert.That(_viewModel.PIN, Is.EqualTo("1234"));
        }

        [Test]
        public void AddNumber_ShouldNotAddNonNumericCharacter()
        {
            // Arrange
            _viewModel.PIN = "12";

            // Act
            bool result = _viewModel.AddNumber('A');

            // Assert
            Assert.That(result, Is.False);
            Assert.That(_viewModel.PIN, Is.EqualTo("12"));
        }

        [Test]
        public void RemoveNumber_ShouldRemoveLastNumberFromPIN()
        {
            // Arrange
            _viewModel.PIN = "1234";

            // Act
            bool result = _viewModel.RemoveNumber();

            // Assert
            Assert.That(result, Is.True);
            Assert.That(_viewModel.PIN, Is.EqualTo("123"));
        }

        [Test]
        public void RemoveNumber_ShouldReturnFalse_WhenPINIsEmpty()
        {
            // Arrange
            _viewModel.PIN = string.Empty;

            // Act
            bool result = _viewModel.RemoveNumber();

            // Assert
            Assert.That(result, Is.False);
            Assert.That(_viewModel.PIN, Is.EqualTo(string.Empty));
        }
    }
}
