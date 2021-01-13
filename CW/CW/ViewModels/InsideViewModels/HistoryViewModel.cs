using CW.Services;
using CW.Models;

using CW.Views.InsideViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CW.ViewModels.InsideViewModels
{
    class HistoryViewModel : BaseViewModel
    {
        private bool _isEnabled;

        public HistoryViewModel(INavigation navigation)
        {
            Navigation = navigation;
            _isEnabled = true;

            OpenProfilePageCommand = new Command(OpenProfilePage);
            BackCommand = new Command(Back, () => _isEnabled);
        }

        public INavigation Navigation { get; private set; }
        public ICommand BackCommand { get; private set; }
        public ICommand OpenProfilePageCommand { get; private set; }


        private void Back()
        {
            Navigation.PopAsync();
        }

        private void OpenProfilePage()
        {
            Navigation.PushAsync(new ProfileView(new ProfileViewModel(Navigation)));
        }
    }
}
