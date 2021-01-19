using CW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CW.Views
{

    public partial class ExchangesRatesView : ContentPage
    {
        public ExchangesRatesView()
        {
            InitializeComponent();      
        }

        protected override void OnDisappearing() => 
            (BindingContext as ExchangesRatesViewModel).StartViewModel.ClosePageCommand.Execute(null);
    }
}