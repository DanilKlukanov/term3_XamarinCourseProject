using CW.Models;
using CW.Services;
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
        public ICommand OpenTransferCommand { get; private set; }
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
            OpenTransferCommand = new Command(OpenTransfer);
        }
        private string numberCard = null;
        public string NumberCard
        {
            get { return numberCard; }
            set
            {
                if (numberCard != value)
                {
                    numberCard = value;
                    OnPropertyChanged();
                }
            }
        }
        private void OpenTransferTap(object item)
        {
            var toCard = item as BankCard;
            Navigation.PushAsync(new TransferPageView(new TransferPageViewModel(SelectedBankItem, toCard)));
        }
        private async void OpenTransfer()
        {
            if (NumberCard == null)
            {
                return;
            }
            if (NumberCard.Length != 10 && NumberCard.Length <= 20)
            {
                var response = await TransactionService.Instance.CheckCard(NumberCard);
                if (!response.Item1)
                {
                    await Application.Current.MainPage.DisplayAlert("Message", response.Item2, "OK");
                }
                else
                {
                    await Navigation.PushAsync(new TransferPageView(new TransferPageViewModel(SelectedBankItem, NumberCard)));
                }
            }
        }
    }
}