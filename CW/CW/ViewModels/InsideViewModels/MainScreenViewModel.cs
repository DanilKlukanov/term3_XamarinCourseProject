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
using System.Threading.Tasks;

namespace CW.ViewModels.InsideViewModels
{
    public class MainScreenViewModel : BaseViewModel
    {
        public MainScreenViewModel(INavigation navigation)
        {
            var user = App.GetUser();
            NameUser = user.firstname + " " + user.surnamme;

            Navigation = navigation;

            OpenProfilePageCommand = new Command(OpenProfilePage);
            BackCommand = new Command(Back);
            OpenBankCardPageCommand = new Command(OpenBankCardPage);
            OpenBankAccountPageCommand = new Command(OpenBankAccounPage);
        }
        public string NameUser { get; private set; }
        public ObservableCollection<BankCard> BankCards { get; private set; }
        public ObservableCollection<BankAccount> BankAccounts { get; private set; }
        public ObservableCollection<BankCredit> BankCredits { get; private set; }

        public INavigation Navigation { get; private set; }
        public ICommand BackCommand { get; private set; }
        public ICommand OpenProfilePageCommand { get; private set; }
        public ICommand OpenBankCardPageCommand { get; private set; }
        public ICommand OpenBankAccountPageCommand { get; private set; }

        private async Task LoadListBankItems()
        {
            var cards = await BillsService.Instance.GetCards();
            var bills = await BillsService.Instance.GetBills();
            var credits = await BillsService.Instance.GetCredits();

            var bankCards = cards.Select(x => new BankCard(x)).ToList();
            var bankBills = bills.Select(x => new BankAccount(x)).ToList();
            var bankCredits = credits.Select(x => new BankCredit(x)).ToList();

            BankCards = new ObservableCollection<BankCard>(bankCards);
            BankAccounts = new ObservableCollection<BankAccount>(bankBills);
            BankCredits = new ObservableCollection<BankCredit>(bankCredits);

            foreach (var propName in new List<string>{ "BankCards", "BankAccounts", "BankCredits" })
            {
                OnPropertyChanged(propName);
            }


        }

        private void Back()
        {
            Navigation.PopAsync();
        }

        private async void OpenProfilePage()
        {
            await RunIsBusyTaskAsync(async () => await Navigation.PushAsync(new ProfileView(new ProfileViewModel(Navigation))));
        }

        private async void OpenBankAccounPage(object item)
        {
            var bankItem = item as BankAccount;

            if (bankItem != null)
            {
                await RunIsBusyTaskAsync(async () => await Navigation.PushAsync(new BankAccountsView(new BankItemViewModel(this, bankItem))));
            }
        }

        private async void OpenBankCardPage(object item)
        {
            var bankItem = item as BankCard;
            
            if (bankItem != null)
            {
                await RunIsBusyTaskAsync(async () => await Navigation.PushAsync(new BankCardsView(new BankItemViewModel(this, bankItem))));
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
                    await LoadListBankItems();
                });
            }
        }
    }
}
