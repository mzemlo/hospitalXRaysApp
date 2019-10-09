using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hospitalAPI.Models
{
    public enum AccountType { Admin, Doctor, Head};

    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public AccountType TypeOfAccount { get; set; }

        public ICollection<Patient> Patients { get; set; }
        //public ICollection<User> Users { get; set; }


    }
}