using System.ComponentModel.DataAnnotations;

namespace SISONKE_Invoicing_ASPNET.Areas.SecurityServices.Models
{
    public class LoginVM
    {
        [StringLength(60, MinimumLength = 3)]
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
