using System;
using System.Collections.Generic;

namespace hospitalAPI.Models
{
    // Modele zwracane przez akcje elementu AccountController.

    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class ManageInfoViewModel
    {
        public string LocalLoginProvider { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    public class UserInfoViewModel
    {
        public string Email { get; set; }
        public string Role { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Contract { get; set; }
        public string License { get; set; }
        public string PESEL { get; set; }
        public string Id { get; set; }
    }

    public class UserLoginInfoViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }
}
