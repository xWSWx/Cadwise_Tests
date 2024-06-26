﻿using EmulatorATM.ViewModels;
using EmulatorATM.ViewModels.Controls;
using ReactiveUI;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmulatorATM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewFor<MainViewModel>
    {

        public MainWindow()
        {
            InitializeComponent();

        }
        public MainViewModel? ViewModel
        {
            get => (MainViewModel)DataContext;
            set => DataContext = value;
        }

        object? IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (MainViewModel?)value;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel?.ClearAfterStart();
        }
    }
}