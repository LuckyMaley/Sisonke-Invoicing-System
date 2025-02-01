using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SISONKE_Invoicing_RESTAPI.AuthModels;
using SISONKE_Invoicing_RESTAPI.Controllers;
using SISONKE_Invoicing_RESTAPI.DesignPatterns;
using SISONKE_Invoicing_RESTAPI.Models;
using SISONKE_Invoicing_RESTAPI.Repository;
using SISONKE_Invoicing_RESTAPI.Services;
using SISONKE_Invoicing_RESTAPI.ViewModels;

namespace LLM_eCommerce_RESTAPI.Controllers
{
    /// <summary>
    /// A summary about CustomersCustomersInvoicesController class.
    /// </summary>
    /// <remarks>
    /// CustomersInvoicesController requires a user to be logged in and have specific role to access the end points.
    /// CustomersInvoicesController has the following end points:
    /// Get current logged in user's invoice details - required role is Customer.
    /// Get All CustomersInvoices information  - required role is (Administrator, Employee).
    /// Get a CustomersInvoices with User id - Authenticated user (Administrator, Employee).
    /// Using CustomersInvoicesRepo.
    /// </remarks>

    [Route("cusinv")]
    [ApiController]
    [Authorize]
    public class CustomersInvoicesController : ControllerBase
    {
        private static readonly ILog logger = LogManagerSingleton.Instance.GetLogger(nameof(CustomersInvoicesController));

        private readonly SISONKE_Invoicing_System_EFDBContext _context;
        private readonly IdentityHelper _identityHelper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AuthenticationContext _authenticationContext;

        public CustomersInvoicesController(SISONKE_Invoicing_System_EFDBContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AuthenticationContext authenticationContext)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _authenticationContext = authenticationContext;
            _identityHelper = new IdentityHelper(_userManager, _authenticationContext, _roleManager);
           
        }

        [Route("MyInvoices")]
        [HttpGet]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> GetMyInvoices()
        {
            logger.Info("CustomersInvoicesController - GET: cusinv/MyInvoices");

            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            string userName = user.UserName;

            var roleCheckStrategy = new AdminRoleCheckStrategy(_identityHelper);
            bool userAuthorised = await roleCheckStrategy.CheckRole(userId);

            if (!userAuthorised)
            {
                var loggedInUser = _context.EfUsers.FirstOrDefault(x => x.IdentityUsername == userName);

                if (loggedInUser == null)
                {
                    logger.Warn($"CustomersInvoicesController - GET: cusinv/MyInvoices - Not Found / invalid user, logged in UserName: {userName}");
                    return new NotFoundResponseFactory().CreateResponse("User not found");
                }

                int id = loggedInUser.EfUserId;
                CustomersInvoicesRepo prod = new CustomersInvoicesRepo(_context);
                var customersInvoices = prod.GetMyInvoices(id).ToList();

                return new SuccessResponseFactory().CreateResponse(customersInvoices);
            }

            return new ErrorResponseFactory().CreateResponse("Not Authorized");
        }

        [HttpGet]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> Get()
        {
            logger.Info("CustomersInvoicesController - GET all CustomersInvoices: /cusinv");

            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var roleCheckStrategy = new AdminRoleCheckStrategy(_identityHelper);
            bool rightRole = await roleCheckStrategy.CheckRole(userId);

            if (rightRole)
            {
                CustomersInvoicesRepo prod = new CustomersInvoicesRepo(_context);
                var customersInvoices = prod.GetMyInvoices(0).ToList();

                return new SuccessResponseFactory().CreateResponse(customersInvoices);
            }

            var user = await _userManager.FindByIdAsync(userId);
            var userRoles = new List<string>(await _userManager.GetRolesAsync(user));

            logger.Error($"CustomersInvoicesController - GET all CustomersInvoices: /cusinv - Not Authorized - logged in User: {user}");

            return new ErrorResponseFactory().CreateResponse(new
            {
                message = "Not Authorized",
                user.UserName,
                rightRole,
                userRoles
            });
        }

        [HttpGet("userId/{id}")]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> Get(int id)
        {
            logger.Info($"CustomersInvoicesController - GET: cusinv/userId/{id}");

            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            string userName = user.UserName;
            var roleCheckStrategy = new EmployeeRoleCheckStrategy(_identityHelper);
            bool rightRole = await roleCheckStrategy.CheckRole(userId);

            if (!string.IsNullOrEmpty(userName) && rightRole)
            {
                CustomersInvoicesRepo prod = new CustomersInvoicesRepo(_context);
                var customersInvoices = prod.GetMyInvoices(id).ToList();
                if (customersInvoices.Count > 0)
                {
                    return new SuccessResponseFactory().CreateResponse(customersInvoices);
                }

                return new NotFoundResponseFactory().CreateResponse("Invoice not found");
            }

            logger.Warn($"CustomersInvoicesController - GET: cusinv/userId/{id} logged in User: {user}");
            return new ErrorResponseFactory().CreateResponse("Not Authorized");
        }

        [HttpGet("InvoiceInfo/{id}")]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> GetInvoiceInfo(int id)
        {
            logger.Info($"CustomersInvoicesController - GET: cusinv/InvoiceInfo/{id}");

            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            string userName = user.UserName;

            if (!string.IsNullOrEmpty(userName))
            {
                CustomersInvoicesRepo prod = new CustomersInvoicesRepo(_context);
                var invoiceInfo = prod.GetInvoiceDetails(id);
                if (invoiceInfo.EfUserId > 0 && invoiceInfo.InvoiceId > 0)
                {
                    return new SuccessResponseFactory().CreateResponse(invoiceInfo);
                }

                return new NotFoundResponseFactory().CreateResponse("Invoice Info not found");
            }

            logger.Warn($"CustomersInvoicesController - GET: cusinv/InvoiceInfo/{id} logged in User: {user}");
            return new ErrorResponseFactory().CreateResponse("Not Authorized");
        }

        [HttpGet("customersDiscountId/{id}")]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> InvoicesByDiscountrId(int id)
        {
            logger.Info($"CustomersInvoicesController - GET: cusinv/customersDiscountId/{id}");

            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            string userName = user.UserName;
            var roleCheckStrategy = new AdminRoleCheckStrategy(_identityHelper);

            if (!string.IsNullOrEmpty(userName))
            {
                CustomersInvoicesRepo prod = new CustomersInvoicesRepo(_context);
                var customersInvoices = prod.GetInvoicesByDiscountId(id).ToList();
                if (customersInvoices.Count > 0)
                {
                    return new SuccessResponseFactory().CreateResponse(customersInvoices);
                }

                return new NotFoundResponseFactory().CreateResponse($"Invoices with user product id {id} not found");
            }

            logger.Warn($"CustomersInvoicesController - GET: cusinv/customersDiscountId/{id} logged in User: {user}");
            return new ErrorResponseFactory().CreateResponse("Not Authorized");
        }
    }
}
