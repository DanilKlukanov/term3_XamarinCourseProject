using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Models
{
    public class Card
    {
        public string card_name { get; set; }
        public string card_number { get; set; }
        public string card_currency { get; set; }
        public string card_type { get; set; }
        public double balance { get; set; }
        public bool card_blocked { get; set; }
    }
}
