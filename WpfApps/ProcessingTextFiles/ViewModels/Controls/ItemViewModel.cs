using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingTextFiles.ViewModels.Controls
{
    public class ItemViewModel : ReactiveObject
    {
        private string _name;

        public ItemViewModel(string name)
        {
            Name = name;
        }

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public ItemViewModel() 
        {
            Name = "Dummy string";
        }
    }
}
