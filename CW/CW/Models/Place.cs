using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xamarin.Forms.Maps;

namespace CW.Models
{
    public class Place
    {
        public string PlaceName { get; set; }
        public string Address { get; set; }
        public string Icon { get; set; }
        public string Distance { get; set; }
        public bool? OpenNow { get; set; }

        public List<string> OpenPeriod { get; set; }
        public Position Position { get; set; }
        public Location Location { get; set; }
    }
}
