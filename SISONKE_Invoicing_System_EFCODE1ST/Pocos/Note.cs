using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Pocos
{
    [Table("Notes")]
    public class Note
    {
        [Key, Column("note_id")]
        public int NoteID { get; set; }

        [Column("invoice_id"), ForeignKey("Invoice"), Required]
        public int InvoiceID { get; set; }
        public virtual Invoice Invoice { get; set; }

        [Column("invoice_notes")]
        public string InvoiceNotes { get; set; }

        [Column("created_date", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

    }
}
