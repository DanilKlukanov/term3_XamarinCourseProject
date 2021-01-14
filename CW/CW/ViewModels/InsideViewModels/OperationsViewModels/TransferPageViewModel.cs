using CW.Models;
using CW.Services;
using CW.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CW.ViewModels.InsideViewModels.OperationsViewModels
{
    public class TransferPageViewModel : BaseViewModel
    {
        public BankCard FromCard { get; private set; }
        public BankCard ToCard { get; private set; }
        public string NumberToCard { get; private set; }
        public ValidationInput Amount { get; set; }
        public string PaymentReceiver { get; private set; }
        public ValidatableObject<string> UserPassword { get; set; }
        public ICommand ToPayCommand { get; private set; }
        public TransferPageViewModel(BankItem fromCard, BankCard toCard, string type)
        {
            FromCard = fromCard as BankCard;
            ToCard = toCard;
            NumberToCard = toCard.Number;
            PaymentReceiver = type;
            ToPayCommand = new Command(ToPay);
            Amount = new ValidationInput();
            UserPassword = new ValidatableObject<string>();
        }
        public TransferPageViewModel(BankItem fromCard, string numberCard, string type)
        {
            FromCard = fromCard as BankCard;
            NumberToCard = numberCard;
            PaymentReceiver = type;
            ToPayCommand = new Command(ToPay);
            Amount = new ValidationInput();
            UserPassword = new ValidatableObject<string>();
        }
        private async void ToPay()
        {
            if (Amount.Validate())
            {
                UserPassword.Value = await Application.Current.MainPage.DisplayPromptAsync("Подтверждение", "Введите пароль");
                if (UserPassword.Validate())
                {
                    User user = App.GetUser();
                    Tuple<bool, string> responseCheck = await UserService.Instance.Login(user.login, UserPassword.Value);
                    if (responseCheck.Item1 == true)
                    {
                        int.TryParse(Amount.Value, out int amount);
                        if (FromCard.Money - amount >= 0)
                        {
                            if (NumberToCard == null)
                            {
                                string response = await TransactionService.Instance.DoOperation(FromCard.Number, ToCard.Number, amount);
                                await Application.Current.MainPage.DisplayAlert("Message", response, "OK");
                            } else
                            {
                                string response = await TransactionService.Instance.DoOperation(FromCard.Number, NumberToCard, amount);
                                await Application.Current.MainPage.DisplayAlert("Message", response, "OK");
                            }
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
        }
    }
}