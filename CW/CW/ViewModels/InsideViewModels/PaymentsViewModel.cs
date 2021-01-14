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
            AllPatterns = new ObservableCollection<Pattern>();

            LoadListBankItems();
            TransferToClientCommand = new Command(OpenTransferToClient);
            TransfeBetweenTheirCommand = new Command(OpenTransferBetweenTheir);
            CreatePatternCommand = new Command(OpenCreatePattern);
            OpenProfilePageCommand = new Command(OpenProfilePage);
            DeletePatternCommand = new Command(DeletePattern);
            BackCommand = new Command(Back, () => _isEnabled);
        }

        public ObservableCollection<Pattern> AllPatterns { get; private set; }
        public ObservableCollection<BankCard> BankCards { get; private set; }
        public ObservableCollection<BankAccount> BankAccounts { get; private set; }
        public INavigation Navigation { get; private set; }
        public ICommand BackCommand { get; private set; }
        public ICommand TransferToClientCommand { get; private set; }
        public ICommand TransfeBetweenTheirCommand { get; private set; }
        public ICommand OpenProfilePageCommand { get; private set; }
        public ICommand CreatePatternCommand { get; private set; }
        public ICommand DeletePatternCommand { get; private set; }
        private async void LoadListBankItems()
        {
            var bills = await BillsService.Instance.GetBills();

            var bankCards = bills.Where(x => x.type != "bill").Select(x => new BankCard(x)).ToList();
            var bankAccounts = bills.Where(x => x.type == "bill").Select(x => new BankAccount(x)).ToList();

            bankCards.ForEach(x => BankCards.Add(x));
            bankAccounts.ForEach(x => BankAccounts.Add(x));

            var patterns = await PatternService.Instance.GetPatterns();
            patterns.ForEach(x => AllPatterns.Add(x));
            Pattern trash = AllPatterns.Last();
        }
        private void Back()
        {
            Navigation.PopAsync();
        }
        private void OpenProfilePage()
        {
            Navigation.PushAsync(new ProfileView(new ProfileViewModel(Navigation)));
        }
        private void OpenCreatePattern(object item)
        {
            var selectedPattern = item as Pattern;
            Navigation.PushAsync(new PaymentTemplatesView(new PaymentTemplatesViewModel(Navigation)));
        }
        private async void DeletePattern(object item)
        {
            var selectedPattern = item as Pattern;
            if (await Application.Current.MainPage.DisplayAlert("Подтверждение", "Вы уверены?", "Да", "Нет")) {
                var response = await PatternService.Instance.RemovePattern(selectedPattern.name);
                if (response.Item1)
                {
                    AllPatterns.Remove(selectedPattern);
                }
                await Application.Current.MainPage.DisplayAlert("Message", response.Item2, "OK");
            }
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
