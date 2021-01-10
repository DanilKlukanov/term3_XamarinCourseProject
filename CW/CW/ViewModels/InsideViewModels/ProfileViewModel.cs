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

        private UserService userService;

        public ProfileViewModel(INavigation navigation)
        {
            LogoutCommand = new Command(Logout);
            ChangeLoginCommand = new Command(ChangeLogin);
            ChangePasswordCommand = new Command(ChangePassword);
            OpenVisitHistoryPageCommand = new Command(OpenVisitHistoryPage);
            OpenApplicationInfoPageCommand = new Command(OpenApplicationInfoPage);
            Navigation = navigation;
            userService = new UserService();
        }

        private void OpenApplicationInfoPage()
        {
            Navigation.PushAsync(new ApplicationInfoView());
        }

        private async void OpenVisitHistoryPage()
        {
            string json = await userService.GetVisitHistory(App.GetUser().id);

            List<string> visits = JsonConvert.DeserializeObject<List<string>>(json);
            var dates = visits.Select(x => DateTime.Parse(x)).OrderByDescending(x => x).ToList();

            json = JsonConvert.SerializeObject(dates);

            await Application.Current.MainPage.DisplayAlert("Message", json, "OK");

            Navigation.PushAsync(new VisitHistoryView());
        }

        private async void ChangePassword()
        {
            string new_password = await Application.Current.MainPage.DisplayPromptAsync("Изменение пароля", "Введите новый пароль");
            if (new_password != null)
            {
                var r = await userService.ChangePassword(App.GetUser().login, new_password);
                await Application.Current.MainPage.DisplayAlert("Message", r, "OK");
            }
        }

        private async void ChangeLogin()
        {
            string new_login = await Application.Current.MainPage.DisplayPromptAsync("Изменение логина", "Введите новый логин");
            if (new_login != null)
            {
                var r = await userService.ChangeLogin(App.GetUser().login, new_login);
                await Application.Current.MainPage.DisplayAlert("Message", r, "OK");
            }
        }

        private void Logout()
        {
            userService.Logout();
            MessagingCenter.Send(this, "logout");
        }
    }
}
