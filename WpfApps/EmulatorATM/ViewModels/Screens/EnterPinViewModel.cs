using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace EmulatorATM.ViewModels.Screens
{
    public class EnterPinViewModel : BasicScreenViewModel
    {
        public override void Clear()
        {
            base.Clear();
            PIN = String.Empty;
        }
        private string _pin;
        public string PIN 
        {
            get => _pin;
            set => this.RaiseAndSetIfChanged(ref _pin, value);
        }
        public EnterPinViewModel() 
        {
            PIN = "1234";
        }
        public bool AddNumber(char c)
        {
            if (char.IsNumber(c)) 
            {
                if (PIN.Length < 4)
                {
                    PIN += c;
                    return true;
                }
            }
            return false;
        }
        public bool RemoveNumber() 
        {
            if (PIN.Length == 0)
                return false;
            //PIN = PIN.Substring(0, PIN.Length - 1);
            //TODO: ОЧУМЕТЬ, это что ещё такое мне предложил VisualStudio?? ААА,А,А
            PIN = PIN[..^1];
            return true;
        }
    }
}
