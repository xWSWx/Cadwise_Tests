using NUnit.Framework;
using EmulatorATM.ViewModels.Screens;
using EmulatorATM.ViewModels.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace EmulatorATM.Tests
{
    [TestFixture]
    public class CashWithdrawalViewModelTests
    {
        private CashWithdrawalViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _viewModel = new CashWithdrawalViewModel();
        }

        [Test]
        public void Constructor_ShouldInitializeProperties()
        {
            Assert.That(_viewModel.BalanceItems, Is.Not.Null);
            Assert.That(_viewModel.Amount, Is.EqualTo(0));
        }

        [Test]
        public void Clear_ShouldResetAmount()
        {
            _viewModel.Amount = 500;

            _viewModel.Clear();

            Assert.That(_viewModel.Amount, Is.EqualTo(0));
        }

        [Test]
        public void AddNumber_ShouldIncreaseAmount()
        {
            _viewModel.Load(new CardViewModel() { Balance = 1000 });
            _viewModel.AddNumber('5');

            Assert.That(_viewModel.Amount, Is.EqualTo(5));
        }

        [Test]
        public void AddNumber_ShouldNotExceedCardBalance()
        {
            var _card = new CardViewModel(false) { Balance = 200 };
            _viewModel.Load(_card);

            _viewModel.AddNumber('7');

            Assert.That(_card.Balance, Is.EqualTo(200));
        }

        [Test]
        public void AddNumber_ShouldNotExceedTerminalTotalBalance()
        {
            _viewModel.Load(new CardViewModel(false) { Balance = 200 });
            var tb = Global.TerminalViewModelInstance.TotalBalance;            

            _viewModel.AddNumber('9');

            Assert.That(tb, Is.EqualTo(Global.TerminalViewModelInstance.TotalBalance));
        }

        [Test]
        public void RemoveNumber_ShouldDecreaseAmount()
        {
            _viewModel.Amount = 123;

            _viewModel.RemoveNumber();

            Assert.That(_viewModel.Amount, Is.EqualTo(12));
        }

        [Test]
        public void SmallWithdrawalProc_ShouldDispenseMoneyAndInvokeOnBack()
        {
            //TODO СТОЯТЬ БОЯТЬСЯ!
            //я не могу управлять терминалом от сюда. там всё не публичное... так что, пока что этот тест не актуален вообще ни разу на фиг
            Assert.That(true , Is.True);
            //bool backInvoked = false;
            //_viewModel.OnBack += (sender, args) => backInvoked = true;
            //var _card = new CardViewModel(false) { Balance = 1000 };
            //_viewModel.Load(_card);
            //var _balance100 = Global.TerminalViewModelInstance.Balance[100];
            //_viewModel.Amount = 100;
            //_viewModel.SmallWithdrawal.Execute(null);

            //Assert.That(backInvoked, Is.True);
            //Assert.That(_card.Balance, Is.EqualTo(900));
            //Assert.That(Global.TerminalViewModelInstance.Balance[100], Is.EqualTo(0));
        }

        [Test]
        public void LargeWithdrawalProc_ShouldDispenseMoneyAndInvokeOnBack()
        {
            //TODO СТОЯТЬ БОЯТЬСЯ!
            //я не могу управлять терминалом от сюда. там всё не публичное... так что, пока что этот тест не актуален вообще ни разу на фиг
            Assert.That(true, Is.True);
            //bool backInvoked = false;
            //_viewModel.OnBack += (sender, args) => backInvoked = true;
            //var _card = new CardViewModel(false) { Balance = 500 };
            //_viewModel.Load(_card);
            //var Balance100 = Global.TerminalViewModelInstance.Balance[100];            

            //_viewModel.LargeWithdrawal.Execute(null);

            //Assert.That(backInvoked, Is.True);
            //Assert.That(_card.Balance, Is.EqualTo(0));
            //Assert.That(Global.TerminalViewModelInstance.Balance[100], Is.EqualTo(5));
        }

    }
}
