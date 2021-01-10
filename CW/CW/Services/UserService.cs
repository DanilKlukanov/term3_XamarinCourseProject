using CW.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CW.Services
{
    public class UserService
    {
        ClientServer client;

        public UserService()
        {
            client = new ClientServer();
        }

        public async Task<Tuple<bool, string>> Login(string username, string password)
        {
                try
                {
                    string json;

                    json = await client.login(username, password);

                    if (json == "\"0\"")
                    {
                        json = await client.find_user_with_login(username);
                        App.SetUser(JsonConvert.DeserializeObject<User>(json));
                        json = await client.add_visit(App.GetUser().id);
                        return new Tuple<bool, string>(true, "");
                    }
                    return new Tuple<bool, string>(false, "Неверный логин или пароль");
                }
                catch (Exception ex)
                {
                    return new Tuple<bool, string>(false, "Возникла непредвиденная ошибка. Повторите позднее");
                }
        }

        public void Logout()
        {
            App.SetUser(null);
        }

        public async Task<string> ChangeLogin(string login, string new_login)
        {
            try
            {
                string json = await client.change_login(login, new_login);

                if (json == "\"0\"")
                {
                    User user = App.GetUser();
                    user.login = new_login;
                    App.SetUser(user);

                    return "Успех.";
                }
                if (json == "\"1\"")
                {
                    return "Ошибка. Логин совпадает";
                }
                if (json == "\"2\"")
                {
                    return "Неверный логин";
                }
                return "Ошибка.";
            }
            catch (Exception ex)
            {
                return "Что-то пошло не так";
            }
        }

        public async Task<string> ChangePassword(string login, string new_password)
        {
            try
            {
                string json = await client.change_password(login, new_password);

                if (json == "\"0\"")
                {
                    return "Успех";
                }
                if (json == "\"1\"")
                {
                    return "Пароли совпадают";
                }
                if (json == "\"2\"")
                {
                    return "Неверный логин";
                }
                return "Возникла непредвиденная ошибка. Повторите позднее.";
            }
            catch (Exception ex)
            {
                return "Возникла непредвиденная ошибка. Повторите позднее.";
            }
            
        }

        public async Task<string> GetVisitHistory(int id)
        {
            string json = await client.get_auth_history(id);
            return json;
        }
    }
}
