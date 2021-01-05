using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Models
{
    public class BankItemsGroup: List<BankItem> 
    {
        public string Name { get; private set; }

        public BankItemsGroup(string name, List<BankItem> itemsGroup) : base(itemsGroup)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
