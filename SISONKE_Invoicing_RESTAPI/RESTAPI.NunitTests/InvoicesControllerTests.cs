
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

using NUnit.Framework;
using RESTAPI.NunitTests;
using SISONKE_Invoicing_RESTAPI.AuthModels;
using SISONKE_Invoicing_RESTAPI.Models;
using SISONKE_Invoicing_RESTAPI.Controllers;
using SISONKE_Invoicing_RESTAPI.ViewModels;

namespace RESTApi.NunitTests
{
    [TestFixture]
    public class InvoicesControllerTests
    {
        private SISONKE_Invoicing_System_EFDBContext _sisonkeContext;
        private InvoicesController _controllerUnderTest;
        private List<Invoice> _invoiceList;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private AuthenticationContext _authenticationContext;
        InvoiceVM _invoiceVM;
        Invoice _invoice;
        IdentityUser identityUser;
        ClaimsPrincipal principal;
        
        [SetUp]
        public void Initialiser()
        {
            _sisonkeContext = (SISONKE_Invoicing_System_EFDBContext)InMemoryContext.GeneratedDB(); 
            var prod = _sisonkeContext.Invoices.Count();
            _authenticationContext = (AuthenticationContext)InMemoryContext.GeneratedAuthDB();
            _userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(_authenticationContext), null, null, null, null, null, null, null, null);

            _roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(_authenticationContext), null, null, null, null);
            var authdb = _authenticationContext;
            var employeeRole = authdb.Roles.FirstOrDefault(c => c.Name == "Customer").Id;
            var userRoles = authdb.UserRoles.FirstOrDefault(c => c.RoleId == employeeRole).UserId;
            identityUser = authdb.ApplicationUsers.FirstOrDefault(c => c.Id == userRoles);
            var user = new ApplicationUser { Id = identityUser.Id };
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("UserID", user.Id),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthentication");
            principal = new ClaimsPrincipal(identity);
            _controllerUnderTest = new InvoicesController(_sisonkeContext, _userManager, _roleManager, _authenticationContext)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = principal }
                }
            };

            _invoiceList = new List<Invoice>();
            _invoice = new Invoice()
            {
                InvoiceId = 11,
                EfUserId = 21,
                DiscountId = 1,
                InvoiceDate = new DateTime(2023, 11, 12, 12, 45, 00),
                TotalAmount = 2100.99
            };

      


        }



        [TearDown]
        public void CleanUpObject()
        {
            _sisonkeContext.Database.EnsureDeleted();
            _controllerUnderTest = null;
            _invoiceList = null;
            _invoice = null;
            _userManager = null;

            _roleManager = null;
            _authenticationContext.Database.EnsureDeleted();
        }

        [Test]
        public async Task _01Test_GetAllInvoice_ReturnsListWithValidCount0()
        {
            // Arrange


            // Act
            var result = await _controllerUnderTest.GetInvoices();

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            var invoiceList = okResult.Value as List<Invoice>;
            Assert.NotNull(invoiceList);
            Assert.AreEqual(10, invoiceList.Count);
        }

        [Test]
        public async Task _02Test_GetAllInvoice_ReturnsListWithValidCountEqualTo11()
        {
            // Arrange
            _sisonkeContext.Invoices.Add(_invoice);
            await _sisonkeContext.SaveChangesAsync();


            _controllerUnderTest = new InvoicesController(_sisonkeContext, _userManager, _roleManager, (AuthenticationContext)_authenticationContext);


            // Act
            var result = await _controllerUnderTest.GetInvoices();

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            var invoiceList = okResult.Value as List<Invoice>;
            Assert.NotNull(invoiceList);
            Assert.AreEqual(11, invoiceList.Count);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        public async Task _03Test_GetInvoiceByID_ReturnsaListwithCount1_WhenCorrectInvoiceIDEntered(int idPassedIn)
        {
            //Arrange
            int id = idPassedIn;

            //Act            
            var result = await _controllerUnderTest.GetInvoices(id);
            var okResult = (OkObjectResult)result.Result;
            var actual = (Invoice)okResult.Value;
            var expected = _sisonkeContext.Invoices.FirstOrDefault(c => c.InvoiceId == id);

            //Assert
            Assert.IsInstanceOf<ActionResult<Invoice>>(result);
            Assert.AreEqual(okResult.StatusCode, 200);
            Assert.AreEqual(actual.InvoiceId, expected.InvoiceId);

        }


        [Test]
        public async Task _04Test_GetInvoiceByID_ReturnsaBackActionResult_WhenInvoiceIDEnteredDoesNotExist()
        {
            //Arrange
            int id = 20;

            //Act            
            var result = await _controllerUnderTest.GetInvoices(id);
            var badResult = (NotFoundObjectResult)result.Result;
            var actual = badResult.Value.ToString();
            var expected = "No Invoice with that ID exists, please try again";

            //Assert
            Assert.IsTrue(actual.Contains(expected));
            Assert.AreEqual(badResult.StatusCode, 404);

        }

        [Test]
        public async Task _05Test_PostAinvoice_ReturnsBadRequest_WhenEmptyInvoiceAdded()
        {
            //Arrange 

            //Act            
            var result = await _controllerUnderTest.PostInvoices(new InvoiceVM());
            var badResult = (BadRequestObjectResult)result.Result;
            var actual = badResult.Value.ToString();
            var expected = "Cannot Add an empty invoice";

            //Assert
      
            Assert.AreEqual(badResult.StatusCode, 400);
            Assert.AreEqual(10, _sisonkeContext.Invoices.Count());

        }

        [Test]
        public async Task _06Test_PutInvoices_ReturnsOkObjectResult()
        {
            //Arrange 
            var authdb = _authenticationContext;
            var employeeRole = authdb.Roles.FirstOrDefault(c => c.Name == "Administrator").Id;
            var userRoles = authdb.UserRoles.FirstOrDefault(c => c.RoleId == employeeRole).UserId;
            identityUser = authdb.ApplicationUsers.FirstOrDefault(c => c.Id == userRoles);
            var user = new ApplicationUser { Id = identityUser.Id };
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("UserID", user.Id),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthentication");
            principal = new ClaimsPrincipal(identity);
            _controllerUnderTest = new InvoicesController(_sisonkeContext, _userManager, _roleManager, _authenticationContext)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = principal }
                }
            };

            var invoice1 = new Invoice()
            {
                InvoiceId = 12,
                EfUserId = 21,
                DiscountId = 1,
                InvoiceDate = new DateTime(2023, 11, 12, 12, 45, 00),
                TotalAmount = 2100.99
            };
            var invoicesVM1 = new InvoiceVM()
            {
                TotalAmount = invoice1.TotalAmount
            };

            var invoice2 = new Invoice()
            {
                InvoiceId = 13,
                EfUserId = 22,
                DiscountId = 12,
                InvoiceDate = new DateTime(2023, 11, 22, 12, 45, 00),
                TotalAmount = 2200.99
            };
            var invoicesVM2 = new InvoiceVM()
            {
                TotalAmount = invoice2.TotalAmount
            };

            var invoice3 = new Invoice()
            {
                InvoiceId = 14,
                EfUserId = 23,
                DiscountId = 14,
                InvoiceDate = new DateTime(2023, 12, 22, 12, 45, 00),
                TotalAmount = 2300.99
            };
            var invoicesVM3 = new InvoiceVM()
            {
                TotalAmount = invoice3.TotalAmount
            };


            //Act            
            var result1 = await _controllerUnderTest.PutInvoices(1, invoicesVM1);
            var result2 = await _controllerUnderTest.PutInvoices(2, invoicesVM2);
            var result3 = await _controllerUnderTest.PutInvoices(3, invoicesVM3);

            var okResult1 = (OkObjectResult)result1;
            var actual1 = okResult1.Value.ToString();
            var expected1 = "Invoice Updated";

            var okResult2 = (OkObjectResult)result2;
            var actual2 = okResult2.Value.ToString();
            var expected2 = "Invoice Updated";

            var okResult3 = (OkObjectResult)result3;
            var actual3 = okResult3.Value.ToString();
            var expected3 = "Invoice Updated";

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result1);
            Assert.AreEqual(okResult1.StatusCode, 200);
            Assert.IsTrue(actual1.Contains(expected1));

            Assert.IsInstanceOf<OkObjectResult>(result2);
            Assert.AreEqual(okResult2.StatusCode, 200);
            Assert.IsTrue(actual2.Contains(expected2));

            Assert.IsInstanceOf<OkObjectResult>(result3);
            Assert.AreEqual(okResult3.StatusCode, 200);
            Assert.IsTrue(actual3.Contains(expected3));

        }

        [Test]
        public async Task _07Test_PutInvoices_ReturnsNotUpdated_WhenEmployee()
        {
            //Arrange 
            var authdb = _authenticationContext;
            var employeeRole = authdb.Roles.FirstOrDefault(c => c.Name == "Employee").Id;
            var userRoles = authdb.UserRoles.FirstOrDefault(c => c.RoleId == employeeRole).UserId;
            identityUser = authdb.ApplicationUsers.FirstOrDefault(c => c.Id == userRoles);
            var user = new ApplicationUser { Id = identityUser.Id };
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("UserID", user.Id),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthentication");
            principal = new ClaimsPrincipal(identity);
            _controllerUnderTest = new InvoicesController(_sisonkeContext, _userManager, _roleManager, _authenticationContext)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = principal }
                }
            };

            var invoice1 = new Invoice()
            {
                InvoiceId = 12,
                EfUserId = 21,
                DiscountId = 1,
                InvoiceDate = new DateTime(2023, 11, 12, 12, 45, 00),
                TotalAmount = 2100.99
            };
            var invoicesVM1 = new InvoiceVM()
            {
                TotalAmount = invoice1.TotalAmount
            };

            var invoice2 = new Invoice()
            {
                InvoiceId = 13,
                EfUserId = 22,
                DiscountId = 12,
                InvoiceDate = new DateTime(2023, 11, 22, 12, 45, 00),
                TotalAmount = 2200.99
            };
            var invoicesVM2 = new InvoiceVM()
            {
                TotalAmount = invoice2.TotalAmount
            };

            var invoice3 = new Invoice()
            {
                InvoiceId = 14,
                EfUserId = 23,
                DiscountId = 14,
                InvoiceDate = new DateTime(2023, 12, 22, 12, 45, 00),
                TotalAmount = 2300.99
            };
            var invoicesVM3 = new InvoiceVM()
            {
                TotalAmount = invoice3.TotalAmount
            };


            //Act            
            var result1 = await _controllerUnderTest.PutInvoices(1, invoicesVM1);
            var result2 = await _controllerUnderTest.PutInvoices(2, invoicesVM2);
            var result3 = await _controllerUnderTest.PutInvoices(3, invoicesVM3);

            var badResult1 = (BadRequestObjectResult)result1;
            var actual1 = badResult1.Value.ToString();
            var expected1 = "Not authorised to update invoices";

            var badResult2 = (BadRequestObjectResult)result2;
            var actual2 = badResult2.Value.ToString();
            var expected2 = "Not authorised to update invoices";

            var badResult3 = (BadRequestObjectResult)result3;
            var actual3 = badResult3.Value.ToString();
            var expected3 = "Not authorised to update invoices";

            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result1);
            Assert.AreEqual(badResult1.StatusCode, 400);
            Assert.IsTrue(actual1.Contains(expected1));

            Assert.IsInstanceOf<BadRequestObjectResult>(result2);
            Assert.AreEqual(badResult2.StatusCode, 400);
            Assert.IsTrue(actual2.Contains(expected2));

            Assert.IsInstanceOf<BadRequestObjectResult>(result3);
            Assert.AreEqual(badResult3.StatusCode, 400);
            Assert.IsTrue(actual3.Contains(expected3));

        }

        [Test]
        public async Task _08Test_PutInvoices_ReturnsNotFoundResult()
        {
            //Arrange 
            var authdb = _authenticationContext;
            var employeeRole = authdb.Roles.FirstOrDefault(c => c.Name == "Administrator").Id;
            var userRoles = authdb.UserRoles.FirstOrDefault(c => c.RoleId == employeeRole).UserId;
            identityUser = authdb.ApplicationUsers.FirstOrDefault(c => c.Id == userRoles);
            var user = new ApplicationUser { Id = identityUser.Id };
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("UserID", user.Id),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthentication");
            principal = new ClaimsPrincipal(identity);
            _controllerUnderTest = new InvoicesController(_sisonkeContext, _userManager, _roleManager, _authenticationContext)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = principal }
                }
            };
            var invoice1 = new Invoice()
            {
                InvoiceId = 12,
                EfUserId = 21,
                DiscountId = 1,
                InvoiceDate = new DateTime(2023, 11, 12, 12, 45, 00),
                TotalAmount = 2100.99
            };
            var invoicesVM1 = new InvoiceVM()
            {
                TotalAmount = invoice1.TotalAmount
            };

            var invoice2 = new Invoice()
            {
                InvoiceId = 13,
                EfUserId = 22,
                DiscountId = 12,
                InvoiceDate = new DateTime(2023, 11, 22, 12, 45, 00),
                TotalAmount = 2200.99
            };
            var invoicesVM2 = new InvoiceVM()
            {
                TotalAmount = invoice2.TotalAmount
            };

            var invoice3 = new Invoice()
            {
                InvoiceId = 14,
                EfUserId = 23,
                DiscountId = 14,
                InvoiceDate = new DateTime(2023, 12, 22, 12, 45, 00),
                TotalAmount = 2300.99
            };
            var invoicesVM3 = new InvoiceVM()
            {
                TotalAmount = invoice3.TotalAmount
            };


            //Act            
            var result1 = await _controllerUnderTest.PutInvoices(11, invoicesVM1);
            var result2 = await _controllerUnderTest.PutInvoices(12, invoicesVM2);
            var result3 = await _controllerUnderTest.PutInvoices(13, invoicesVM3);

            var badResult1 = (NotFoundObjectResult)result1;
            var actual1 = badResult1.Value.ToString();
            var expected1 = "No Invoice with that ID exists, please try again";

            var badResult2 = (NotFoundObjectResult)result2;
            var actual2 = badResult2.Value.ToString();
            var expected2 = "No Invoice with that ID exists, please try again";

            var badResult3 = (NotFoundObjectResult)result3;
            var actual3 = badResult3.Value.ToString();
            var expected3 = "No Invoice with that ID exists, please try again";

            //Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result1);
            Assert.AreEqual(badResult1.StatusCode, 404);
            Assert.IsTrue(actual1.Contains(expected1));

            Assert.IsInstanceOf<NotFoundObjectResult>(result2);
            Assert.AreEqual(badResult2.StatusCode, 404);
            Assert.IsTrue(actual2.Contains(expected2));

            Assert.IsInstanceOf<NotFoundObjectResult>(result3);
            Assert.AreEqual(badResult3.StatusCode, 404);
            Assert.IsTrue(actual3.Contains(expected3));

        }

        


        [TestCase(11)]
        [TestCase(12)]
        [TestCase(10)]
        [TestCase(9)]
        [TestCase(8)]
        [TestCase(7)]
        public async Task _10Test_GetInvoiceByID_ReturnsWithCorrectType_WhenPassedInID(int id)
        {
            //Arrange 
            var invoice1 = new Invoice()
            {
                InvoiceId = 12,
                EfUserId = 21,
                DiscountId = 1,
                InvoiceDate = new DateTime(2023, 11, 12, 12, 45, 00),
                TotalAmount = 2100.99
            };
            var invoicesVM1 = new InvoiceVM()
            {
                TotalAmount = invoice1.TotalAmount
            };

            var invoice2 = new Invoice()
            {
                InvoiceId = 13,
                EfUserId = 22,
                DiscountId = 12,
                InvoiceDate = new DateTime(2023, 11, 22, 12, 45, 00),
                TotalAmount = 2200.99
            };
            var invoicesVM2 = new InvoiceVM()
            {
                TotalAmount = invoice2.TotalAmount
            };

            var invoice3 = new Invoice()
            {
                InvoiceId = 14,
                EfUserId = 23,
                DiscountId = 14,
                InvoiceDate = new DateTime(2023, 12, 22, 12, 45, 00),
                TotalAmount = 2300.99
            };
            var invoicesVM3 = new InvoiceVM()
            {
                TotalAmount = invoice3.TotalAmount
            };

            await _controllerUnderTest.PostInvoices(invoicesVM1);
            await _controllerUnderTest.PostInvoices(invoicesVM2);

            //Act            
            var actionResult = await _controllerUnderTest.GetInvoices(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsInstanceOf<ActionResult<Invoice>>(actionResult);
        }



        [Test]
        public async Task _11Test_GetAllinvoice_ReturnsWithCorrectTypeAndCount()
        {
            //Arrange 
            var invoice1 = new Invoice()
            {
                InvoiceId = 12,
                EfUserId = 21,
                DiscountId = 1,
                InvoiceDate = new DateTime(2023, 11, 12, 12, 45, 00),
                TotalAmount = 2100.99
            };
            var invoicesVM1 = new InvoiceVM()
            {
                TotalAmount = invoice1.TotalAmount
            };

            var invoice2 = new Invoice()
            {
                InvoiceId = 13,
                EfUserId = 22,
                DiscountId = 12,
                InvoiceDate = new DateTime(2023, 11, 22, 12, 45, 00),
                TotalAmount = 2200.99
            };
            var invoicesVM2 = new InvoiceVM()
            {
                TotalAmount = invoice2.TotalAmount
            };

            var invoice3 = new Invoice()
            {
                InvoiceId = 14,
                EfUserId = 23,
                DiscountId = 14,
                InvoiceDate = new DateTime(2023, 12, 22, 12, 45, 00),
                TotalAmount = 2300.99
            };
            var invoicesVM3 = new InvoiceVM()
            {
                TotalAmount = invoice3.TotalAmount
            };

            await _controllerUnderTest.PostInvoices(invoicesVM1);
            await _controllerUnderTest.PostInvoices(invoicesVM2);
            await _controllerUnderTest.PostInvoices(invoicesVM3);

            //Act            
            var actionResult = await _controllerUnderTest.GetInvoices();

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsInstanceOf<ActionResult<IEnumerable<Invoice>>>(actionResult);
            var result = (OkObjectResult)actionResult.Result;
            var value = (List<Invoice>)result.Value;
            Assert.AreEqual(_sisonkeContext.Invoices.Count(), value.Count);
        }

        [Test]
        public async Task _12Test_GetinvoiceById_ReturnsWithCorrectType()
        {
            //Arrange 
            var invoice1 = new Invoice()
            {
                InvoiceId = 12,
                EfUserId = 21,
                DiscountId = 1,
                InvoiceDate = new DateTime(2023, 11, 12, 12, 45, 00),
                TotalAmount = 2100.99
            };
            var invoicesVM1 = new InvoiceVM()
            {
                TotalAmount = invoice1.TotalAmount
            };

            var invoice2 = new Invoice()
            {
                InvoiceId = 13,
                EfUserId = 22,
                DiscountId = 12,
                InvoiceDate = new DateTime(2023, 11, 22, 12, 45, 00),
                TotalAmount = 2200.99
            };
            var invoicesVM2 = new InvoiceVM()
            {
                TotalAmount = invoice2.TotalAmount
            };

            var invoice3 = new Invoice()
            {
                InvoiceId = 14,
                EfUserId = 23,
                DiscountId = 14,
                InvoiceDate = new DateTime(2023, 12, 22, 12, 45, 00),
                TotalAmount = 2300.99
            };
            var invoicesVM3 = new InvoiceVM()
            {
                TotalAmount = invoice3.TotalAmount
            };


            await _controllerUnderTest.PostInvoices(_invoiceVM);
            await _controllerUnderTest.PostInvoices(invoicesVM2);

            //Act            
            var actionResult = await _controllerUnderTest.GetInvoices(11);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsInstanceOf<ActionResult<Invoice>>(actionResult);
        }


        [Test]
        public async Task _13Test_PostInvoices_NotAddedAndShowsInContextCount_WhenUserIsAEmployee()
        {
            //Arrange
            var authdb = _authenticationContext;
            var employeeRole = authdb.Roles.FirstOrDefault(c => c.Name == "Employee").Id;
            var userRoles = authdb.UserRoles.FirstOrDefault(c => c.RoleId == employeeRole).UserId;
            identityUser = authdb.ApplicationUsers.FirstOrDefault(c => c.Id == userRoles);
            var user = new ApplicationUser { Id = identityUser.Id };
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("UserID", user.Id),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthentication");
            principal = new ClaimsPrincipal(identity);
            _controllerUnderTest = new InvoicesController(_sisonkeContext, _userManager, _roleManager, _authenticationContext)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = principal }
                }
            };


            //Act            
            var actionResult = await _controllerUnderTest.PostInvoices(_invoiceVM);
            //Assert
            Assert.IsFalse(_sisonkeContext.Invoices.Where(c => c.InvoiceId == 11).Count() > 0);
            Assert.IsTrue(_sisonkeContext.Invoices.Count() == 10);

        }


        [Test]
        public async Task _14Test_PostInvoices_ReturnsBadObjectResult_WhenUserIsACustomer()
        {
            //Arrange
            

            //Act            
            var actionResult = await _controllerUnderTest.PostInvoices(_invoiceVM);

            //Assert
            var result = (ActionResult<Invoice>)actionResult.Result;
            var badResult = (BadRequestObjectResult)actionResult.Result;
            var expected = "Not authorised to add invoices as a customer";
            var actual = badResult.Value.ToString();

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsInstanceOf<ActionResult<Invoice>>(actionResult);
            Assert.AreEqual(badResult.StatusCode, 400);
            Assert.IsFalse(actual.Contains(expected));
            Assert.AreEqual(10, _sisonkeContext.Invoices.Count());
        }


        [Test]
        public async Task _15Test_DeleteInvoices_ReturnsMessageThatEmployeeCannotDeleteInvoice()
        {
            //Arrange 
            var authdb = _authenticationContext;
            var employeeRole = authdb.Roles.FirstOrDefault(c => c.Name == "Employee").Id;
            var userRoles = authdb.UserRoles.FirstOrDefault(c => c.RoleId == employeeRole).UserId;
            identityUser = authdb.ApplicationUsers.FirstOrDefault(c => c.Id == userRoles);
            var user = new ApplicationUser { Id = identityUser.Id };
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("UserID", user.Id),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthentication");
            principal = new ClaimsPrincipal(identity);
            _controllerUnderTest = new InvoicesController(_sisonkeContext, _userManager, _roleManager, _authenticationContext)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = principal }
                }
            };

            var invoice1 = new Invoice()
            {
                InvoiceId = 12,
                EfUserId = 21,
                DiscountId = 1,
                InvoiceDate = new DateTime(2023, 11, 12, 12, 45, 00),
                TotalAmount = 2100.99
            };
            var invoicesVM1 = new InvoiceVM()
            {
                TotalAmount = invoice1.TotalAmount
            };

            var invoice2 = new Invoice()
            {
                InvoiceId = 13,
                EfUserId = 22,
                DiscountId = 12,
                InvoiceDate = new DateTime(2023, 11, 22, 12, 45, 00),
                TotalAmount = 2200.99
            };
            var invoicesVM2 = new InvoiceVM()
            {
                TotalAmount = invoice2.TotalAmount
            };

            var invoice3 = new Invoice()
            {
                InvoiceId = 14,
                EfUserId = 23,
                DiscountId = 14,
                InvoiceDate = new DateTime(2023, 12, 22, 12, 45, 00),
                TotalAmount = 2300.99
            };
            var invoicesVM3 = new InvoiceVM()
            {
                TotalAmount = invoice3.TotalAmount
            };

            //Act

            var actionResultDeleted = await _controllerUnderTest.DeleteInvoices(8);
            var result = (ActionResult<Invoice>)actionResultDeleted.Result;
            var badResult = (BadRequestObjectResult)actionResultDeleted.Result;
            var expected = "Not authorised to delete invoices";
            var actual = badResult.Value.ToString();

            //Assert
            Assert.IsInstanceOf<ActionResult<Invoice>>(actionResultDeleted);
            Assert.AreEqual(badResult.StatusCode, 400);
            Assert.IsTrue(actual.Contains(expected));
            Assert.AreEqual(10, _sisonkeContext.Invoices.Count());
        }



        [Test]
        public async Task _16Test_DeleteInvoices_DeleteSuccessfullyReturnsWithCorrectTypeAndShowsInContextCount_WhenAdministrator()
        {
            //Arrange
            var authdb = _authenticationContext;
            var employeeRole = authdb.Roles.FirstOrDefault(c => c.Name == "Administrator").Id;
            var userRoles = authdb.UserRoles.FirstOrDefault(c => c.RoleId == employeeRole).UserId;
            identityUser = authdb.ApplicationUsers.FirstOrDefault(c => c.Id == userRoles);
            var user = new ApplicationUser { Id = identityUser.Id };
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("UserID", user.Id),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthentication");
            principal = new ClaimsPrincipal(identity);
            _controllerUnderTest = new InvoicesController(_sisonkeContext, _userManager, _roleManager, _authenticationContext)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = principal }
                }
            };

            var invoice2 = new Invoice()
            {
                InvoiceId = 11,
                EfUserId = 21,
                DiscountId = 1,
                InvoiceDate = new DateTime(2023, 11, 12, 12, 45, 00),
                TotalAmount = 2100.99
            };
            var invoicesVM2 = new InvoiceVM()
            {
                TotalAmount = invoice2.TotalAmount
            };

            //Act
            var actionResult = await _controllerUnderTest.PostInvoices(invoicesVM2);
            var actionResultDeleted = await _controllerUnderTest.DeleteInvoices(invoice2.InvoiceId);

            //Assert
            Assert.NotNull(actionResultDeleted);
            Assert.IsInstanceOf<ActionResult<Invoice>>(actionResultDeleted);
            Assert.AreEqual(10, _sisonkeContext.Invoices.Count());
        }


        [Test]
        public async Task _17Test_DeleteInvoices_AddMultipleDeleteOne_DeleteSuccessfullyReturnsWithCorrectTypeAndShowsInContextCount_WhenAdministrator()
        {
            //Arrange 
            var authdb = _authenticationContext;
            var employeeRole = authdb.Roles.FirstOrDefault(c => c.Name == "Employee").Id;
            var userRoles = authdb.UserRoles.FirstOrDefault(c => c.RoleId == employeeRole).UserId;
            identityUser = authdb.ApplicationUsers.FirstOrDefault(c => c.Id == userRoles);
            var user = new ApplicationUser { Id = identityUser.Id };
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("UserID", user.Id),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthentication");
            principal = new ClaimsPrincipal(identity);
            _controllerUnderTest = new InvoicesController(_sisonkeContext, _userManager, _roleManager, _authenticationContext)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = principal }
                }
            };

            var invoice2 = new Invoice()
            {
                InvoiceId = 12,
                EfUserId = 21,
                DiscountId = 1,
                InvoiceDate = new DateTime(2023, 11, 12, 12, 45, 00),
                DueDate = new DateTime(2024, 3, 29, 09, 34, 00),
                Subtotal = 834.34,
                Tax = 12,
                Status = "Pending",
                TotalAmount = 2100.99
            };
            var invoicesVM2 = new InvoiceVM()
            {
                TotalAmount = invoice2.TotalAmount
            };

            //Act
            await _controllerUnderTest.PostInvoices(_invoiceVM);
            await _controllerUnderTest.PostInvoices(invoicesVM2);

            var actionResultDeleted = await _controllerUnderTest.DeleteInvoices(_invoice.InvoiceId);
            var actionResultDeleted2 = await _controllerUnderTest.DeleteInvoices(invoice2.InvoiceId);

            //Assert
            Assert.NotNull(actionResultDeleted);
            Assert.IsInstanceOf<ActionResult<Invoice>>(actionResultDeleted);
            Assert.NotNull(actionResultDeleted2);
            Assert.IsInstanceOf<ActionResult<Invoice>>(actionResultDeleted2);
            Assert.AreEqual(10, _sisonkeContext.Invoices.Count());
        }

        [TestCase("2024-03-03")]
        [TestCase("2024-03-04")]
        [TestCase("2024-03-05")]
        public async Task _18Test_GetInvoiceByDate_ReturnsaListwithCount1_WhenCorrectInvoiceDateEntered(DateTime datePassedIn)
        {
            //Arrange
            DateTime date = datePassedIn;

            //Act            
            var result = await _controllerUnderTest.GetInvoiceByDate(date);
            var okResult = (OkObjectResult)result.Result;
            var actual = (List<Invoice>)okResult.Value;
            var expected = _sisonkeContext.Invoices.FirstOrDefault(c => c.InvoiceDate == date);

            //Assert
            Assert.IsInstanceOf<ActionResult<List<Invoice>>>(result);
            Assert.AreEqual(okResult.StatusCode, 200);
            Assert.IsTrue(actual.Count() > 0);

        }

        [TestCase("2023-12-12")]
        [TestCase("2017-05-22")]
        [TestCase("2018-11-22")]
        [TestCase("2018-03-22")]
        [TestCase("2018-03-02")]
        [TestCase("2012-10-13")]
        [TestCase("2022-02-02")]
        [TestCase("2012-04-22")]
        [TestCase("2022-12-12")]
        [TestCase("2023-12-10")]
        [TestCase("2021-03-22")]
        public async Task _19Test_GetInvoiceByDate_ReturnsaListwithCount0_WhenWrongInvoiceDateEntered(DateTime datePassedIn)
        {
            //Arrange
            DateTime date = datePassedIn;

            //Act            
            var result = await _controllerUnderTest.GetInvoiceByDate(date);
            var notFoundResult = (NotFoundObjectResult)result.Result;
            var actual = "No Invoice with that date exists, please try again";
            var expected = "No Invoice with that date exists, please try again";

            //Assert
            Assert.AreEqual(notFoundResult.StatusCode, 404);
            Assert.IsTrue(actual.Contains(expected));

        }


        [TestCase("2024-03-01", "2024-03-04")]
        [TestCase("2024-03-01", "2024-03-06")]
        [TestCase("2024-03-01", "2024-03-08")]
        [TestCase("2024-03-01", "2024-03-10")]
        public async Task _20Test_GetInvoiceByDate_ReturnsaListwithCount1_WhenCorrectInvoiceDateEntered(DateTime date1PassedIn, DateTime date2PassedIn)
        {
            //Arrange
            DateTime date1 = date1PassedIn;
            DateTime date2 = date2PassedIn;

            //Act            
            var result = await _controllerUnderTest.GetInvoiceByBetweenDates(date1, date2);
            var okResult = (OkObjectResult)result.Result;
            var actual = (List<Invoice>)okResult.Value;

            //Assert
            Assert.IsInstanceOf<ActionResult<List<Invoice>>>(result);
            Assert.AreEqual(okResult.StatusCode, 200);
            Assert.IsTrue(actual.Count() > 0);

        }

        [TestCase("2012-01-01", "2012-12-12")]
        [TestCase("2013-01-01", "2013-12-12")]
        [TestCase("2014-01-01", "2014-12-12")]
        [TestCase("2015-01-01", "2015-12-12")]
        [TestCase("2016-01-01", "2016-12-12")]
        public async Task _21Test_GetInvoiceByDate_ReturnsaListwithCount0_WhenWrongInvoiceDateEntered(DateTime date1PassedIn, DateTime date2PassedIn)
        {
            //Arrange
            DateTime date1 = date1PassedIn;
            DateTime date2 = date2PassedIn;
               

            //Act            
            var result = await _controllerUnderTest.GetInvoiceByBetweenDates(date1, date2);
            var notFoundResult = (NotFoundObjectResult)result.Result;
            var actual = "No Invoice with that date range exists, please try again";
            var expected = "No Invoice with that date range exists, please try again";

            //Assert
            Assert.AreEqual(notFoundResult.StatusCode, 404);
            Assert.IsTrue(actual.Contains(expected));

        }
    }
}
