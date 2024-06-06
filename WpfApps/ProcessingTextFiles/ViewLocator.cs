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
        public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
        {
            if (viewModel == null) return null;

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
