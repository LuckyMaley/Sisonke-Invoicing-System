using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SISONKE_Invoicing_RESTAPI.AuthModels;
using SISONKE_Invoicing_RESTAPI.Controllers;
using SISONKE_Invoicing_RESTAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace RESTAPI.NunitTests
{

    [TestFixture]
    public class ProductControllerTests
    {
        private SISONKE_Invoicing_System_EFDBContext _context;
        private ProductsController _controllerUnderTest;
        private List<Product> _productList;
        private SignInManager<ApplicationUser> _signInManager;
        private ApplicationSettings _appSettings;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private AuthenticationContext _authenticationContext;
        ProductVM _productVM;
        Product _product;
        IdentityUser identityUser;
        ClaimsPrincipal principal;

        [SetUp]
        public void Initialiser()
        {
            _context = (SISONKE_Invoicing_System_EFDBContext)InMemoryContext.GeneratedDB();
            var prod = _context.Products.Count();
            _authenticationContext = (AuthenticationContext)InMemoryContext.GeneratedAuthDB();
            _userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(_authenticationContext), null, null, null, null, null, null, null, null);


            _roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(_authenticationContext), null, null, null, null);
            var authdb = _authenticationContext;

            identityUser = authdb.ApplicationUsers.First();
            var user = new ApplicationUser { Id = identityUser.Id };
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("UserID", user.Id),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthentication");
            principal = new ClaimsPrincipal(identity);
            _controllerUnderTest = new ProductsController(_userManager, _roleManager, _context, _authenticationContext)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = principal }
                }
            };

            _productList = new List<Product>();
            _product = new Product()
            {
                ProductId = 243,
                Name = "Laptop",

                Description = "Full HD, Intel Core i5, 8GB RAM, 512GB SSD",

                Price = 2100.99m,

                StockQuantity = 100,
                ModifiedDate = new DateTime(2018, 11, 12, 12, 45, 00)
            };
            _productVM = new ProductVM()
            {
                Name = _product.Name,

                Description = _product.Description,

                Price = _product.Price,

                StockQuantity = _product.StockQuantity
            };

        }



        [TearDown]
        public void CleanUpObject()
        {
            _context.Database.EnsureDeleted();
            _controllerUnderTest = null;
            _productList = null;
            _product = null;
            _userManager = null;

            _roleManager = null;
            _authenticationContext.Database.EnsureDeleted();
        }

        [Test]
        public async Task _01Test_GetAllProduct_ReturnsListWithValidCount0()
        {
            // Arrange


            // Act
            var result = await _controllerUnderTest.GetProducts();

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            var productList = okResult.Value as List<Product>;
            Assert.NotNull(productList);
            Assert.AreEqual(10, productList.Count);
        }

        [Test]
        public async Task _02Test_GetAllProduct_ReturnsListWithValidCountEqualTo11()
        {
            // Arrange
            _context.Products.Add(_product);
            await _context.SaveChangesAsync();


            _controllerUnderTest = new ProductsController(_userManager, _roleManager, _context, _authenticationContext);


            // Act
            var result = await _controllerUnderTest.GetProducts();

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            var productList = okResult.Value as List<Product>;
            Assert.NotNull(productList);
            Assert.AreEqual(11, productList.Count);
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
        public async Task _03Test_GetProductByID_ReturnsaListwithCount1_WhenCorrectProductIDEntered(int idPassedIn)
        {
            //Arrange
            int id = idPassedIn;

            //Act            
            var result = await _controllerUnderTest.GetProducts(id);
            var okResult = (OkObjectResult)result.Result;
            var actual = (Product)okResult.Value;
            var expected = _context.Products.FirstOrDefault(c => c.ProductId == id);

            //Assert
            Assert.IsInstanceOf<ActionResult<Product>>(result);
            Assert.AreEqual(okResult.StatusCode, 200);
            Assert.AreEqual(actual.ProductId, expected.ProductId);

        }


        [TestCase("Laptop")]
        [TestCase("Smartphone")]
        [TestCase("Smartwatch")]
        [TestCase("Headphones")]
        [TestCase("Tablet")]
        public async Task _04Test_GetProductByName_ReturnsaListwithCount1_WhenCorrectProductIDEntered(string namePassedIn)
        {
            //Arrange
            string name = namePassedIn;

            //Act            
            var result = await _controllerUnderTest.GetProductByName(name);
            var okResult = (OkObjectResult)result.Result;
            var actual = (List<Product>)okResult.Value;
            var expected = _context.Products.FirstOrDefault(c => c.Name == name);

            //Assert
            Assert.IsInstanceOf<ActionResult<List<Product>>>(result);
            Assert.AreEqual(okResult.StatusCode, 200);
            Assert.AreEqual(actual.FirstOrDefault(c => c.Name == name).ProductId, expected.ProductId);

        }

        [TestCase("Waptop")]
        [TestCase("Smartbone")]
        [TestCase("Smartlatch")]
        [TestCase("Nosephones")]
        [TestCase("Tablett")]
        public async Task _05Test_GetProductByName_ReturnsNotFound_WhenIncorrectProductNameEntered(string namePassedIn)
        {
            //Arrange
            string name = namePassedIn;

            //Act            
            var result = await _controllerUnderTest.GetProductByName(name);
            var notFoundResult = (NotFoundObjectResult)result.Result;
            var actual = notFoundResult.Value.ToString();
            var expected = "No Product with that Name exists, please try again";

            //Assert
            Assert.AreEqual(notFoundResult.StatusCode, 404);
            Assert.IsTrue(actual.Contains(expected));

        }

        [Test]
        public async Task _06Test_GetProductByID_ReturnsaBackActionResult_WhenProductIDEnteredDoesNotExist()
        {
            //Arrange
            int id = 20;

            //Act            
            var result = await _controllerUnderTest.GetProducts(id);
            var badResult = (NotFoundObjectResult)result.Result;
            var actual = badResult.Value.ToString();
            var expected = "No Product with that ID exists, please try again";

            //Assert
            Assert.IsTrue(actual.Contains(expected));
            Assert.AreEqual(badResult.StatusCode, 404);

        }

        [Test]
        public async Task _07Test_PostAproduct_ReturnsBadRequest_WhenEmptyProductAdded()
        {
            //Arrange 

            //Act            
            var result = await _controllerUnderTest.PostProducts(new ProductVM());
            var badResult = (BadRequestObjectResult)result.Result;
            var actual = badResult.Value.ToString();
            var expected = "Cannot to add products an empty product";

            //Assert
            Assert.IsTrue(actual.Contains(expected));
            Assert.AreEqual(badResult.StatusCode, 400);
            Assert.AreEqual(10, _context.Products.Count());
        }

        [Test]
        public async Task _08Test_PutProducts_ReturnsOkObjectResult()
        {
            //Arrange 
            var product1 = new Product();
            product1.Name = "Laptop";
            ProductVM productsVM1 = new ProductVM()
            {
                Name = product1.Name,

                Description = product1.Description,
                Price = product1.Price,

                StockQuantity = product1.StockQuantity,

            };

            var product2 = new Product();
            product2.Name = "Smartphone";
            ProductVM productsVM2 = new ProductVM()
            {
                Name = product2.Name,

                Description = product2.Description,
                Price = product2.Price,

                StockQuantity = product2.StockQuantity,

            };

            var product3 = new Product();
            product3.Name = "Smartwatch";
            ProductVM productsVM3 = new ProductVM()
            {
                Name = product3.Name,

                Description = product3.Description,
                Price = product3.Price,

                StockQuantity = product3.StockQuantity,

            };


            //Act            
            var result1 = await _controllerUnderTest.PutProducts(1, productsVM1);
            var result2 = await _controllerUnderTest.PutProducts(2, productsVM2);
            var result3 = await _controllerUnderTest.PutProducts(3, productsVM3);

            var okResult1 = (OkObjectResult)result1;
            var actual1 = okResult1.Value.ToString();
            var expected1 = "Product Updated";

            var okResult2 = (OkObjectResult)result2;
            var actual2 = okResult2.Value.ToString();
            var expected2 = "Product Updated";

            var okResult3 = (OkObjectResult)result3;
            var actual3 = okResult3.Value.ToString();
            var expected3 = "Product Updated";

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
            Assert.AreEqual(product1.Name, _context.Products.FirstOrDefault(c => c.ProductId == 1).Name);
            Assert.AreEqual(product2.Name, _context.Products.FirstOrDefault(c => c.ProductId == 2).Name);
            Assert.AreEqual(product3.Name, _context.Products.FirstOrDefault(c => c.ProductId == 3).Name);

        }

        [Test]
        public async Task _09Test_PutProducts_ReturnsNotUpdated_WhenCustomerDidNotAddThem()
        {
            //Arrange 
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
            _controllerUnderTest = new ProductsController(_userManager, _roleManager, _context, _authenticationContext)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = principal }
                }
            };

            var product1 = new Product();
            product1.Name = "Smartwatch";
            ProductVM productsVM1 = new ProductVM()
            {
                Name = product1.Name,

                Description = product1.Description,
                Price = product1.Price,

                StockQuantity = product1.StockQuantity

            };

            var product2 = new Product();
            product2.Name = "Smartwatch";
            ProductVM productsVM2 = new ProductVM()
            {
                Name = product2.Name,

                Description = product2.Description,
                Price = product2.Price,

                StockQuantity = product2.StockQuantity

            };

            var product3 = new Product();
            product3.Name = "Adidas Yeezy Black Slides";
            ProductVM productsVM3 = new ProductVM()
            {
                Name = product3.Name,

                Description = product3.Description,
                Price = product3.Price,

                StockQuantity = product3.StockQuantity

            };




            //Act            
            var result1 = await _controllerUnderTest.PutProducts(1, productsVM1);
            var result2 = await _controllerUnderTest.PutProducts(2, productsVM2);
            var result3 = await _controllerUnderTest.PutProducts(3, productsVM3);

            var badResult1 = (BadRequestObjectResult)result1;
            var actual1 = badResult1.Value.ToString();
            var expected1 = "Not authorised to update products as a customer";

            var badResult2 = (BadRequestObjectResult)result2;
            var actual2 = badResult2.Value.ToString();
            var expected2 = "Not authorised to update products as a customer";

            var badResult3 = (BadRequestObjectResult)result3;
            var actual3 = badResult3.Value.ToString();
            var expected3 = "Not authorised to update products as a customer";

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
        public async Task _10Test_PutProducts_ReturnsNotFoundResult()
        {
            //Arrange 
            var product1 = new Product();
            product1.ProductId = 11;
            product1.Name = "Laptop";
            ProductVM productsVM1 = new ProductVM()
            {
                Name = product1.Name,
            };

            var product2 = new Product();
            product2.ProductId = 13;
            product2.Name = "Smartwatch";
            ProductVM productsVM2 = new ProductVM()
            {
                Name = product2.Name,
            };

            var product3 = new Product();
            product3.ProductId = 19;
            product3.Name = "External Hard Drive";
            ProductVM productsVM3 = new ProductVM()
            {
                Name = product3.Name,
            };


            //Act            
            var result1 = await _controllerUnderTest.PutProducts(11, productsVM1);
            var result2 = await _controllerUnderTest.PutProducts(13, productsVM2);
            var result3 = await _controllerUnderTest.PutProducts(19, productsVM3);

            var badResult1 = (NotFoundObjectResult)result1;
            var actual1 = badResult1.Value.ToString();
            var expected1 = "No Product with that ID exists, please try again";

            var badResult2 = (NotFoundObjectResult)result2;
            var actual2 = badResult2.Value.ToString();
            var expected2 = "No Product with that ID exists, please try again";

            var badResult3 = (NotFoundObjectResult)result3;
            var actual3 = badResult3.Value.ToString();
            var expected3 = "No Product with that ID exists, please try again";

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

        [Test]
        public async Task _11Test_PostProduct_ReturnsActionResultObjectWith13Products_WhenAdded3Products()
        {
            //Arrange
            Product _product2 = new Product()
            {
                ProductId = 9,
                Name = "External Hard Drive",

                Description = "2TB Portable USB 3.0 External HDD, Backup Solution",

                Price = 2700.99m,

                StockQuantity = 100,
                ModifiedDate = new DateTime(2017, 1, 22, 15, 35, 10)
            };
            ProductVM _productVM2 = new ProductVM()
            {
                Name = _product2.Name,

                Description = _product2.Description,

                Price = _product2.Price,

                StockQuantity = _product2.StockQuantity,
            };
            Product _product3 = new Product()
            {
                ProductId = 1,
                Name = "Laptop",

                Description = "Full HD, Intel Core i5, 8GB RAM, 512GB SSD",

                Price = 900.99m,

                StockQuantity = 100,
                ModifiedDate = new DateTime(2019, 10, 12, 12, 45, 00)
            };
            ProductVM _productVM3 = new ProductVM()
            {
                Name = _product3.Name,

                Description = _product3.Description,

                Price = _product3.Price,

                StockQuantity = _product3.StockQuantity
            };

            //Act            
            var result1 = await _controllerUnderTest.PostProducts(_productVM);
            var result2 = await _controllerUnderTest.PostProducts(_productVM2);
            var result3 = await _controllerUnderTest.PostProducts(_productVM3);

            //Assert
            Assert.NotNull(_context.Products);
            Assert.AreEqual(13, _context.Products.Count());

        }


        [TestCase(11)]
        [TestCase(12)]
        [TestCase(10)]
        [TestCase(9)]
        [TestCase(8)]
        [TestCase(7)]
        public async Task _12Test_GetProductByID_ReturnsWithCorrectType_WhenPassedInID(int id)
        {
            //Arrange 
            Product _product2 = new Product()
            {
                ProductId = 1,
                Name = "Laptop",

                Description = "Full HD, Intel Core i5, 8GB RAM, 512GB SSD",

                Price = 2700.99m,

                StockQuantity = 100,
                ModifiedDate = new DateTime(2017, 1, 22, 15, 35, 10)
            };
            ProductVM _productVM2 = new ProductVM()
            {
                Name = _product2.Name,

                Description = _product2.Description,

                Price = _product2.Price,

                StockQuantity = _product2.StockQuantity
            };

            await _controllerUnderTest.PostProducts(_productVM);
            await _controllerUnderTest.PostProducts(_productVM2);

            //Act            
            var actionResult = await _controllerUnderTest.GetProducts(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsInstanceOf<ActionResult<Product>>(actionResult);
        }



        [Test]
        public async Task _12Test_GetAllproduct_ReturnsWithCorrectTypeAndCount()
        {
            //Arrange 
            Product _product2 = new Product()
            {
                ProductId = 1,
                Name = "Laptop",

                Description = "Full HD, Intel Core i5, 8GB RAM, 512GB SSD",

                Price = 2700.99m,

                StockQuantity = 100,
                ModifiedDate = new DateTime(2017, 1, 22, 15, 35, 10)
            };
            ProductVM _productVM2 = new ProductVM()
            {
                Name = _product2.Name,

                Description = _product2.Description,

                Price = _product2.Price,

                StockQuantity = _product2.StockQuantity
            };
            Product _product3 = new Product()
            {
                ProductId = 1,
                Name = "Laptop",

                Description = "Full HD, Intel Core i5, 8GB RAM, 512GB SSD",

                Price = 2700.99m,

                StockQuantity = 100,
                ModifiedDate = new DateTime(2017, 1, 22, 15, 35, 10)
            };
            ProductVM _productVM3 = new ProductVM()
            {
                Name = _product3.Name,

                Description = _product3.Description,

                Price = _product3.Price,

                StockQuantity = _product3.StockQuantity
            };

            await _controllerUnderTest.PostProducts(_productVM);
            await _controllerUnderTest.PostProducts(_productVM2);
            await _controllerUnderTest.PostProducts(_productVM3);

            //Act            
            var actionResult = await _controllerUnderTest.GetProducts();

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsInstanceOf<ActionResult<IEnumerable<Product>>>(actionResult);
            var result = (OkObjectResult)actionResult.Result;
            var value = (List<Product>)result.Value;
            Assert.AreEqual(_context.Products.Count(), value.Count);
        }

        [Test]
        public async Task _13Test_GetproductById_ReturnsWithCorrectType()
        {
            //Arrange 
            Product _product2 = new Product()
            {
                ProductId = 1,
                Name = "Laptop",

                Description = "Full HD, Intel Core i5, 8GB RAM, 512GB SSD",

                Price = 2700.99m,

                StockQuantity = 100,
                ModifiedDate = new DateTime(2017, 1, 22, 15, 35, 10)
            };
            ProductVM _productVM2 = new ProductVM()
            {
                Name = _product2.Name,

                Description = _product2.Description,

                Price = _product2.Price,

                StockQuantity = _product2.StockQuantity
            };


            await _controllerUnderTest.PostProducts(_productVM);
            await _controllerUnderTest.PostProducts(_productVM2);

            //Act            
            var actionResult = await _controllerUnderTest.GetProducts(11);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsInstanceOf<ActionResult<Product>>(actionResult);
        }


        [Test]
        public async Task _14Test_PostProducts_AddedSuccessfullyAndShowsInContextCount_WhenUserIsAEmployee()
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
            _controllerUnderTest = new ProductsController(_userManager, _roleManager, _context, _authenticationContext)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = principal }
                }
            };


            //Act            
            var actionResult = await _controllerUnderTest.PostProducts(_productVM);
            var userId = _context.EfUsers.FirstOrDefault(c => c.IdentityUsername == identityUser.UserName).EfUserId;
            var prodId = _context.Products.FirstOrDefault(c => c.ProductId == _context.Products.Max(c => c.ProductId)).ProductId;
            //Assert
            Assert.NotNull(actionResult);


        }


        [Test]
        public async Task _15Test_PostProducts_ReturnsBadObjectResult_WhenUserIsACustomer()
        {
            //Arrange
            var authdb = _authenticationContext;
            var customerRole = authdb.Roles.FirstOrDefault(c => c.Name == "Customer").Id;
            var userRoles = authdb.UserRoles.FirstOrDefault(c => c.RoleId == customerRole).UserId;
            identityUser = authdb.ApplicationUsers.FirstOrDefault(c => c.Id == userRoles);
            var user = new ApplicationUser { Id = identityUser.Id };
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("UserID", user.Id),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthentication");
            principal = new ClaimsPrincipal(identity);
            _controllerUnderTest = new ProductsController(_userManager, _roleManager, _context, _authenticationContext)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = principal }
                }
            };

            //Act            
            var actionResult = await _controllerUnderTest.PostProducts(_productVM);

            //Assert
            var result = (ActionResult<Product>)actionResult.Result;
            var badResult = (BadRequestObjectResult)actionResult.Result;
            var expected = "Not authorised to add products as a customer";
            var actual = badResult.Value.ToString();

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsInstanceOf<ActionResult<Product>>(actionResult);
            Assert.AreEqual(badResult.StatusCode, 400);
            Assert.IsTrue(actual.Contains(expected));
            Assert.AreEqual(10, _context.Products.Count());
        }


        [Test]
        public async Task _16Test_DeleteProducts_ReturnsMessageThatEmployeeCannotDeleteProduct_WhenTheyDidNotAddIt()
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
            _controllerUnderTest = new ProductsController(_userManager, _roleManager, _context, _authenticationContext)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = principal }
                }
            };

            Product _product2 = new Product()
            {
                ProductId = 1,
                Name = "Laptop",

                Description = "Full HD, Intel Core i5, 8GB RAM, 512GB SSD",

                Price = 2700.99m,

                StockQuantity = 100,
                ModifiedDate = new DateTime(2017, 1, 22, 15, 35, 10)
            };
            ProductVM _productVM2 = new ProductVM()
            {
                Name = _product2.Name,

                Description = _product2.Description,

                Price = _product2.Price,

                StockQuantity = _product2.StockQuantity
            };

            //Act
            var actionResult = await _controllerUnderTest.PostProducts(_productVM2);

            var actionResultDeleted = await _controllerUnderTest.DeleteProducts(8);
            var result = (ActionResult<Product>)actionResultDeleted.Result;
            var badResult = (BadRequestObjectResult)actionResultDeleted.Result;
            var expected = "Not authorised to delete products";
            var actual = badResult.Value.ToString();

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsInstanceOf<ActionResult<Product>>(actionResultDeleted);
            Assert.AreEqual(badResult.StatusCode, 400);
            Assert.IsTrue(actual.Contains(expected));
            Assert.AreEqual(11, _context.Products.Count());
        }



        [Test]
        public async Task _17Test_DeleteProducts_DeleteSuccessfullyReturnsWithCorrectTypeAndShowsInContextCount()
        {
            //Arrange
            Product _product2 = new Product()
            {
                ProductId = 11,
                Name = "Laptop",

                Description = "Full HD, Intel Core i5, 8GB RAM, 512GB SSD",

                Price = 2700.99m,

                StockQuantity = 100,
                ModifiedDate = new DateTime(2017, 1, 22, 15, 35, 10)
            };
            ProductVM _productVM2 = new ProductVM()
            {
                Name = _product2.Name,

                Description = _product2.Description,

                Price = _product2.Price,

                StockQuantity = _product2.StockQuantity
            };
            //Act
            var actionResult = await _controllerUnderTest.PostProducts(_productVM2);
            var actionResultDeleted = await _controllerUnderTest.DeleteProducts(_product2.ProductId);

            //Assert
            Assert.NotNull(actionResultDeleted);
            Assert.IsInstanceOf<ActionResult<Product>>(actionResultDeleted);
            Assert.AreEqual(10, _context.Products.Count());
        }

        [Test]
        public async Task _18Test_DeleteProducts_ReturnsBadObjectResult_WhenTryToDeleteAProductThatHasBeenOrdered()
        {
            //Arrange
            Product _product2 = new Product()
            {
                ProductId = 1,
                Name = "Laptop",

                Description = "Full HD, Intel Core i5, 8GB RAM, 512GB SSD",

                Price = 2700.99m,

                StockQuantity = 100,
                ModifiedDate = new DateTime(2017, 1, 22, 15, 35, 10)
            };
            ProductVM _productVM2 = new ProductVM()
            {
                Name = _product2.Name,

                Description = _product2.Description,

                Price = _product2.Price,

                StockQuantity = _product2.StockQuantity
            };
            //Act
            var actionResult = await _controllerUnderTest.PostProducts(_productVM2);
            var actionResultDeleted = await _controllerUnderTest.DeleteProducts(_product2.ProductId);
            var badResult = (BadRequestObjectResult)actionResultDeleted.Result;
            var expected = "Error, Cannot delete Products that have been invoiced";
            var actual = badResult.Value.ToString();

            //Assert
            Assert.NotNull(actionResult);
            Assert.AreEqual(badResult.StatusCode, 400);
            Assert.IsTrue(actual.Contains(expected));
            Assert.AreEqual(11, _context.Products.Count());
        }


        [Test]
        public async Task _19Test_DeleteProducts_AddMultipleDeleteOne_DeleteSuccessfullyReturnsWithCorrectTypeAndShowsInContextCount()
        {
            //Arrange 
            Product _product2 = new Product()
            {
                ProductId = 11,
                Name = "Laptop",

                Description = "Full HD, Intel Core i5, 8GB RAM, 512GB SSD",

                Price = 2700.99m,

                StockQuantity = 100,
                ModifiedDate = new DateTime(2017, 1, 22, 15, 35, 10)
            };
            ProductVM _productVM2 = new ProductVM()
            {
                Name = _product2.Name,

                Description = _product2.Description,

                Price = _product2.Price,

                StockQuantity = _product2.StockQuantity
            };
            Product _product3 = new Product()
            {
                ProductId = 12,
                Name = "Laptop",

                Description = "Full HD, Intel Core i5, 8GB RAM, 512GB SSD",

                Price = 2700.99m,

                StockQuantity = 100,
                ModifiedDate = new DateTime(2017, 1, 22, 15, 35, 10)
            };
            ProductVM _productVM3 = new ProductVM()
            {
                Name = _product3.Name,

                Description = _product3.Description,

                Price = _product3.Price,

                StockQuantity = _product3.StockQuantity
            };

            //Act
            await _controllerUnderTest.PostProducts(_productVM2);
            await _controllerUnderTest.PostProducts(_productVM3);

            var actionResultDeleted = await _controllerUnderTest.DeleteProducts(_product2.ProductId);
            var actionResultDeleted2 = await _controllerUnderTest.DeleteProducts(_product3.ProductId);

            //Assert
            Assert.NotNull(actionResultDeleted);
            Assert.IsInstanceOf<ActionResult<Product>>(actionResultDeleted);
            Assert.NotNull(actionResultDeleted2);
            Assert.IsInstanceOf<ActionResult<Product>>(actionResultDeleted2);
            Assert.AreEqual(10, _context.Products.Count());
        }
    }
}