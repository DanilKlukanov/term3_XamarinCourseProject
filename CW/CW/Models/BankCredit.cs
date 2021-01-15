using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Models
{
    public class BankCredit : BankItem
    {
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public string PaymentInfo { get; set; }
        public string Currency { get; set; }

        public BankCredit(Bill bill)
        {
            Money = bill.bal;
            Number = bill.number;
            Currency = bill.cur;
        }
    }
}
