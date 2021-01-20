using CW.Models;
using CW.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CW.ViewModels.InsideViewModels.DetailHistoryModels
{
    public class DetailHistoryGiveModel : BaseViewModel
    {
        public History Payment { get; private set; }
        public string Name { get; private set; }
        public string ImgUrl { get; private set; }
        public double Money { get; private set; }
        public string Currency { get; private set; }
        public ICommand CreatePatternCommand { get; private set; }
        private ObservableCollection<BankCard> Cards;
        private ObservableCollection<BankAccount> Accounts;
        public DetailHistoryGiveModel(History payment, ObservableCollection<BankCard> cards, ObservableCollection<BankAccount> accounts)
        {
            Payment = payment;
            Cards = cards;
            Accounts = accounts;
            CreatePatternCommand = new Command(CreatePattern);
            LoadInfo();
        }
        private void LoadInfo()
        {
            if (Payment.user_number.Length == 16)
            {
                var cardInfo = Cards.Where(card => card.Number == Payment.user_number).FirstOrDefault();
                Name = cardInfo.Name;
                Money = cardInfo.Money;
                Currency = cardInfo.Currency;
            }
            else
            {
                var cardInfo = Accounts.Where(card => card.Number == Payment.user_number).FirstOrDefault();
                Name = cardInfo.Name;
                Money = cardInfo.Money;
                Currency = cardInfo.Currency;
            }
        }
        private async void CreatePattern()
        {
            string namePattern = await Application.Current.MainPage.DisplayPromptAsync("Создание шаблона", "Введите название");
            if (namePattern != null)
            {
                string response = await PatternService.Instance.CreatePattern(namePattern, Payment.user_number, Payment.other_number, Payment.amount);
                await Application.Current.MainPage.DisplayAlert("Message", response, "OK");
            }
        }
    }
}