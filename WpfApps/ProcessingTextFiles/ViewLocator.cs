using ProcessingTextFiles.ViewModels.Controls;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingTextFiles
{
    public class ViewLocator : IViewLocator
    {
        private Dictionary<Type, Type> views = new Dictionary<Type, Type>()
        {
            { typeof(FileProcessingViewModel), typeof(IViewFor<FileProcessingViewModel>) },
            { typeof(FileViewModel), typeof(IViewFor<FileViewModel>) }
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
