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
                new KeyValuePair<string, string>("login", username),
                new KeyValuePair<string, string>("password", password)
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
                new KeyValuePair<string, string>("login", username)

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
                new KeyValuePair<string, string>("login", login)
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
                new KeyValuePair<string, string>("login", login),
                new KeyValuePair<string, string>("new_password", new_password)
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
                new KeyValuePair<string, string>("login", login),
                new KeyValuePair<string, string>("new_login", new_login)
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
                new KeyValuePair<string, string>("login", login)
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
                new KeyValuePair<string, string>("login", login)
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
                new KeyValuePair<string, string>("login", login)
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
                new KeyValuePair<string, string>("login", login)
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
                new KeyValuePair<string, string>("start_number", number_from),
                new KeyValuePair<string, string>("target_number", number_to),
                new KeyValuePair<string, string>("amount", amount.ToString()),
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
                 new KeyValuePair<string, string>("number", number),
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }
        public async Task<string> create_pattern(int user_id, string pattern_name, string from, string to, int amount)
        {
            var content = new FormUrlEncodedContent(new[] 
            { 
                new KeyValuePair<string, string>("operation", "create_pattern"),
                new KeyValuePair<string, string>("id", user_id.ToString()),
                new KeyValuePair<string, string>("name", pattern_name),
                new KeyValuePair<string, string>("from", from),
                new KeyValuePair<string, string>("to", to),
                new KeyValuePair<string, string>("amount", amount.ToString()),
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();
            
            return json;
        }
        public async Task<string> get_patterns(int user_id)
        {
            var content = new FormUrlEncodedContent(new[] 
            {
                new KeyValuePair<string, string>("operation", "get_patterns"),
                new KeyValuePair<string, string>("id", user_id.ToString()),
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }
        public async Task<string> remove_pattern(int user_id, string user_name)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("operation", "remove_pattern"),
                new KeyValuePair<string, string>("id", user_id.ToString()),
                new KeyValuePair<string, string>("name", user_name),
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
                new KeyValuePair<string, string>("login", login)
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
                new KeyValuePair<string, string>("number", number)
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
                new KeyValuePair<string, string>("number", number)
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
                new KeyValuePair<string, string>("number", number),
                new KeyValuePair<string, string>("name", name)
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }
    }
}
