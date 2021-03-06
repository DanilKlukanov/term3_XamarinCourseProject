﻿using CW.Models;
using CW.Services;
using CW.Validations;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CW.ViewModels.InsideViewModels.OperationsViewModels
{
    public class PaymentTopUpViewModel : BaseViewModel
    {
        private bool _isButtonEnabled;
        public BankItem SelectedBankItem { get; private set; }
        public BankCard FromBankCard { get; private set; }
        public BankAccount FromAccount { get; private set; }
        public bool IsCardVisible { get; private set; }
        public bool IsAccountVisible { get; private set; }
        public ValidatableObject<string> UserPassword { get; set; }
        public ValidationInput Amount { get; set; }
        public ICommand ToPayCommand { get; private set; }
        public PaymentTopUpViewModel(BankItem selectedBankItem, BankCard fromCard)
        {
            SelectedBankItem = selectedBankItem;
            FromBankCard = fromCard;
            _isButtonEnabled = true;
            IsCardVisible = true;
            IsAccountVisible = false;
            ToPayCommand = new Command(ToPay);
            Amount = new ValidationInput();
            UserPassword = new ValidatableObject<string>();
        }

        public PaymentTopUpViewModel(BankItem selectedBankItem, BankAccount fromAccount)
        {
            SelectedBankItem = selectedBankItem;
            FromAccount = fromAccount;
            _isButtonEnabled = true;
            IsCardVisible = false;
            IsAccountVisible = true;
            ToPayCommand = new Command(ToPay, () => IsButtonEnabled);
            Amount = new ValidationInput();
            UserPassword = new ValidatableObject<string>();
        }
        private bool IsButtonEnabled
        {
            get => _isButtonEnabled;

            set
            {
                if (value != _isButtonEnabled)
                {
                    _isButtonEnabled = value;

                    (ToPayCommand as Command)?.ChangeCanExecute();
                }
            }
        }
        private async void ToPay()
        {
            if (Amount.Validate())
            {
                IsButtonEnabled = false;
                UserPassword.Value = await Application.Current.MainPage.DisplayPromptAsync("Подтверждение", "Введите пароль");
                IsButtonEnabled = true;
                if (UserPassword.Value == null)
                    return;
                if (UserPassword.Validate())
                {
                    Tuple<bool, string> response = await UserService.Instance.Login(App.GetUser().login, UserPassword.Value);
                    if (response.Item1 == true)
                    {
                        var toCard = SelectedBankItem as BankCard;
                        if (IsCardVisible)
                        {
                            TransferFromCard(toCard);
                        }
                        else if (IsAccountVisible)
                        {
                            TransferFromAccount(toCard);
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Message", "Неправильно введен пароль", "OK");
                    }
                }
            }
        }

        private async void TransferFromCard(BankCard toCard)
        {
            double.TryParse(Amount.Value, out double amount);
            string response = await TransactionService.Instance.DoTransfer(FromBankCard.Number, toCard.Number, amount);
            await Application.Current.MainPage.DisplayAlert("Message", response, "OK");
            await (Application.Current.MainPage as Shell).Navigation.PopToRootAsync();
        }
        private async void TransferFromAccount(BankCard toCard)
        {
            double.TryParse(Amount.Value, out double amount);
            string response = await TransactionService.Instance.DoTransfer(FromAccount.Number, toCard.Number, amount);
            await Application.Current.MainPage.DisplayAlert("Message", response, "OK");
            await (Application.Current.MainPage as Shell).Navigation.PopToRootAsync();
        }
    }
}