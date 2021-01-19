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
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace CW.ViewModels
{
    public class ExchangesRatesViewModel : BaseViewModel
    {
        public LoginViewModel StartViewModel { get; private set; }

        private ObservableCollection<ExchangeRatesModel> _rates;


        public ObservableCollection<ExchangeRatesModel> Rates
        {
            get => _rates;
            set
            {
                _rates = value;
                OnPropertyChanged();
            }
        }

        public string CurrentDate { get; private set; }

        public ExchangesRatesViewModel()
        {
            CurrentDate = DateTime.Today.ToString("d");
            Rates = new ObservableCollection<ExchangeRatesModel>();
        }

        public override Task InitializeAsync(object parameter)
        {
            if (parameter != null)
            {
                StartViewModel = parameter as LoginViewModel;
                //StartViewModel.Rates.ForEach(x => Rates.Add(x));
                Rates = StartViewModel.Rates;
            }

            return base.InitializeAsync(parameter);
        }
    }
}