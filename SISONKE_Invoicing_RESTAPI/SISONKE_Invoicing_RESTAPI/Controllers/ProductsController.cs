using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SISONKE_Invoicing_RESTAPI.AuthModels;
using SISONKE_Invoicing_RESTAPI.DesignPatterns;
using SISONKE_Invoicing_RESTAPI.Models;
using SISONKE_Invoicing_RESTAPI.Services;
using System.Xml.Linq;

namespace SISONKE_Invoicing_RESTAPI.Controllers
{
    /// <summary>
    /// A summary about ProductsController class.
    /// </summary>
    /// <remarks>
    /// ProductsController has the following end points:
    /// Get all Products
    /// Get Products with id
    /// Get Products with Name
    /// Put (update) Product with id and Product object
    /// Post (Add) Product using a Products View Model 
    /// Delete Product with id
    /// </remarks>
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private static readonly ILog logger = LogManagerSingleton.Instance.GetLogger(nameof(ProductsController));
        private readonly IdentityHelper _identityHelper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;
        private readonly SISONKE_Invoicing_System_EFDBContext _context;
        private readonly AuthenticationContext _authenticationContext;

        public ProductsController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            SISONKE_Invoicing_System_EFDBContext context, AuthenticationContext authenticationContext)
        {
            _userManager = userManager;
           
            _context = context;
            _authenticationContext = authenticationContext;
            _roleManager = roleManager;
            _identityHelper = new IdentityHelper(_userManager, _authenticationContext, _roleManager);
        }

        // GET: api/Products        
        [EnableCors("AllowOrigin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            logger.Info("ProductsController -Get : api/Products");
            var productDB = await _context.Products.ToListAsync();

            return Ok(productDB);
        }

        // GET: api/Products/5
        [EnableCors("AllowOrigin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            logger.Info($"ProductsController -Get : api/Products{id}");
            List<Product> allProducts = new List<Product>();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var products = await _context.Products.FindAsync(id);

            if (products == null)
            {
               
                logger.Warn($"ProductsController -Get : api/Products{id} / No Product with that ID exists, please try again");
                return NotFound(new { message = "No Product with that ID exists, please try again" });
            }
            else
            {
                products.InvoiceItems = GetAllInvoiceItemsByProductId(id);
               
            }

            return Ok(products);
        }


        // GET: api/Products/specificProduct/name
        [EnableCors("AllowOrigin")]
        [HttpGet("specificProduct/{name}")]
        public async Task<ActionResult<List<Product>>> GetProductByName(string name)
        {
            logger.Info($"ProductsController -Get : api/Products{name}");
            List<Product> products = new List<Product>();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var productsQuery = _context.Products.Where(x => x.Name == name);
            if (productsQuery.Count() == 0)
            {
                logger.Warn($"ProductsController -Get : api/Products{name} No Product with that Name exists, please try again");
                return NotFound(new { message = "No Product with that Name exists, please try again" });
            }
            var item = productsQuery;
            foreach (var productItem in item)
            {
                int id = productItem.ProductId;


                if (productItem == null)
                {
                    logger.Warn($"ProductsController -Get : api/Products{name} No Product with that Name exists, please try again");
                    return NotFound(new { message = "No Product with that Name exists, please try again" });
                }
                else
                {
                    productItem.InvoiceItems = GetAllInvoiceItemsByProductId(id);
                   
                }

                products.Add(productItem);
            }
            return Ok(products);
        }

        // PUT: api/Products/5
        [EnableCors("AllowOrigin")]
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutProducts(int id, ProductVM product)
        {
            logger.Info($"ProductsController -POST : api/Products{id}");
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            bool userSuperUserAuthorised = await _identityHelper.IsSuperUserRole(userId);
            bool userEmployeeAuthorised = await _identityHelper.IsEmployeeUserRole(userId);
            if (!userSuperUserAuthorised && !userEmployeeAuthorised)
            {

                logger.Warn($"ProductsController -POST : api/Products{id} / Not authorised to update products as a customer");
                return BadRequest(new { message = "Not authorised to update products as a customer" });
            }



            var efUser = _context.EfUsers.FirstOrDefault(c => c.IdentityUsername == user.UserName);
           
            int currentProductId = 0;

            try
            {
                var updateProduct = _context.Products.FirstOrDefault(c => c.ProductId == id);
                int count = 0;
                if (updateProduct == null)
                {
                    logger.Warn($"ProductsController -POST : api/Products{id} /No Product with that ID exists, please try again");
                    return NotFound(new { message = "No Product with that ID exists, please try again" });
                }
                if (product.Name != "" || product.Name != null)
                {
                    if (updateProduct.Name != product.Name)
                    {
                        updateProduct.Name = product.Name;
                        count++;
                    }
                }
               
                if (updateProduct.Description != "" || product.Description != null)
                {
                    if (updateProduct.Description != product.Description)
                    {
                        updateProduct.Description = product.Description;
                        count++;
                    }
                }
                
                if (product.Price != 0m)
                {
                    if (updateProduct.Price != product.Price)
                    {
                        updateProduct.Price = product.Price;
                        count++;
                    }
                }
                if (product.StockQuantity != 0)
                {
                    if (updateProduct.StockQuantity != product.StockQuantity)
                    {
                        updateProduct.StockQuantity = product.StockQuantity;
                        count++;
                    }
                }
                

                if (count > 0)
                {
                    updateProduct.ModifiedDate = DateTime.Now;
                    await _context.SaveChangesAsync();
                    currentProductId = updateProduct.ProductId;
                }
                else
                {
                    logger.Warn($"ProductsController -POST : api/Products{id} / no updates made");
                    return Ok(new { message = "no updates made" });
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
                {
                    logger.Warn($"ProductsController -POST : api/Products{id} / Product Id not found, no changes made, please try again");
                    return NotFound(new { message = "Product Id not found, no changes made, please try again" });
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

            return Ok(new { message = "Product Updated - ProductId:" + currentProductId });
        }

        // POST: api/Products
        [EnableCors("AllowOrigin")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Product>> PostProducts(ProductVM product)
        {
            logger.Info($"ProductsController -POST : api/Products");
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            bool userSuperUserAuthorised = await _identityHelper.IsSuperUserRole(userId);
            bool userEmployeeAuthorised = await _identityHelper.IsEmployeeUserRole(userId);
            if (!userSuperUserAuthorised && !userEmployeeAuthorised)
            {
                logger.Warn($"ProductsController -POST : api/Products / Not authorised to add products as a customer");
                return BadRequest(new { message = "Not authorised to add products as a customer" });
            }

            if (product.Name == null || product.Name == "" && product.Description == null || product.Description == "" && product.Price == 0 || product.Price == null)
            {
                logger.Warn($"ProductsController -POST : api/Products / Cannot to add products an empty product");
                return BadRequest(new { message = "Cannot to add products an empty product" });
            }


            Product newProduct = new Product();
            newProduct.Name = product.Name;
           
            newProduct.Description = product.Description;
           
            newProduct.Price = product.Price;
            newProduct.StockQuantity = product.StockQuantity;
            newProduct.ModifiedDate = DateTime.Now;
           
           
            int currentProductId = 0;

            try
            {
                _context.Products.Add(newProduct);
                await _context.SaveChangesAsync();
                newProduct.InvoiceItems = GetAllInvoiceItemsByProductId(newProduct.ProductId);               
                await _context.SaveChangesAsync();
                currentProductId = _context.Products.Max(c => c.ProductId);
            }
            catch (DbUpdateConcurrencyException)
            {
                logger.Error($"ProductsController -POST : api/Products / Error in adding Product, please try again");
                return BadRequest(new { message = "Error in adding Product, please try again" });
            }
            catch (Exception e)
            {
                logger.Error($"ProductsController -POST : api/Products / Error in adding Product");
                return BadRequest(new { message = "Error in adding Product, " + e.Message });
            }

            return Ok(new { message = "New Product Created - ProductId:" + currentProductId });
        }

        // DELETE: api/Products/5
        [EnableCors("AllowOrigin")]
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Product>> DeleteProducts(int id)
        {
            logger.Info($"ProductsController -DELETE : api/Products");
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            bool userSuperUserAuthorised = await _identityHelper.IsSuperUserRole(userId);
            bool userEmployeeAuthorised = await _identityHelper.IsEmployeeUserRole(userId);
            if (!userSuperUserAuthorised)
            {
                logger.Warn($"ProductsController -DELETE : api/Products / Not authorised to delete products");
                return BadRequest(new { message = "Not authorised to delete products" });
            }

           
            if (_context.InvoiceItems.Where(c => c.ProductId == id).Count() > 0)
            {
                logger.Error($"ProductsController -DELETE : api/Products / Error, Cannot delete Products that have been invoiced");
                return BadRequest(new { message = "Error, Cannot delete Products that have been invoiced" });
            }
            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                logger.Warn($"ProductsController -DELETE : api/Products / Error, Cannot delete Products that have been invoiced");
                return NotFound(new { message = "Product ID not found, please try again" });
            }

            try
            {

                _context.Products.Remove(products);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                logger.Error($"ProductsController -DELETE : api/Products");
                return BadRequest(new { message = "Error in deleting Product, please try again" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error, " + e.Message });
            }
            return products;
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }


        private List<InvoiceItem> GetAllInvoiceItemsByProductId(int id)
        {
            List<InvoiceItem> allInvoiceItemsForProduct = new List<InvoiceItem>();

            var invoiceItemsQuery =
                    (from invoiceItems in _context.InvoiceItems
                     where (invoiceItems.ProductId == id)
                     select new
                     {
                         invoiceItems.InvoiceItemId,
                         invoiceItems.ProductId,
                         invoiceItems.InvoiceId,
                         invoiceItems.Quantity,
                         invoiceItems.UnitPrice,
                         invoiceItems.TotalPrice,
                         invoiceItems.Invoice,
                         invoiceItems.Product,
                     }).ToList();


            foreach (var inv in invoiceItemsQuery)
            {
                allInvoiceItemsForProduct.Add(new InvoiceItem()
                {
                    InvoiceItemId = inv.InvoiceItemId,
                    ProductId = inv.ProductId,
                    InvoiceId = inv.InvoiceId,
                    Quantity = inv.Quantity,
                    UnitPrice = inv.UnitPrice,
                    TotalPrice = inv.TotalPrice,
                    Invoice = inv.Invoice,
                    Product = inv.Product,
                    
                });
            }

            return allInvoiceItemsForProduct;
        }

        
    }
}
