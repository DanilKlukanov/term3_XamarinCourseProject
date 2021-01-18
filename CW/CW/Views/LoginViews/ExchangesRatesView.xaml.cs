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
        ExchangesRatesViewModel ViewModel { get; set; }
        public ExchangesRatesView()
        {
            InitializeComponent();      
        }

/*        public ExchangeRatesView(LoginViewModel s) : this()
        {
            ViewModel = new ExchangesRatesViewModel(s);
            BindingContext = ViewModel;
        }*/

        protected override void OnDisappearing() => (BindingContext as ExchangesRatesViewModel).StartViewModel.ClosePageCommand.Execute(null);
    }
}