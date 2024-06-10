using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static EmulatorATM.CustomEvents.CustomEventHandlers;

namespace EmulatorATM.ViewModels.Controls
{
    public enum DialButtons
    {
        Empty, Zero, TripleZero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Cancel, Clear, Enter
    }
    public class DialViewModel : ReactiveObject
    {

        public event DialButtonsEventHandler? OnButtonPressed;


        public ReactiveCommand<DialButtons, Unit> DialButtonCommand { get; }

        public DialViewModel() 
        {
            DialButtonCommand = ReactiveCommand.Create<DialButtons>( (x) => OnButtonPressed?.Invoke(this, new (x)) );
        }
        
    }
}
