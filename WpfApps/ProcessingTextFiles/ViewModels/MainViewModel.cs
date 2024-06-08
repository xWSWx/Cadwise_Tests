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

        public MainViewModel()
        {
            

            Items = new ObservableCollection<FileProcessingViewModel>
            {
                new FileProcessingViewModel{ CurentProcessingText = "Some Text For user 1", CompletePercents = 20 },
                new FileProcessingViewModel{ CurentProcessingText = "Some Text For user 2", CompletePercents = 40 },
                new FileProcessingViewModel{ CurentProcessingText = "Some Text For user 3", CompletePercents = 60 }
            };
        }
        public void Clear() => Items.Clear();
        
    }
}
