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
        bool bComeIn;
        public StartPage()
        {
            InitializeComponent();
            bMap = false;
            bExchangeRates = false;
            bComeIn = false;
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
            if (!bComeIn)
            {
                bComeIn = true;

                // Navigation.PushModalAsync();
                bComeIn = false;
            }
        }
    }
}