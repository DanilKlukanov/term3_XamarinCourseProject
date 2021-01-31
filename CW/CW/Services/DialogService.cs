using CW.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace CW.Services
{
    public class DialogService
    {
        public async Task<ObservableCollection<Message>> GetMessages()
        {
            var response = await new ClientServer().get_messages(App.GetUser().login);

            var messages = JsonConvert.DeserializeObject<ObservableCollection<Message>>(response);

            return messages;
        }

        public async Task<IResponse<string>> SendMessage(string login_to, string message)
        {
            var result = new ApiResponse<string>();
            result.ErrorMessage = "Не удалось отправить сообщение.";

            var response = await new ClientServer().send_message(App.GetUser().login, login_to, message);

            if (JsonConvert.DeserializeObject<string>(response) == "1")
            {
                result.Value = "Успех";
            }

            return result;
        }
    }
}
