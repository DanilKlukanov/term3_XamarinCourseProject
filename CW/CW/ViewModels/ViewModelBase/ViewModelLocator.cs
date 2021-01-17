using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace CW.ViewModels.ViewModelBase
{
    public class ViewModelLocator
    {
        private static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.Create(nameof(AutoWireViewModel), typeof(bool), typeof(ViewModelLocator),
                false, propertyChanged: OnAutoWireViewModelChanged);

        public bool AutoWireViewModel { get; set; }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;

            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;

            var viewModelName = viewType.FullName.Replace(".Views.", ".ViewModels.") + "Model";
            viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewModelName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);

            if (viewModelType == null)
            {
                return;
            }

            //var viewModel = _container.Resolve(viewModelType);
            //view.BindingContext = viewModel;
        }
    }
}
