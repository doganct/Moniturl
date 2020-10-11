using Moniturl.Core;
using System.ComponentModel.DataAnnotations;

namespace Moniturl.Hosting
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = Messages.PleaseFillEmail)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = Messages.PleaseEnterEmailAddressCorrectFormat )]
        [Display(Name = Messages.EmailField )]
        public string Email { get; set; }

        [Required(ErrorMessage = Messages.ThisFieldIsRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = Messages.ThisFieldIsRequired)]
        public string Surname { get; set; }

        [Required(ErrorMessage = Messages.ThisFieldIsRequired)]
        public string Password { get; set; }
    }
}
