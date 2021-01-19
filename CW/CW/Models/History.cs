using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Models
{
    public class History
    {
        public string user_number { get; set; }
        public string other_number { get; set; }
        public double amount { get; set; }
        public string operation_currency { get; set; }
        public string operation_type { get; set; }
        public string operation_time { get; set; }
    }
}
