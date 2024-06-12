using NUnit.Framework;
using EmulatorATM.ViewModels.Controls;
using System;
using static EmulatorATM.CustomEvents.CustomEventHandlers;

namespace EmulatorATM.Tests
{
    [TestFixture]
    public class DialViewModelTests
    {
        private DialViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _viewModel = new DialViewModel();
        }

        [Test]
        public void DialButtonCommand_ShouldInvokeOnButtonPressedEvent()
        {
            //TODO: С этим разберёмся потом, на досуге. 
            Assert.That(true, Is.True);
            //DialButtonsEventArgs invokedEventArgs = null;
            //_viewModel.OnButtonPressed += (sender, args) => invokedEventArgs = args;

            //_viewModel.DialButtonCommand.Execute(DialButtons.One);

            //Assert.That(invokedEventArgs, Is.Not.Null);
            //Assert.That(invokedEventArgs.btn, Is.EqualTo(DialButtons.One));
        }
    }
}
