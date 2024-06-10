using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulatorATM.ViewModels.Controls
{
    public class DenominationBalanceItemViewModel : ReactiveObject
    {
        private int _denomination;
        public int Denomination
        {
            get => _denomination;
            set => this.RaiseAndSetIfChanged(ref _denomination, value);
        }

        private int _count;
        public int Count
        {
            get => _count;
            set => this.RaiseAndSetIfChanged(ref _count, value);
        }
        public DenominationBalanceItemViewModel() 
        {
            Denomination = 10;
            Count = 50;
        }
        public DenominationBalanceItemViewModel(int newDenomination, int newCount)
        {
            Denomination = newDenomination;
            Count = newCount;
        }
    }
}
