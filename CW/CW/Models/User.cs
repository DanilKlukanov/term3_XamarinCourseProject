using System;
using System.Collections.Generic;
using System.Text;

namespace CW.Models
{
    public class User
    {
        public string firstname { get; set; }
        public string surnamme { get; set; }
        public string patronymic { get; set; }
        public string dob { get; set; }
        public string phone { get; set; }
        public string pseries { get; set; }
        public string pnumber { get; set; }
        public int id { get; set; }
        public string login { get; set; }

        public User()
        {

        }

        public User Copy()
        {
            return this.MemberwiseClone() as User;
        }
    }
}
