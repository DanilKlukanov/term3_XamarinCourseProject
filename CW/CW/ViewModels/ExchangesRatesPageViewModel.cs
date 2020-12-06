using System;
using System.Collections.Generic;
using System.Text;

namespace CW.ViewModels
{
    public class ExchangesRatesPageViewModel : BaseViewModel
    {
        public StartPageViewModel StartViewModel { get; private set; }
        public ExchangesRatesPageViewModel(StartPageViewModel startViewModel)
        {
            StartViewModel = startViewModel;
        }
    }
}
