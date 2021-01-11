using CW.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CW.ViewModels.InsideViewModels.PaymentsViewsModels
{
    public class TransferBetweenViewModel : BaseViewModel
    {
        public INavigation Navigation { get; private set; }
        public ObservableCollection<BankCard> BankCards { get; private set; }
        public ObservableCollection<BankAccount> BankAccounts { get; private set; }
        public TransferBetweenViewModel(INavigation navigation)
        {
            Navigation = navigation;
            //BankCards = cards;
            //BankAccounts = accounts;
        }
    }
}