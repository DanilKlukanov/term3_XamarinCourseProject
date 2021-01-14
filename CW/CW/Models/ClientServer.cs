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

                new KeyValuePair<string, string>("operation", "login"),
                new KeyValuePair<string, string>("login", username),
                new KeyValuePair<string, string>("pass", password)
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }

        public async Task<string> find_user_with_login(string username)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("operation", "find_user_with_login"),
                new KeyValuePair<string, string>("login", username)

            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }

        public async Task<string> add_visit(int id)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("operation", "add_visit"),
                new KeyValuePair<string, string>("id", id.ToString())
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        public async Task<string> change_password(string login, string new_password)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("operation", "change_password"),
                new KeyValuePair<string, string>("login", login),
                new KeyValuePair<string, string>("new", new_password)
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        public async Task<string> change_login(string login, string new_login)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("operation", "change_login"),
                new KeyValuePair<string, string>("login", login),
                new KeyValuePair<string, string>("new", new_login)
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        public async Task<string> get_auth_history(int id)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("operation", "get_auth_history"),
                new KeyValuePair<string, string>("id", id.ToString())
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        public async Task<string> get_bills(int id)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("operation", "get_bills"),
                new KeyValuePair<string, string>("id", id.ToString())
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }

        public async Task<string> do_operation(string number_from, string number_to, int amount)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("operation", "do_operation"),
                new KeyValuePair<string, string>("from", number_from),
                new KeyValuePair<string, string>("to", number_to),
                new KeyValuePair<string, string>("amount", amount.ToString()),
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }
        public async Task<string> check_card(string number)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                 new KeyValuePair<string, string>("operation", "check_card"),
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
        public async Task<string> get_bills_history(int user_id)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("operation", "get_bills_history"),
                new KeyValuePair<string, string>("id", user_id.ToString())
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }
        public async Task<string> get_bill_history(string bill_number)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("operation", "get_bill_history"),
                new KeyValuePair<string, string>("number", bill_number)
            });

            var response = await client.PostAsync("http://77.34.49.138", content);
            var json = await response.Content.ReadAsStringAsync();

            return json;
        }
    }
}
