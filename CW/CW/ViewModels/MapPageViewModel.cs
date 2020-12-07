using System;
using System.Collections.Generic;
using System.Text;

namespace CW.ViewModels
{
    public class MapPageViewModel : BaseViewModel
    {
        public StartPageViewModel StartViewModel { get; private set; }
        public MapPageViewModel(StartPageViewModel startViewModel)
        {
            StartViewModel = startViewModel;
        }
    }
}
