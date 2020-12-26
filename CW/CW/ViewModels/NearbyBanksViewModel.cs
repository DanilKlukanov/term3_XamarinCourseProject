using CW.Models;
using CW.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace CW.ViewModels
{
    public class NearbyBanksViewModel : BaseViewModel
    {
        public StartPageViewModel StartViewModel { get; private set; }

        private ObservableCollection<Place> _places;
        public ObservableCollection<Place> Places
        {
            get { return _places; }
            set
            {
                if (_places != value)
                {
                    _places = value;
                    OnPropertyChanged();
                }
            }
        }

        private Position _position;
        public Position Position
        {
            get => _position;
            set
            {
                if (_position != value)
                {
                    _position = value;
                    OnPropertyChanged();
                }
            }
        }

        public NearbyBanksViewModel(StartPageViewModel startViewModel)
        {
            StartViewModel = startViewModel;
            _places = new ObservableCollection<Place>();

            // TODO (kekouke): Catch exception
            GetUserLocation();
        }

        

        private async void GetUserLocation()
        {
            var response = await LocationService.Instance.Get();
            Position = new Position(response.Latitude, response.Longitude);
            Thread t = new Thread(GetMapInformation);
            t.Start();
        }

        private async void GetMapInformation()
        {
            var places = new ObservableCollection<Place>();
            var response = await MapService.Instance.Get(Position);

            foreach (var place in response.results)
            {
                var opening_hours = await MapService.Instance.GetDetail(place.place_id);
                places.Add(new Place()
                {
                    PlaceName = place.name,
                    Address = place.vicinity,
                    Location = place.geometry.location,
                    Position = new Position(place.geometry.location.lat, place.geometry.location.lng),
                    OpenNow = place?.opening_hours?.open_now,
                    OpenPeriod = opening_hours?.result?.opening_hours?.weekday_text ?? new List<string>()
                });
            }

            Places = places;
            OnPropertyChanged("Places");
        }
    }
}
