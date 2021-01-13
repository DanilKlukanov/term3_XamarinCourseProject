using CW.Models;
using CW.Views.InsideViews.OperationsViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CW.ViewModels.InsideViewModels.OperationsViewModels
{
    public class TransferCardViewModel : BaseViewModel
    {
        public ObservableCollection<BankCard> BankCards { get; private set; }
        public ObservableCollection<BankAccount> BankAccounts { get; private set; }
        public BankItem SelectedBankItem { get; private set; }
        public INavigation Navigation { get; private set; }
        public ICommand OpenTransferTapCommand { get; private set; }

        public TransferCardViewModel(INavigation navigation, ObservableCollection<BankCard> cards, ObservableCollection<BankAccount> accounts, BankItem item)
        {
            Navigation = navigation;
            BankCards = new ObservableCollection<BankCard>();
            foreach (BankCard element in cards)
            {
                if (element != item as BankCard)
                {
                    BankCards.Add(element);
                }
            }
            SelectedBankItem = item;
            BankAccounts = accounts;
            OpenTransferTapCommand = new Command(OpenTransferTap);
        }

        private void OpenTransferTap(object item)
        {
            var toCard = item as BankCard;
            Navigation.PushAsync(new TransferPageView(new TransferPageViewModel(SelectedBankItem, toCard)));
        }
    }
}