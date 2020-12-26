using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CW.Services
{
    public class LocationService
    {
        public static LocationService Instance { get => _instance.Value; }

        #region Singleton
        private static readonly Lazy<LocationService> _instance = new Lazy<LocationService>(() => new LocationService());

        private LocationService() { }
        #endregion

        public async Task<Location> Get()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                return await Geolocation.GetLocationAsync(request);
            } 
            catch (Exception ex)
            {
                throw new Exception("");
            }
        }
    }
}
