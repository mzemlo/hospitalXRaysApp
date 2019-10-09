using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace hospitalAPI.Models
{
    // Modele użyte jako parametry akcji elementu AccountController.

    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "Token dostępu zewnętrznego")]
        public string ExternalAccessToken { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Bieżące hasło")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi zawierać co najmniej następującą liczbę znaków: {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź nowe hasło")]
        [Compare("NewPassword", ErrorMessage = "Nowe hasło i jego potwierdzenie są niezgodne.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterBindingModel
    {
        [Required]
        [Display(Name = "Nazwa uzytkownika")]
        public string UserName { get; set; }

        //[Required]
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi zawierać co najmniej następującą liczbę znaków: {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasło i jego potwierdzenie są niezgodne.")]
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
        public string RoleOfUser { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Contract { get; set; }
        public string License { get; set; }
        public string PESEL { get; set; }
    }

    public class EditBindingModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Contract { get; set; }
        public string License { get; set; }
        public string PESEL { get; set; }
    }

    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }
    }

    public class RemoveLoginBindingModel
    {
        [Required]
        [Display(Name = "Dostawca logowania")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "Klucz dostawcy")]
        public string ProviderKey { get; set; }
    }

    public class SetPasswordBindingModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "{0} musi zawierać co najmniej następującą liczbę znaków: {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź nowe hasło")]
        [Compare("NewPassword", ErrorMessage = "Nowe hasło i jego potwierdzenie są niezgodne.")]
        public string ConfirmPassword { get; set; }
    }
}
