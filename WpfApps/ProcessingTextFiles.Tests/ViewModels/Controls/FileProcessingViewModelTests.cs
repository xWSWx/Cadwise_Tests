using NUnit.Framework;
using ProcessingTextFiles.Tests.Wrappers;
using ProcessingTextFiles.ViewModels.Controls;
using System;
using System.Linq;

namespace ProcessingTextFiles.Tests.ViewModels.Controls
{
    [TestFixture]
    public class FileProcessingViewModelTests
    {
        //[Test]
        //public void PlayCommand_Executed_ShouldSetPropertiesCorrectly()
        //{
        //    // Arrange
        //    var viewModel = new FileProcessingViewModel();
        //    viewModel.Files.Add(new FileViewModel("test1.txt"));
        //    viewModel.Files.Add(new FileViewModel("test2.txt"));
        //    viewModel.SelectedActionIndex = 0; // Set action to RemoveWordsLessThan
        //    viewModel.CharactersCount = 6; // Set characters count

        //    // Act
        //    viewModel.Play.Execute(null);

        //    // Assert
        //    Assert.That(viewModel.IsPlayEnabled, Is.True);
        //    Assert.That(viewModel.IsPauseEnabled, Is.True);
        //    Assert.That(viewModel.IsCancelEnabled, Is.True);
        //    Assert.That(viewModel.IsSelectEnabled, Is.False);
        //    // Add more assertions as needed
        //}

        //[Test]
        //public void CancelCommand_Executed_ShouldSetPropertiesCorrectly()
        //{
        //    // Arrange
        //    var viewModel = new FileProcessingViewModel();
        //    viewModel.Play.Execute(null); // Execute Play command first

        //    // Act
        //    viewModel.Cancel.Execute(null);

        //    // Assert
        //    Assert.That(viewModel.IsCancelEnabled, Is.False);
        //    Assert.That(viewModel.IsPauseEnabled, Is.False);
        //    Assert.That(viewModel.IsPlayEnabled, Is.True);
        //    Assert.That(viewModel.IsSelectEnabled, Is.True);
        //    // Add more assertions as needed
        //}

        //[Test]
        //public void SelectFilesCommand_Executed_ShouldAddFilesToCollection()
        //{
        //    // Arrange
        //    var viewModel = new FileProcessingViewModel();

        //    // Act
        //    viewModel.Select.Execute(null);

        //    // Assert
        //    Assert.That(3, Is.EqualTo(viewModel.Files.Count));
        //    Assert.That(viewModel.IsPlayEnabled, Is.True);
        //    // Add more assertions as needed
        //}

        [Test]
        public void PauseCommand_Executed_ShouldPauseProcessing()
        {
            // Arrange
            var viewModel = new FileProcessingViewModel();
            viewModel.CharactersCount = 1;
            viewModel.Play.Execute(null); // Start processing first

            // Act
            viewModel.Pause.Execute(null);

            // Assert
            Assert.That(viewModel.EventSlim.IsSet, Is.Not.True);
            Assert.That(viewModel.Paused, Is.True);            
            // Add more assertions as needed
        }

        [Test]
        public void ResumeCommand_Executed_ShouldResumeProcessing()
        {
            // Arrange
            var viewModel = new FileProcessingViewModel();
            viewModel.CharactersCount = 1;
            viewModel.Play.Execute(null); // Start processing first
            viewModel.Pause.Execute(null); // Pause processing

            // Act
            viewModel.Pause.Execute(null); // Resume processing

            // Assert
            Assert.That(viewModel.CancelToken.Paused, Is.False);
            // Add more assertions as needed
        }

        //[Test]
        //public void Files_CollectionChangedEvent_ShouldNotifyViewModel()
        //{
        //    // Arrange
        //    var viewModel = new FileProcessingViewModel();
        //    bool eventRaised = false;
        //    viewModel.PropertyChanged += (sender, args) =>
        //    {
        //        if (args.PropertyName == nameof(viewModel.Files))
        //        {
        //            eventRaised = true;
        //        }
        //    };

        //    // Act
        //    viewModel.Files.Add(new FileViewModel("test.txt"));

        //    // Assert
        //    Assert.That(eventRaised, Is.True);
        //}

        [Test]
        public void ClearCommand_Executed_ShouldClearFilesCollection()
        {
            // Arrange
            var viewModel = new FileProcessingViewModel();
            viewModel.Files.Add(new FileViewModel("test1.txt"));
            viewModel.Files.Add(new FileViewModel("test2.txt"));
            viewModel.Files.Add(new FileViewModel("test3.txt"));

            // Act
            viewModel.Clear();

            // Assert
            Assert.That(viewModel.Files, Is.Empty);
            // Add more assertions as needed
        }

    }
}
