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
        public string TypeName { get; set; }
        private List<History> _histories;

        public HistoryViewModel(INavigation navigation, string type, BankItemViewModel viewModel = null)
        {
            Navigation = navigation;

            _histories = new List<History>();

            OpenProfilePageCommand = new Command(OpenProfilePage);
            OpenDetailPageCommand = new Command(OpenDetailPage);

            //AllHistory = new ObservableCollection<History>();

            TypeName = type;

            LoadAllHistory(viewModel);
        }
        public List<History> AllHistory
        {
            get => _histories;

            set
            {
                _histories = value;
                OnPropertyChanged();
            }
        }
        public INavigation Navigation { get; private set; }
        public ICommand OpenProfilePageCommand { get; private set; }
        public ICommand OpenDetailPageCommand { get; private set; }

        private async Task GetHistoryBill(BankItemViewModel viewModel)
        {
            AllHistory = await BillsService.Instance.GetPartHistory(viewModel.SelectedBankItem.Number);
        }
        private async Task GetHistoryBills()
        {
            AllHistory =  await BillsService.Instance.GetHistory();
        }
        public async Task LoadAllHistory(BankItemViewModel viewModel = null)
        {      
            List<History> histories = new List<History>();

            if (viewModel != null && Type.GetType(TypeName) == typeof(BankItemViewModel))
            {
                await GetHistoryBill(viewModel);
            }

            if (Type.GetType(TypeName) == typeof(HistoryView))
            {
                await GetHistoryBills();
            }

            //_histories.ForEach(x => AllHistory.Add(x));

            ChangeHistory();
            AllHistory = _histories;
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

        private async void OpenProfilePage()
        {
            await RunIsBusyTaskAsync(async () => await Navigation.PushAsync(new ProfileView(new ProfileViewModel(Navigation))));
        }
        private async void OpenDetailPage(object item)
        {
            var payment = item as History;
            if (payment.operation_type == "Перевод на карту")
            {
                await RunIsBusyTaskAsync(async () => await Navigation.PushAsync(new DetailHistoryGiveView(new DetailHistoryGiveModel(payment))));
            } else
            {
                await RunIsBusyTaskAsync(async () => await Navigation.PushAsync(new DetailHistoryGetView(new DetailHistoryGetModel(payment))));
            }
        }
    }
}
