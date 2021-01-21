using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CW.Models
{
    public class ClientServer
    {
        private HttpClient client;

        public ClientServer()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://77.34.49.138/");
        }

        public async Task<string> login(string username, string password)
        {
           var content = new FormUrlEncodedContent(new[]
           {

                new KeyValuePair<string, string>("operation", nameof(login)),
                new KeyValuePair<string, string>("login", ToUTF8(username)),
                new KeyValuePair<string, string>("password", ToUTF8(password))
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }

        public async Task<string> get_user_data(string username)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("operation", nameof(get_user_data)),
                new KeyValuePair<string, string>("login", ToUTF8(username))

            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }

        public async Task<string> add_visit(string login)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("operation", nameof(add_visit)),
                new KeyValuePair<string, string>("login", ToUTF8(login))
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        public async Task<string> change_password(string login, string new_password)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("operation", nameof(change_password)),
                new KeyValuePair<string, string>("login", ToUTF8(login)),
                new KeyValuePair<string, string>("new_password", ToUTF8(new_password))
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        public async Task<string> change_login(string login, string new_login)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("operation", nameof(change_login)),
                new KeyValuePair<string, string>("login", ToUTF8(login)),
                new KeyValuePair<string, string>("new_login", ToUTF8(new_login))
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        public async Task<string> get_auth_history(string login)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("operation", nameof(get_auth_history)),
                new KeyValuePair<string, string>("login", ToUTF8(login))
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        public async Task<string> get_cards(string login)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("operation", nameof(get_cards)),
                new KeyValuePair<string, string>("login", ToUTF8(login))
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }

        public async Task<string> get_bills(string login)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("operation", nameof(get_bills)),
                new KeyValuePair<string, string>("login", ToUTF8(login))
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }

        public async Task<string> get_credits(string login)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("operation", nameof(get_credits)),
                new KeyValuePair<string, string>("login", ToUTF8(login))
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }

        public async Task<string> do_transfer(string number_from, string number_to, double amount)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("operation", "do_transfer"),
                new KeyValuePair<string, string>("start_number", ToUTF8(number_from)),
                new KeyValuePair<string, string>("target_number", ToUTF8(number_to)),
                new KeyValuePair<string, string>("amount", ToUTF8(amount.ToString())),
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }
        public async Task<string> can_transfer_to(string number)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                 new KeyValuePair<string, string>("operation", "can_transfer_to"),
                 new KeyValuePair<string, string>("number", ToUTF8(number)),
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }
        public async Task<string> create_pattern(string login, string pattern_name, string from, string to, double amount)
        {
            var content = new FormUrlEncodedContent(new[] 
            { 
                new KeyValuePair<string, string>("operation", "create_pattern"),
                new KeyValuePair<string, string>("login", ToUTF8(login)),
                new KeyValuePair<string, string>("name", ToUTF8(pattern_name)),
                new KeyValuePair<string, string>("from", ToUTF8(from)),
                new KeyValuePair<string, string>("to", ToUTF8(to)),
                new KeyValuePair<string, string>("amount", ToUTF8(amount.ToString())),
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();
            
            return json;
        }
        public async Task<string> get_patterns(string login)
        {
            var content = new FormUrlEncodedContent(new[] 
            {
                new KeyValuePair<string, string>("operation", "get_patterns"),
                new KeyValuePair<string, string>("login", ToUTF8(login)),
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }
        public async Task<string> remove_pattern(string login, string name)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("operation", "remove_pattern"),
                new KeyValuePair<string, string>("login", ToUTF8(login)),
                new KeyValuePair<string, string>("name", ToUTF8(name)),
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }
        public async Task<string> get_history(string login)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("operation", "get_history"),
                new KeyValuePair<string, string>("login", ToUTF8(login))
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }
        public async Task<string> get_part_history(string number)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("operation", "get_part_history"),
                new KeyValuePair<string, string>("number", ToUTF8(number))
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }

        public async Task<string> block_card(string number)
        {
            var content = new FormUrlEncodedContent(new[]
            { 
                new KeyValuePair<string, string>("operation", nameof(block_card)),
                new KeyValuePair<string, string>("number", ToUTF8(number))
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }

        public async Task<string> rename_card(string number, string name)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("operation", nameof(rename_card)),
                new KeyValuePair<string, string>("number", ToUTF8(number)),
                new KeyValuePair<string, string>("name", ToUTF8(name))
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }

        public async Task<string> get_messages(string login)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("operation", nameof(get_messages)),
                    new KeyValuePair<string, string>("login", ToUTF8(login)),
            });

            return await SendRequest(content);
        }

        public async Task<string> send_message(string login_from, string login_to, string message)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("operation", nameof(send_message)),
                    new KeyValuePair<string, string>("from_", ToUTF8(login_from)),
                    new KeyValuePair<string, string>("to_", ToUTF8(login_to)),
                    new KeyValuePair<string, string>("msg", ToUTF8(message))
            });

            return await SendRequest(content);
        }

        private async Task<string> SendRequest(FormUrlEncodedContent content)
        {

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }

        private string ToUTF8(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            string new_str = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                new_str += "%" + Convert.ToString(bytes[i], 16);
            }
            return new_str;
        }
    }
}
