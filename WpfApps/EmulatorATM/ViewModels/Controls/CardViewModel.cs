using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmulatorATM.ViewModels.Controls
{
    public class CardViewModel : ReactiveObject
    {
        private Guid cardId;
        private Guid cardSecret;
        private char[] pin = new char[4] {'0','0','0','0'};
        public char[] Pin
        {
            get => pin;
            set 
            {
                PinString = new string(value);
                this.RaiseAndSetIfChanged(ref pin, value); 
            }
        }
        private string _pinString;
        public string PinString
        {
            get => _pinString;
            set => this.RaiseAndSetIfChanged(ref  _pinString, value);
        }
        public char[] GetPIN() => pin;
        private string _cardNumber;
        public string CardNumber 
        {
            get => _cardNumber;
            set => this.RaiseAndSetIfChanged(ref _cardNumber, value);
        }
        private bool _isAdminCard;
        public bool IsAdminCard
        {
            get => _isAdminCard;
            private set
            {
                if (value)
                    AdminCardTextVisibility = Visibility.Visible;
                else 
                    AdminCardTextVisibility = Visibility.Collapsed;
                this.RaiseAndSetIfChanged(ref _isAdminCard, value);
            }
        }
        private Visibility _adminCardTextVisibility;
        public Visibility AdminCardTextVisibility
        {
            get => _adminCardTextVisibility;
            private set => this.RaiseAndSetIfChanged(ref _adminCardTextVisibility, value);
        }

        private double _balance;
        public double Balance 
        {
            get => _balance;
            set => this.RaiseAndSetIfChanged(ref _balance, value);
        }
        public CardViewModel() 
        {
            Pin = new char[4] { '0', '0', '0', '0' };
            cardId = Guid.NewGuid();
            cardSecret = Guid.NewGuid();
            CardNumber = String.Format("{0}  {1}  {2}  {3}",
                ATMutils.GenerateRandomNumberString(4),
                ATMutils.GenerateRandomNumberString(4),
                ATMutils.GenerateRandomNumberString(4),
                ATMutils.GenerateRandomNumberString(4));
            IsAdminCard = true;
        }
        public CardViewModel(bool IsAdmin) : this() 
        {
            IsAdminCard = IsAdmin;
            Balance = 200000.00;
        }
    }
}
