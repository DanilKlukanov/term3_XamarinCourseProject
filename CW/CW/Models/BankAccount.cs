using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Models
{
    public class BankAccount : BankItem
    {
        
        public BankAccount(Card card) : base(card)
        {
            Name = "Текущий счет";
        }
    }
}
