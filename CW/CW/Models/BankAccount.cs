using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Models
{
    public class BankAccount : BankItem
    {
        public BankAccount(Bill bill)
        {
            Name = "Текущий счет";
            Number = bill.bill_number;
            Money = bill.balance;
            Currency = bill.bill_currency;
        }
    }
}
