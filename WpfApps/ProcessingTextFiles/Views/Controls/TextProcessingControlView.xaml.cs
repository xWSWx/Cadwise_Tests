﻿using ProcessingTextFiles.ViewModels.Controls;
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

namespace ProcessingTextFiles.Views
{
    /// <summary>
    /// Логика взаимодействия для TextProcessingControlView.xaml
    /// </summary>
    public partial class TextProcessingControlView : UserControl, IViewFor<TextProcessingControlViewModel>
    {
        public TextProcessingControlView()
        {
            InitializeComponent();
        }
        public TextProcessingControlViewModel ViewModel
        {
            get => (TextProcessingControlViewModel)DataContext;
            set => DataContext = value;
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (TextProcessingControlViewModel)value;
        }
    }
}
