using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Models
{
    public class Bill
    {
        public string bill_number { get; set; }
        public string bill_currency { get; set; }
        public float balance { get; set; }
    }
}
