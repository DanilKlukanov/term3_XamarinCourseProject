using System;

namespace CW.Models
{
    public class ExchangesRatesPageModel
    {
        public string ID { get; set; }
        public double NumCode { get; set; }
        public string CharCode { get; set; }
        public short Nominal { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public decimal Previous { get; set; }
        public decimal Buy { get; set; }
        public decimal Sell { get; set; }
        public string Flag { get; set; }
        public string ArrowBuy { get; set; }
        public string ColorBuy { get; set; }
        public string ArrowSell { get; set; }
        public string ColorSell { get; set; }
    }
}
