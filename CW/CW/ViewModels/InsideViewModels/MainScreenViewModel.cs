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
            BillsService bil= new BillsService();
            
            var bankCards = (await bil.GetBills()).Where(x => x.type != "bill").Select(x => new BankCard(x)).ToList();
            bankCards.ForEach(x => BankCards.Add(x));

            /*BankCards = new ObservableCollection<BankCard>
            {
                new BankCard
                {
                    Name = "Дебетовая карта",
                    Number = "2202201948567017",
                    ImgUrl = "rates_icon",
                    Money = 12000
                },
                new BankCard
                {
                    Name = "Дебетовая карта",
                    Number = "2202201950501111",
                    ImgUrl = "rates_icon",
                    Money = 800
                },
                new BankCard
                {
                    Name = "Дебетовая карта",
                    Number = "2202201945211111",
                    ImgUrl = "rates_icon",
                    Money = 232211
                }
            };*/

            var bankAccounts = (await bil.GetBills()).Where(x => x.type == "bill").Select(x => new BankAccount(x)).ToList();
            bankAccounts.ForEach(x => BankAccounts.Add(x));


            /* BankAccounts = new ObservableCollection<BankAccount>
             {
                 new BankAccount
                 {
                     Name = "Текущий счет",
                     Number = "2202201948567017",
                     Money = 9001112
                 },
                 new BankAccount
                 {
                     Name = "Текущий счет",
                     Number = "2202201948523232",
                     Money = 5
                 }
             };*/

            BankCredits.Add(new BankCredit
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

            await Application.Current.MainPage.DisplayAlert("title", "message", "cancel");
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
    }
}
