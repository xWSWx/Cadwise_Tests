using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingTextFiles.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        //protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        //{
        //    if (Equals(storage, value))
        //    {
        //        return false;
        //    }
        //    storage = value;
        //    this.OnPropertyChanged(propertyName);
        //    return true;
        //}

        //#region INotifyPropertyChanged

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChangedEventHandler eventHandler = this.PropertyChanged;
        //    if (eventHandler != null)
        //    {
        //        eventHandler(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}

        //#endregion
    }
}
