using CW.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CW.Services
{
    class PatternService
    {
        private readonly ClientServer _client;
        private static PatternService _instance;

        private PatternService()
        {
            _client = new ClientServer();
        }

        public static PatternService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PatternService();
                }

                return _instance;
            }
        }

        public async Task<string> CreatePattern(int user_id, string pattern_name, string bill_number, int amount)
        {
            string json = await _client.create_patterns(user_id, pattern_name, bill_number, amount);
            try
            {
                if (json == "\"0\"")
                {
                    return "Операция выполнена успешно.";
                }
                else if (json == "\"1\"")
                {
                    return "Данный шаблон уже существует.";
                }
                else
                {
                    return "Неверный счет.";
                }
            }
            catch (Exception ex)
            {
                return "Возникла непредвиденная ошибка. Повторите позднее.";
            }
        }
    }
}
