using System.ComponentModel.DataAnnotations;

namespace SISONKE_Invoicing_ASPNET.Areas.SecurityServices.Models
{
    public class RegistrationVM
    {
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? FirstName { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? LastName { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? UserName { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Email { get; set; }

        [StringLength(255, MinimumLength = 3)]
        [Required]
        public string? Address { get; set; }

        [StringLength(50, MinimumLength = 10)]
        [Required]
        public string? PhoneNumber { get; set; }

        [StringLength(60, MinimumLength = 12)]
        [Required]
        public string? Password { get; set; }

        public string? Role { get; set; }
    }
}
