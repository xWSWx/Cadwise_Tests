using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmulatorATM.ViewModels
{
    public class TerminalViewModel: ReactiveObject
    {
        public static EventHandler<TerminalViewModel>? OnTerminalChanged;
        /// <summary>
        /// для просторы вычислений и дебага, вместимость будет не более 10
        /// </summary>
        static public readonly int maxCount = 10;
        static readonly Random rnd = new Random();
        private Dictionary<int, int> _balance;
        public Dictionary<int, int> Balance 
        {
            get => _balance;
            set => this.RaiseAndSetIfChanged(ref _balance, value);
        }
        public void HandMadeChange() 
        {
            OnTerminalChanged?.Invoke(this, this);
        }
        public Dictionary<int, int> maxCapacity { get; private set; } = new Dictionary<int, int>()
        {
            { 10, maxCount },
            { 50, maxCount },
            { 100, maxCount },
            { 500, maxCount },
            { 1000, maxCount },
            { 2000, maxCount },
            { 5000, maxCount }
        };
        public int TotalBalance 
        {
            get 
            {
                var res = 0;
                foreach (var a in Balance)
                {
                    //Бугагаг... без контекста выглядит даже смешно
                    res += a.Key * a.Value;
                }
                return res;
            }
        }
        private object lockMoney = new object();
        
        /// <summary>
        /// Вернёт невзятые купюры
        /// </summary>
        /// <param name="inputedCash"></param>
        /// <returns>Not accepted Cash</returns>
        public Dictionary<int, int> DepositMoney(Dictionary<int, int> inputedCash)
        {
            lock (lockMoney)
            {
                var copyedBalance = new Dictionary<int, int>(Balance);
                try
                {
                    var notAsseptedCash = new Dictionary<int, int>(inputedCash);
                    
                    foreach (var a in inputedCash)
                    {
                        var nominal = a.Key;
                        var amount = a.Value;
                        if (Balance[nominal] >= maxCapacity[nominal])
                        {
                            continue;
                        }
                        int canGet = maxCapacity[nominal] - Balance[nominal];
                        int getted = Math.Min(canGet, amount);

                        Balance[a.Key] += getted;

                        notAsseptedCash[a.Key] = amount - getted;
                    }
                    OnTerminalChanged?.Invoke(this, this);
                    return notAsseptedCash;
                }
                catch(Exception ex) 
                {
                    MessageBox.Show(ex.ToString());
                    Balance = copyedBalance;
                    return inputedCash;
                }
            }
        }

        public TerminalViewModel() 
        {
            Balance = new Dictionary<int, int>()
            {
                { 10, rnd.Next(1, maxCount) },
                { 50, rnd.Next(1, maxCount) },
                { 100, rnd.Next(1, maxCount) },
                { 500, rnd.Next(1, maxCount) },
                { 1000, rnd.Next(1, maxCount) },
                { 2000, rnd.Next(1, maxCount) },
                { 5000, rnd.Next(1, maxCount) },
            };
        }
    }
}
