using CW.ViewModels;
using CW.ViewModels.InsideViewModels;
using CW.Views;
using CW.Views.InsideViews;
using Xamarin.Forms;

namespace CW.Services.MessagingService
{
    class MessagingService
    {
        private static MessagingService _instance;
        private MessagingService() { }

        public static MessagingService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MessagingService();
                }

                return _instance;
            }
        }

        public void Subscribe()
        {
            MessagingCenter.Subscribe<StartPageViewModel>(this, "authorized", (_) => App.Current.MainPage = new RootPageView());
            MessagingCenter.Subscribe<RootPageView>(this, "exit", (_) => System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow());
            MessagingCenter.Subscribe<ProfileViewModel>(this, "logout", (_) => App.Current.MainPage = App.CreateStartPage());
        }
    }
}
