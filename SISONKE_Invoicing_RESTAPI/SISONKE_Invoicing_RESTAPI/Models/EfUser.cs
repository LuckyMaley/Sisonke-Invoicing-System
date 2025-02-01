using System;
using System.Collections.Generic;

namespace SISONKE_Invoicing_RESTAPI.Models
{
    public partial class EfUser
    {
        public EfUser()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int EfUserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string IdentityUsername { get; set; } = null!;
        public string Role { get; set; } = null!;

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
