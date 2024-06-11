using EmulatorATM.ViewModels.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulatorATM.ViewModels.Screens
{
    public class BasicScreenViewModel : ReactiveObject
    {
        public BasicScreenViewModel() { }
        public virtual void Clear() { }

        protected CardViewModel? cardVM;
        public void Load(CardViewModel? cardvm)
        {
            cardVM = cardvm;
        }
    }
}
