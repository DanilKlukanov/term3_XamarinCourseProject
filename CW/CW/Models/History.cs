using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Models
{
    public class History
    {
        public int amount { get; set; }
        public string from { get; set; }
        public DateTime time { get; set; }
        public string to { get; set; }
        public string type { get; set; }
    }
}
