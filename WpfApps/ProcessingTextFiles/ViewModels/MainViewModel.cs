using Microsoft.Win32;
using ProcessingTextFiles.ViewModels.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProcessingTextFiles.ViewModels
{
    public class MainViewModel : ReactiveObject
    {

        public ObservableCollection<FileProcessingViewModel> Items { get; }
        public ICommand Add { get; }
        public MainViewModel()
        {
            Add = ReactiveCommand.Create(() => 
            {
                FileProcessingViewModel item = new();
                item.OnDelete += Item_OnDelete;
                Items?.Add(item);
             });

            Items = new ObservableCollection<FileProcessingViewModel>
            {
                new FileProcessingViewModel{ CurentProcessingText = "Some Text For user 1", CompletePercents = 20 },
                new FileProcessingViewModel{ CurentProcessingText = "Some Text For user 2", CompletePercents = 40 },
                new FileProcessingViewModel{ CurentProcessingText = "Some Text For user 3", CompletePercents = 60 }
            };
        }

        private void Item_OnDelete(object? sender, EventArgs e)
        {
            if (sender is  FileProcessingViewModel item) 
            {
                Items.Remove(item);
            }
        }

        public void Clear() => Items.Clear();
        
    }
}
