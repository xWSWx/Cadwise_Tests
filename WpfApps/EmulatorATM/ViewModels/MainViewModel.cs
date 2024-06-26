﻿using EmulatorATM.ViewModels.Controls;
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
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace EmulatorATM.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        Dictionary<ePages, BasicScreenViewModel> Pages = new();
        public EnterPinViewModel PinPage = new EnterPinViewModel();
        public DefaultScreenViewModel GreetingsPage = new DefaultScreenViewModel();
        public SelectCardOptionViewModel SelectOptionPage = new SelectCardOptionViewModel();
        public DepositCashViewModel DepositCashPage = new DepositCashViewModel();
        public CashWithdrawalViewModel CashWithdrawalPage = new CashWithdrawalViewModel();
        public enum ePages
        {
            Greetings, PIN, SelectOption, DepositCash, Withdrawal
        }
        private ePages _curPage;
        public ePages CurPage 
        {
            get => _curPage;
            set 
            {
                CurrentPage = Pages[value];
                this.RaiseAndSetIfChanged(ref _curPage, value); 
            }
        }

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
            set 
            {
                if (value != null)
                {
                    IsInsertCardActionEnable = false;
                    CurPage = ePages.PIN;
                    DepositCashPage.TerminalBalanceVisibility = value.AdminCardTextVisibility;
                    CashWithdrawalPage.TerminalBalanceVisibility = value.AdminCardTextVisibility;
                }
                if (value == null)
                {
                    IsInsertCardActionEnable = true;
                }
                this.RaiseAndSetIfChanged(ref _selectedCard, value); 
            }
        }
        private DialViewModel _dialViewModel;
        public DialViewModel DialViewModel
        {
            get => _dialViewModel;
            set => this.RaiseAndSetIfChanged(ref _dialViewModel, value);
        }

        public ReactiveCommand<CardViewModel, Unit> TryInserdCard { get; }
        

        private BasicScreenViewModel _currentPage;
        public BasicScreenViewModel CurrentPage 
        {
            get => _currentPage;
            set 
            {
                if (value != null)
                {
                    value.Clear();
                    value.Load(SelectedCard);
                }
                if (value == GreetingsPage)
                {
                    SelectedCard = null;
                }
                if (value == SelectOptionPage)
                {
                    SelectOptionPage.LoadCard(SelectedCard);
                }
                if (value == DepositCashPage)
                {
                    HandMoneyVisibility = Visibility.Visible;
                }
                else
                {
                    HandMoneyVisibility = Visibility.Collapsed;
                }

                this.RaiseAndSetIfChanged(ref _currentPage, value); 
            }
        }
        private bool _isInsertCardActionEnable;
        public bool IsInsertCardActionEnable
        {
            get => _isInsertCardActionEnable;
            set => this.RaiseAndSetIfChanged(ref _isInsertCardActionEnable, value);
        }
        private Visibility _handMoneyVisibility;
        public Visibility HandMoneyVisibility
        {
            get => _handMoneyVisibility;
            set => this.RaiseAndSetIfChanged(ref _handMoneyVisibility, value);
        }
        public ReactiveCommand<int, Unit> InsertCash { get; }
        public MainViewModel()
        {
            InsertCash = ReactiveCommand.Create<int>((x) =>
            {
                if (CurrentPage == DepositCashPage)
                {
                    DepositCashPage.InsertCash(x, 1);
                }
            });
            Pages.Add(ePages.PIN, PinPage);
            Pages.Add(ePages.Greetings, GreetingsPage);
            Pages.Add(ePages.SelectOption, SelectOptionPage);
            Pages.Add(ePages.DepositCash, DepositCashPage);
            Pages.Add(ePages.Withdrawal, CashWithdrawalPage);
            
            //CardNumber = string.Format("{0}  {1}  {2}  {3}", ATMutils.GenerateRandomNumberString(4), ATMutils.GenerateRandomNumberString(4), ATMutils.GenerateRandomNumberString(4), ATMutils.GenerateRandomNumberString(4))
            CardViewModelFirst = new CardViewModel(IsAdmin: false) { Balance = 100000 };
            CardViewModelSecond = new CardViewModel(IsAdmin: false) { Balance= 22312 };
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

            //CurrentPage = new DefaultScreenViewModel();
            CurPage = ePages.Greetings;
            IsInsertCardActionEnable = true;

            SelectOptionPage.OnBack += (x,y) => CurPage = ePages.Greetings;
            SelectOptionPage.OnSelectWithdraw += (x, y) => CurPage = ePages.Withdrawal;
            SelectOptionPage.OnSelectDepositCash += (x, y) => CurPage = ePages.DepositCash;

            DepositCashPage.OnAccept += (x, y) => CurPage = ePages.SelectOption;
            DepositCashPage.OnCancel += (x, y) => CurPage = ePages.SelectOption;

            CashWithdrawalPage.OnBack += (x, y) => CurPage = ePages.SelectOption;

            HandMoneyVisibility = Visibility.Visible;
        }
        public void ClearAfterStart() 
        {
            SelectedCard = null;
            CurPage = ePages.Greetings;
        }
        public void DialViewModel_OnButtonPressed(object? sender, CustomEvents.CustomEventHandlers.DialButtonsEventArgs e)
        {

            /////////////
            //// О боже... но у меня сейчас нет времени выстраивать красивую архитектуру обработки циферблата в зависимости от состояния системы..
            //// поэтому будут тупые switch case, простите

            switch (CurPage)
            {
                case ePages.PIN:
                    switch (e.btn)
                    {
                        case DialButtons.Zero:
                            PinPage.AddNumber('0');
                            break;
                        case DialButtons.One:
                            PinPage.AddNumber('1');
                            break;
                        case DialButtons.Two:
                            PinPage.AddNumber('2');
                            break;
                        case DialButtons.Three:
                            PinPage.AddNumber('3');
                            break;
                        case DialButtons.Four:
                            PinPage.AddNumber('4');
                            break;
                        case DialButtons.Five:
                            PinPage.AddNumber('5');
                            break;
                        case DialButtons.Six:
                            PinPage.AddNumber('6');
                            break;
                        case DialButtons.Seven:
                            PinPage.AddNumber('7');
                            break;
                        case DialButtons.Eight:
                            PinPage.AddNumber('8');
                            break;
                        case DialButtons.Nine:
                            PinPage.AddNumber('9');
                            break;
                        case DialButtons.Clear:
                            PinPage.RemoveNumber();
                            break;
                        case DialButtons.Enter:
                            if (PinPage.PIN != SelectedCard?.PinString)
                            {
                                System.Diagnostics.Debug.WriteLine("Ну да.. забыл логи присоеденить.. Не верный пин!!!");
                                Console.WriteLine("Ну да.. забыл логи присоеденить.. Не верный пин!!!");
                            }
                            else                             
                            {
                                CurPage = ePages.SelectOption;
                            }
                            break;
                        case DialButtons.Cancel:
                            CurPage = ePages.Greetings;
                            break;

                    }
                    break;
                    case ePages.Withdrawal:
                    switch (e.btn)
                    {
                        case DialButtons.Zero:
                            CashWithdrawalPage.AddNumber('0');
                            break;
                        case DialButtons.One:
                            CashWithdrawalPage.AddNumber('1');
                            break;
                        case DialButtons.Two:
                            CashWithdrawalPage.AddNumber('2');
                            break;
                        case DialButtons.Three:
                            CashWithdrawalPage.AddNumber('3');
                            break;
                        case DialButtons.Four:
                            CashWithdrawalPage.AddNumber('4');
                            break;
                        case DialButtons.Five:
                            CashWithdrawalPage.AddNumber('5');
                            break;
                        case DialButtons.Six:
                            CashWithdrawalPage.AddNumber('6');
                            break;
                        case DialButtons.Seven:
                            CashWithdrawalPage.AddNumber('7');
                            break;
                        case DialButtons.Eight:
                            CashWithdrawalPage.AddNumber('8');
                            break;
                        case DialButtons.Nine:
                            CashWithdrawalPage.AddNumber('9');
                            break;
                        case DialButtons.Clear:
                            CashWithdrawalPage.RemoveNumber();
                            break;                                                
                    }
                    break;
            }
            

            
        }        
    }
}
