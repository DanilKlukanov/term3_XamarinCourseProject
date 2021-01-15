using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Models
{
    public abstract class BankItem
    {
        public string Name { get; set; } //  TODO (dma117)
        public decimal Money { get; set; }
        public string Currency { get; set; }
        public string Number { get; set; }

        public BankItem() { }
        public BankItem(Bill bill)
        {
            Money = bill.bal;
            Currency = bill.cur;
            Number = bill.number;
        }
    }
}
