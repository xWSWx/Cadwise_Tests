using Microsoft.Win32;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProcessingTextFiles.ViewModels.Controls
{
    public class FileProcessingViewModel : ReactiveObject
    {
        public ObservableCollection<FileViewModel> Files { get; }

        private string _curentProcessingText;
        public string CurentProcessingText
        {
            get => _curentProcessingText;
            set => this.RaiseAndSetIfChanged(ref _curentProcessingText, value);
        }

        private int _completePercent;
        public int CompletePercents
        {
            get => _completePercent;
            set => this.RaiseAndSetIfChanged(ref _completePercent, value);
        }
        public ICommand Delete { get; }
        public ICommand Play { get; }
        public ICommand Stop { get; }
        public ICommand Pause { get; }
        public ICommand Select { get; }
        public FileProcessingViewModel() 
        {
            Select = ReactiveCommand.Create(SelectFiles);

            CurentProcessingText = "Dummy string";
            CompletePercents = 30;

            Files = new ObservableCollection<FileViewModel>
            {
                new(@"C:\Path\To\File1.txt"),
                new(@"C:\Path\To\File2.txt"),
                new(@"C:\Path\To\File3.txt")
            };
        }
        void SelectFiles()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true, 
                Filter = "All files (*.*)|*.*", 
                Title = "Select Files"
            };           

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {                
                Files.Clear();                
                foreach (string fileName in openFileDialog.FileNames)
                {
                    Files.Add(new FileViewModel(fileName));
                }
            }
        }
    }
}
