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
    /// Логика взаимодействия для DenominationBalanceItemView.xaml
    /// </summary>
    public partial class DenominationBalanceItemView : UserControl, IViewFor<DenominationBalanceItemViewModel>
    {
        public DenominationBalanceItemView()
        {
            InitializeComponent();
        }
        public DenominationBalanceItemViewModel ViewModel
        {
            get => (DenominationBalanceItemViewModel)DataContext;
            set => DataContext = value;
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (DenominationBalanceItemViewModel)value;
        }
    }
}
