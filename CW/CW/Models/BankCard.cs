using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Models
{
    public class BankCard : BankItem
    {
        public string ImgUrl { get; set; }

        public BankCard(Bill bill) : base(bill)
        {
            Name = "Дебетовая карта";
            ImgUrl = bill.type;
        }
    }
}
