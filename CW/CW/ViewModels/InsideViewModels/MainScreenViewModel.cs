using CW.Views.InsideViews;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using CW.Models;
using System.Collections.ObjectModel;
using CW.Views.InsideViews.Operations;
using CW.Services;

namespace CW.ViewModels.InsideViewModels
{
    public class MainScreenViewModel : BaseViewModel
    {
        private bool _isEnabled;

        public MainScreenViewModel(INavigation navigation)
        {
            BankCards = new ObservableCollection<BankCard>();
            BankAccounts = new ObservableCollection<BankAccount>();
            BankCredits = new ObservableCollection<BankCredit>();

            LoadListBankItems();
            Navigation = navigation;
            _isEnabled = true;

            OpenProfilePageCommand = new Command(OpenProfilePage);
            BackCommand = new Command(Back, () => _isEnabled);
            OpenBankCardPageCommand = new Command(OpenBankCardPage);
            OpenBankAccountPageCommand = new Command(OpenBankAccounPage);
        }

        public ObservableCollection<BankCard> BankCards { get; private set; }
        public ObservableCollection<BankAccount> BankAccounts { get; private set; }
        public ObservableCollection<BankCredit> BankCredits { get; private set; }

        public INavigation Navigation { get; private set; }
        public ICommand BackCommand { get; private set; }
        public ICommand OpenProfilePageCommand { get; private set; }
        public ICommand OpenBankCardPageCommand { get; private set; }
        public ICommand OpenBankAccountPageCommand { get; private set; }

        private async void LoadListBankItems()
        {
            var bills = await BillsService.Instance.GetBills();

            var bankCards = bills.Where(x => x.type != "bill").Select(x => new BankCard(x)).ToList();
            var bankAccounts = bills.Where(x => x.type == "bill").Select(x => new BankAccount(x)).ToList();

            bankCards.ForEach(x => BankCards.Add(x));
            bankAccounts.ForEach(x => BankAccounts.Add(x));

            BankCredits.Add(new BankCredit()
            {
                Name = "Ипотека",
                Date = DateTime.Now,
                PaymentInfo = "Платеж",
                Money = 788
            });

            BankCredits.Add(new BankCredit()
            {
                Name = "Кредит наличными",
                Date = DateTime.Now,
                PaymentInfo = "Платеж",
                Money = 1020
            });
        }

        private void Back()
        {
            Navigation.PopAsync();
        }

        private void OpenProfilePage()
        {
            Navigation.PushAsync(new ProfileView(new ProfileViewModel(Navigation)));
        }

        private void OpenBankAccounPage(object item)
        {
            var bankItem = item as BankAccount;

            if (bankItem != null)
            {
                Navigation.PushAsync(new BankAccountsView(new BankItemViewModel(this, bankItem)));
            }
        }

        private void OpenBankCardPage(object item)
        {
            var bankItem = item as BankCard;
            
            if (bankItem != null)
            {
                Navigation.PushAsync(new BankCardsView(new BankItemViewModel(this, bankItem)));
            }
        }
        private bool isRefreshing = false;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    BankCards.Clear();
                    //BankAccounts.Clear();

                    //LoadListBankItems()
                    var bills = await BillsService.Instance.GetBills();

                    var bankCards = bills.Where(x => x.type != "bill").Select(x => new BankCard(x)).ToList();
                    //var bankAccounts = bills.Where(x => x.type == "bill").Select(x => new BankAccount(x)).ToList();

                    bankCards.ForEach(x => BankCards.Add(x));
                    //bankAccounts.ForEach(x => BankAccounts.Add(x));

                    IsRefreshing = false;
                });
            }
        }
    }
}
