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
        public async Task<string> DoTransfer(string number_from, string number_to, int amount)
        {
            try
            {
                string json = await _client.do_transfer(number_from, number_to, amount);
                if (json == "\"0\"")
                {
                    return "Неудача";
                } else
                {
                    return "Операция успешно выполнена";
                }
            }
            catch (Exception ex)
            {
                return "Возникла непредвиденная ошибка. Повторите позднее";
            }

        }
        public async Task<Tuple<bool, string>> CanTransferTo(string number)
        {
            try
            {
                string json = await _client.can_transfer_to(number);
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
