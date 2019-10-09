using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hospitalAPI.Models
{
    public class UserDTO
    {
        public string Login { get; set; }
        //public AccountType TypeOfAccount { get; set; }
        public string Id { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
        public string Contract { get; set; }
        public string License { get; set; }
        public string PESEL { get; set; }
    }
}