using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CW.Views;
using CW.ViewModels;
using CW.Services.MessagingService;
using CW.Views.InsideViews;
using CW.Models;
using System.Threading.Tasks;
using CW.Services;

namespace CW
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MessagingService.Instance.Subscribe();

            //MainPage = CreateStartPage();
            InitNavigation();
            //MainPage = new RootPageView();
        }

        public static Page CreateStartPage()
        {
            NavigationPage page = new NavigationPage(new LoginView());
            page.BarBackgroundColor = Color.FromHex("#86c5da");
            return page;
        }

        public static User GetUser()
        {
            return user.Copy();
        }

        public static void SetUser(User u)
        {
            user = u;
        }

        private Task InitNavigation()
        {
            return NavigationService.Instance.InitializeAsync();
        }

        private static User user;

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
