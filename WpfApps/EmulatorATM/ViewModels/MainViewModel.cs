using EmulatorATM.ViewModels.Controls;
using EmulatorATM.ViewModels.Screens;
using Microsoft.Win32;
using NUnit.Framework.Interfaces;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace EmulatorATM.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private CardViewModel _cardViewModelFirst;
        public CardViewModel CardViewModelFirst
        {
            get => _cardViewModelFirst;
            set => this.RaiseAndSetIfChanged(ref _cardViewModelFirst, value);   
        }
        private CardViewModel _cardViewModelSecond;
        public CardViewModel CardViewModelSecond
        {
            get => _cardViewModelSecond;
            set => this.RaiseAndSetIfChanged(ref _cardViewModelSecond, value);
        }
        private CardViewModel _cardViewModelAdmin;
        public CardViewModel CardViewModelAdmin 
        {
            get => _cardViewModelAdmin; 
            set => this.RaiseAndSetIfChanged(ref _cardViewModelAdmin, value);
        }

        private CardViewModel? _selectedCard;
        public CardViewModel? SelectedCard
        {
            get => _selectedCard; 
            set => this.RaiseAndSetIfChanged(ref _selectedCard, value);
        }
        private DialViewModel _dialViewModel;
        public DialViewModel DialViewModel
        {
            get => _dialViewModel;
            set => this.RaiseAndSetIfChanged(ref _dialViewModel, value);
        }

        public ReactiveCommand<CardViewModel, Unit> TryInserdCard { get; }

        private BasicScreenViewModel _screenVM;
        public BasicScreenViewModel ScreenVM 
        {
            get => _screenVM;
            set => this.RaiseAndSetIfChanged(ref _screenVM, value);
        }
        private DefaultScreenViewModel _defaultScreenViewModel;
        public DefaultScreenViewModel DefaultScreenViewModel
        {
            get => _defaultScreenViewModel;
            set => this.RaiseAndSetIfChanged(ref _defaultScreenViewModel, value);
        }
        public MainViewModel()
        {
            //CardNumber = string.Format("{0}  {1}  {2}  {3}", ATMutils.GenerateRandomNumberString(4), ATMutils.GenerateRandomNumberString(4), ATMutils.GenerateRandomNumberString(4), ATMutils.GenerateRandomNumberString(4))
            CardViewModelFirst = new CardViewModel(IsAdmin: false);
            CardViewModelSecond = new CardViewModel(IsAdmin: false);
            CardViewModelAdmin = new CardViewModel(IsAdmin: true);

            DialViewModel = new ();
            DialViewModel.OnButtonPressed += DialViewModel_OnButtonPressed;
            SelectedCard = CardViewModelFirst;

            TryInserdCard = ReactiveCommand.Create<CardViewModel>((x) =>
            {
                //TODO: проверка на состояние терминала
                if (true)
                {
                    SelectedCard = x;
                }
            });

            ScreenVM = new DefaultScreenViewModel();
            DefaultScreenViewModel = new DefaultScreenViewModel();
        }
        public void ClearAfterStart() 
        {
            SelectedCard = null;
        }
        private void DialViewModel_OnButtonPressed(object? sender, CustomEvents.CustomEventHandlers.DialButtonsEventArgs e)
        {
            ;
        }
    }
}
