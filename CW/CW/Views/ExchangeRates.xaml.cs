using CW.Models;
using CW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExchangeRates : ContentPage
    {
        ExchangesRatesPageViewModel ViewModel { get; set; }
        public ExchangeRates()
        {
            InitializeComponent();
            
        }

        public ExchangeRates(StartPageViewModel s) : this()
        {
            ViewModel = new ExchangesRatesPageViewModel(s);
            BindingContext = ViewModel;
        }

        protected override void OnDisappearing() => ViewModel.StartViewModel.ClosePageCommand.Execute(null);
    }
}