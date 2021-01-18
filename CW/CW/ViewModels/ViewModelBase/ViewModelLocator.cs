using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using TinyIoC;
using CW.Services;
using CW.ViewModels.InsideViewModels;

namespace CW.ViewModels.ViewModelBase
{
    public static class ViewModelLocator
    {
        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
        }

        private static TinyIoCContainer _container;

        static ViewModelLocator()
        {
            _container = new TinyIoCContainer();

            _container.Register<LoginViewModel>();
            _container.Register<ExchangesRatesViewModel>();
            _container.Register<NearbyBanksViewModel>();
        }

        public static void Resolve<T>() where T : class
        {
            _container.Resolve<T>();
        }

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

            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}
