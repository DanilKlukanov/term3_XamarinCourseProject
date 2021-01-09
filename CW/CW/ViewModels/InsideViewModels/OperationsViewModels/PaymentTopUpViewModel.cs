using CW.Models;
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
        public BankItem ToBankItem { get; private set; }
        public BankAccount ToAccount { get; private set; }
        public bool IsCardVisible { get; private set; }
        public bool IsAccountVisible { get; private set; }
        public string Sum { get; set; }
        public ICommand ToPayCommand { get; private set; }
        public PaymentTopUpViewModel(BankItem selectedBankItem, BankItem toCard)
        {
            SelectedBankItem = selectedBankItem;
            ToBankItem = toCard;
            IsCardVisible = true;
            IsAccountVisible = false;
            ToPayCommand = new Command(ToPay);
        }
        public PaymentTopUpViewModel(BankItem selectedBankItem, BankAccount toAccount)
        {
            SelectedBankItem = selectedBankItem;
            ToAccount = toAccount;
            IsCardVisible = false;
            IsAccountVisible = true;
            ToPayCommand = new Command(ToPay);
        }
        private async void ToPay()
        {
            if (int.Parse(Sum) != 0)
            {
                string confirmPassword = await Application.Current.MainPage.DisplayPromptAsync("Подтверждение", "Введите пароль");
                if (confirmPassword != null)
                {
                    //
                }
            }
        }
    }
}