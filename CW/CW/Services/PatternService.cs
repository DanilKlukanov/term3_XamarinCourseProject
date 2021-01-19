using CW.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

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

        public async Task<string> CreatePattern(string pattern_name, string from, string to, double amount)
        {
            string json = await _client.create_pattern(App.GetUser().login, pattern_name, from, to, amount);
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

        public async Task<List<Pattern>> GetPatterns()
        {
            try
            {
                string json = await _client.get_patterns(App.GetUser().login);
                return JsonConvert.DeserializeObject<List<Pattern>>(json);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Нет связи с сервером", "Ок");
                return new List<Pattern>();
            }
        }

        public async Task<Tuple<bool, string>> RemovePattern(string pattern_name)
        {
            string json = await _client.remove_pattern(App.GetUser().login, pattern_name);
            try
            {
                if (json == "\"1\"")
                {
                    return new Tuple<bool, string>(true, "Удаление выполнено успешно.");
                }
                else
                {
                    return new Tuple<bool, string>(false, "Невозможно удалить шаблон");
                }
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, "Возникла непредвиденная ошибка. Повторите позднее.");
            }
        }
    }
}
