namespace SISONKE_Invoicing_RESTAPI.ViewModels
{
	public class NoteVM
	{
		
		public int NoteId { get; set; }
		public int InvoiceId { get; set; }
		public string? InvoiceNotes { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
