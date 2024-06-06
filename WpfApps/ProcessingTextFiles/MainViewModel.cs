using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingTextFiles
{
    public class MainViewModel : ReactiveObject
    {
        public ObservableCollection<ItemViewModel> Items { get; }

        public MainViewModel()
        {
            Items = new ObservableCollection<ItemViewModel>
            {
                new ItemViewModel("Item 1"),
                new ItemViewModel("Item 2"),
                new ItemViewModel("Item 3")
            };
        }
    }
}
