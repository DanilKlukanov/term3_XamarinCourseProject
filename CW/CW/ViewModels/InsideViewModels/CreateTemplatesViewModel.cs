using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CW.ViewModels.InsideViewModels
{
    public class CreateTemplatesViewModel : BaseViewModel
    {
        public INavigation Navigation { get; private set; }
        public CreateTemplatesViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }
    }
}