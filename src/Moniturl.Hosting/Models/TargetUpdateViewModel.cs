using Moniturl.Core;
using System.ComponentModel.DataAnnotations;

namespace Moniturl.Hosting
{
    public class TargetUpdateViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = Messages.ThisFieldIsRequired)]
        [Display(Name = Messages.Name)]
        public string Name { get; set; }

        [Required(ErrorMessage = Messages.ThisFieldIsRequired)]
        [Display(Name = Messages.Url)]
        public string Url { get; set; }

        [Required(ErrorMessage = Messages.ThisFieldIsRequired)]
        [Display(Name = Messages.Interval)]
        public int Interval { get; set; }

        public int? UserId { get; set; }
    }
}
