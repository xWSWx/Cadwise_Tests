using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingTextFiles.ViewModels.Controls
{
    public class FileViewModel : ReactiveObject
    {
        private string _path;
        private bool _isSelected;

        public string Path
        {
            get => _path;
            set => this.RaiseAndSetIfChanged(ref _path, value);
        }

        public bool IsSelected
        {
            get => _isSelected;
            set => this.RaiseAndSetIfChanged(ref _isSelected, value);
        }

        public FileViewModel(string path, bool isSelected = false)
        {
            Path = path;
            IsSelected = isSelected;
        }

        public FileViewModel()
        {
            Path = "DummyPath";
            IsSelected = true;
        }

    }
}
