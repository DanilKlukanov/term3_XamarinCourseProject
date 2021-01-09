using CW.Models;
using CW.Views.InsideViews.OperationsViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace CW.ViewModels.InsideViewModels.OperationsViewModels
{
    public class TopUpCardViewModel : BaseViewModel
    {
        public ObservableCollection<BankCard> BankCards { get; private set; }
        public ObservableCollection<BankAccount> BankAccounts { get; private set; }
        public BankItem SelectedBankItem { get; private set; }
        public INavigation Navigation { get; private set; }
        public ICommand OpenCard { get; private set; }
        public ICommand OpenAccount { get; private set; }
        public ICommand OpenTopUp { get; private set; }
        public ICommand OpenTopUpAccount { get; private set; }
        public TopUpCardViewModel(BankItemViewModel info, ObservableCollection<BankCard> cards, ObservableCollection<BankAccount> accounts, BankItem item)
        {
            Navigation = info.Navigation;
            BankCards = new ObservableCollection<BankCard>();
            foreach(BankCard element in cards)
            {
                if (element != item as BankCard)
                {
                    BankCards.Add(element);
                }
            }
            BankAccounts = accounts;
            SelectedBankItem = item;
            OpenCard = new Command(() => {
                IsOpenCardVisible = true;
                IsOpenAccountVisible = false;
            });
            OpenAccount = new Command(() => {
                IsOpenCardVisible = false;
                IsOpenAccountVisible = true;
            });
            OpenTopUp = new Command(TopUp);
            OpenTopUpAccount = new Command(TopUpAccount);
        }
        private bool isOpenCardVisible = true;
        public bool IsOpenCardVisible
        {
            get => isOpenCardVisible;
            set
            {
                if (value != isOpenCardVisible)
                {
                    isOpenCardVisible = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool isOpenAccountVisible;
        public bool IsOpenAccountVisible
        {
            get => isOpenAccountVisible;
            set
            {
                if (value != isOpenAccountVisible)
                {
                    isOpenAccountVisible = value;
                    OnPropertyChanged();
                }
            }
        }
        private void TopUp(object item)
        {
            var toCard = item as BankCard;
            Navigation.PushAsync(new PaymentTopUpView(new PaymentTopUpViewModel(SelectedBankItem, toCard)));
        }
        private void TopUpAccount(object item)
        {
            var toAccount = item as BankAccount;
            Navigation.PushAsync(new PaymentTopUpView(new PaymentTopUpViewModel(SelectedBankItem, toAccount)));
        }
    }
}