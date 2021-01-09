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
        public BankItem SelectedBankItem { get; private set; }
        public INavigation Navigation { get; private set; }

        public TransferCardViewModel(BankItemViewModel info, ObservableCollection<BankCard> cards, BankItem item)
        {
            Navigation = info.Navigation;
            BankCards = cards;
            SelectedBankItem = item;
        }
    }
}