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
    public partial class ItemView : UserControl, IViewFor<ItemViewModel>
    {
        public ItemView()
        {
            InitializeComponent();
            this.WhenActivated(d => { /* Handle activation */ });
        }

        public ItemViewModel? ViewModel
        {
            get => (ItemViewModel)DataContext;
            set => DataContext = value;
        }

        object? IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (ItemViewModel?)value;
        }
    }
}
