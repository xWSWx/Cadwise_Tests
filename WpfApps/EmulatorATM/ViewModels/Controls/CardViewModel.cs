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
        public CardViewModel() 
        {
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
        }
    }
}
