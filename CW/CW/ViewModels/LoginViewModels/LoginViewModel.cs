using System;
using CW.Views;
using System.Windows.Input;
using Xamarin.Forms;
using CW.Validations;
using Rg.Plugins.Popup.Services;
using CW.Services;
using CW.Models;
using System.Collections.ObjectModel;

namespace CW.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public ObservableCollection<ExchangeRatesModel> Rates { get; private set; }
        public ICommand ShowLoginFormCommand { get; protected set; }
        public ICommand OpenMapPageCommand { get; protected set; }
        public ICommand ClosePageCommand { get; protected set; }
        public ICommand OpenExchangesRatesPageCommand { get; protected set; }
        public INavigation Navigation { get; set; }

        private bool _isButtonEnabled;
        public string CharCode { get; protected set; }

        public LoginViewModel()
        {
            _isButtonEnabled = true;

            OpenMapPageCommand = new Command(OpenMapPage, () => IsButtonEnabled);
            OpenExchangesRatesPageCommand = new Command(OpenExchangesRatesPage, () => IsButtonEnabled);
            ClosePageCommand = new Command(Back);

            ShowLoginFormCommand = new Command(OpenLoginPopupPage, () => IsButtonEnabled);

            Rates = new ObservableCollection<ExchangeRatesModel>();
            LoadExchangeRates();
        }

        // Enable or disable all buttons on the current page
        private bool IsButtonEnabled
        {
            get => _isButtonEnabled;

            set
            {
                if (value != _isButtonEnabled)
                {
                    _isButtonEnabled = value;

                    (OpenMapPageCommand as Command)?.ChangeCanExecute();
                    (OpenExchangesRatesPageCommand as Command)?.ChangeCanExecute();
                }
            }
        }

        private bool _isLoginFormVisible;
        public bool IsLoginFormVisible
        {
            get => _isLoginFormVisible;
            set
            {
                if (value != _isLoginFormVisible)
                {
                    _isLoginFormVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        private async void LoadExchangeRates()
        {
            var response = await ExchangesRatesService.Instance.GetExchangesRates();

            if (response.IsSuccessful)
            {
                Rates = new ObservableCollection<ExchangeRatesModel>(response.Value);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Уведомление", response.ErrorMessage, "OK");
            }

        }

        private void OpenMapPage()
        {
            IsButtonEnabled = false;
            NavigationService.Instance.NavigateToAsync<NearbyBanksViewModel>(this);
        }

        private void OpenExchangesRatesPage()
        {
            IsButtonEnabled = false;
            NavigationService.Instance.NavigateToAsync<ExchangesRatesViewModel>(this);
        }

        private async void Back()
        {
            IsButtonEnabled = true;
            await NavigationService.Instance.RemoveLastFromBackStackAsync();
        }

        private async void OpenLoginPopupPage()
        {
            IsButtonEnabled = false;
            await PopupNavigation.Instance.PushAsync(new LoginPopupPageView(new LoginPopupPageViewModel()));
            IsButtonEnabled = true;
        }
    }
}
