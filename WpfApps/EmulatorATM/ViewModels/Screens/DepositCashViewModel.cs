using EmulatorATM.ViewModels.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Input;

namespace EmulatorATM.ViewModels.Screens
{
    public class DepositCashViewModel : BasicScreenViewModel
    {
        public EventHandler? OnAccept;
        public EventHandler? OnCancel;
        public ObservableCollection<DenominationBalanceItemViewModel> BalanceItems { get; set; }
        public Dictionary<int, int> currentInsertedCash { get; private set; } = new Dictionary<int, int>();
        public ICommand Cancel { get; }
        public ICommand Accept { get; }
        private int _insertedBalance;
        public int InsertedBalance 
        {
            get => _insertedBalance;
            set
            {
                TotalInsertedCash = value.ToString();
                this.RaiseAndSetIfChanged(ref _insertedBalance, value);
            }
        }
        private string _totalInsertedCash = String.Empty;
        public string TotalInsertedCash
        {
            get => _totalInsertedCash;
            set => this.RaiseAndSetIfChanged(ref _totalInsertedCash, value);
        }
        private Visibility _terminalBalanceBisibility;
        public Visibility TerminalBalanceVisibility 
        {
            get => _terminalBalanceBisibility;
            set => this.RaiseAndSetIfChanged(ref _terminalBalanceBisibility, value);
        }
        public DepositCashViewModel()
        {
            BalanceItems = new ObservableCollection<DenominationBalanceItemViewModel>();
            TerminalViewModel.OnTerminalChanged += TerminalViewModel_OnTerminalChanged;
            TerminalViewModel_OnTerminalChanged(null, Global.TerminalViewModelInstance);
            Accept = ReactiveCommand.Create(() =>
            {
                currentInsertedCash.Clear();
                if (cardVM != null)
                    cardVM.Balance += InsertedBalance;
                OnAccept?.Invoke(this, new EventArgs());
                Global.TerminalViewModelInstance.HandMadeChange();
            });
            Cancel = ReactiveCommand.Create(() =>
            {
                foreach (var a in currentInsertedCash)
                {
                    Global.TerminalViewModelInstance.Balance[a.Key] -= a.Value;
                }
                currentInsertedCash.Clear();
                OnCancel?.Invoke(this, new EventArgs());
                Global.TerminalViewModelInstance.HandMadeChange();
            });
            //забираем из терминала копию и обнуляем
            Clear();
            InsertedBalance = 123456;
            TotalInsertedCash = "123456";
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
        public override void Clear()
        {
            base.Clear();
            currentInsertedCash = new Dictionary<int, int>(Global.TerminalViewModelInstance.Balance);
            foreach (var a in Global.TerminalViewModelInstance.Balance)
            {
                currentInsertedCash[a.Key] = 0;
            }
            InsertedBalance = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Denomination"></param>
        /// <param name="amount"></param>
        /// <returns>amount of NOT accepted cash, so zero is good</returns>
        public int InsertCash(int Denomination, int amount)
        {            
            if (!Global.TerminalViewModelInstance.maxCapacity.ContainsKey(Denomination))
                return amount;

            int notAccepted = amount;
            int canAccept = Global.TerminalViewModelInstance.maxCapacity[Denomination] - Global.TerminalViewModelInstance.Balance[Denomination];
            
            var howMuchAccepted = amount >= canAccept? canAccept : amount;
            Global.TerminalViewModelInstance.Balance[Denomination] += howMuchAccepted;
            InsertedBalance += howMuchAccepted * Denomination;
            notAccepted -= howMuchAccepted;

            if (howMuchAccepted>0)
                Global.TerminalViewModelInstance.HandMadeChange();
            return notAccepted;
        }
    }
}
