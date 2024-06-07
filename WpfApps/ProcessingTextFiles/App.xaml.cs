using ProcessingTextFiles.ViewModels.Controls;
using ProcessingTextFiles.Views.Controls;
using ReactiveUI;
using Splat;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ProcessingTextFiles
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Locator.CurrentMutable.RegisterLazySingleton(() => new ViewLocator(), typeof(IViewLocator));
            Locator.CurrentMutable.Register(() => new FileProcessingView(), typeof(IViewFor<FileProcessingViewModel>));
            Locator.CurrentMutable.Register(() => new FileView(), typeof(IViewFor<FileViewModel>));
        }
    }

}
