using NUnit.Framework;
using EmulatorATM.ViewModels.Screens;
using EmulatorATM.ViewModels.Controls;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using EmulatorATM.ViewModels;

namespace EmulatorATM.Tests
{
    [TestFixture]
    public class DepositCashViewModelTests
    {
        private DepositCashViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _viewModel = new DepositCashViewModel();
        }

        [Test]
        public void Constructor_ShouldInitializeProperties()
        {
            Assert.That(_viewModel.BalanceItems, Is.Not.Null);
            Assert.That(_viewModel.InsertedBalance, Is.EqualTo(123456));
            Assert.That(_viewModel.TotalInsertedCash, Is.EqualTo("123456"));
            Assert.That(_viewModel.currentInsertedCash, Is.Not.Null);
        }

        [Test]
        public void Clear_ShouldResetInsertedBalanceAndCurrentInsertedCash()
        {
            _viewModel.Clear();

            Assert.That(_viewModel.InsertedBalance, Is.EqualTo(0));
            foreach (var kvp in _viewModel.currentInsertedCash)
            {
                Assert.That(kvp.Value, Is.EqualTo(0));
            }
        }

        [Test]
        public void InsertCash_ShouldIncreaseInsertedBalance()
        {
            int denomination = 100;
            int amount = 5;
            int initialBalance = _viewModel.InsertedBalance;

            int notAccepted = _viewModel.InsertCash(denomination, amount);

            //Assert.That(notAccepted, Is.EqualTo(0));
            Assert.That(_viewModel.InsertedBalance, Is.EqualTo(initialBalance + denomination * (amount- notAccepted)));
        }

        [Test]
        public void InsertCash_ShouldReturnNotAcceptedAmount_WhenExceedingMaxCapacity()
        {
            //TODO: АЙ АЙ, так нельзя тесты писать. Не должно быть никаких IF.. Я исправлю это позже, на досуге.


            int denomination = 100;
            int amount = 10000; // Exceeding the capacity
            int initialBalance = _viewModel.InsertedBalance;
            bool IsMaxAlreadyMaximum = Global.TerminalViewModelInstance.Balance[denomination] == TerminalViewModel.maxCount;
            int notAccepted = _viewModel.InsertCash(denomination, amount);

            if (!IsMaxAlreadyMaximum)
            {
                Assert.That(notAccepted, Is.GreaterThan(0));
                Assert.That(_viewModel.InsertedBalance, Is.GreaterThan(initialBalance));
            }
            else 
            {
                Assert.That(notAccepted, Is.Not.GreaterThan(0));
                Assert.That(_viewModel.InsertedBalance, Is.Not.GreaterThan(initialBalance));
            }
        }

        [Test]
        public void AcceptCommand_ShouldClearCurrentInsertedCashAndInvokeOnAccept()
        {
            bool acceptInvoked = false;
            _viewModel.OnAccept += (sender, args) => acceptInvoked = true;

            _viewModel.Accept.Execute(null);

            Assert.That(acceptInvoked, Is.True);
            foreach (var kvp in _viewModel.currentInsertedCash)
            {
                Assert.That(kvp.Value, Is.EqualTo(0));
            }
        }

        [Test]
        public void CancelCommand_ShouldResetCurrentInsertedCashAndInvokeOnCancel()
        {
            bool cancelInvoked = false;
            _viewModel.OnCancel += (sender, args) => cancelInvoked = true;

            _viewModel.Cancel.Execute(null);

            Assert.That(cancelInvoked, Is.True);
            foreach (var kvp in _viewModel.currentInsertedCash)
            {
                Assert.That(kvp.Value, Is.EqualTo(0));
            }
        }
    }
}
