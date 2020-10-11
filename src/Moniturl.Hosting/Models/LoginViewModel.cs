using Moniturl.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace Moniturl.Hosting
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = Messages.PleaseFillEmail)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = Messages.PleaseEnterEmailAddressCorrectFormat )]
        [Display(Name = Messages.EmailField )]
        public string Email { get; set; }

        [Required(ErrorMessage = Messages.PleaseFillPassword)]
        [Display(Name = Messages.PasswordField )]
        public string Password { get; set; }
    }
}
