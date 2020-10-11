using System;
using System.ComponentModel.DataAnnotations;

namespace Moniturl.Hosting
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Lütfen E-mail alanını doldurunuz.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Lütfen uygun formatta bir email giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen şifre alanını doldurunuz.")]
        public string Password { get; set; }
    }
}
