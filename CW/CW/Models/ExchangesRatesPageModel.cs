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
        public decimal PreviousBuy { get; set; }
        public decimal PreviousSell { get; set; }
        public string Flag { get; set; }
    }
}
