using CW.Services;
using CW.Validations;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CW.ViewModels
{
    public class LoginPopupPageViewModel : BaseViewModel
    {
        private string _autorizationIngo = "Введите Ваш логин и пароль";
        private bool _isButtonEnabled;
        private bool _isLoginFormVisible;

        public LoginPopupPageViewModel()
        {
            _isButtonEnabled = true;
            _isLoginFormVisible = true;

            UserLogin = new ValidatableObject<string>();
            UserPassword = new ValidatableObject<string>();

            AuthorizationCommand = new Command(Authorize, (_) => IsButtonEnabled);
            HideLoginFormCommand = new Command(HideLoginPopupPage);

            AddValidations();
        }
        public ICommand AuthorizationCommand { get; protected set; }
        public ICommand HideLoginFormCommand { get; protected set; }

        public ValidatableObject<string> UserLogin { get; set; }
        public ValidatableObject<string> UserPassword { get; set; }
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

        public bool IsButtonEnabled
        {
            get => _isButtonEnabled;

            set
            {
                if (value != _isButtonEnabled)
                {
                    _isButtonEnabled = value;

                    (AuthorizationCommand as Command)?.ChangeCanExecute();
                }
            }
        }

        public bool IsLoginFormVisible
        {
            get => _isLoginFormVisible;

            set
            {
                if (value != _isLoginFormVisible)
                {
                    _isLoginFormVisible = value;
                }
            }
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
        
        private void HideLoginPopupPage()
        {
            AutorizationInfo = "Введите Ваш логин и пароль";
            PopupNavigation.Instance.PopAsync();
        }
    }
}
