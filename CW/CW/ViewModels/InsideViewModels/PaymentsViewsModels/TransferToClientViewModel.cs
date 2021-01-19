using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using CW.Services;
using Xamarin.Forms;
using CW.Views.InsideViews.Operations;
using System.Collections.ObjectModel;
using CW.Models;
using CW.Converters;
using CW.ViewModels.InsideViewModels.OperationsViewModels;
using CW.Commands;

namespace CW.ViewModels.InsideViewModels.PaymentsViewsModels
{
    public class TransferToClientViewModel : BaseViewModel
    {
        public INavigation Navigation { get; private set; }
        public ICommand TransferCommand { get; private set; }
        public ObservableCollection<BankCard> BankCards { get; private set; }
        public ObservableCollection<BankAccount> BankAccounts { get; private set; }

        public TransferToClientViewModel(INavigation navigation, ObservableCollection<BankCard> cards, ObservableCollection<BankAccount> accounts)
        {
            Navigation = navigation;
            BankCards = cards;
            BankAccounts = accounts;
            TransferCommand = new TapCardCommand(Transfer);
        }
        private void Transfer(object item)
        {
            var selectedItem = item as BankCard;

            Navigation.PushAsync(new TransferCardView(new TransferCardViewModel(Navigation, BankCards, BankAccounts,selectedItem)));
        }

    }
}