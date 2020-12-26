using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CW.Views;
using CW.ViewModels;

namespace CW
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            NavigationPage page = new NavigationPage(new StartPage());
            page.BarBackgroundColor = Color.FromHex("#86c5da");
            MainPage = page;
            MessagingCenter.Subscribe<StartPageViewModel>(this, "authorized", (_) => OpenInsideApp());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void OpenInsideApp()
        {
            MainPage = new Views.InsideViews.RootPageView();
        }
    }
}
