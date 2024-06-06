using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace ProcessingTextFiles
{
    public class ViewModelDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
                return null;

            var viewLocator = Locator.Current.GetService<IViewLocator>();
            var view = viewLocator?.ResolveView(item);
            if (view == null)
                return null;

            view.ViewModel = item;

            var factory = new FrameworkElementFactory(view.GetType());
            factory.SetValue(FrameworkElement.DataContextProperty, item);

            var dataTemplate = new DataTemplate
            {
                VisualTree = factory
            };
            return dataTemplate;
        }
    }
}
