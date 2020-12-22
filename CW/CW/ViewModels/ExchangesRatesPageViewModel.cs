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
        public ObservableCollection<ExchangesRatesPageModel> Rates { get; private set; }
        public string CurrentDate { get; private set; }
        private List<string> FlagCode = new List<string>
        {
            "🇦🇺",
            "🇦🇿",
            "🇬🇧",
            "🇦🇲",
        };
        public ExchangesRatesPageViewModel(StartPageViewModel startViewModel)
        {
            StartViewModel = startViewModel;
            Rates = new ObservableCollection<ExchangesRatesPageModel>();
            LoadRates();
        }
        async private void LoadRates()
        {
            CurrentDate = DateTime.Today.ToString("d");
            string url = "https://www.cbr-xml-daily.ru/daily_json.js";
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                var response = await client.GetAsync(client.BaseAddress);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(content);
                JObject valutes = json["Valute"].Value<JObject>();
                foreach (KeyValuePair<string, JToken> sign in valutes)
                {
                    if (valutes[sign.Key]["Nominal"].ToString() == "1")
                    {
                        var about = sign.Value.ToString();
                        var clear = Decimal.Parse(valutes[sign.Key]["Value"].ToString());
                        var previousClear = Decimal.Parse(valutes[sign.Key]["Previous"].ToString());
                        valutes[sign.Key]["Buy"] = Math.Round(clear + (clear * 0.025m), 2, MidpointRounding.AwayFromZero).ToString();
                        valutes[sign.Key]["Sell"] = Math.Round(clear + (clear * -0.012m), 2, MidpointRounding.AwayFromZero).ToString();
                        valutes[sign.Key]["PreviousBuy"] = Math.Round(previousClear + (previousClear * 0.025m), 2, MidpointRounding.AwayFromZero).ToString();
                        valutes[sign.Key]["PreviousSell"] = Math.Round(previousClear + (previousClear * -0.012m), 2, MidpointRounding.AwayFromZero).ToString();
                        Rates.Add(JsonConvert.DeserializeObject<ExchangesRatesPageModel>(sign.Value.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                StartViewModel.ClosePageCommand.Execute(null);
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message.ToString(), "Оk");
            }
        }
    }
}