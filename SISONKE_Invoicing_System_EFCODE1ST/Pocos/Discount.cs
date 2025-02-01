using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocos
{
    [Table("discounts")]
    public class Discount
    {
        [Key, Column("discount_id")]
        public int DiscountID { get; set; }

        [StringLength(100), Column("name")]
        public string Name { get; set; }

        [Column("rate")]
        public decimal Rate { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
