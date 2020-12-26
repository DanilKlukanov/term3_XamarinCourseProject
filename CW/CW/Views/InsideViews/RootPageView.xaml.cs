using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CW.Views.InsideViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPageView : Shell
    {
        public RootPageView()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();

            Device.BeginInvokeOnMainThread(async () => {
                var result = await DisplayAlert("Выход", "Вы уверены, что хотите выйти?", "Да", "Нет");

                if (result)
                {
                    MessagingCenter.Send<Shell>(this, "exit");
                }
            });

            return true;
        }
    }
}