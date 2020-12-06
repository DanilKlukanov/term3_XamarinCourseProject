using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
    }
}
