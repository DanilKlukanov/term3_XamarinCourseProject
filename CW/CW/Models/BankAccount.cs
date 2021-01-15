using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Models
{
    public class BankAccount : BankItem
    {
        public BankAccount(Bill bill) : base(bill)
        {
            Name = "Текущий счет";
        }
    }
}
