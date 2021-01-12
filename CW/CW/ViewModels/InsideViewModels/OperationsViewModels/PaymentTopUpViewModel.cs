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
    public class PaymentTopUpViewModel : BaseViewModel
    {
        public BankItem SelectedBankItem { get; private set; }
        public BankCard FromBankCard { get; private set; }
        public BankAccount FromAccount { get; private set; }
        public bool IsCardVisible { get; private set; }
        public bool IsAccountVisible { get; private set; }
        public ValidatableObject<string> UserPassword { get; set; }
        public string Amount { get; set; }
        public ICommand ToPayCommand { get; private set; }
        public PaymentTopUpViewModel(BankItem selectedBankItem, BankCard fromCard)
        {
            SelectedBankItem = selectedBankItem;
            FromBankCard = fromCard;
            IsCardVisible = true;
            IsAccountVisible = false;
            ToPayCommand = new Command(ToPay);
            UserPassword = new ValidatableObject<string>();
        }
        public PaymentTopUpViewModel(BankItem selectedBankItem, BankAccount fromAccount)
        {
            SelectedBankItem = selectedBankItem;
            FromAccount = fromAccount;
            IsCardVisible = false;
            IsAccountVisible = true;
            ToPayCommand = new Command(ToPay);
            UserPassword = new ValidatableObject<string>();
        }
        private async void ToPay()
        {
            if (Amount != null)
            {
                UserPassword.Value = await Application.Current.MainPage.DisplayPromptAsync("Подтверждение", "Введите пароль");
                if (UserPassword.Validate())
                {
                    User user = App.GetUser();
                    Tuple<bool, string> response = await UserService.Instance.Login(user.login, UserPassword.Value);
                    if (response.Item1 == true)
                    {
                        var toCard = SelectedBankItem as BankCard;
                        if (IsCardVisible)
                        {
                            TransferFromCard(toCard);
                        } else if (IsAccountVisible)
                        {
                            TransferFromAccount(toCard);
                        }
                    } else
                    {
                        await Application.Current.MainPage.DisplayAlert("Message", "Неправильно введен пароль", "OK");
                    }
                }
            }
        }

        private async void TransferFromCard(BankCard toCard)
        {
            string response = await TransactionService.Instance.DoOperation(FromBankCard.Number, toCard.Number, int.Parse(Amount));
            FromBankCard.Money -= int.Parse(Amount);
            toCard.Money += int.Parse(Amount);
            await Application.Current.MainPage.DisplayAlert("Message", response, "OK");
        }
        private async void TransferFromAccount(BankCard toCard)
        {
            string response = await TransactionService.Instance.DoOperation(FromAccount.Number, toCard.Number, int.Parse(Amount));
            FromAccount.Money -= int.Parse(Amount);
            toCard.Money += int.Parse(Amount);
            await Application.Current.MainPage.DisplayAlert("Message", response, "OK");
        }
    }
}