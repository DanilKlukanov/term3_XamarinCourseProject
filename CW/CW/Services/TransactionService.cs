using CW.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CW.Services
{
    class TransactionService
    {
        private readonly ClientServer _client;
        private static TransactionService _instance;

        private TransactionService()
        {
            _client = new ClientServer();
        }
        public static TransactionService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TransactionService();
                }

                return _instance;
            }
        }
        public async Task<string> DoOperation(string number_from, string number_to, int amount)
        {
            try
            {
                string json = await _client.do_operation(number_from, number_to, amount);
                if (json == "\"0\"")
                {
                    return "Операция успешно выполнена";
                }  else if (json == "\"1\"")
                {
                    return "Такого номера не существует";
                } else
                {
                    return "Разные валюты";
                }
            }
            catch (Exception ex)
            {
                return "Возникла непредвиденная ошибка. Повторите позднее";
            }

        }
        public async Task<Tuple<bool, string>> CheckCard(string number)
        {
            try
            {
                string json = await _client.check_card(number);
                if (json == "\"0\"")
                {
                    return new Tuple<bool, string>(false, "Такого номера не существует");
                } else
                {
                    return new Tuple<bool, string> (true, "Карта существует");
                }
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, "Возникла непредвиденная ошибка. Повторите позднее"); ;
            }
        }

    }
}
