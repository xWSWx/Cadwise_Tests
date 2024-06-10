using EmulatorATM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulatorATM
{
    public static class Global
    {
        private static TerminalViewModel? terminalViewModelInstance;
        public static TerminalViewModel TerminalViewModelInstance 
        {
            get
            {
                //if (terminalViewModelInstance == null)
                //    terminalViewModelInstance = new TerminalViewModel();
                /////////////это что за чудеса опять???                
                terminalViewModelInstance ??= new TerminalViewModel();
                return terminalViewModelInstance;                
            }            
        }
    }
}
