using CW.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CW.ViewModels.InsideViewModels.OperationsViewModels
{
    public class TransferCardViewModel : BaseViewModel
    {
        public ObservableCollection<BankCard> BankCards { get; private set; }
        public ObservableCollection<BankAccount> BankAccounts { get; private set; }
        public BankItem SelectedBankItem { get; private set; }
        public INavigation Navigation { get; private set; }

        public TransferCardViewModel(INavigation navigation, ObservableCollection<BankCard> cards, ObservableCollection<BankAccount> accounts, BankItem item)
        {
            Navigation = navigation;
            BankCards = cards;
            SelectedBankItem = item;
            BankAccounts = accounts;
        }
    }
}