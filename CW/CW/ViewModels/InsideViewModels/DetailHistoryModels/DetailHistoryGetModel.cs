using CW.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CW.ViewModels.InsideViewModels.DetailHistoryModels
{
    public class DetailHistoryGetModel : BaseViewModel
    {
        public History Payment { get; private set; }
        public string Name { get; private set; }
        public string ImgUrl { get; private set; }
        public double Money { get; private set; }
        public string Currency { get; private set; }
        public DetailHistoryGetModel(History payment)
        {
            Payment = payment;
        }
    }
}