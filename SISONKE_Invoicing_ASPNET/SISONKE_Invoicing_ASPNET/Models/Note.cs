using System;
using System.Collections.Generic;

namespace SISONKE_Invoicing_ASPNET.Models
{
    public partial class Note
    {
        public int NoteId { get; set; }
        public int InvoiceId { get; set; }
        public string? InvoiceNotes { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Invoice Invoice { get; set; } = null!;
    }
}
