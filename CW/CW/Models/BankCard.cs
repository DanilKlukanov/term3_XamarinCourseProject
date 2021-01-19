using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Models
{
    public class BankCard : BankItem
    {
        public bool IsWorked { get; set; }
        public float Balance { get; set; }
        public string ImgUrl { get; set; }

        public BankCard(Card card) : base(card)
        {
            ImgUrl = card.card_type;
            IsWorked = !card.card_blocked;
        }
    }
}
