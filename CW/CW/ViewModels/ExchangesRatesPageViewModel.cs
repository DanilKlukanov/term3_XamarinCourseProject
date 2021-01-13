using CW.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Input;
using System.Xml;
using Xamarin.Forms;

namespace CW.ViewModels
{
    public class ExchangesRatesPageViewModel : BaseViewModel
    {
        public StartPageViewModel StartViewModel { get; private set; }
        public ObservableCollection<ExchangeRatesModel> Rates { get; private set; }
        public string CurrentDate { get; private set; }

        public ExchangesRatesPageViewModel(StartPageViewModel startViewModel)
        {
            StartViewModel = startViewModel;
            Rates = startViewModel.Rates;
            CurrentDate = DateTime.Today.ToString("d");
        }
    }
}