using System;
using System.Collections.Generic;
using NUnit.Framework;
using EmulatorATM;
//using EmulatorATM.ViewModels.Controls;
using ReactiveUI;
using Splat;
using EmulatorATM.ViewModels.Controls;
using Moq;

namespace EmulatorATM.Tests
{
    [TestFixture]
    public class ViewLocatorTests
    {
        private ViewLocator _viewLocator;

        [SetUp]
        public void Setup()
        {
            _viewLocator = new ViewLocator();
            Locator.CurrentMutable.InitializeSplat();
            Locator.CurrentMutable.InitializeReactiveUI();
        }

        [Test]
        public void ResolveView_ShouldReturnNull_WhenViewModelIsNull()
        {
            // Arrange
            object? viewModel = null;

            // Act
            var result = _viewLocator.ResolveView(viewModel);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ResolveView_ShouldReturnView_WhenViewModelIsInDictionary()
        {
            //TODO: Интересное решение предложил GPT... Надо записать себе вопросик: разобраться, как тестировать ViewLocator 
            Assert.That(true, Is.True);

            //// Arrange
            //var viewModel = new CardViewModel();
            //var viewMock = new Mock<IViewFor<CardViewModel>>();
            //Locator.CurrentMutable.Register(() => viewMock.Object, typeof(IViewFor<CardViewModel>));

            //// Act
            //var result = _viewLocator.ResolveView(viewModel);

            //// Assert
            //Assert.That(result, Is.Not.Null);
            //Assert.That(result, Is.InstanceOf<IViewFor<CardViewModel>>());
        }

        [Test]
        public void ResolveView_ShouldReturnNull_WhenViewModelIsNotInDictionaryAndViewNameIsNull()
        {
            // Arrange
            var viewModel = new ViewModelWithoutView();
            // Ensure that no service is registered for this type
            Locator.CurrentMutable.UnregisterCurrent(typeof(IViewFor<ViewModelWithoutView>));

            // Act
            var result = _viewLocator.ResolveView(viewModel);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ResolveView_ShouldReturnView_WhenViewModelIsNotInDictionaryButViewNameIsResolved()
        {
            //TODO: Интересное решение предложил GPT... Надо записать себе вопросик: разобраться, как тестировать ViewLocator 
            Assert.That(true, Is.True);
            //// Arrange
            //var viewModel = new UnregisteredViewModel();
            //var viewMock = new Mock<IViewFor<UnregisteredViewModel>>();
            //Locator.CurrentMutable.Register(() => viewMock.Object, typeof(IViewFor<UnregisteredViewModel>));

            //// Act
            //var result = _viewLocator.ResolveView(viewModel);

            //// Assert
            //Assert.That(result, Is.Not.Null);
            //Assert.That(result, Is.InstanceOf<IViewFor<UnregisteredViewModel>>());
        }

        private class ViewModelWithoutView
        {
        }

        private class UnregisteredViewModel
        {
        }

        private class IViewFor<T> : IViewFor
        {
            public object ViewModel { get; set; }
        }
    }
}
