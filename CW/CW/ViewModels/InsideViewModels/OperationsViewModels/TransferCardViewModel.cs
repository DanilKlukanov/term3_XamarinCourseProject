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

        public TransferCardViewModel(ObservableCollection<BankCard> cards, BankItem item)
        {
            BankCards = cards;
            SelectedBankItem = item;
        }
    }
}