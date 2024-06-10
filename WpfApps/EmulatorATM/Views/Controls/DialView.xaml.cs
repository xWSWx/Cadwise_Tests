using EmulatorATM.ViewModels.Controls;
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

namespace EmulatorATM.Views.Controls
{
    /// <summary>
    /// Логика взаимодействия для DialView.xaml
    /// </summary>
    public partial class DialView : UserControl, IViewFor<DialViewModel>
    {
        public DialView()
        {
            InitializeComponent();
        }
        public DialViewModel ViewModel
        {
            get => (DialViewModel)DataContext;
            set => DataContext = value;
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (DialViewModel)value;
        }
    }
}
