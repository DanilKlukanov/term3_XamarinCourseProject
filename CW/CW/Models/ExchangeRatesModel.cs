using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Models
{
    public class ExchangeRatesModel
    {
        public string CharCode { get; set; }
        public int Nominal { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public double Previous { get; set; }
        public decimal Buy { get; set; }
        public decimal Sell { get; set; }
        public string Flag { get; set; }
        public string ArrowBuy { get; set; }
        public string ColorBuy { get; set; }
        public string ArrowSell { get; set; }
        public string ColorSell { get; set; }
        [JsonIgnore]
        public string ID { get; set; }
        [JsonIgnore]
        public string NumCode { get; set; }
    }
}
