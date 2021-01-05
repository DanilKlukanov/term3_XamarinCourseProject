using CW.Views.InsideViews;
using System;
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

        private void OpenVisitHistoryPage()
        {
            Navigation.PushAsync(new VisitHistoryView());
        }

        private async void ChangePassword()
        {
            string new_password = await Application.Current.MainPage.DisplayPromptAsync("Изменение пароля", "Введите новый пароль");
            if (new_password != null)
            {
                // Logic for password changing
            }
        }

        private async void ChangeLogin()
        {
            string new_login = await Application.Current.MainPage.DisplayPromptAsync("Изменение логина", "Введите новый логин");
            if (new_login != null)
            {
                // Logic for login changing
            }
        }

        private void Logout()
        {
            MessagingCenter.Send(this, "logout");
        }
    }
}
