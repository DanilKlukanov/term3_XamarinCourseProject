using System;
using System.Collections.Generic;
using System.Text;
using CW.Views;
using System.Windows.Input;
using Xamarin.Forms;
using CW.Validations;
using CW.Views.InsideViews;
using Rg.Plugins.Popup.Services;
using CW.Services;
using CW.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace CW.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public ObservableCollection<ExchangeRatesModel> Rates { get; private set; }
        public ICommand AuthorizationCommand { get; protected set; }
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
            AuthorizationCommand = new Command(Authorize, (_) => IsButtonEnabled);

            OpenMapPageCommand = new Command(OpenMapPage, () => IsButtonEnabled);
            OpenExchangesRatesPageCommand = new Command(OpenExchangesRatesPage, () => IsButtonEnabled);
            ClosePageCommand = new Command(Back);

            ShowLoginFormCommand = new Command(OpenLoginPopupPage, () => IsButtonEnabled);

            UserLogin = new ValidatableObject<string>();
            UserPassword = new ValidatableObject<string>();

            AddValidations();
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

                    (AuthorizationCommand as Command)?.ChangeCanExecute();
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

        private string _autorizationIngo = "Введите Ваш логин и пароль";
        public string AutorizationInfo
        {
            get => _autorizationIngo;
            set
            {
                if (_autorizationIngo != value)
                {
                    _autorizationIngo = value;
                    OnPropertyChanged();
                }
            }
        }

        public ValidatableObject<string> UserLogin { get; set; }

        public ValidatableObject<string> UserPassword { get; set; }

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

        private async void Authorize(object obj)
        {
            IsButtonEnabled = false;

            if (Validate())
            {
                Tuple<bool, string> response = await UserService.Instance.Login(UserLogin.Value, UserPassword.Value);

                if (response.Item1 == true)
                {
                    IsLoginFormVisible = false;
                    MessagingCenter.Send(this, "authorized");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Message", response.Item2, "OK");
                    IsButtonEnabled = true;
                }
            }
            else
            {
                IsButtonEnabled = true;
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

        private void AddValidations()
        {
            UserLogin.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Username is required."
            });

            UserPassword.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Password is required."
            });
        }

        private bool Validate()
        {
            bool isValidUser = ValidateUserName();
            bool isValidPassword = ValidatePassword();
            return isValidUser && isValidPassword;
        }

        private bool ValidateUserName()
        {
            return UserLogin.Validate();
        }

        private bool ValidatePassword()
        {
            return UserPassword.Validate();
        }

        private async void OpenLoginPopupPage()
        {
            IsButtonEnabled = false;
            await PopupNavigation.Instance.PushAsync(new LoginPopupPageView(new LoginPopupPageViewModel()));
            IsButtonEnabled = true;
        }
    }
}
