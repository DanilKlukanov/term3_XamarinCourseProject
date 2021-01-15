using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Models
{
    public class BankCredit : BankItem
    {
        public DateTime Date { get; set; }
        public string PaymentInfo { get; set; }
        public BankCredit(Bill bill) : base(bill) { }
    }
}
