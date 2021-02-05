using CW.Models;
using CW.Services;
using CW.Validations;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CW.ViewModels.InsideViewModels.OperationsViewModels
{
    public class TransferPageViewModel : BaseViewModel
    {
        private bool _isButtonEnabled;
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
            _isButtonEnabled = true;
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
            _isButtonEnabled = true;
            PaymentReceiver = type;
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
                    Tuple<bool, string> responseCheck = await UserService.Instance.Login(App.GetUser().login, UserPassword.Value);
                    if (responseCheck.Item1 == true)
                    {
                        double.TryParse(Amount.Value, out double amount);
                        string response = String.Empty;
                        if (NumberToCard == null)
                        {
                            response = await TransactionService.Instance.DoTransfer(FromCard.Number, ToCard.Number, amount);
                        }
                        else
                        {
                            response = await TransactionService.Instance.DoTransfer(FromCard.Number, NumberToCard, amount);
                        }

                        await Application.Current.MainPage.DisplayAlert("Message", response, "OK");
                        await (Application.Current.MainPage as Shell).Navigation.PopToRootAsync();
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