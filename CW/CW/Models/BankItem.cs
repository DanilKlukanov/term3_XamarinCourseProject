using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Models
{
    public abstract class BankItem
    {
        public string Name { get; set; }
        public decimal Money { get; set; }
    }
}
