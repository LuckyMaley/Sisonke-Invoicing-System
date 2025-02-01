namespace SISONKE_Invoicing_ASPNET.Areas.SecurityServices.Models
{
    public class UserAccountVM
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public List<string> RolesHeld { get; set; }
    }
}
