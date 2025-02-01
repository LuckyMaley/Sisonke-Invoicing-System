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

namespace SISONKE_Invoicing_RESTAPI.Controllers
{
	/// <summary>
	/// A summary about DiscountsController class.
	/// </summary>
	/// <remarks>
	/// DiscountsController has the following end points:
	/// Get all Discounts
	/// Get Discounts with id
	/// Get Discounts with Name
	/// Put (update)  with id and  object
	/// Post (Add)  using a Discounts View Model 
	/// Delete  with id
	/// </remarks>
	[Route("api/[controller]")]
	[ApiController]
	public class DiscountsController : ControllerBase
	{
        private static readonly ILog logger = LogManagerSingleton.Instance.GetLogger(nameof(DiscountsController));
        private readonly SISONKE_Invoicing_System_EFDBContext _context;
		private UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly AuthenticationContext _authenticationContext;
		private readonly IdentityHelper _identityHelper;
		public DiscountsController(SISONKE_Invoicing_System_EFDBContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AuthenticationContext authenticationContext)
		{
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
			_authenticationContext = authenticationContext;
			_identityHelper = new IdentityHelper(_userManager, _authenticationContext, _roleManager);
		}


		// GET: api/Discounts        
		[EnableCors("AllowOrigin")]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Discount>>> GetDiscounts()
		{

			var discountDB = await _context.Discounts.ToListAsync();

			return Ok(discountDB);
		}

		// GET: api/Discounts/5
		[EnableCors("AllowOrigin")]
		[HttpGet("{id}")]
		public async Task<ActionResult<Discount>> GetDiscounts(int id)
		{
			List<Discount> allDiscounts = new List<Discount>();

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var discounts = await _context.Discounts.FindAsync(id);

			if (discounts == null || discounts.DiscountId == 0)
			{
				return NotFound(new { message = "No Discount with that ID exists, please try again" });
			}
			else
			{
				discounts.Invoices = GetAllInvoicesByDiscountId(id);
			}

			return Ok(discounts);
		}


		// GET: api/Discounts/specificDiscount/name
		[EnableCors("AllowOrigin")]
		[HttpGet("specificDiscount/{name}")]
		public async Task<ActionResult<Discount>> GetDiscountByName(string name)
		{
			List<Discount> discounts = new List<Discount>();

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}


			var discountsQuery = _context.Discounts.Where(x => x.Name == name);
			if (discountsQuery.Count() == 0)
			{
				return NotFound(new { message = "No Discount with that Name exists, please try again" });
			}
			var Item = discountsQuery;
			foreach (var discountItem in Item)
			{
				int id = discountItem.DiscountId;


				if (discountItem == null)
				{

					return NotFound(new { message = "No Discount with that Name exists, please try again" });
				}
				else
				{
					discountItem.Invoices = GetAllInvoicesByDiscountId(id);
				}

				discounts.Add(discountItem);
			}
			return Ok(discounts);
		}

		// PUT: api/Discounts/5
		[EnableCors("AllowOrigin")]
		[HttpPut("{id}")]
		[Authorize]
		public async Task<IActionResult> PutDiscounts(int id, DiscountsVM discount)
		{
			string userId = User.Claims.First(c => c.Type == "UserID").Value;
			var user = await _userManager.FindByIdAsync(userId);
			bool userSuperUserAuthorised = await _identityHelper.IsSuperUserRole(userId);
			if (!userSuperUserAuthorised)
			{
				return BadRequest(new { message = "Not authorised to update discounts" });
			}

			if (!DiscountsExists(id))
			{
				return NotFound(new { message = "Discount Id not found, no changes made, please try again" });
			}

			int currentDiscountId = 0;

			try
			{
				var disc = _context.Discounts.FirstOrDefault(c => c.DiscountId == id);

				int count = 0;

				if (discount.Name != "" || discount.Name != null)
				{
					if (disc.Name != discount.Name)
					{
						disc.Name = discount.Name;
						count++;
					}
				}


				if (discount.Rate != 0)
				{

					if (disc.Rate != discount.Rate)
					{
						disc.Rate = discount.Rate;
						count++;
					}
				}


				if (count > 0)
				{
					await _context.SaveChangesAsync();
					currentDiscountId = disc.DiscountId;
				}
				else
				{
					return Ok(new { message = "no updates made" });
				}


			}
			catch (DbUpdateConcurrencyException)
			{
				if (!DiscountsExists(id))
				{
					return NotFound(new { message = "Discount Id not found, no changes made, please try again" });
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

			return Ok(new { message = "Discount Updated - DiscountId:" + currentDiscountId });
		}

		// POST: api/Discounts
		[EnableCors("AllowOrigin")]
		[HttpPost]
		[Authorize]
		public async Task<ActionResult<Discount>> PostDiscounts(DiscountsVM discount)
		{
			string userId = User.Claims.First(c => c.Type == "UserID").Value;
			var user = await _userManager.FindByIdAsync(userId);
			bool userSuperUserAuthorised = await _identityHelper.IsSuperUserRole(userId);
			if (!userSuperUserAuthorised)
			{
				return BadRequest(new { message = "Not authorised to add discounts" });
			}

			if (discount.Name == "" || discount.Name == null)
			{
				return BadRequest(new { message = "Cannot Add an empty discount" });
			}

			Discount newDiscount = new Discount();
			newDiscount.Name = discount.Name;
			int currentDiscountId = 0;

			try
			{
				_context.Discounts.Add(newDiscount);

				await _context.SaveChangesAsync();
				newDiscount.Invoices = GetAllInvoicesByDiscountId(newDiscount.DiscountId);
				await _context.SaveChangesAsync();
				currentDiscountId = newDiscount.DiscountId;
			}
			catch (DbUpdateConcurrencyException)
			{
				return BadRequest(new { message = "Error in adding Discount, please try again" });
			}
			catch (Exception e)
			{
				return BadRequest(new { message = "Error in adding Discount, " + e.Message });
			}

			return Ok("New Discount Created - DiscountId:" + currentDiscountId);
		}

		// DELETE: api/Discounts/5
		[EnableCors("AllowOrigin")]
		[HttpDelete("{id}")]
		[Authorize]
		public async Task<ActionResult<Discount>> DeleteDiscounts(int id)
		{
			string userId = User.Claims.First(c => c.Type == "UserID").Value;
			var user = await _userManager.FindByIdAsync(userId);
			bool userSuperUserAuthorised = await _identityHelper.IsSuperUserRole(userId);
			bool userEmployeeAuthorised = await _identityHelper.IsEmployeeUserRole(userId);
			if (!userSuperUserAuthorised)
			{
				return BadRequest(new { message = "Not authorised to delete discounts" });
			}

			var discounts = await _context.Discounts.FindAsync(id);
			if (discounts == null)
			{
				return NotFound(new { message = "Discount ID not found, please try again" });
			}

			if (_context.Discounts.Where(c => c.DiscountId == id).Count() > 0)
			{
				return BadRequest(new { message = "Error, Cannot delete Discounts that have been assigned to a product" });
			}

			try
			{

				_context.Discounts.Remove(discounts);
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				return BadRequest(new { message = "Error in deleting Discount, please try again" });
			}
			catch (Exception e)
			{
				return BadRequest(new { message = "Error, " + e.Message });
			}
			return discounts;
		}

		private bool DiscountsExists(int id)
		{
			return _context.Discounts.Any(e => e.DiscountId == id);
		}


		private List<Invoice> GetAllInvoicesByDiscountId(int id)
		{
			List<Invoice> allInvoicesForDiscount = new List<Invoice>();

			var invoicesQuery =
					(from invoices in _context.Invoices
					 where (invoices.DiscountId == id)
					 select new
					 {
						 invoices.InvoiceId,
						 invoices.DiscountId,
						 invoices.EfUser,
						 invoices.Discount,
						 invoices.InvoiceDate,
						 invoices.DueDate,
						 invoices.Subtotal,
						 invoices.Tax,
						 invoices.TotalAmount,
						 invoices.Status,
						 invoices.InvoiceItems,
						 invoices.Notes,
						 invoices.Payments

					 }).ToList();


			foreach (var disc in invoicesQuery)
			{
				allInvoicesForDiscount.Add(new Invoice()
				{
					InvoiceId = disc.InvoiceId,
					DiscountId = disc.DiscountId,
					EfUser = disc.EfUser,
					Discount = disc.Discount,
					InvoiceDate = disc.InvoiceDate,
					DueDate = disc.DueDate,
					Subtotal = disc.Subtotal,
					Tax = disc.Tax,
					TotalAmount = disc.TotalAmount,
					Status = disc.Status,
					InvoiceItems = disc.InvoiceItems,
					Notes = disc.Notes,
					Payments = disc.Payments
				});
			}

			return allInvoicesForDiscount;
		}
	}
}
