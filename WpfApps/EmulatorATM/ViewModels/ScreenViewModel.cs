using EmulatorATM.ViewModels.Screens;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulatorATM.ViewModels
{
    public class ScreenViewModel : ReactiveObject
    {
        private BasicScreenViewModel _currentScreen;
        public BasicScreenViewModel CurrentScreen
        {
            get => _currentScreen;
            set => this.RaiseAndSetIfChanged(ref _currentScreen, value);
        }
        public ScreenViewModel() 
        {
            CurrentScreen = new DefaultScreenViewModel();
        }
    }
}
