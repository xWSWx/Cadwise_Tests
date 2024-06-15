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
    public class CashWithdrawalViewModel : BasicScreenViewModel
    {
        public event EventHandler? OnBack;
        public ObservableCollection<DenominationBalanceItemViewModel> BalanceItems { get; set; }
        public ICommand SmallWithdrawal { get; }
        public ICommand LargeWithdrawal { get; }
        public ICommand Back { get; }
        private int _amount;
        public int Amount
        {
            get => _amount;
            set
            {
                AmountString = value.ToString();
                this.RaiseAndSetIfChanged(ref _amount, value);
            }
        }
        private string _amountString;
        public string AmountString
        {
            get => _amountString;
            set => this.RaiseAndSetIfChanged(ref _amountString, value);
        }
        private Visibility _terminalBalanceVisibility;
        public Visibility TerminalBalanceVisibility
        {
            get => _terminalBalanceVisibility;
            set => this.RaiseAndSetIfChanged(ref _terminalBalanceVisibility, value);
        }
        public CashWithdrawalViewModel()
        {
            BalanceItems = new ObservableCollection<DenominationBalanceItemViewModel>();
            TerminalViewModel.OnTerminalChanged += TerminalViewModel_OnTerminalChanged;
            Global.TerminalViewModelInstance.HandMadeChange();
            SmallWithdrawal = ReactiveCommand.Create(SmallWithdrawalProc);
            LargeWithdrawal = ReactiveCommand.Create(LargeWithdrawalProc);
            Back = ReactiveCommand.Create(() => OnBack?.Invoke(this, EventArgs.Empty));
            AmountString = "0";
        }
        public override void Clear()
        {
            base.Clear();
            Amount = 0;
        }
        private void SmallWithdrawalProc() { DispenseMoney(Amount, false); OnBack?.Invoke(this, EventArgs.Empty); }
        private void LargeWithdrawalProc() { DispenseMoney(Amount, true); OnBack?.Invoke(this, EventArgs.Empty); }
        private void DispenseMoney(int amount, bool useHighestDenominations)
        {
            var sortedDenominations = useHighestDenominations ? Global.TerminalViewModelInstance.Balance.Keys.OrderByDescending(k => k) : Global.TerminalViewModelInstance.Balance.Keys.OrderBy(k => k);
            var dispensed = new Dictionary<int, int>();

            //foreach (var denomination in sortedDenominations)
            //{
            //    int count = Math.Min(amount / denomination, Global.TerminalViewModelInstance.Balance[denomination]);
            //    if (count > 0)
            //    {
            //        dispensed[denomination] = count;
            //        amount -= denomination * count;
            //    }

            //    if (amount == 0)
            //    {
            //        break;
            //    }
            //}
            var tempBalance = new Dictionary<int, int>(Global.TerminalViewModelInstance.Balance);
            int remainingAmount = amount;
            foreach (var denomination in sortedDenominations)
            {
                if (remainingAmount == 0) break;

                int numNotes = remainingAmount / denomination;
                if (numNotes > 0)
                {
                    int availableNotes = tempBalance[denomination];
                    int notesToDispense = Math.Min(numNotes, availableNotes);

                    if (notesToDispense > 0)
                    {
                        dispensed[denomination] = notesToDispense;
                        tempBalance[denomination] -= notesToDispense;
                        remainingAmount -= notesToDispense * denomination;
                    }
                }
            }

            if (remainingAmount > 0)
            {
                MessageBox.Show("Unable to dispense the exact amount with the available denominations.");
                var dialogResult = MessageBox.Show(
                    "Unable to dispense the exact amount with the available denominations.\n\t"
                    +
                    "but i can do next situation:\n\t"
                    +
                    ((int)amount - (int)remainingAmount)
                    +
                    " \n\t " + string.Join(", ", dispensed.Select(kv => $"{kv.Value} x {kv.Key}"))+
                    "\nAre u agree?"
                    , "may be, this varian?", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question);
                if (dialogResult == MessageBoxResult.No)
                    return;
            }

            int resultCash = 0;
            double prevouseCardBalance = cardVM!=null ? cardVM.Balance : 0;
            foreach (var denomination in dispensed.Keys)
            {
                cardVM.Balance -= dispensed[denomination]* denomination;
                Global.TerminalViewModelInstance.Balance[denomination] -= dispensed[denomination];
            }
            int realGettingMoney = (int)prevouseCardBalance - (int)cardVM.Balance;
            MessageBox.Show("Dispensed: " + realGettingMoney+ " \n\t " + string.Join(", ", dispensed.Select(kv => $"{kv.Value} x {kv.Key}")));
            Global.TerminalViewModelInstance.HandMadeChange();
        }
        public bool AddNumber(char c)
        {
            if (cardVM == null)
                return false;

            if (char.IsNumber(c))
            {
                int newNumber = (int)char.GetNumericValue(c);

                //какой то тупой кастыль
                if (Amount == 0)
                {
                    Amount = Math.Min(newNumber, (int)cardVM.Balance);
                    Amount = Math.Min(Amount, Global.TerminalViewModelInstance.TotalBalance);
                    return true;
                }
                
                Amount = Math.Min(Amount * 10 + newNumber, (int)cardVM.Balance);
                Amount = Math.Min(Amount, Global.TerminalViewModelInstance.TotalBalance);
                return true;
            }
            return false;
        }
        public bool RemoveNumber()
        {
            Amount = Amount / 10;            
            return true;
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
    }
}
