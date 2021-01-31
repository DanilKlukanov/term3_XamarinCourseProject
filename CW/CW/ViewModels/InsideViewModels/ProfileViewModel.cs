using CW.Models;
using CW.Services;
using CW.Views.InsideViews;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using CW.Validations;

namespace CW.ViewModels.InsideViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private ValidatableObject<string> _newPassword;
        private ValidatableObject<string> _newLogin;
        private bool _isButtonEnabled;

        public INavigation Navigation { get; set; }
        public ICommand LogoutCommand { get; private set; }
        public ICommand ChangeLoginCommand { get; private set; }
        public ICommand ChangePasswordCommand { get; private set; }
        public ICommand OpenVisitHistoryPageCommand { get; private set; }
        public ICommand OpenApplicationInfoPageCommand { get; private set; }

        public ProfileViewModel(INavigation navigation)
        {
            LogoutCommand = new Command(Logout);
            ChangeLoginCommand = new Command(ChangeLogin, () => IsButtonEnabled);
            ChangePasswordCommand = new Command(ChangePassword, () => IsButtonEnabled);
            OpenVisitHistoryPageCommand = new Command(OpenVisitHistoryPage, () => IsButtonEnabled);
            OpenApplicationInfoPageCommand = new Command(OpenApplicationInfoPage, () => IsButtonEnabled);
            _isButtonEnabled = true;
            Navigation = navigation;

            _newLogin = new ValidatableObject<string>();
            _newPassword = new ValidatableObject<string>();

            AddValidations();
        }
        private bool IsButtonEnabled
        {
            get => _isButtonEnabled;

            set
            {
                if (value != _isButtonEnabled)
                {
                    _isButtonEnabled = value;

                    (ChangeLoginCommand as Command)?.ChangeCanExecute();
                    (ChangePasswordCommand as Command)?.ChangeCanExecute();
                    (OpenVisitHistoryPageCommand as Command)?.ChangeCanExecute();
                    (OpenApplicationInfoPageCommand as Command)?.ChangeCanExecute();
                }
            }
        }

        private async void OpenApplicationInfoPage()
        {
            IsButtonEnabled = false;
            await Navigation.PushAsync(new ApplicationInfoView());
            IsButtonEnabled = true;
        }

        private async void OpenVisitHistoryPage()
        {
            try
            {
                IsButtonEnabled = false;
                string json = await UserService.Instance.GetVisitHistory(App.GetUser().login);

                var raw_dates = JsonConvert.DeserializeObject<List<string>>(json);
                List<string> dates = new List<string>();
                raw_dates.ForEach(a => dates.Add(DateTime.Parse(a).ToString("MM/dd/yyyy HH:mm:ss")));
                await Navigation.PushAsync(new VisitHistoryView(dates));
                IsButtonEnabled = true;
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Message", "Возникла непредвиденная ошибка. Повторите позднее.", "OK");
            }          

        }

        private async void ChangePassword()
        {
            IsButtonEnabled = false;
            _newPassword.Value = await Application.Current.MainPage.DisplayPromptAsync("Изменение пароля", "Введите новый пароль");
            IsButtonEnabled = true;

            if (_newPassword.Value == null)
                return;

            if (ValidateInput(_newPassword))
            {
                var r = await UserService.Instance.ChangePassword(App.GetUser().login, _newPassword.Value);
                await Application.Current.MainPage.DisplayAlert("Message", r, "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Message", _newPassword.Errors[0], "OK");
            }
        }

        private async void ChangeLogin()
        {
            IsButtonEnabled = false;
            _newLogin.Value = await Application.Current.MainPage.DisplayPromptAsync("Изменение логина", "Введите новый логин");
            IsButtonEnabled = true;

            if (_newLogin.Value == null)
                return;

            if (ValidateInput(_newLogin))
            {
                var response = await UserService.Instance.ChangeLogin(App.GetUser().login, _newLogin.Value);
                IsButtonEnabled = false;
                await Application.Current.MainPage.DisplayAlert("Message", response, "OK");
                IsButtonEnabled = true;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Message", _newLogin.Errors[0], "OK");
            }
        }

        private void Logout()
        {
            UserService.Instance.Logout();
            MessagingCenter.Send(this, "logout");
        }

        private bool ValidateInput<T>(ValidatableObject<T> validatableObject)
        {
            return validatableObject.Validate();
        }

        private void AddValidations()
        {
            _newPassword.Validations.Add(new IncorrectStringLenghtRule<string>()
            {
                ValidationMessage = "Некорректный ввод. Длина пароля должна составлять от 6 до 16 символов"
            });
            _newPassword.Validations.Add(new IncorrectSymbolsRule<string>()
            {
                ValidationMessage = "Некорректный ввод. Пароль содержит недопустимые символы."
            });

            _newLogin.Validations.Add(new IncorrectStringLenghtRule<string>()
            {
                ValidationMessage = "Некорректный ввод. Длина логина должна составлять от 6 до 16 символов"
            });
            _newLogin.Validations.Add(new IncorrectSymbolsRule<string>()
            {
                ValidationMessage = "Некорректный ввод. Логин содержит недопустимые символы."
            });
        }
    }
}
