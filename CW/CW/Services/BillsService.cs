using CW.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CW.Services
{
    public class BillsService
    {
        ClientServer client;

        public BillsService()
        {
            client = new ClientServer();
        }

        public async Task<List<Bill>> GetBills()
        {
            try
            {
                string json = await client.get_bills(App.GetUser().id);
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
