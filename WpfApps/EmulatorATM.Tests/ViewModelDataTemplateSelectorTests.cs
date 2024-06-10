using System;
using System.Windows;
using System.Windows.Controls;
using Moq;
using NUnit.Framework;
using EmulatorATM;
using ReactiveUI;
using Splat;

namespace EmulatorATM.Tests
{
    [TestFixture]
    public class ViewModelDataTemplateSelectorTests
    {
        private Mock<IViewLocator> _viewLocatorMock;
        private ViewModelDataTemplateSelector _dataTemplateSelector;

        [SetUp]
        public void SetUp()
        {
            _viewLocatorMock = new Mock<IViewLocator>();
            Locator.CurrentMutable.Register(() => _viewLocatorMock.Object, typeof(IViewLocator));

            _dataTemplateSelector = new ViewModelDataTemplateSelector();
        }

        [Test]
        public void SelectTemplate_ShouldReturnNull_WhenItemIsNull()
        {
            // Act
            var result = _dataTemplateSelector.SelectTemplate(null, new DependencyObject());

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void SelectTemplate_ShouldReturnNull_WhenViewLocatorReturnsNull()
        {
            // Arrange
            var viewModel = new object();
            _viewLocatorMock.Setup(v => v.ResolveView(It.IsAny<object>(), null)).Returns((IViewFor)null);


            // Act
            var result = _dataTemplateSelector.SelectTemplate(viewModel, new DependencyObject());

            // Assert
            Assert.That(result, Is.Null);
        }


        //TODO: Потом разобраться. Mock<> не подходит, потому, что должен быть кто то из FrameworkElement<>
        //[Test]
        //public void SelectTemplate_ShouldReturnDataTemplate_WhenViewLocatorReturnsValidView()
        //{
        //    // Arrange
        //    var viewModel = new object();
        //    var viewMock = new Mock<IViewFor>();
        //    _viewLocatorMock.Setup(v => v.ResolveView(It.IsAny<object>(), null)).Returns(viewMock.Object);


        //    // Act
        //    var result = _dataTemplateSelector.SelectTemplate(viewModel, new DependencyObject());

        //    // Assert
        //    Assert.That(result, Is.Not.Null);
        //    Assert.That(result, Is.InstanceOf<DataTemplate>());

        //    // Additional checks for the DataTemplate
        //    var dataTemplate = (DataTemplate)result;
        //    var frameworkElementFactory = dataTemplate.VisualTree;
        //    Assert.That(frameworkElementFactory, Is.Not.Null);
        //    Assert.That(frameworkElementFactory.Type, Is.EqualTo(viewMock.Object.GetType()));
        //}
    }
}
