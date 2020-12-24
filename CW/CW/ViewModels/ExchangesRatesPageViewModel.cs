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
            "🇧🇾",
            "🇧🇬",
            "🇧🇷",
            "🇭🇺",
            "🇭🇰",
            "🇩🇰",
            "🇺🇸",
            "🇪🇺",
            "🇮🇳",
            "🇰🇿",
            "🇨🇦",
            "🇰🇬",
            "🇨🇳",
            "🇲🇩",
            "🇳🇴",
            "🇵🇱",
            "🇷🇴",
            "🚫",
            "🇸🇬",
            "🇹🇯",
            "🇹🇷",
            "🇹🇲",
            "🇺🇿",
            "🇺🇦",
            "🇨🇿",
            "🇸🇪",
            "🇨🇭",
            "🇿🇦",
            "🇰🇷",
            "🇯🇵"
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
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(url)
                };
                var response = await client.GetAsync(client.BaseAddress);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(content);
                JObject valutes = json["Valute"].Value<JObject>();
                var index = 0;
                foreach (KeyValuePair<string, JToken>sign in valutes)
                {
                    if (sign.Key != "XDR")
                    {
                        if (sign.Value["Name"].ToString() == "Фунт стерлингов Соединенного королевства")
                        {
                            sign.Value["Name"] = "Фунт стерлингов";
                        }
                        var clear = Decimal.Parse(sign.Value["Value"].ToString());
                        var previous = Decimal.Parse(sign.Value["Previous"].ToString());
                        var buy = clear + (clear * 0.013556m);
                        var sell = clear + (clear * -0.0260691m);
                        var previousBuy = previous + (previous * 0.013556m);
                        var previousSell = previous + (previous * -0.0260691m);
                        if (sign.Value["Nominal"].ToString() != "1")
                        {
                            sign.Value["CharCode"] = sign.Value["Nominal"].ToString() + " " + sign.Value["CharCode"];
                        }
                        sign.Value["Buy"] = Math.Round(buy, 2, MidpointRounding.AwayFromZero);
                        sign.Value["Sell"] = Math.Round(sell, 2, MidpointRounding.AwayFromZero);
                        sign.Value["Flag"] = FlagCode[index];
                        if (buy > previousBuy)
                        {
                            sign.Value["ArrowBuy"] = "▲";
                            sign.Value["ColorBuy"] = "Green";
                        } else if (buy < previousBuy)
                        {
                            sign.Value["ArrowBuy"] = "▼";
                            sign.Value["ColorBuy"] = "Red";
                        } else if (buy == previousBuy)
                        {
                            sign.Value["ArrowBuy"] = "";
                        }
                        if (sell > previousSell)
                        {
                            sign.Value["ArrowSell"] = "▲";
                            sign.Value["ColorSell"] = "Green";
                        }
                        else if (sell < previousSell)
                        {
                            sign.Value["ArrowSell"] = "▼";
                            sign.Value["ColorSell"] = "Red";
                        }
                        else if (sell == previousSell)
                        {
                            sign.Value["ArrowSell"] = "";
                        }
                        Rates.Add(JsonConvert.DeserializeObject<ExchangesRatesPageModel>(sign.Value.ToString()));
                    }
                    index++;
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