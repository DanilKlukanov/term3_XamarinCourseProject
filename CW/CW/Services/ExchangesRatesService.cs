using CW.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CW.Services
{
    class ExchangesRatesService
    {
        private static ExchangesRatesService _instance;
        public static ExchangesRatesService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ExchangesRatesService();
                }

                return _instance;
            }
        }
        private readonly decimal RatioBuy = 0.013556m;
        private readonly decimal RatioSell = -0.0260691m;
        private List<string> FlagCode = new List<string>
        {
            "🇦🇺", "🇦🇿", "🇬🇧", "🇦🇲", "🇧🇾", "🇧🇬",
            "🇧🇷", "🇭🇺", "🇭🇰", "🇩🇰", "🇺🇸", "🇪🇺",
            "🇮🇳", "🇰🇿", "🇨🇦", "🇰🇬", "🇨🇳", "🇲🇩",
            "🇳🇴", "🇵🇱", "🇷🇴", "🇸🇬", "🇹🇯", "🇹🇷",
            "🇹🇲", "🇺🇿", "🇺🇦", "🇨🇿", "🇸🇪", "🇨🇭",
            "🇿🇦","🇰🇷", "🇯🇵"
        };
        public async Task<List<ExchangeRatesModel>> GetExchangesRates()
        {
            try
            {
                var uri = "https://www.cbr-xml-daily.ru/daily_json.js";
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        JObject content = JObject.Parse(json);
                        JObject valutes = content["Valute"].Value<JObject>();
                        valutes.Property("XDR").Remove();
                        valutes.Property("GBP").Value["Name"] = "Фунт стерлингов";
                        JArray valutesArray = new JArray();
                        var index = 0;
                        foreach (KeyValuePair<string, JToken> sign in valutes)
                        {
                            if (sign.Value["Nominal"].ToString() != "1")
                            {
                                sign.Value["CharCode"] = sign.Value["Nominal"].ToString() + " " + sign.Value["CharCode"];
                            }
                            decimal.TryParse(sign.Value["Value"].ToString(), out decimal value);
                            decimal.TryParse(sign.Value["Previous"].ToString(), out decimal previous);
                            var buy = value + (value * RatioBuy);
                            var sell = value + (value * RatioSell);
                            var previousBuy = previous + (previous * RatioBuy);
                            var previousSell = previous + (previous * RatioSell);
                            sign.Value["Buy"] = Math.Round(buy, 2, MidpointRounding.AwayFromZero);
                            sign.Value["Sell"] = Math.Round(sell, 2, MidpointRounding.AwayFromZero);
                            sign.Value["Flag"] = FlagCode[index];
                            if (buy > previousBuy)
                            {
                                sign.Value["ArrowBuy"] = "▲";
                                sign.Value["ColorBuy"] = "Green";
                            }
                            else if (buy < previousBuy)
                            {
                                sign.Value["ArrowBuy"] = "▼";
                                sign.Value["ColorBuy"] = "Red";
                            }
                            else if (buy == previousBuy)
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
                            valutesArray.Add(sign.Value);
                            index++;
                        }
                        return JsonConvert.DeserializeObject<List<ExchangeRatesModel>>(valutesArray.ToString());
                    }
                    else
                    {
                        throw new HttpRequestException("");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("");
            }
        }
    }
}
