using NUnit.Framework;
using ProcessingTextFiles.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProcessingTextFiles.Tests.ViewModels
{
    [TestFixture]
    public class MainViewModelTests
    {
        [Test]
        public void AddCommand_Executed_ShouldAddNewItem()
        {
            // Arrange
            var viewModel = new MainViewModel();
            int initialItemCount = viewModel.Items.Count;

            // Act
            viewModel.Add.Execute(null);

            // Assert
            Assert.That(initialItemCount + 1, Is.EqualTo(viewModel.Items.Count));
        }

        [Test]
        public void ClearCommand_Executed_ShouldClearItemsCollection()
        {
            // Arrange
            var viewModel = new MainViewModel();

            // Act
            viewModel.Clear();

            // Assert
            Assert.That(viewModel.Items, Is.Empty);
        }
    }
}
