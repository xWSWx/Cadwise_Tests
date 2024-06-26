﻿using EmulatorATM.ViewModels.Screens;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmulatorATM.Views.Screens
{
    /// <summary>
    /// Логика взаимодействия для EnterPinView.xaml
    /// </summary>
    public partial class EnterPinView : UserControl, IViewFor<EnterPinViewModel>
    {
        public EnterPinView()
        {
            InitializeComponent();
        }
        public EnterPinViewModel ViewModel
        {
            get => (EnterPinViewModel)DataContext;
            set => DataContext = value;
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (EnterPinViewModel)value;
        }
    }
}
