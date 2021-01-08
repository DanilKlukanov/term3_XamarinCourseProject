using CW.Models;
using CW.ViewModels.InsideViewModels.OperationsViewModels;
using CW.Views.InsideViews.Operations;
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
        public ICommand TopUpCard { get; private set; }
        public ICommand TransferCard { get; private set; }
        public BankItemViewModel(MainScreenViewModel viewModel, BankItem bankItem)
        {
            BankCards = viewModel.BankCards;
            BankAccounts = viewModel.BankAccounts;
            Navigation = viewModel.Navigation;
            SelectedBankItem = bankItem;
            TopUpCard = new Command(TopUp);
            TransferCard = new Command(Transfer);
        }
        private void TopUp()
        {
            Navigation.PushAsync(new TopUpCardView(new TopUpCardViewModel(this, BankCards, BankAccounts, SelectedBankItem)));
        }
        private void Transfer()
        {
            Navigation.PushAsync(new TransferCardView(new TransferCardViewModel(BankCards, SelectedBankItem)));
        }
    }
}
