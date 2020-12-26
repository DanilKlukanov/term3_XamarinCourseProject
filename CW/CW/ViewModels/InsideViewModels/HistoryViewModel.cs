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

            BackCommand = new Command(Back, () => _isEnabled);
        }

        public INavigation Navigation { get; private set; }
        public ICommand BackCommand { get; private set; }


        private void Back()
        {
            Navigation.PopAsync();
        }
    }
}
