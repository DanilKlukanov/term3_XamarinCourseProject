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
    }
}
