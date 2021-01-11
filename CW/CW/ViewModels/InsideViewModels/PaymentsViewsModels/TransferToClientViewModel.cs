using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CW.ViewModels.InsideViewModels.PaymentsViewsModels
{
    public class TransferToClientViewModel : BaseViewModel
    {
        public INavigation Navigation { get; private set; }
        public TransferToClientViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }
    }
}