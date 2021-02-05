using CW.Models;
using CW.Services;
using CW.Validations;
using CW.ViewModels.InsideViewModels.PaymentsViewsModels;
using CW.Views.InsideViews;
using CW.Views.InsideViews.PaymentsViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CW.ViewModels.InsideViewModels
{
    public class PaymentsViewModel : BaseViewModel
    {
        public PaymentsViewModel(INavigation navigation)
        {
            Navigation = navigation;
            UserPassword = new ValidatableObject<string>();

            TransferToClientCommand = new Command(OpenTransferToClient);
            TransfeBetweenTheirCommand = new Command(OpenTransferBetweenTheir);
            CreatePatternCommand = new Command(OpenCreatePattern);
            OpenProfilePageCommand = new Command(OpenProfilePage);
            DeletePatternCommand = new Command(DeletePattern);
            ExecutePatternCommand = new Command(ExecutePatternAsync);
        }

        public ObservableCollection<Pattern> AllPatterns { get; private set; }
        public ObservableCollection<BankCard> BankCards { get; private set; }
        public ObservableCollection<BankAccount> BankAccounts { get; private set; }
        public ValidatableObject<string> UserPassword { get; set; }
        public INavigation Navigation { get; private set; }
        public ICommand TransferToClientCommand { get; private set; }
        public ICommand TransfeBetweenTheirCommand { get; private set; }
        public ICommand OpenProfilePageCommand { get; private set; }
        public ICommand CreatePatternCommand { get; private set; }
        public ICommand DeletePatternCommand { get; private set; }
        public ICommand ExecutePatternCommand { get; private set; }

        public async Task LoadAllItems()
        {
            BankCards = new ObservableCollection<BankCard>();
            BankAccounts = new ObservableCollection<BankAccount>();

            var cards = await BillsService.Instance.GetCards();
            var bills = await BillsService.Instance.GetBills();

            var bankCards = cards.Select(x => new BankCard(x)).ToList();
            var bankBills = bills.Select(x => new BankAccount(x)).ToList();

            bankCards.ForEach(x => BankCards.Add(x));
            bankBills.ForEach(x => BankAccounts.Add(x));

            AllPatterns = new ObservableCollection<Pattern>();

            var patterns = await PatternService.Instance.GetPatterns();
            patterns.ForEach(x =>
            {
                x.current = GetCurrent(x.from_);
                AllPatterns.Insert(0, x);
            });

            foreach (var propName in new List<string> { "BankCards", "BankAccounts", "AllPatterns" })
            {
                OnPropertyChanged(propName);
            }
        }
        private async void OpenProfilePage()
        {
            await RunIsBusyTaskAsync(async () => await Navigation.PushAsync(new ProfileView(new ProfileViewModel(Navigation))));
        }
        private void OpenCreatePattern(object item)
        {
            var selectedPattern = item as Pattern;
            Navigation.PushAsync(new CreatePatternView(new CreatePatternViewModel(selectedPattern)));
        }
        private async void DeletePattern(object item)
        {
            var selectedPattern = item as Pattern;
            if (await Application.Current.MainPage.DisplayAlert("Подтверждение", "Вы уверены?", "Да", "Нет"))
            {
                var response = await PatternService.Instance.RemovePattern(selectedPattern.pattern_name);
                if (response.Item1)
                {
                    AllPatterns.Remove(selectedPattern);
                }
                await Application.Current.MainPage.DisplayAlert("Message", response.Item2, "OK");
            }
        }
        private async void OpenTransferToClient()
        {
            await RunIsBusyTaskAsync(async () =>
                await Navigation.PushAsync(new TransferToClientView(new TransferToClientViewModel(Navigation, BankCards, BankAccounts))));
        }
        private async void OpenTransferBetweenTheir()
        {
            await RunIsBusyTaskAsync(async () =>
                await Navigation.PushAsync(new TransferBetweenView(new TransferBetweenViewModel(Navigation, BankCards, BankAccounts))));
        }
        private async void ExecutePatternAsync(object item)
        {
            Pattern selectedPattern = item as Pattern;
            UserPassword.Value = await Application.Current.MainPage.DisplayPromptAsync("Подтверждение перевода", "Введите пароль");
            if (UserPassword.Value == null)
                return;
            if (UserPassword.Validate())
            {
                Tuple<bool, string> responseCheck = await UserService.Instance.Login(App.GetUser().login, UserPassword.Value);
                if (responseCheck.Item1 == true)
                {
                    string response = await TransactionService.Instance.DoTransfer(selectedPattern.from_, selectedPattern.to_, selectedPattern.amount);
                    await Application.Current.MainPage.DisplayAlert("Message", response, "OK");

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Message", "Неправильно введен пароль", "OK");
                }
            }
        }
        private void AddCurrentInto()
        {
            foreach (var pattern in AllPatterns)
            {
                pattern.current = GetCurrent(pattern.from_);
            }
        }
        private string GetCurrent(string number)
        {
            if (number.Length == 16)
            {
                return BankCards.Where(item => item.Number == number).FirstOrDefault().Currency;
            } else
            {
                return BankAccounts.Where(item => item.Number == number).FirstOrDefault().Currency;
            }
        }
    }
}
