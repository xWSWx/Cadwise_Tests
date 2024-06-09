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
        private void Size_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Int32.TryParse(e.Text, out _);
        }
        private void SizeBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                int size = 0;
                if (!Int32.TryParse(text, out size))
                {
                    e.CancelCommand();
                }
                ViewModel.CharactersCount = size;
            }
            else
            {
                e.CancelCommand();
            }
        }
        private void SizeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int a;
            var txt = SizeBox.Text.Replace(" ", "");
            if (txt.Length > 1)
            {
                while (txt.Length > 0 && txt[0] == '0')
                    txt = txt.Remove(0, 1);
                if (txt == "")
                    txt = "0";
                SizeBox.Text = txt;
            }
            if (Int32.TryParse(txt, out a))
            {
                a = a > 600 ? 600 : a;
                SizeBox.Text = a.ToString();
                ViewModel.CharactersCount = a;
            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel?.Clear();
        }
    }
}
