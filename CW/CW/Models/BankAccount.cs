using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Models
{
    public class BankAccount : BankItem
    {
        public string Number { get; set; }

        public BankAccount(Bill bill) : base(bill)
        {
            Name = "Текущий счет";
            Number = bill.number;
        }
    }
}
