using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CW.Models
{
    public class Message
    {

        public string from_ { get; set; }
        public string to_ { get; set; }
        public string msg { get; set; }
        public string msg_time { get; set; }
        public Color col { get; set; }
    }
}
