using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Models
{
    public abstract class BankItem
    {
        public string Name { get; set; } //  TODO (dma117)
        public decimal Money { get; set; }
        public string Cur { get; set; }

        public BankItem() { }
        public BankItem(Bill bill)
        {
            Name = "Дебетова карта";
            Money = bill.bal;
            Cur = bill.cur;
        }
    }
}
