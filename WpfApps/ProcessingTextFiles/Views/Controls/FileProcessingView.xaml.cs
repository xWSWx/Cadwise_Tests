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
using ProcessingTextFiles.ViewModels.Controls;

namespace ProcessingTextFiles.Views.Controls
{
    /// <summary>
    /// Логика взаимодействия для ItemView.xaml
    /// </summary>
    public partial class FileProcessingView : UserControl, IViewFor<FileProcessingViewModel>
    {
        public FileProcessingView()
        {
            InitializeComponent();
            this.WhenActivated(d => { /* Handle activation */ });
        }

        public FileProcessingViewModel? ViewModel
        {
            get => (FileProcessingViewModel)DataContext;
            set => DataContext = value;
        }

        object? IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (FileProcessingViewModel?)value;
        }
    }
}
