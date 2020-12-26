using CW.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace CW.Services
{
    public class MapService
    {
        public static MapService Instance { get => _instance.Value; }

        #region Singleton
        private static readonly Lazy<MapService> _instance = new Lazy<MapService>(() => new MapService());
        private MapService() { }
        #endregion

        private readonly string ID_KEY = "AIzaSyDwhbUO5TZQY2wnKO00E3OGsIhfqhtVw40";

        public async Task<Root> Get(Position pos)
        {
            try
            {
                string user_position = pos.Latitude.ToString().Replace(",", ".") +
                    "," + pos.Longitude.ToString().Replace(",", ".");

                var uri = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={user_position}&radius=15000&type=atm&language=ru&key={ID_KEY}";
               
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Root>(json);
                    }
                    else
                    {
                        throw new HttpRequestException("");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("");
            }

        }

        public async Task<PlaceDetail> GetDetail(string place_id)
        {
            var placeDetailRequest = $"https://maps.googleapis.com/maps/api/place/details/json?place_id={place_id}&fields=opening_hours/open_now,opening_hours/weekday_text&language=ru&key={ID_KEY}";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(placeDetailRequest);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<PlaceDetail>(json);
                }
                else
                {
                    throw new HttpRequestException("");
                }
            }
        }
    }
}
