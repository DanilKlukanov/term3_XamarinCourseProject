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
        public LoginViewModel StartViewModel { get; private set; }

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

        public NearbyBanksViewModel()
        {
            _places = new ObservableCollection<Place>();

            // TODO (kekouke): Catch exception
            GetUserLocation();
        }


        public override Task InitializeAsync(object parameter)
        {
            if (parameter != null)
            {
                var viewModel = parameter as LoginViewModel;
                StartViewModel = viewModel;
            }

            return base.InitializeAsync(parameter);
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

            if (response.IsSuccessful)
            {
                            
                foreach (var place in response.Value.results)
                {
                    var opening_hours = await MapService.Instance.GetDetail(place.place_id);

                    if (opening_hours.IsSuccessful)
                    {
                        places.Add(new Place()
                        {
                            PlaceName = place.name,
                            Address = place.vicinity,
                            Location = place.geometry.location,
                            Position = new Position(place.geometry.location.lat, place.geometry.location.lng),
                            OpenNow = place?.opening_hours?.open_now,
                            OpenPeriod = opening_hours.Value?.result?.opening_hours?.weekday_text ?? new List<string>()
                        });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        await Application.Current.MainPage.DisplayAlert("Уведомление", response.ErrorMessage, "ОК"));
                    }
                }

                Places = places;
                OnPropertyChanged("Places");
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                await Application.Current.MainPage.DisplayAlert("Уведомление", response.ErrorMessage, "ОК"));
            }

        }
    }
}
