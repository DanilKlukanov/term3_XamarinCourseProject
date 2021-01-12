using CW.Views.InsideViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using CW.Models;
using System.Collections.ObjectModel;
using CW.Views.InsideViews.PaymentsViews;
using CW.ViewModels.InsideViewModels.PaymentsViewsModels;
using CW.Services;
using System.Linq;

namespace CW.ViewModels.InsideViewModels
{
    public class PaymentsViewModel : BaseViewModel
    {
        private bool _isEnabled;

        public PaymentsViewModel(INavigation navigation)
        {
            Navigation = navigation;
            _isEnabled = true;
            BankCards = new ObservableCollection<BankCard>();
            BankAccounts = new ObservableCollection<BankAccount>();

            ListTamplates();
            LoadListBankItems();
            TransferToClientCommand = new Command(OpenTransferToClient);
            TransfeBetweenTheirCommand = new Command(OpenTransferBetweenTheir);
            CreateTemplatesCommand = new Command(OpenCreateTemplates);
            OpenProfilePageCommand = new Command(OpenProfilePage);
            BackCommand = new Command(Back, () => _isEnabled);
        }

        public ObservableCollection<NameOperationInTemplate> PayTemplates { get; private set; }
        public ObservableCollection<BankCard> BankCards { get; private set; }
        public ObservableCollection<BankAccount> BankAccounts { get; private set; }
        public INavigation Navigation { get; private set; }
        public ICommand BackCommand { get; private set; }
        public ICommand TransferToClientCommand { get; private set; }
        public ICommand TransfeBetweenTheirCommand { get; private set; }
        public ICommand OpenProfilePageCommand { get; private set; }
        public ICommand CreateTemplatesCommand { get; private set; }
        private async void LoadListBankItems()
        {
            var bills = await BillsService.Instance.GetBills();

            var bankCards = bills.Where(x => x.type != "bill").Select(x => new BankCard(x)).ToList();
            var bankAccounts = bills.Where(x => x.type == "bill").Select(x => new BankAccount(x)).ToList();

            bankCards.ForEach(x => BankCards.Add(x));
            bankAccounts.ForEach(x => BankAccounts.Add(x));
        }
        private void Back()
        {
            Navigation.PopAsync();
        }

        private void ListTamplates()
        {
            PayTemplates = new ObservableCollection<NameOperationInTemplate>
            {
                new NameOperationInTemplate
                {
                    Name = "Перевод между своими счетами и картами",
                    NameOperation = "Перевод",
                    Money = 1
                },
                new NameOperationInTemplate
                {
                    Name = "Данил Вадимович У.",
                    NameOperation = "Перевод",
                    Money = 1800
                },
                new NameOperationInTemplate
                {
                    Name = "МТС",
                    NameOperation = "Оплата услуг",
                    Money = 550
                },
                new NameOperationInTemplate
                {
                    Name = "Перевож между своими счетами и картами",
                    NameOperation = "Пополнение баланса",
                    Money = 150
                },
            };
        }

        private void OpenProfilePage()
        {
            Navigation.PushAsync(new ProfileView(new ProfileViewModel(Navigation)));
        }
        private void OpenCreateTemplates()
        {
            Navigation.PushAsync(new PaymentTemplatesView(new PaymentTemplatesViewModel(Navigation)));
        }
        
        private void OpenTransferToClient()
        {
            Navigation.PushAsync(new TransferToClientView(new TransferToClientViewModel(Navigation, BankCards, BankAccounts)));
        }
        private void OpenTransferBetweenTheir()
        {
            Navigation.PushAsync(new TransferBetweenView(new TransferBetweenViewModel(Navigation, BankCards, BankAccounts)));
        }
    }
}
