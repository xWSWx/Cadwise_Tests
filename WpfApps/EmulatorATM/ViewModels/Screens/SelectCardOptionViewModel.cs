using EmulatorATM.ViewModels.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmulatorATM.ViewModels.Screens
{
    public class SelectCardOptionViewModel : BasicScreenViewModel
    {
        public EventHandler? OnBack;
        public EventHandler? OnSelectWithdraw;
        public EventHandler? OnSelectDepositCash;
        public ICommand Back { get; }
        private double _balance;
        public double Balance 
        {
            get => _balance;
            set
            {
                BalanceString = value.ToString();
                this.RaiseAndSetIfChanged(ref _balance, value);
            }
        }
        private string _balanceString;
        public string BalanceString
        {
            get => _balanceString;
            set => this.RaiseAndSetIfChanged(ref _balanceString, value);
        }
        public SelectCardOptionViewModel() 
        {
            Back = ReactiveCommand.Create(() => OnBack?.Invoke(this, EventArgs.Empty));
            Balance = 50000.00;
        }
        public void LoadCard(CardViewModel cardViewModel)
        {
            if (cardViewModel == null)
            {
                //TODO.. забьём...
                return;
            }
            Balance = cardViewModel.Balance;
        }
        public override void Clear()
        {
            base.Clear();
        }
    }
}
