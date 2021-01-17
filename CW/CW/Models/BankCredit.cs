using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Models
{
    public class BankCredit : BankItem
    {
        public string Date { get; set; }
        public string PaymentInfo { get; set; }
        public BankCredit(Card bill) : base(bill) { }

        public BankCredit(Credit credit)
        {
            Number = credit.credit_number;
            Money = credit.balance;
            Date = credit.pay_time;
        }
    }
}
