using CW.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Globalization;

namespace CW.Services
{
    public class NavigationService : INavigationService
    {
        #region Singleton
        private static NavigationService _instance;
        public static NavigationService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NavigationService();
                }

                return _instance;
            }
        }
        #endregion

        public BaseViewModel PreviousPageViewModel => throw new NotImplementedException();

        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task RemoveBackStackAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveLastFromBackStackAsync()
        {
            throw new NotImplementedException();
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            var page = CreatePage(viewModelType);
            var currentPage = (Shell.Current.CurrentItem.CurrentItem as IShellSectionController).PresentedPage as Page;
            await currentPage.Navigation.PushAsync(page);
            //(page.BindingContext as BaseViewModel).InitializeAsync(parameter);
        }

        private Page CreatePage(Type viewModelType)
        {
            Type pageType = GetTypeForViewModel(viewModelType);
            var page = Activator.CreateInstance(pageType) as Page;
            
            return page;
        }

        private Type GetTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(
                CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            
            return viewType;
        }
    }
}
