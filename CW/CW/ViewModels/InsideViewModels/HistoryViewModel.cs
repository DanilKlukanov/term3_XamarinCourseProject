using CW.Services;
using CW.Models;
using CW.Views.InsideViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using CW.ViewModels.InsideViewModels.PaymentsViewsModels;
using System.Threading.Tasks;
using CW.Views.InsideViews.DetailHistoryViews;
using CW.ViewModels.InsideViewModels.DetailHistoryModels;

namespace CW.ViewModels.InsideViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        private bool _isEnabled;
        
        public string TypeName { get; set; }

        public HistoryViewModel(INavigation navigation, string type, BankItemViewModel viewModel = null)
        {
            Navigation = navigation;
            _isEnabled = true;

            OpenProfilePageCommand = new Command(OpenProfilePage);
            AddPatternCommand = new Command(OnAddPatternAsync);
            BackCommand = new Command(Back, () => _isEnabled);
            OpenDetailPageCommand = new Command(OpenDetailPage);

            AllHistory = new ObservableCollection<History>();
            BankCards = new ObservableCollection<BankCard>();
            BankAccounts = new ObservableCollection<BankAccount>();

            TypeName = type;

            LoadAllHistory(viewModel);
            LoadListBankItems();
        }
        public ObservableCollection<History> AllHistory { get; private set; }
        public ObservableCollection<BankCard> BankCards { get; private set; }
        public ObservableCollection<BankAccount> BankAccounts { get; private set; }
        public INavigation Navigation { get; private set; }
        public ICommand BackCommand { get; private set; }
        public ICommand OpenProfilePageCommand { get; private set; }
        public ICommand AddPatternCommand { get; private set; }
        public ICommand OpenDetailPageCommand { get; private set; }

        private async Task<List<History>> GetHistoryBill(BankItemViewModel viewModel)
        {
            return await BillsService.Instance.GetPartHistory(viewModel.SelectedBankItem.Number);
        }
        private async Task<List<History>> GetHistoryBills()
        {
            return await BillsService.Instance.GetHistory();
        }
        private async void LoadAllHistory(BankItemViewModel viewModel)
        {      
            List<History> histories = new List<History>();

            if (Type.GetType(TypeName) == typeof(BankItemViewModel))
            {
                histories = await GetHistoryBill(viewModel);
            }

            if (Type.GetType(TypeName) == typeof(HistoryView))
            {
                histories = await GetHistoryBills();
            }

            histories.ForEach(x => AllHistory.Add(x));

            ChangeHistory();
        }
        private void ChangeHistory()
        {
            foreach(History item in AllHistory)
            {
                if (item.operation_type == "give")
                {
                    item.operation_type = "Перевод на карту";
                }
                if (item.operation_type == "get")
                {
                    item.operation_type = "Входящий перевод";
                }
            }

        }
        private async Task LoadListBankItems()
        {
            var cards = await BillsService.Instance.GetCards();
            var bills = await BillsService.Instance.GetBills();

            var bankCards = cards.Select(x => new BankCard(x)).ToList();
            var bankBills = bills.Select(x => new BankAccount(x)).ToList();

            bankCards.ForEach(x => BankCards.Add(x));
            bankBills.ForEach(x => BankAccounts.Add(x));
        }
        private void Back()
        {
            Navigation.PopAsync();
        }
        private void OpenProfilePage()
        {
            Navigation.PushAsync(new ProfileView(new ProfileViewModel(Navigation)));
        }
        private void OpenDetailPage(object item)
        {
            var payment = item as History;
            if (payment.operation_type == "Перевод на карту")
            {
                Navigation.PushAsync(new DetailHistoryGiveView(new DetailHistoryGiveModel(payment, BankCards, BankAccounts)));
            } else
            {
                Navigation.PushAsync(new DetailHistoryGetView(new DetailHistoryGetModel(payment, BankCards, BankAccounts)));
            }
        }
        public async void OnAddPatternAsync (object item)
        {
            var payment = item as History;
            if (payment.operation_type == "Перевод на карту") {
                string namePattern = await Application.Current.MainPage.DisplayPromptAsync("Создание шаблона", "Введите название");
                if (namePattern != null)
                {
                    string response = await PatternService.Instance.CreatePattern(namePattern, payment.user_number, payment.other_number, payment.amount);
                    await Application.Current.MainPage.DisplayAlert("Message", response, "OK");
                }
            }
        }
    }
}
