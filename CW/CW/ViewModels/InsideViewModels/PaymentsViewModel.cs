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
using CW.Validations;

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
            UserPassword = new ValidatableObject<string>();

            LoadListBankItems();
            TransferToClientCommand = new Command(OpenTransferToClient);
            TransfeBetweenTheirCommand = new Command(OpenTransferBetweenTheir);
            CreatePatternCommand = new Command(OpenCreatePattern);
            OpenProfilePageCommand = new Command(OpenProfilePage);
            DeletePatternCommand = new Command(DeletePattern);
            ExecutePatternCommand = new Command(ExecutePatternAsync);
            BackCommand = new Command(Back, () => _isEnabled);
        }

        public ObservableCollection<Pattern> AllPatterns { get; private set; }
        public ObservableCollection<BankCard> BankCards { get; private set; }
        public ObservableCollection<BankAccount> BankAccounts { get; private set; }
        public ValidatableObject<string> UserPassword { get; set; }
        public INavigation Navigation { get; private set; }
        public ICommand BackCommand { get; private set; }
        public ICommand TransferToClientCommand { get; private set; }
        public ICommand TransfeBetweenTheirCommand { get; private set; }
        public ICommand OpenProfilePageCommand { get; private set; }
        public ICommand CreatePatternCommand { get; private set; }
        public ICommand DeletePatternCommand { get; private set; }
        public ICommand ExecutePatternCommand { get; private set; }
        private async void LoadListBankItems()
        {
            var bills = await BillsService.Instance.GetBills();

            //var bankCards = bills.Where(x => x.type != "bill" && x.type != "cred").Select(x => new BankCard(x)).ToList();
            //var bankAccounts = bills.Where(x => x.type == "bill" && x.type != "cred").Select(x => new BankAccount(x)).ToList();

            //bankCards.ForEach(x => BankCards.Add(x));
            //bankAccounts.ForEach(x => BankAccounts.Add(x));

            //var patterns = await PatternService.Instance.GetPatterns();
            //patterns.ForEach(x => AllPatterns.Add(x));
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
            Navigation.PushAsync(new CreatePatternView(new CreatePatternViewModel(selectedPattern)));
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
        private async void ExecutePatternAsync(object item)
        {
            Pattern selectedPattern = item as Pattern;
            UserPassword.Value = await Application.Current.MainPage.DisplayPromptAsync("Подтверждение перевода", "Введите пароль");
            if (UserPassword.Value == null)
                return;
            if (UserPassword.Validate())
            {
                User user = App.GetUser();
                Tuple<bool, string> responseCheck = await UserService.Instance.Login(user.login, UserPassword.Value);
                if (responseCheck.Item1 == true)
                {
                    int balance = GetMoney(selectedPattern);
                    if (balance - selectedPattern.amount >= 0)
                    {
                        string response = await TransactionService.Instance.DoTransfer(selectedPattern.from, selectedPattern.to, selectedPattern.amount);
                        await Application.Current.MainPage.DisplayAlert("Message", response, "OK");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Message", "Недостаточно средств на карте", "OK");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Message", "Неправильно введен пароль", "OK");
                }
            }
        }
        private int GetMoney(Pattern pattern)
        {
            /*            if (pattern.from.Length == 16)
                        {
                            return decimal.ToInt32(BankCards.Where(card => card.Number == pattern.from).FirstOrDefault().Money);
                        }
                        else
                        {
                            return decimal.ToInt32(BankAccounts.Where(card => card.Number == pattern.from).FirstOrDefault().Money);
                        }*/
            return 1;
        }
    }
}
