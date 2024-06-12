using NUnit.Framework;
using EmulatorATM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmulatorATM.Tests
{
    [TestFixture]
    public class TerminalViewModelTests
    {
        private TerminalViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _viewModel = new TerminalViewModel();
        }

        [Test]
        public void Constructor_ShouldInitializeBalanceWithRandomValues()
        {
            // Act
            var balance = _viewModel.Balance;

            // Assert
            Assert.That(balance, Is.Not.Null);
            Assert.That(balance.Keys, Is.EquivalentTo(new[] { 10, 50, 100, 500, 1000, 2000, 5000 }));
            foreach (var value in balance.Values)
            {
                Assert.That(value, Is.InRange(1, 10));
            }
        }

        [Test]
        public void TotalBalance_ShouldReturnCorrectSum()
        {
            // Arrange
            _viewModel.Balance = new Dictionary<int, int>
            {
                { 10, 1 },
                { 50, 1 },
                { 100, 1 },
                { 500, 1 },
                { 1000, 1 },
                { 2000, 1 },
                { 5000, 1 }
            };
            int expectedTotal = 10 + 50 + 100 + 500 + 1000 + 2000 + 5000;

            // Act
            var totalBalance = _viewModel.TotalBalance;

            // Assert
            Assert.That(totalBalance, Is.EqualTo(expectedTotal));
        }        

        [Test]
        public void DepositMoney_ShouldDepositCorrectAmountAndReturnExcess()
        {
            // Arrange
            _viewModel.Balance = new Dictionary<int, int>
            {
                { 10, 9 },
                { 50, 9 },
                { 100, 9 },
                { 500, 9 },
                { 1000, 9 },
                { 2000, 9 },
                { 5000, 9 }
            };
            var deposit = new Dictionary<int, int>
            {
                { 10, 2 },
                { 50, 2 },
                { 100, 2 },
                { 500, 2 },
                { 1000, 2 },
                { 2000, 2 },
                { 5000, 2 }
            };

            // Act
            var notAccepted = _viewModel.DepositMoney(deposit);

            // Assert
            Assert.That(_viewModel.Balance[10], Is.EqualTo(10));
            Assert.That(_viewModel.Balance[50], Is.EqualTo(10));
            Assert.That(_viewModel.Balance[100], Is.EqualTo(10));
            Assert.That(_viewModel.Balance[500], Is.EqualTo(10));
            Assert.That(_viewModel.Balance[1000], Is.EqualTo(10));
            Assert.That(_viewModel.Balance[2000], Is.EqualTo(10));
            Assert.That(_viewModel.Balance[5000], Is.EqualTo(10));

            Assert.That(notAccepted[10], Is.EqualTo(1));
            Assert.That(notAccepted[50], Is.EqualTo(1));
            Assert.That(notAccepted[100], Is.EqualTo(1));
            Assert.That(notAccepted[500], Is.EqualTo(1));
            Assert.That(notAccepted[1000], Is.EqualTo(1));
            Assert.That(notAccepted[2000], Is.EqualTo(1));
            Assert.That(notAccepted[5000], Is.EqualTo(1));
        }

        [Test]
        public void HandMadeChange_ShouldRaiseOnTerminalChangedEvent()
        {
            // Arrange
            bool eventRaised = false;
            TerminalViewModel.OnTerminalChanged += (sender, args) => eventRaised = true;

            // Act
            _viewModel.HandMadeChange();

            // Assert
            Assert.That(eventRaised, Is.True);
        }
    }
}
