using System.ComponentModel.DataAnnotations;

namespace Moniturl.Hosting
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Lütfen e-mail adresinizi giriniz.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Lütfen uygun formatta bir email giriniz.")]
        [Display(Name = "E-mail Adresi")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen adınızı giriniz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lütfen soyadınızı giriniz.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        public string Password { get; set; }
    }
}
