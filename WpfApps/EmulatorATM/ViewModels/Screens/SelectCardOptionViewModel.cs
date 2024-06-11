using EmulatorATM.ViewModels.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EmulatorATM.ViewModels.Screens
{
    public class SelectCardOptionViewModel : BasicScreenViewModel
    {

        public EventHandler? OnBack;
        public EventHandler? OnSelectWithdraw;
        public EventHandler? OnSelectDepositCash;

        public ObservableCollection<DenominationBalanceItemViewModel> BalanceItems { get; set; }
        public ICommand Back { get; }
        public ICommand SelectWithdraw { get; }
        public ICommand SelectDepositCash { get; }
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
        private bool _isAdmin;
        public bool IsAdmin 
        {
            get => _isAdmin;
            set
            {
                if (value)
                {
                    TerminalBalanceVisibility = Visibility.Visible;
                }
                else
                {
                    TerminalBalanceVisibility = Visibility.Collapsed;
                }
                this.RaiseAndSetIfChanged(ref _isAdmin, value);
            }
        }
        private Visibility _terminalBalanceVisibility;
        public Visibility TerminalBalanceVisibility
        {
            get => _terminalBalanceVisibility;
            set => this.RaiseAndSetIfChanged(ref _terminalBalanceVisibility, value);
        }
        public SelectCardOptionViewModel() 
        {
            BalanceItems = new ObservableCollection<DenominationBalanceItemViewModel>();
            Back = ReactiveCommand.Create(() => OnBack?.Invoke(this, EventArgs.Empty));
            SelectWithdraw = ReactiveCommand.Create(() => OnSelectWithdraw?.Invoke(this, EventArgs.Empty));
            SelectDepositCash = ReactiveCommand.Create(() => OnSelectDepositCash?.Invoke(this, EventArgs.Empty));
            Balance = 50000.00;
            TerminalViewModel.OnTerminalChanged += TerminalViewModel_OnTerminalChanged;
            TerminalViewModel_OnTerminalChanged(null, Global.TerminalViewModelInstance);


        }

        private void TerminalViewModel_OnTerminalChanged(object? sender, TerminalViewModel e)
        {
            foreach (var a in e.Balance)
            {
                var key = a.Key;
                var value = a.Value;
                var exists = false;
                for (int i = 0; i < BalanceItems.Count; i++)
                {
                    if (BalanceItems[i].Denomination == key)
                    {
                        BalanceItems[i].Count = value;
                        exists = true;
                    }
                }
                if (!exists)
                    BalanceItems.Add(new DenominationBalanceItemViewModel(key, value));
            }
        }

        public void LoadCard(CardViewModel cardViewModel)
        {
            if (cardViewModel == null)
            {
                //TODO.. забьём...
                return;
            }
            Balance = cardViewModel.Balance;
            IsAdmin = cardViewModel.IsAdminCard;
        }
        public override void Clear()
        {
            base.Clear();            
        }
    }
}
