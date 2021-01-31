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
        private bool _isButtonEnabled;
        public ObservableCollection<BankCard> BankCards { get; private set; }
        public ObservableCollection<BankAccount> BankAccounts { get; private set; }
        public BankItem SelectedBankItem { get; private set; }
        public INavigation Navigation { get; private set; }
        public ICommand OpenCard { get; private set; }
        public ICommand OpenAccount { get; private set; }
        public ICommand OpenTopUp { get; private set; }
        public ICommand OpenTopUpAccount { get; private set; }
        public TopUpCardViewModel(INavigation navigation, ObservableCollection<BankCard> cards, ObservableCollection<BankAccount> accounts, BankItem item)
        {
            Navigation = navigation;
            _isButtonEnabled = true;
            BankCards = new ObservableCollection<BankCard>();
            cards.Where(x => x != item as BankCard && x.IsWorked == true).ForEach(x => BankCards.Add(x));
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
            OpenTopUp = new Command(TopUp, (_) => IsButtonEnabled);
            OpenTopUpAccount = new Command(TopUpAccount, (_) => IsButtonEnabled);
        }
        private bool IsButtonEnabled
        {
            get => _isButtonEnabled;

            set
            {
                if (value != _isButtonEnabled)
                {
                    _isButtonEnabled = value;

                    (OpenTopUp as Command)?.ChangeCanExecute();
                    (OpenTopUpAccount as Command)?.ChangeCanExecute();
                }
            }
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
        private async void TopUp(object item)
        {
            var toCard = item as BankCard;
            IsButtonEnabled = false;
            await Navigation.PushAsync(new PaymentTopUpView(new PaymentTopUpViewModel(SelectedBankItem, toCard)));
            IsButtonEnabled = true;
        }
        private async void TopUpAccount(object item)
        {
            var toAccount = item as BankAccount;
            IsButtonEnabled = false;
            await Navigation.PushAsync(new PaymentTopUpView(new PaymentTopUpViewModel(SelectedBankItem, toAccount)));
            IsButtonEnabled = true;
        }
    }
}