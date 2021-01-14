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

namespace CW.ViewModels.InsideViewModels
{
    class HistoryViewModel : BaseViewModel
    {
        private bool _isEnabled;

        public HistoryViewModel(INavigation navigation)
        {
            Navigation = navigation;
            _isEnabled = true;
            OpenProfilePageCommand = new Command(OpenProfilePage);
            AddPatternCommand = new Command(OnAddPatternAsync);
            BackCommand = new Command(Back, () => _isEnabled);
            AllHistory = new ObservableCollection<History>();
            LoadAllHistory();
        }
        public ObservableCollection<History> AllHistory { get; private set; }
        public INavigation Navigation { get; private set; }
        public ICommand BackCommand { get; private set; }
        public ICommand OpenProfilePageCommand { get; private set; }
        public ICommand AddPatternCommand { get; private set; }
        private async void LoadAllHistory()
        {
            var historyBills = await BillsService.Instance.GetBillsHistory();
            historyBills.ForEach(x => AllHistory.Add(x));
            ChangeAllHistory();
        }
        private void ChangeAllHistory()
        {
            foreach (var item in AllHistory.OrderBy(item => item.time.Date))
            {
                if (item.type == "give")
                {
                    item.type = "Перевод на карту";
                }
                if (item.type == "get")
                {
                    item.type = "Получение на карту";
                }
            }
        }
        private void Back()
        {
            Navigation.PopAsync();
        }
        private void OpenProfilePage()
        {
            Navigation.PushAsync(new ProfileView(new ProfileViewModel(Navigation)));
        }
        public async void OnAddPatternAsync (object item)
        {
            var payment = item as History;
            if (payment.type == "Перевод на карту") {
                string namePattern = await Application.Current.MainPage.DisplayPromptAsync("Создание шаблона", "Введите название");
                if (namePattern != null)
                {
                    //byte[] bytes = Encoding.Default.GetBytes(namePattern);
                    //namePattern = Encoding.UTF8.GetString(bytes);
                    string response = await PatternService.Instance.CreatePattern(namePattern, payment.from, payment.to, payment.amount);
                    await Application.Current.MainPage.DisplayAlert("Message", response, "OK");
                }
            }
        }
    }
}
