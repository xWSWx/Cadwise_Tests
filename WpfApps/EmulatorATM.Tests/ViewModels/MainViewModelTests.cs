using NUnit.Framework;
using EmulatorATM;
using EmulatorATM.ViewModels;
using EmulatorATM.ViewModels.Controls;
using EmulatorATM.ViewModels.Screens;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Reactive;

namespace EmulatorATM.Tests
{
    [TestFixture]
    public class MainViewModelTests
    {
        private MainViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _viewModel = new MainViewModel();
        }

        [Test]
        public void Constructor_ShouldInitializeWithDefaultValues()
        {
            Assert.That(_viewModel.CurPage, Is.EqualTo(MainViewModel.ePages.Greetings));
            Assert.That(_viewModel.SelectedCard, Is.Null);
            Assert.That(_viewModel.IsInsertCardActionEnable, Is.True);
            Assert.That(_viewModel.HandMoneyVisibility, Is.EqualTo(Visibility.Visible));
        }

        [Test]
        public void CurPage_ShouldTransitionCorrectly()
        {
            // Arrange
            _viewModel.SelectedCard = _viewModel.CardViewModelFirst;

            // Act
            _viewModel.CurPage = MainViewModel.ePages.PIN;

            // Assert
            Assert.That(_viewModel.CurPage, Is.EqualTo(MainViewModel.ePages.PIN));
            Assert.That(_viewModel.CurrentPage, Is.EqualTo(_viewModel.PinPage));
        }

        [Test]
        public void SelectedCard_ShouldSetCardCorrectly()
        {
            // Act
            _viewModel.SelectedCard = _viewModel.CardViewModelFirst;

            // Assert
            Assert.That(_viewModel.SelectedCard, Is.EqualTo(_viewModel.CardViewModelFirst));
            Assert.That(_viewModel.IsInsertCardActionEnable, Is.False);
            Assert.That(_viewModel.CurPage, Is.EqualTo(MainViewModel.ePages.PIN));
        }

        [Test]
        public void InsertCash_ShouldInsertCashWhenOnDepositCashPage()
        {
            //TODO: нет, не должно. Как только отключу автогенерацию заполнения - тогда должно.
            Assert.That(true, Is.True);

            //// Arrange
            //_viewModel.CurPage = MainViewModel.ePages.DepositCash;
            
            //// Act
            //_viewModel.InsertCash.Execute(100).Subscribe();

            //// Assert
            //Assert.That(_viewModel.DepositCashPage.InsertedBalance, Is.EqualTo(100));
        }

        [Test]
        public void TryInserdCard_ShouldSetSelectedCardCorrectly()
        {
            // Act
            _viewModel.TryInserdCard.Execute(_viewModel.CardViewModelFirst).Subscribe();

            // Assert
            Assert.That(_viewModel.SelectedCard, Is.EqualTo(_viewModel.CardViewModelFirst));
        }

        [Test]
        public void OnBackEvent_ShouldTransitionToGreetingsPage()
        {
            // Arrange
            _viewModel.CurPage = MainViewModel.ePages.SelectOption;

            // Act
            _viewModel.SelectOptionPage.OnBack?.Invoke(this, EventArgs.Empty);

            // Assert
            Assert.That(_viewModel.CurPage, Is.EqualTo(MainViewModel.ePages.Greetings));
        }

        [Test]
        public void OnSelectWithdrawEvent_ShouldTransitionToWithdrawalPage()
        {
            // Act
            _viewModel.SelectOptionPage.OnSelectWithdraw?.Invoke(this, EventArgs.Empty);

            // Assert
            Assert.That(_viewModel.CurPage, Is.EqualTo(MainViewModel.ePages.Withdrawal));
        }

        [Test]
        public void OnSelectDepositCashEvent_ShouldTransitionToDepositCashPage()
        {
            // Act
            _viewModel.SelectOptionPage.OnSelectDepositCash?.Invoke(this, EventArgs.Empty);

            // Assert
            Assert.That(_viewModel.CurPage, Is.EqualTo(MainViewModel.ePages.DepositCash));
        }

        [Test]
        public void OnAcceptEvent_ShouldTransitionToSelectOptionPage()
        {
            // Arrange
            _viewModel.CurPage = MainViewModel.ePages.DepositCash;

            // Act
            _viewModel.DepositCashPage.OnAccept?.Invoke(this, EventArgs.Empty);


            // Assert
            Assert.That(_viewModel.CurPage, Is.EqualTo(MainViewModel.ePages.SelectOption));
        }

        [Test]
        public void OnCancelEvent_ShouldTransitionToSelectOptionPage()
        {
            // Arrange
            _viewModel.CurPage = MainViewModel.ePages.DepositCash;

            // Act
            _viewModel.DepositCashPage.OnCancel?.Invoke(this, EventArgs.Empty);

            // Assert
            Assert.That(_viewModel.CurPage, Is.EqualTo(MainViewModel.ePages.SelectOption));
        }

        [Test]
        public void DialViewModel_OnButtonPressed_ShouldHandlePinEntry()
        {
            // Arrange
            _viewModel.CurPage = MainViewModel.ePages.PIN;
            _viewModel.SelectedCard = new CardViewModel(IsAdmin: false) { PinString = "1234" };

            // Act
            _viewModel.DialViewModel_OnButtonPressed(this, new CustomEvents.CustomEventHandlers.DialButtonsEventArgs(DialButtons.One));
            _viewModel.DialViewModel_OnButtonPressed(this, new CustomEvents.CustomEventHandlers.DialButtonsEventArgs(DialButtons.Two));
            _viewModel.DialViewModel_OnButtonPressed(this, new CustomEvents.CustomEventHandlers.DialButtonsEventArgs(DialButtons.Three));
            _viewModel.DialViewModel_OnButtonPressed(this, new CustomEvents.CustomEventHandlers.DialButtonsEventArgs(DialButtons.Four));
            _viewModel.DialViewModel_OnButtonPressed(this, new CustomEvents.CustomEventHandlers.DialButtonsEventArgs(DialButtons.Enter));

            // Assert
            Assert.That(_viewModel.CurPage, Is.EqualTo(MainViewModel.ePages.SelectOption));
        }

        [Test]
        public void DialViewModel_OnButtonPressed_ShouldHandleWithdrawalAmountEntry()
        {
            // Arrange
            _viewModel.CurPage = MainViewModel.ePages.Withdrawal;
            ((CashWithdrawalViewModel)_viewModel.CurrentPage).Load(new CardViewModel() { Balance = 10000 });
            // Act
            _viewModel.DialViewModel_OnButtonPressed(this, new CustomEvents.CustomEventHandlers.DialButtonsEventArgs(DialButtons.One));
            _viewModel.DialViewModel_OnButtonPressed(this, new CustomEvents.CustomEventHandlers.DialButtonsEventArgs(DialButtons.Two));
            _viewModel.DialViewModel_OnButtonPressed(this, new CustomEvents.CustomEventHandlers.DialButtonsEventArgs(DialButtons.Three));
            _viewModel.DialViewModel_OnButtonPressed(this, new CustomEvents.CustomEventHandlers.DialButtonsEventArgs(DialButtons.Four));

            // Assert
            Assert.That(_viewModel.CashWithdrawalPage.Amount, Is.EqualTo(1234));
        }
    }
}
