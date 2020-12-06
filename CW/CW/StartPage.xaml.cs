using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CW
{
    public partial class StartPage : ContentPage
    {
        bool bMap;
        bool bExchangeRates;
        public StartPage()
        {
            InitializeComponent();
            bMap = false;
            bExchangeRates = false;
        }

        private void Map_Tapped(object sender, EventArgs e)
        {
            if (!bMap)
            {
                bMap = true;
                Map page = new Map();
                page.Disappearing += (s, ev) => {
                    bMap = false;
                };
                Navigation.PushAsync(page);
            }
        }

        private void ExchangeRates_Tapped(object sender, EventArgs e)
        {
            if (!bExchangeRates)
            {
                bExchangeRates = true;
                ExchangeRates page = new ExchangeRates();
                page.Disappearing += (s, ev) => {
                    bExchangeRates = false;
                };
                Navigation.PushAsync(page);
            }
        }

        private void ComeIn_Tapped(object sender, EventArgs e)
        {
            message.Text = "Введите Ваш логин и пароль";
            message.TextColor = Color.Black;
            login.Text = "";
            password.Text = "";
            form.IsVisible = true;
        }

        private void Hide(object sender, EventArgs e)
        {
            form.IsVisible = false;
        }

        private void Confirm_Clicked(object sender, EventArgs e)
        {
            string test_login = "basov";
            string test_password = "qwerty";
            // Check user data
            bool error = (login.Text != test_login || password.Text != test_password);
            if (error)
            {
                message.Text = "Ошибка, проверьте данные";
                message.TextColor = Color.Red;
            }
            else
            {
                message.Text = "Успех";
                message.TextColor = Color.Green;
                UserPage page = new UserPage();
                Navigation.PushModalAsync(page);
                form.IsVisible = false;
            }
        }
    }
}