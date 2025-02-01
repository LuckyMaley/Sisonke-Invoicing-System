using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocos
{
	[Table("ef_users")]
	public class EFUser
	{
		[Key, Column("ef_user_id")]
		public int EFUserID { get; set; }

		[StringLength(50), Column("first_name")]
		public string FirstName { get; set; }

		[StringLength(50), Column("last_name")]
		public string LastName { get; set; }

		[StringLength(100), Column("email")]
		public string Email { get; set; }

		[StringLength(255), Column("address")]
		public string Address { get; set; }

		[StringLength(50), Column("phone_number")]
		public string PhoneNumber { get; set; }

		[StringLength(100), Column("identity_username")]
		public string IdentityUserName { get; set; }

		[StringLength(100), Required, Column("role")]
		public string Role { get; set; }

		public virtual ICollection<Invoice> Invoices { get; set; }
	}
}
