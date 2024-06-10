using EmulatorATM.ViewModels;
using EmulatorATM.ViewModels.Controls;
using EmulatorATM.ViewModels.Screens;
using EmulatorATM.Views;
using EmulatorATM.Views.Controls;
using EmulatorATM.Views.Screens;
using ReactiveUI;
using Splat;
using System.Configuration;
using System.Data;
using System.Windows;

namespace EmulatorATM
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
            Locator.CurrentMutable.Register(() => new CardView(), typeof(IViewFor<CardViewModel>));
            Locator.CurrentMutable.Register(() => new DialView(), typeof(IViewFor<DialViewModel>));
            Locator.CurrentMutable.Register(() => new DefaultScreenView(), typeof(IViewFor<DefaultScreenViewModel>));
            Locator.CurrentMutable.Register(() => new ScreenView(), typeof(IViewFor<ScreenViewModel>));



        }
    }

}
