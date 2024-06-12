using EmulatorATM.ViewModels.Controls;
using NUnit.Framework;
using System.Linq;
using System.Windows;

namespace EmulatorATM.Tests.ViewModels.Controls
{
    public class CardViewModelTests
    {
        [Test]
        public void PinString_SetValue_ShouldRaisePropertyChangedEvent()
        {
            // Arrange
            var cardViewModel = new CardViewModel();
            bool propertyChanged = false;
            cardViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "PinString")
                {
                    propertyChanged = true;
                }
            };

            // Act
            cardViewModel.PinString = "1234";

            // Assert
            Assert.That(propertyChanged, Is.True);
        }

        [Test]
        public void IsAdminCard_SetToTrue_AdminCardTextVisibilityShouldBeVisible()
        {
            // Arrange
            var cardViewModel = new CardViewModel(IsAdmin: true);

            // Act
            

            // Assert
            Assert.That(cardViewModel.AdminCardTextVisibility, Is.EqualTo(Visibility.Visible));
        }

        [Test]
        public void IsAdminCard_SetToFalse_AdminCardTextVisibilityShouldBeCollapsed()
        {
            // Arrange
            var cardViewModel = new CardViewModel(IsAdmin: false) ;

            // Act            

            // Assert
            Assert.That(cardViewModel.AdminCardTextVisibility, Is.EqualTo(Visibility.Collapsed));
        }

        [Test]
        public void GetPIN_ShouldReturnCorrectValue()
        {
            // Arrange
            var cardViewModel = new CardViewModel();
            cardViewModel.Pin = new[] { '1', '2', '3', '4' };

            // Act
            var pin = cardViewModel.GetPIN();

            // Assert
            Assert.That(new string(pin), Is.EqualTo("1234"));
        }

        [TestCase(true, 200000.0)]
        [TestCase(false, 0.0)]
        public void Constructor_WithIsAdminParameter_ShouldSetIsAdminCardAndBalance(bool isAdmin, double expectedBalance)
        {
            // Arrange & Act
            var cardViewModel = new CardViewModel(isAdmin) { Balance = expectedBalance };

            // Assert
            Assert.That(cardViewModel.IsAdminCard, Is.EqualTo(isAdmin));
            Assert.That(cardViewModel.Balance, Is.EqualTo(expectedBalance));
        }
    }
}
