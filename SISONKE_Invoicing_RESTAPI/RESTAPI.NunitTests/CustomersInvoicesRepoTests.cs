using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using SISONKE_Invoicing_RESTAPI.Models;
using SISONKE_Invoicing_RESTAPI.Repository;
using SISONKE_Invoicing_RESTAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTAPI.NunitTests
{
    [TestFixture]
    public class CustomersInvoicesRepoTests
    {
        private List<InvoiceRepoVM> _invoiceRepoList;
        private EfUser _user;
        private Invoice _invoice;
        private Discount _discount;
        private Mock<DbSet<EfUser>> _mockUsersDBSet;
        private Mock<DbSet<Discount>> _mockDiscountDBSet;
        private Mock<DbSet<Invoice>> _mockInvoiceDBSet;
        private Mock<SISONKE_Invoicing_System_EFDBContext> _mockContext;

        private CustomersInvoicesRepo _RepoUnderTest;



        [SetUp]
        public void Initialiser()
        {
            _invoiceRepoList = new List<InvoiceRepoVM>();
            _user = new EfUser();
            _discount = new Discount();
            _invoice = new Invoice();
            _mockUsersDBSet = new Mock<DbSet<EfUser>>();
            _mockDiscountDBSet = new Mock<DbSet<Discount>>();
            _mockInvoiceDBSet = new Mock<DbSet<Invoice>>();

            _mockContext = new Mock<SISONKE_Invoicing_System_EFDBContext>();
            _RepoUnderTest = new CustomersInvoicesRepo(_mockContext.Object);
        }


        [TearDown]
        public void Cleanup()
        {
            _mockContext = null;
            _RepoUnderTest = null;

            _invoiceRepoList = null;
            _user = null;
            _discount = null;
            _invoice = null;
            _mockUsersDBSet = null;
            _mockDiscountDBSet = null;
            _mockInvoiceDBSet = null;
        }


        [Test]
        public void _01Test_GetAllInvoiceRepo_ReturnsAListOfInvoiceRepoVM()
        {
            //Arrange 

            var userData = new List<EfUser> { _user, }.AsQueryable();
            _mockUsersDBSet.As<IQueryable<EfUser>>().Setup(m => m.Provider).Returns(userData.Provider);
            _mockUsersDBSet.As<IQueryable<EfUser>>().Setup(m => m.Expression).Returns(userData.Expression);
            _mockUsersDBSet.As<IQueryable<EfUser>>().Setup(m => m.ElementType).Returns(userData.ElementType);
            _mockUsersDBSet.As<IQueryable<EfUser>>().Setup(m => m.GetEnumerator()).Returns(() => userData.GetEnumerator());
            _mockContext.Setup(m => m.EfUsers).Returns(_mockUsersDBSet.Object);


            var discountData = new List<Discount> { _discount, }.AsQueryable();
            _mockDiscountDBSet.As<IQueryable<Discount>>().Setup(m => m.Provider).Returns(discountData.Provider);
            _mockDiscountDBSet.As<IQueryable<Discount>>().Setup(m => m.Expression).Returns(discountData.Expression);
            _mockDiscountDBSet.As<IQueryable<Discount>>().Setup(m => m.ElementType).Returns(discountData.ElementType);
            _mockDiscountDBSet.As<IQueryable<Discount>>().Setup(m => m.GetEnumerator()).Returns(() => discountData.GetEnumerator());
            _mockContext.Setup(m => m.Discounts).Returns(_mockDiscountDBSet.Object);


            var invoiceData = new List<Invoice> { _invoice, }.AsQueryable();
            _mockInvoiceDBSet.As<IQueryable<Invoice>>().Setup(m => m.Provider).Returns(invoiceData.Provider);
            _mockInvoiceDBSet.As<IQueryable<Invoice>>().Setup(m => m.Expression).Returns(invoiceData.Expression);
            _mockInvoiceDBSet.As<IQueryable<Invoice>>().Setup(m => m.ElementType).Returns(invoiceData.ElementType);
            _mockInvoiceDBSet.As<IQueryable<Invoice>>().Setup(m => m.GetEnumerator()).Returns(() => invoiceData.GetEnumerator());
            _mockContext.Setup(m => m.Invoices).Returns(_mockInvoiceDBSet.Object);


            //Act
            var actual = _RepoUnderTest.GetMyInvoices(0);

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Count);
        }

        [Test]
        public void _02Test_GetAllInvoiceRepo_ReturnsAListOfInvoiceRepoVM_WhenPassingSpecificUserId1()
        {
            //Arrange 
            _user = new EfUser()
            {
                EfUserId = 21,
                FirstName = "Test",
                LastName = "Test",
                Email = "Test",
            };
            var userData = new List<EfUser> { _user, }.AsQueryable();
            _mockUsersDBSet.As<IQueryable<EfUser>>().Setup(m => m.Provider).Returns(userData.Provider);
            _mockUsersDBSet.As<IQueryable<EfUser>>().Setup(m => m.Expression).Returns(userData.Expression);
            _mockUsersDBSet.As<IQueryable<EfUser>>().Setup(m => m.ElementType).Returns(userData.ElementType);
            _mockUsersDBSet.As<IQueryable<EfUser>>().Setup(m => m.GetEnumerator()).Returns(() => userData.GetEnumerator());
            _mockContext.Setup(m => m.EfUsers).Returns(_mockUsersDBSet.Object);


            _discount = new Discount(){ DiscountId = 1, Name = "Summer Sale", Rate = 0.10m };
            var discountData = new List<Discount> { _discount, }.AsQueryable();
            _mockDiscountDBSet.As<IQueryable<Discount>>().Setup(m => m.Provider).Returns(discountData.Provider);
            _mockDiscountDBSet.As<IQueryable<Discount>>().Setup(m => m.Expression).Returns(discountData.Expression);
            _mockDiscountDBSet.As<IQueryable<Discount>>().Setup(m => m.ElementType).Returns(discountData.ElementType);
            _mockDiscountDBSet.As<IQueryable<Discount>>().Setup(m => m.GetEnumerator()).Returns(() => discountData.GetEnumerator());
            _mockContext.Setup(m => m.Discounts).Returns(_mockDiscountDBSet.Object);

            _invoice = new Invoice() { InvoiceId = 1, EfUserId = 21, InvoiceDate = new DateTime(2024, 3, 4, 09, 34, 00), DueDate = new DateTime(2024, 3, 29, 09, 34, 00), Subtotal = 834.34, Tax = 12, DiscountId = 1, TotalAmount = 492.96, Status = "Pending" };
            var invoiceData = new List<Invoice> { _invoice, }.AsQueryable();
            _mockInvoiceDBSet.As<IQueryable<Invoice>>().Setup(m => m.Provider).Returns(invoiceData.Provider);
            _mockInvoiceDBSet.As<IQueryable<Invoice>>().Setup(m => m.Expression).Returns(invoiceData.Expression);
            _mockInvoiceDBSet.As<IQueryable<Invoice>>().Setup(m => m.ElementType).Returns(invoiceData.ElementType);
            _mockInvoiceDBSet.As<IQueryable<Invoice>>().Setup(m => m.GetEnumerator()).Returns(() => invoiceData.GetEnumerator());
            _mockContext.Setup(m => m.Invoices).Returns(_mockInvoiceDBSet.Object);


            //Act
            var actual = _RepoUnderTest.GetMyInvoices(21);

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Count);
        }

        [Test]
        public void _03Test_GetAllInvoiceRepo_ReturnsAListOfInvoiceRepoVM_WhenPassingSpecificInvoiceId21()
        {
            //Arrange 
            _user = new EfUser()
            {
                EfUserId = 21,
                FirstName = "Test",
                LastName = "Test",
                Email = "Test",
            };
            var userData = new List<EfUser> { _user, }.AsQueryable();
            _mockUsersDBSet.As<IQueryable<EfUser>>().Setup(m => m.Provider).Returns(userData.Provider);
            _mockUsersDBSet.As<IQueryable<EfUser>>().Setup(m => m.Expression).Returns(userData.Expression);
            _mockUsersDBSet.As<IQueryable<EfUser>>().Setup(m => m.ElementType).Returns(userData.ElementType);
            _mockUsersDBSet.As<IQueryable<EfUser>>().Setup(m => m.GetEnumerator()).Returns(() => userData.GetEnumerator());
            _mockContext.Setup(m => m.EfUsers).Returns(_mockUsersDBSet.Object);

            _discount = new Discount() { DiscountId = 1, Name = "Summer Sale", Rate = 0.10m };
            var discountData = new List<Discount> { _discount, }.AsQueryable();
            _mockDiscountDBSet.As<IQueryable<Discount>>().Setup(m => m.Provider).Returns(discountData.Provider);
            _mockDiscountDBSet.As<IQueryable<Discount>>().Setup(m => m.Expression).Returns(discountData.Expression);
            _mockDiscountDBSet.As<IQueryable<Discount>>().Setup(m => m.ElementType).Returns(discountData.ElementType);
            _mockDiscountDBSet.As<IQueryable<Discount>>().Setup(m => m.GetEnumerator()).Returns(() => discountData.GetEnumerator());
            _mockContext.Setup(m => m.Discounts).Returns(_mockDiscountDBSet.Object);

            _invoice = new Invoice() { InvoiceId = 21, EfUserId = 21, InvoiceDate = new DateTime(2024, 3, 4, 09, 34, 00), DueDate = new DateTime(2024, 3, 29, 09, 34, 00), Subtotal = 834.34, Tax = 12, DiscountId = 1, TotalAmount = 492.96, Status = "Pending" };
            var invoiceData = new List<Invoice> { _invoice, }.AsQueryable();
            _mockInvoiceDBSet.As<IQueryable<Invoice>>().Setup(m => m.Provider).Returns(invoiceData.Provider);
            _mockInvoiceDBSet.As<IQueryable<Invoice>>().Setup(m => m.Expression).Returns(invoiceData.Expression);
            _mockInvoiceDBSet.As<IQueryable<Invoice>>().Setup(m => m.ElementType).Returns(invoiceData.ElementType);
            _mockInvoiceDBSet.As<IQueryable<Invoice>>().Setup(m => m.GetEnumerator()).Returns(() => invoiceData.GetEnumerator());
            _mockContext.Setup(m => m.Invoices).Returns(_mockInvoiceDBSet.Object);


            //Act
            var actual = _RepoUnderTest.GetInvoiceDetails(21);

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(_invoice.InvoiceId, actual.InvoiceId);
        }

        [Test]
        public void _04Test_GetAllInvoiceRepo_ReturnsAListOfInvoiceRepoVM_WhenPassingSpecificUserDiscountId12()
        {
            //Arrange 
            _user = new EfUser()
            {
                EfUserId = 21,
                FirstName = "Test",
                LastName = "Test",
                Email = "Test",
            };
            var userData = new List<EfUser> { _user, }.AsQueryable();
            _mockUsersDBSet.As<IQueryable<EfUser>>().Setup(m => m.Provider).Returns(userData.Provider);
            _mockUsersDBSet.As<IQueryable<EfUser>>().Setup(m => m.Expression).Returns(userData.Expression);
            _mockUsersDBSet.As<IQueryable<EfUser>>().Setup(m => m.ElementType).Returns(userData.ElementType);
            _mockUsersDBSet.As<IQueryable<EfUser>>().Setup(m => m.GetEnumerator()).Returns(() => userData.GetEnumerator());
            _mockContext.Setup(m => m.EfUsers).Returns(_mockUsersDBSet.Object);


            _discount = new Discount() { DiscountId = 12, Name = "Summer Sale", Rate = 0.10m };
            var discountData = new List<Discount> { _discount, }.AsQueryable();
            _mockDiscountDBSet.As<IQueryable<Discount>>().Setup(m => m.Provider).Returns(discountData.Provider);
            _mockDiscountDBSet.As<IQueryable<Discount>>().Setup(m => m.Expression).Returns(discountData.Expression);
            _mockDiscountDBSet.As<IQueryable<Discount>>().Setup(m => m.ElementType).Returns(discountData.ElementType);
            _mockDiscountDBSet.As<IQueryable<Discount>>().Setup(m => m.GetEnumerator()).Returns(() => discountData.GetEnumerator());
            _mockContext.Setup(m => m.Discounts).Returns(_mockDiscountDBSet.Object);

            _invoice = new Invoice() { InvoiceId = 21, EfUserId = 21, InvoiceDate = new DateTime(2024, 3, 4, 09, 34, 00), DueDate = new DateTime(2024, 3, 29, 09, 34, 00), Subtotal = 834.34, Tax = 12, DiscountId = 12, TotalAmount = 492.96, Status = "Pending" };
            var invoiceData = new List<Invoice> { _invoice, }.AsQueryable();
            _mockInvoiceDBSet.As<IQueryable<Invoice>>().Setup(m => m.Provider).Returns(invoiceData.Provider);
            _mockInvoiceDBSet.As<IQueryable<Invoice>>().Setup(m => m.Expression).Returns(invoiceData.Expression);
            _mockInvoiceDBSet.As<IQueryable<Invoice>>().Setup(m => m.ElementType).Returns(invoiceData.ElementType);
            _mockInvoiceDBSet.As<IQueryable<Invoice>>().Setup(m => m.GetEnumerator()).Returns(() => invoiceData.GetEnumerator());
            _mockContext.Setup(m => m.Invoices).Returns(_mockInvoiceDBSet.Object);


            //Act
            var actual = _RepoUnderTest.GetInvoicesByDiscountId(12);

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Count);
        }

        [Test]
        public void _05Test_GetAllInvoiceRepo_ReturnsZeroListOfInvoiceRepoVM_WhenPassingAnInvalidUserId()
        {
            //Arrange 
            _user = new EfUser()
            {
                EfUserId = 1,
                FirstName = "Test",
                LastName = "Test",
                Email = "Test",
            };
            var userData = new List<EfUser> { _user, }.AsQueryable();
            _mockUsersDBSet.As<IQueryable<EfUser>>().Setup(m => m.Provider).Returns(userData.Provider);
            _mockUsersDBSet.As<IQueryable<EfUser>>().Setup(m => m.Expression).Returns(userData.Expression);
            _mockUsersDBSet.As<IQueryable<EfUser>>().Setup(m => m.ElementType).Returns(userData.ElementType);
            _mockUsersDBSet.As<IQueryable<EfUser>>().Setup(m => m.GetEnumerator()).Returns(() => userData.GetEnumerator());
            _mockContext.Setup(m => m.EfUsers).Returns(_mockUsersDBSet.Object);


            _discount = new Discount() { DiscountId = 12, Name = "Summer Sale", Rate = 0.10m };
            var discountData = new List<Discount> { _discount, }.AsQueryable();
            _mockDiscountDBSet.As<IQueryable<Discount>>().Setup(m => m.Provider).Returns(discountData.Provider);
            _mockDiscountDBSet.As<IQueryable<Discount>>().Setup(m => m.Expression).Returns(discountData.Expression);
            _mockDiscountDBSet.As<IQueryable<Discount>>().Setup(m => m.ElementType).Returns(discountData.ElementType);
            _mockDiscountDBSet.As<IQueryable<Discount>>().Setup(m => m.GetEnumerator()).Returns(() => discountData.GetEnumerator());
            _mockContext.Setup(m => m.Discounts).Returns(_mockDiscountDBSet.Object);

            _invoice = new Invoice() { InvoiceId = 21, EfUserId = 21, InvoiceDate = new DateTime(2024, 3, 4, 09, 34, 00), DueDate = new DateTime(2024, 3, 29, 09, 34, 00), Subtotal = 834.34, Tax = 12, DiscountId = 12, TotalAmount = 492.96, Status = "Pending" };
            var invoiceData = new List<Invoice> { _invoice, }.AsQueryable();
            _mockInvoiceDBSet.As<IQueryable<Invoice>>().Setup(m => m.Provider).Returns(invoiceData.Provider);
            _mockInvoiceDBSet.As<IQueryable<Invoice>>().Setup(m => m.Expression).Returns(invoiceData.Expression);
            _mockInvoiceDBSet.As<IQueryable<Invoice>>().Setup(m => m.ElementType).Returns(invoiceData.ElementType);
            _mockInvoiceDBSet.As<IQueryable<Invoice>>().Setup(m => m.GetEnumerator()).Returns(() => invoiceData.GetEnumerator());
            _mockContext.Setup(m => m.Invoices).Returns(_mockInvoiceDBSet.Object);


            //Act
            var actual = _RepoUnderTest.GetMyInvoices(22);

            //Assert
            Assert.AreEqual(0, actual.Count);
        }

        [Test]
        public void _06Test_GetAllInvoiceRepo_ReturnsZeroListOfInvoiceRepoVM_WhenPassingAnInvalidUserInvoiceId()
        {
            //Arrange 
            _user = new EfUser()
            {
                EfUserId = 21,
                FirstName = "Test",
                LastName = "Test",
                Email = "Test",
            };
            var userData = new List<EfUser> { _user, }.AsQueryable();
            _mockUsersDBSet.As<IQueryable<EfUser>>().Setup(m => m.Provider).Returns(userData.Provider);
            _mockUsersDBSet.As<IQueryable<EfUser>>().Setup(m => m.Expression).Returns(userData.Expression);
            _mockUsersDBSet.As<IQueryable<EfUser>>().Setup(m => m.ElementType).Returns(userData.ElementType);
            _mockUsersDBSet.As<IQueryable<EfUser>>().Setup(m => m.GetEnumerator()).Returns(() => userData.GetEnumerator());
            _mockContext.Setup(m => m.EfUsers).Returns(_mockUsersDBSet.Object);


            _discount = new Discount() { DiscountId = 12, Name = "Summer Sale", Rate = 0.10m };
            var discountData = new List<Discount> { _discount, }.AsQueryable();
            _mockDiscountDBSet.As<IQueryable<Discount>>().Setup(m => m.Provider).Returns(discountData.Provider);
            _mockDiscountDBSet.As<IQueryable<Discount>>().Setup(m => m.Expression).Returns(discountData.Expression);
            _mockDiscountDBSet.As<IQueryable<Discount>>().Setup(m => m.ElementType).Returns(discountData.ElementType);
            _mockDiscountDBSet.As<IQueryable<Discount>>().Setup(m => m.GetEnumerator()).Returns(() => discountData.GetEnumerator());
            _mockContext.Setup(m => m.Discounts).Returns(_mockDiscountDBSet.Object);

            _invoice = new Invoice() { InvoiceId = 21, EfUserId = 21, InvoiceDate = new DateTime(2024, 3, 4, 09, 34, 00), DueDate = new DateTime(2024, 3, 29, 09, 34, 00), Subtotal = 834.34, Tax = 12, DiscountId = 12, TotalAmount = 492.96, Status = "Pending" };
            var invoiceData = new List<Invoice> { _invoice, }.AsQueryable();
            _mockInvoiceDBSet.As<IQueryable<Invoice>>().Setup(m => m.Provider).Returns(invoiceData.Provider);
            _mockInvoiceDBSet.As<IQueryable<Invoice>>().Setup(m => m.Expression).Returns(invoiceData.Expression);
            _mockInvoiceDBSet.As<IQueryable<Invoice>>().Setup(m => m.ElementType).Returns(invoiceData.ElementType);
            _mockInvoiceDBSet.As<IQueryable<Invoice>>().Setup(m => m.GetEnumerator()).Returns(() => invoiceData.GetEnumerator());
            _mockContext.Setup(m => m.Invoices).Returns(_mockInvoiceDBSet.Object);


            //Act
            var actual = _RepoUnderTest.GetInvoiceDetails(20);

            //Assert
            Assert.Null(actual.IdentityUsername);
        }
    }
}
