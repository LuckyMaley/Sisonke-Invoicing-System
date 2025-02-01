using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SISONKE_Invoicing_RESTAPI.AuthModels;
using SISONKE_Invoicing_RESTAPI.DesignPatterns;
using SISONKE_Invoicing_RESTAPI.Models;
using SISONKE_Invoicing_RESTAPI.Services;
using SISONKE_Invoicing_RESTAPI.ViewModels;
using System.Globalization;

namespace SISONKE_Invoicing_RESTAPI.Controllers
{
	/// <summary>
	/// A summary about NotesController class.
	/// </summary>
	/// <remarks>
	/// NotesController has the following end points:
	/// Get all Notes
	/// Get Notes with id
	/// Get Notes with method
	/// Get Notes with date
	/// Get Notes between dates
	/// Put (update) Note with id and Note object
	/// Post (Add) Note using a Notes View Model 
	/// Delete Note with id
	/// </remarks>
	[Route("api/[controller]")]
	[ApiController]
	public class NotesController : ControllerBase
	{
        private static readonly ILog logger = LogManagerSingleton.Instance.GetLogger(nameof(NotesController));

        private readonly SISONKE_Invoicing_System_EFDBContext _context;
		private UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly AuthenticationContext _authenticationContext;
		private readonly IdentityHelper _identityHelper;
		public NotesController(SISONKE_Invoicing_System_EFDBContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AuthenticationContext authenticationContext)
		{
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
			_authenticationContext = authenticationContext;
			_identityHelper = new IdentityHelper(_userManager, _authenticationContext, _roleManager);
		}


		// GET: api/Notes        
		[EnableCors("AllowOrigin")]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
		{

			var noteDB = await _context.Notes.ToListAsync();

			return Ok(noteDB);
		}

		// GET: api/Notes/5
		[EnableCors("AllowOrigin")]
		[HttpGet("{id}")]
		public async Task<ActionResult<Note>> GetNotes(int id)
		{
			List<Note> allNotes = new List<Note>();

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var notes = await _context.Notes.FindAsync(id);

			if (notes == null)
			{
				return NotFound(new { message = "No Note with that ID exists, please try again" });
			}
			else
			{
				var invId = _context.Notes.FirstOrDefault(c => c.NoteId == id).InvoiceId;
				notes.Invoice = _context.Invoices.FirstOrDefault(o => o.InvoiceId == invId);
			}

			return Ok(notes);
		}


		// GET: api/Notes/SpecificDate/date
		[EnableCors("AllowOrigin")]
		[HttpGet("SpecificDateASyyyy-mm-dd/{date}")]
		public async Task<ActionResult<List<Note>>> GetNoteByDate(DateTime date)
		{
			List<Note> notes = new List<Note>();
			DateTime dateOutput;
			bool valid = DateTime.TryParse(date.ToShortDateString(), CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out dateOutput);
			if (!valid)
			{
				return BadRequest("Error the format of the date is incorrect");
			}
			if (!ModelState.IsValid)
			{
				logger.Error("  NotesController - Get - SpecifiedDate"  + date +"- Bad request - ModelState");

				return BadRequest(ModelState);
			}

			List<Note> temNotes = _context.Notes.ToList();
			var notesQuery = temNotes.Where(x => x.CreatedDate.Date == dateOutput.Date);
			if (notesQuery.Count() == 0)
			{
				return NotFound(new { message = "No Note with that date exists, please try again" });
			}
			var item = notesQuery;
			foreach (var noteItem in item)
			{
				int id = noteItem.NoteId;


				if (noteItem == null)
				{
                    logger.Error("NotesController - Get: SpecifiedDate - No note with" + date + " exists - Not found");

                    return NotFound(new { message = "No Note with that date exists, please try again" });
				}
				else
				{
					var invId = _context.Notes.FirstOrDefault(c => c.NoteId == id).InvoiceId;
					noteItem.Invoice = _context.Invoices.FirstOrDefault(o => o.InvoiceId == invId);
				}

				notes.Add(noteItem);
			}
			return Ok(notes);
		}

		// GET: api/Notes/BetweenDates/date1/date2
		[EnableCors("AllowOrigin")]
		[HttpGet("BetweenDatesBothASyyyy-mm-dd/{{date1}}/{{date2}}")]
		public async Task<ActionResult<List<Note>>> GetNoteByBetweenDates(DateTime date1, DateTime date2)
		{
			List<Note> notes = new List<Note>();
			DateTime date1Output;
			bool valid = DateTime.TryParse(date1.ToShortDateString(), CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out date1Output);
			if (!valid)
			{
				return BadRequest("Error the format of the date is incorrect");
			}

			DateTime date2Output;
			bool validDate2 = DateTime.TryParse(date2.ToShortDateString(), CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out date2Output);
			if (!validDate2)
			{
				return BadRequest("Error the format of the date is incorrect");
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			List<Note> temNotes = _context.Notes.ToList();
			var notesQuery = temNotes.Where(x => x.CreatedDate.Date >= date1Output.Date && x.CreatedDate.Date <= date2Output);
			if (notesQuery.Count() == 0)
			{
                logger.Error("NotesController - Get: SpecifiedDate - No note with" + date1 + " exists - Not found");

                return NotFound(new { message = "No Note with that date range exists, please try again" });
			}
			var item = notesQuery;
			foreach (var noteItem in item)
			{
				int id = noteItem.NoteId;


				if (noteItem == null)
				{
                    logger.Error("NotesController - Get: SpecifiedDate - No note with" + date2 + " exists - Not found");

                    return NotFound(new { message = "No Note with that date exists, please try again" });
				}
				else
				{
					var invId = _context.Notes.FirstOrDefault(c => c.NoteId == id).InvoiceId;
					noteItem.Invoice = _context.Invoices.FirstOrDefault(o => o.InvoiceId == invId);
				}

				notes.Add(noteItem);
			}
			return Ok(notes);
		}

		// PUT: api/Notes/5
		[EnableCors("AllowOrigin")]
		[HttpPut("{id}")]
		[Authorize]
		public async Task<IActionResult> PutNotes(int id, NoteVM note)
		{
			string userId = User.Claims.First(c => c.Type == "UserID").Value;
			var user = await _userManager.FindByIdAsync(userId);
			bool userSuperUserAuthorised = await _identityHelper.IsSuperUserRole(userId);
			if (!userSuperUserAuthorised)
			{
				logger.Warn("Not authorized to update" + user + "Bad Request");

				return BadRequest(new { message = "Not authorised to update notes" });
			}


			int currentNoteId = 0;

			try
			{
				Note updateNote = _context.Notes.FirstOrDefault(o => o.NoteId == id);
				int count = 0;
				if (updateNote == null)
				{

                    logger.Error("NotesController - PUT: SpecifiedID - No note with ID:" + id + " exists - Not found");

                    return NotFound(new { message = "No Note with that ID exists, please try again" });
				}


				if (note.InvoiceId != 0)
				{

					if (updateNote.InvoiceId != note.InvoiceId)
					{
						updateNote.InvoiceId = note.InvoiceId;
						count++;
					}
				}


				if (note.InvoiceNotes != "" || note.InvoiceNotes != null)
				{
					if (updateNote.InvoiceNotes != note.InvoiceNotes)
					{
						updateNote.InvoiceNotes = note.InvoiceNotes;
						count++;
					}
				}

				if (count > 0)
				{
					updateNote.CreatedDate = DateTime.Now;
					await _context.SaveChangesAsync();
					currentNoteId = id;
				}
				else
				{
					return Ok(new { message = "no updates made" });
				}
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!NotesExists(id))
				{
                    logger.Error("NotesController - PUT: SpecifiedID - No note with ID" + id + " exists - Not found");

                    return NotFound(new { message = "Note Id not found, no changes made, please try again" });
				}
				else
				{
					throw;
				}
			}
			catch (Exception e)
			{
				return BadRequest(new { message = "Error, " + e.Message });
			}

			return Ok(new { message = "Note Updated - NoteId:" + currentNoteId });
		}

		// POST: api/Notes
		[EnableCors("AllowOrigin")]
		[HttpPost]
		[Authorize]
		public async Task<ActionResult<Note>> PostNotes(NoteVM note)
		{
			string userId = User.Claims.First(c => c.Type == "UserID").Value;
			var user = await _userManager.FindByIdAsync(userId);
			bool userSuperUserAuthorised = await _identityHelper.IsSuperUserRole(userId);
			bool userEmployeeAuthorised = await _identityHelper.IsEmployeeUserRole(userId);

			if (!userSuperUserAuthorised || !userEmployeeAuthorised)
			{
                logger.Error("NotesController - POST - Could not update" + note.NoteId+ " Unauthorized USER:"+user);

                return BadRequest(new { message = "Not authorised to update notes" });
			}

			if (note.InvoiceId == 0 || note.InvoiceNotes == null || note.InvoiceNotes == "")
			{
				return BadRequest(new { message = "Cannot Add an empty note, please you enter a valid note" });
			}

			int currentNoteId = 0;

			try
			{
				var newNote = new Note();

				if (_context.Invoices.Where(c => c.InvoiceId == note.InvoiceId).Count() == 0)
				{
					return BadRequest(new { message = "That order does not exist please choose InvoiceIdincluded in the list below", _context.Invoices });
				}
				newNote.InvoiceId = note.InvoiceId;

				newNote.InvoiceNotes = note.InvoiceNotes;
				newNote.CreatedDate = DateTime.Now;
				newNote.Invoice = _context.Invoices.FirstOrDefault(o => o.InvoiceId == note.InvoiceId);
				_context.Notes.Add(newNote);
				await _context.SaveChangesAsync();
				currentNoteId = newNote.NoteId;
			}
			catch (DbUpdateConcurrencyException)
			{
                logger.Error("NotesController - POST: Error" + note.NoteId + " could not be added");

                return BadRequest(new { message = "Error in adding Note, please try again" });
			}
			catch (Exception e)
			{
				return BadRequest(new { message = "Error in adding Note, " + e.Message });
			}

			return Ok("New Note Created - NoteId:" + currentNoteId);
		}

		// DELETE: api/Notes/5
		[EnableCors("AllowOrigin")]
		[HttpDelete("{id}")]
		[Authorize]
		public async Task<ActionResult<Note>> DeleteNotes(int id)
		{
			string userId = User.Claims.First(c => c.Type == "UserID").Value;
			var user = await _userManager.FindByIdAsync(userId);
			bool userSuperUserAuthorised = await _identityHelper.IsSuperUserRole(userId);
			if (!userSuperUserAuthorised)
			{
                logger.Warn("Not authorized to delete" + user + "Bad Request");

                return BadRequest(new { message = "Not authorised to delete notes" });
			}

			var notes = await _context.Notes.FindAsync(id);
			if (notes == null)
			{
                logger.Error("NotesController - Get: SpecifiedID - No note with ID:" + id + " exists - Not found");

                return NotFound(new { message = "Note ID not found, please try again" });
			}

			try
			{

				_context.Notes.Remove(notes);
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				return BadRequest(new { message = "Error in deleting Note, please try again" });
			}
			catch (Exception e)
			{
				return BadRequest(new { message = "Error, " + e.Message });
			}
			return notes;
		}

		private bool NotesExists(int id)
		{
			return _context.Notes.Any(e => e.NoteId == id);
		}
	}
}
