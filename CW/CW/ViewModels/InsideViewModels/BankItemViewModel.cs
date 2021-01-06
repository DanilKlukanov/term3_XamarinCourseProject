using CW.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CW.ViewModels.InsideViewModels
{
    public class BankItemViewModel : BaseViewModel
    {
        public ObservableCollection<BankCard> BankCards { get; private set; }
        public ObservableCollection<BankAccount> BankAccounts { get; private set; }
        public BankItem SelectedBankItem { get; private set; }
        public INavigation Navigation { get; private set; }


        public BankItemViewModel(MainScreenViewModel viewModel, BankItem bankItem)
        {
            BankCards = viewModel.BankCards;
            BankAccounts = viewModel.BankAccounts;
            Navigation = viewModel.Navigation;
            SelectedBankItem = bankItem;
        }
    }
}
