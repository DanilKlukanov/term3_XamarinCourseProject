using CW.Views.InsideViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using CW.Models;
<<<<<<< HEAD
using System.Collections.ObjectModel;
=======
>>>>>>> dc9f924... Add models for MainScreen, group of bank items

namespace CW.ViewModels.InsideViewModels
{
    public class MainScreenViewModel : BaseViewModel
    {
        private bool _isEnabled;
        

        public MainScreenViewModel(INavigation navigation)
        {
            Navigation = navigation;
            _isEnabled = true;

            LoadListBankItems();

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
        public ICommand OpenBankCardPageCommand { get; private set; }
        public ICommand OpenBankAccountPageCommand { get; private set; }

        private void LoadListBankItems()
        {
            BankCards = new ObservableCollection<BankCard>
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
                }
            };

            BankAccounts = new ObservableCollection<BankAccount>
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
            };

            BankCredits = new ObservableCollection<BankCredit>
            {
                new BankCredit
                {
                     Name = "Ипотека",
                     Date = DateTime.Now,
                     PaymentInfo = "Платеж",
                     Money = 788
                },
                new BankCredit
                {
                    Name = "Кредит наличными",
                    Date = DateTime.Now,
                    PaymentInfo = "Платеж",
                    Money = 1020
                }
            };
        }

        private void Back()
        {
            Navigation.PopAsync();
        }

        private void OpenProfilePage()
        {
            Navigation.PushAsync(new ProfileView(new ProfileViewModel(Navigation)));
        }

        private void OpenBankAccounPage()
        {
            Navigation.PushAsync(new BankCardsView(new BankItemViewModel(this)));
        }

        private void OpenBankCardPage()
        {
            Navigation.PushAsync(new BankAccountsView(new BankItemViewModel(this)));
        }

        public ICommand OpenProfilePageCommand { get; private set; }
    }
}
