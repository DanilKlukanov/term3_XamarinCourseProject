using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Models
{
    public abstract class BankItem
    {
        public string Name { get; set; } //  TODO (dma117)
        public double Money { get; set; }
        public string Currency { get; set; }
        public string Number { get; set; }

        public BankItem() { }
        public BankItem(Card card)
        {
            Money = card.balance;
            Currency = card.card_currency;
            Number = card.card_number;
            Name = card.card_name;
        }
    }
}
