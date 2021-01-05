using System;
using System.Collections.Generic;
using System.Text;
using CW.Models;

namespace CW.ViewModels.InsideViewModels
{
    public class GroupedBankItemsViewModel
    {
        public List<BankItemsGroup> ListBankItemsGroups { get; private set; }

        public GroupedBankItemsViewModel()
        {
            ListBankItemsGroups = new List<BankItemsGroup>();
            LoadBankItemsGroups();
        }

        public void LoadBankItemsGroups()
        {
            ListBankItemsGroups.Add(new BankItemsGroup("Cards", new List<BankItem>
            { 
                new BankCard
                {
                    Name = "Дебетовая карта",
                    Number = "2202201948567017",
                    ImgUrl = "rates_icon",
                    Money = 12000
                },
                new BankCard
                {
                    Name = "Дебетовая карта",
                    Number = "2202201950501111",
                    ImgUrl = "rates_icon",
                    Money = 800
                }
            }));

            ListBankItemsGroups.Add(new BankItemsGroup("Accounts", new List<BankItem>
            {
                new BankAccount
                {
                    Name = "Текущий счет",
                    Number = "2202201948567017",
                    Money = 9001112
                },
                new BankAccount
                {
                    Name = "Текущий счет",
                    Number = "2202201948523232",
                    Money = 5
                }
            }));

            ListBankItemsGroups.Add(new BankItemsGroup("Cards", new List<BankItem>
            {
                new BankCredit
                {
                     Name = "Ипотека",
                     Date = DateTime.Now,
                     Money = 788
                },
                new BankCredit
                {
                    Name = "Кредит наличными",
                    Date = DateTime.Now,
                    Money = 1020
                }
            })); ;
        }
    }
}
