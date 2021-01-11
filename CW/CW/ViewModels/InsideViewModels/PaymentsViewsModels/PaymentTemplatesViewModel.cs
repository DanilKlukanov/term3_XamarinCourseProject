using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CW.ViewModels.InsideViewModels
{
    public class PaymentTemplatesViewModel : BaseViewModel
    {
        public INavigation Navigation { get; private set; }
        public PaymentTemplatesViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }
    }
}