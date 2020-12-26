using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace CW.Models
{
    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Northeast
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Southwest
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Viewport
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
        public Viewport viewport { get; set; }
    }

    public class Photo
    {
        public int height { get; set; }
        public List<string> html_attributions { get; set; }
        public string photo_reference { get; set; }
        public int width { get; set; }
    }

    public class PlusCode
    {
        public string compound_code { get; set; }
        public string global_code { get; set; }
    }

    public class OpeningHours
    {
        public bool open_now { get; set; }
        public List<string> weekday_text { get; set; }
    }

    public class Result
    {
        public string business_status { get; set; }
        public Geometry geometry { get; set; }
        public string icon { get; set; }
        public string name { get; set; }
        public bool permanently_closed { get; set; }
        public List<Photo> photos { get; set; }
        public string place_id { get; set; }
        public PlusCode plus_code { get; set; }
        public int price_level { get; set; }
        public double rating { get; set; }
        public string reference { get; set; }
        public string scope { get; set; }
        public List<string> types { get; set; }
        public int user_ratings_total { get; set; }
        public string vicinity { get; set; }
        public OpeningHours opening_hours { get; set; }
    }

    public class Root
    {
        //public List<object> html_attributions { get; set; }
        //public string next_page_token { get; set; }
        public List<Result> results { get; set; }
        public string status { get; set; }
    }

    public class PlaceDetail
    {
        public List<object> html_attributions { get; set; }
        public DetailtResult result { get; set; }
    }

    public class DetailtResult
    {
        public OpeningHours opening_hours { get; set; }
    }
}
