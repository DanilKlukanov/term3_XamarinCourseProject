using CW.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CW.Services
{
    public class BillsService
    {
        private static BillsService _instance;
        private readonly ClientServer _client;

        private BillsService()
        {
            _client = new ClientServer();
        }

        public static BillsService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BillsService();
                }

                return _instance;
            }
        }

        public async Task<List<Bill>> GetBills()
        {
            try
            {
                string json = await _client.get_bills(App.GetUser().id);
                return JsonConvert.DeserializeObject<List<Bill>>(json);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Нет связи с сервером", "Ок");
                return new List<Bill>();
            }
        }
<<<<<<< HEAD
        public async Task<List<History>> GetBillsHistory()
        {
            try
            {
                string json = await _client.get_bills_history(App.GetUser().id);
                return JsonConvert.DeserializeObject<List<History>>(json);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Нет связи с сервером", "Ок");
                return new List<History>();
            }
        }

        public async Task<List<History>> GetBillHistory(string number)
=======

        public async Task<List<Bill>> GetBillHistory(string number)
>>>>>>> 2c85df9... Start making get_bill_history
        {
            try
            {
                string json = await _client.get_bill_history(number);
<<<<<<< HEAD
                return JsonConvert.DeserializeObject<List<History>>(json);
            }
            catch (Exception ex)
            {
                
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Нет связи с сервером", "Ок");
                return new List<History>();
=======
                return JsonConvert.DeserializeObject<List<Operation>>(json);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Нет связи с сервером", "Ок");
                return new List<Bill>();
>>>>>>> 2c85df9... Start making get_bill_history
            }
        }
    }
}
