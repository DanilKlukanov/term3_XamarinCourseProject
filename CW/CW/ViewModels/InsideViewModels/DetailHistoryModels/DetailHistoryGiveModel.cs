using CW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CW.ViewModels.InsideViewModels.DetailHistoryModels
{
    public class DetailHistoryGiveModel : BaseViewModel
    {
        public History Payment { get; private set; }
        public DetailHistoryGiveModel(History payment)
        {
            Payment = payment;
        }
    }
}