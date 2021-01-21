using CW.Models;
using CW.ViewModels.InsideViewModels.OperationsViewModels;
using CW.Views.InsideViews.Operations;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using CW.Services;
using CW.Views.InsideViews;
using System;

namespace CW.ViewModels.InsideViewModels
{
    public class BankItemViewModel : BaseViewModel
    {
        private string _path = "CW.ViewModels.InsideViewModels.";

        private ObservableCollection<BankCard> bankCards { get; set; }
        public ObservableCollection<BankAccount> BankAccounts { get; private set; }
        public ObservableCollection<History> BankItemHistory { get; set; }

        private BankItem selectedBankItem;

        public INavigation Navigation { get; private set; }
        public ICommand TopUpCard { get; private set; }
        public ICommand TransferCard { get; private set; }
        public ICommand HistoryCommand { get; private set; }
        public ICommand BlockCardCommand { get; private set; }
        public ICommand RenameCardCommand { get; private set; }

        public BankItemViewModel(MainScreenViewModel viewModel, BankItem bankItem)
        {
            BankCards = viewModel.BankCards;
            BankAccounts = viewModel.BankAccounts;
            Navigation = viewModel.Navigation;
            SelectedBankItem = bankItem;

            BankItemHistory = new ObservableCollection<History>();

            TopUpCard = new Command(TopUp);
            TransferCard = new Command(Transfer);
            HistoryCommand = new Command(OpenHistory);
            BlockCardCommand = new Command(BlockCard);
            RenameCardCommand = new Command(RenameCard);
        }

        // TODO (Change return value for BlockCard from BillService)
        private async void BlockCard()
        {
            var result = await BillsService.Instance.BlockCard(SelectedBankItem.Number);

            if (result.IsSuccessful)
            {
                IsCardInterfaceActive = false;
            }

            await Application.Current.MainPage.DisplayAlert("Уведомление", result.ErrorMessage, "ОК");
        }

        private async void RenameCard()
        {
            var result = await Application.Current.MainPage.DisplayPromptAsync("Имя карты", "Введите новое имя карты", "OK");
            if (result != null)
            {
                await BillsService.Instance.RenameCard(SelectedBankItem.Number, result);
            }
        }

        public ObservableCollection<BankCard> BankCards
        {
            get { return bankCards; }
            set
            {
                if (bankCards != value)
                {
                    bankCards = value;
                    OnPropertyChanged();
                }
            }
        }

        public BankItem SelectedBankItem
        {
            get
            {
                return selectedBankItem;

            }
            set
            {
                if (selectedBankItem != value)
                {
                    selectedBankItem = value;
                    IsCardInterfaceActive = (selectedBankItem as BankCard)?.IsWorked ?? true;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsCardInterfaceActive
        {
            get => (SelectedBankItem as BankCard)?.IsWorked ?? true;
            set
            {
                if ((SelectedBankItem as BankCard)?.IsWorked != null)
                {
                    (SelectedBankItem as BankCard).IsWorked = value;
                    OnPropertyChanged();
                }
            }
        }

        private void TopUp()
        {
            Navigation.PushAsync(new TopUpCardView(new TopUpCardViewModel(Navigation, BankCards, BankAccounts, SelectedBankItem)));
        }

        private void Transfer()
        {
            Navigation.PushAsync(new TransferCardView(new TransferCardViewModel(Navigation, BankCards, BankAccounts, SelectedBankItem)));
        }

        private void OpenHistory()
        {
            Navigation.PushAsync(new HistoryView(new HistoryViewModel(Navigation, _path + "BankItemViewModel", this)));
        }
    }
}
