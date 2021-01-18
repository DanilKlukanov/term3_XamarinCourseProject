using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Models
{
    public class BankCard : BankItem
    {
        public bool IsBlocked { get; set; }
        public float Balance { get; set; }
        public string ImgUrl { get; set; }

        public BankCard(Card card) : base(card)
        {
            ImgUrl = card.card_type;
            IsBlocked = card.card_blocked;
        }
    }
}
