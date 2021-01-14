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
        public INavigation Navigation { get; set; }
        public ICommand LogoutCommand { get; private set; }
        public ICommand ChangeLoginCommand { get; private set; }
        public ICommand ChangePasswordCommand { get; private set; }
        public ICommand OpenVisitHistoryPageCommand { get; private set; }
        public ICommand OpenApplicationInfoPageCommand { get; private set; }

        public ProfileViewModel(INavigation navigation)
        {
            LogoutCommand = new Command(Logout);
            ChangeLoginCommand = new Command(ChangeLogin);
            ChangePasswordCommand = new Command(ChangePassword);
            OpenVisitHistoryPageCommand = new Command(OpenVisitHistoryPage);
            OpenApplicationInfoPageCommand = new Command(OpenApplicationInfoPage);
            Navigation = navigation;
        }

        private void OpenApplicationInfoPage()
        {
            Navigation.PushAsync(new ApplicationInfoView());
        }

        private async void OpenVisitHistoryPage()
        {
            try
            {
                string json = await UserService.Instance.GetVisitHistory(App.GetUser().id);

                var dates = JsonConvert.DeserializeObject<List<string>>(json);
                Navigation.PushAsync(new VisitHistoryView(dates));
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Message", "Возникла непредвиденная ошибка. Повторите позднее.", "OK");
            }          

        }

        private async void ChangePassword()
        {
            var new_password = new ValidatableObject<string>();
            new_password.Validations.Add(new IncorrectStringLenghtRule<string>()
            {
                ValidationMessage = "Некорректный ввод. Длина пароля должна составлять от 6 до 16 символов"
            });
            new_password.Validations.Add(new IncorrectSymbolsRule<string>()
            {
                ValidationMessage = "Некорректный ввод. Пароль содержит недопустимые символы."
            });

            new_password.Value = await Application.Current.MainPage.DisplayPromptAsync("Изменение пароля", "Введите новый пароль");

            if (new_password.Value == null)
                return;

            if (ValidateInput(new_password))
            {
                var r = await UserService.Instance.ChangePassword(App.GetUser().login, new_password.Value);
                await Application.Current.MainPage.DisplayAlert("Message", r, "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Message", new_password.Errors[0], "OK");
            }
        }

        private async void ChangeLogin()
        {
            var new_login = new ValidatableObject<string>();
            new_login.Validations.Add(new IncorrectStringLenghtRule<string>()
            {
                ValidationMessage = "Некорректный ввод. Длина логина должна составлять от 6 до 16 символов"
            });
            new_login.Validations.Add(new IncorrectSymbolsRule<string>()
            {
                ValidationMessage = "Некорректный ввод. Логин содержит недопустимые символы."
            });
            
            new_login.Value = await Application.Current.MainPage.DisplayPromptAsync("Изменение логина", "Введите новый логин");

            if (new_login.Value == null)
                return;

            if (ValidateInput(new_login))
            {
                var response = await UserService.Instance.ChangeLogin(App.GetUser().login, new_login.Value);
                await Application.Current.MainPage.DisplayAlert("Message", response, "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Message", new_login.Errors[0], "OK");
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
    }
}
