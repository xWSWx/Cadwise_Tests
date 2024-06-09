using System;
using System.Collections.Generic;
using NUnit.Framework;
using ProcessingTextFiles;
using ProcessingTextFiles.ViewModels.Controls;
using ReactiveUI;
using Splat;

namespace ProcessingTextFiles.Tests
{
    [TestFixture]
    public class ViewLocatorTests
    {
        private ViewLocator _viewLocator;

        [SetUp]
        public void SetUp()
        {
            _viewLocator = new ViewLocator();

            // Registering services in Splat's Locator for testing purposes
            Locator.CurrentMutable.Register(() => new TestViewForFileProcessingViewModel(), typeof(IViewFor<FileProcessingViewModel>));
            Locator.CurrentMutable.Register(() => new TestViewForFileViewModel(), typeof(IViewFor<FileViewModel>));
        }

        [Test]
        public void ResolveView_ShouldReturnCorrectView_WhenViewModelIsInDictionary()
        {
            // Arrange
            var viewModel = new FileProcessingViewModel();

            // Act
            var view = _viewLocator.ResolveView(viewModel);

            // Assert
            Assert.That(view, Is.Not.Null);
            Assert.That(view, Is.InstanceOf<TestViewForFileProcessingViewModel>());
        }

        [Test]
        public void ResolveView_ShouldReturnNull_WhenViewModelIsNull()
        {
            // Act
            var view = _viewLocator.ResolveView<FileProcessingViewModel>(null);

            // Assert
            Assert.That(view, Is.Null);
        }

        [Test]
        public void ResolveView_ShouldReturnNull_WhenViewModelNotInDictionaryAndViewModelNameCannotBeResolved()
        {
            // Arrange
            var viewModel = new UnknownViewModel();

            // Act
            var view = _viewLocator.ResolveView(viewModel);

            // Assert
            Assert.That(view, Is.Null);
        }

        [Test]
        public void ResolveView_ShouldReturnNull_WhenViewModelNameCannotBeResolvedToViewType()
        {
            // Arrange
            var viewModel = new AnotherUnknownViewModel();

            // Act
            var view = _viewLocator.ResolveView(viewModel);

            // Assert
            Assert.That(view, Is.Null);
        }

        // Test classes for View Models        
        public class UnknownViewModel : ReactiveObject { }
        public class AnotherUnknownViewModel : ReactiveObject { }

        // Test classes for Views
        public class TestViewForFileProcessingViewModel : IViewFor<FileProcessingViewModel>
        {
            object IViewFor.ViewModel { get; set; }
            public FileProcessingViewModel ViewModel { get; set; }
        }

        public class TestViewForFileViewModel : IViewFor<FileViewModel>
        {
            object IViewFor.ViewModel { get; set; }
            public FileViewModel ViewModel { get; set; }
        }
    }
}
