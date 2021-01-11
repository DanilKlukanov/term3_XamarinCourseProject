using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Models
{
    public class BankCard : BankItem
    {
        public string Number { get; set; }
        public string ImgUrl { get; set; }

        public BankCard(Bill bill) : base(bill)
        {
            ImgUrl = bill.type;
            Number = bill.number;
        }
    }
}
