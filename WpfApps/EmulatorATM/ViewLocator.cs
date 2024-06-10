//using ProcessingTextFiles.ViewModels.Controls;
using EmulatorATM.ViewModels;
using EmulatorATM.ViewModels.Controls;
using EmulatorATM.ViewModels.Screens;
using EmulatorATM.Views.Screens;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulatorATM
{
    public class ViewLocator : IViewLocator
    {
        private Dictionary<Type, Type> views = new Dictionary<Type, Type>()
        {
            { typeof(CardViewModel), typeof(IViewFor<CardViewModel>) },
            { typeof(DialViewModel), typeof(IViewFor<DialViewModel>) },
            { typeof(ScreenViewModel), typeof(IViewFor<ScreenViewModel>) },
            { typeof(DefaultScreenViewModel), typeof(IViewFor<DefaultScreenViewModel>) },
            { typeof(BasicScreenViewModel), typeof(IViewFor<BasicScreenViewModel>) },
            { typeof(EnterPinViewModel), typeof(IViewFor<EnterPinViewModel>) },
            { typeof(SelectCardOptionViewModel), typeof(IViewFor<SelectCardOptionViewModel>) }
            
            //{ typeof(FileViewModel), typeof(IViewFor<FileViewModel>) }
        };
        public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
        {
            if (viewModel == null) 
                return null;

            if (views.ContainsKey(viewModel.GetType()))
                return (IViewFor?)Locator.Current.GetService(views[viewModel.GetType()]);

            //////////////
            //// TODO: А оно мне надо?? Почему бы просто не держать словарик всех разрешённых view/viewmodel? 
            //// Зачем нужны вот эти приседания с ковырянием в текстовом нейминге руками (который всё равно вот так не заработает, там шаблончик класса, не просто класс)???
            //// Очень хочется написать здесь return null;
          
            var viewModelName = viewModel.GetType().FullName;
            var viewName = viewModelName?.Replace("ViewModel", "View");
            if (viewName == null)
                return null;

            var viewType = Type.GetType(viewName);
            if (viewType == null)
                return null;

            return Locator.Current.GetService(viewType) as IViewFor;
        }
    }
}
