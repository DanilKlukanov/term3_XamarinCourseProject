using CW.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CW.ViewModels.InsideViewModels.DetailHistoryModels
{
    public class DetailHistoryGetModel : BaseViewModel
    {
        public History Payment { get; private set; }
        public string Name { get; private set; }
        public string ImgUrl { get; private set; }
        public double Money { get; private set; }
        public string Currency { get; private set; }
        private ObservableCollection<BankCard> Cards;
        private ObservableCollection<BankAccount> Accounts;
        public DetailHistoryGetModel(History payment, ObservableCollection<BankCard> cards, ObservableCollection<BankAccount> accounts)
        {
            Payment = payment;
            Cards = cards;
            Accounts = accounts;
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
            } else
            {
                var cardInfo = Accounts.Where(card => card.Number == Payment.user_number).FirstOrDefault();
                Name = cardInfo.Name;
                Money = cardInfo.Money;
                Currency = cardInfo.Currency;
            }
        }
    }
}