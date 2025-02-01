using SISONKE_Invoicing_RESTAPI.AuthModels;
using SISONKE_Invoicing_RESTAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace RESTAPI.NunitTests
{
	public class InMemoryContext
	{
		public static Object GeneratedDB()
		{

			var _contextOptions = new DbContextOptionsBuilder<SISONKE_Invoicing_System_EFDBContext>()
				.UseInMemoryDatabase("ControllerTest")
				.ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
				.Options;
			var context = new SISONKE_Invoicing_System_EFDBContext(_contextOptions);

			EFDBSeedData(context);

			return context;
		}

		public static void EFDBSeedData(SISONKE_Invoicing_System_EFDBContext context)
		{
			context.EfUsers.Add(new EfUser() { EfUserId = 1, FirstName = "Zandile", LastName = "Zimela", Email = "Zzimela@gmail.com", IdentityUsername = "ZzimelaAdmin", Address = "18 Jack avenue, 2001", PhoneNumber = "0743244345", Role = "Administrator" });
			context.EfUsers.Add(new EfUser() { EfUserId = 2, FirstName = "Andile", LastName = "Zuma", Email = "Azuma22@gmail.com", IdentityUsername = "Azuma22Admin", Address = "2003 Field street, 4001", PhoneNumber = "0844544278", Role = "Administrator" });
			context.EfUsers.Add(new EfUser() { EfUserId = 3, FirstName = "Jack", LastName = "Harrison", Email = "Harrionj01@gmail.com", IdentityUsername = "Harrison01Admin", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Administrator" });
			context.EfUsers.Add(new EfUser() { EfUserId = 4, FirstName = "Hannah", LastName = "Sancho", Email = "Sanchoh@gmail.com", IdentityUsername = "SanchohAdmin", Address = "15 Zimbali avenue, 4501", PhoneNumber = "0673004545", Role = "Administrator" });
			context.EfUsers.Add(new EfUser() { EfUserId = 5, FirstName = "Zack", LastName = "Wake", Email = "Wakez007@gmail.com", IdentityUsername = "Wakez007Admin", Address = "28 Sandton avenue, 2001", PhoneNumber = "0745667386", Role = "Administrator" });
			context.EfUsers.Add(new EfUser() { EfUserId = 6, FirstName = "Bruno", LastName = "Sterling", Email = "Bruno22@gmail.com", IdentityUsername = "Brunos22Admin", Address = "20 King Edwards avenue, 3201", PhoneNumber = "0843778347", Role = "Administrator" });
			context.EfUsers.Add(new EfUser() { EfUserId = 7, FirstName = "Luis", LastName = "Diaz", Email = "Luisd7@gmail.com", IdentityUsername = "Luis7Admin", Address = "19 Jack avenue, 2001", PhoneNumber = "0742687829", Role = "Administrator" });
			context.EfUsers.Add(new EfUser() { EfUserId = 8, FirstName = "Cristiano", LastName = "Walker", Email = "Walkerc07@gmail.com", IdentityUsername = "Walkerc07Admin", Address = "15 Harrison avenue, 3001", PhoneNumber = "0743264748", Role = "Administrator" });
			context.EfUsers.Add(new EfUser() { EfUserId = 9, FirstName = "Phil", LastName = "Foden", Email = "Fodenp47@gmail.com", IdentityUsername = "Fodenp47Admin", Address = "100 Everton street, 6001", PhoneNumber = "0746277885", Role = "Administrator" });
			context.EfUsers.Add(new EfUser() { EfUserId = 10, FirstName = "Kylian", LastName = "Carrick", Email = "Kcarrick@gmail.com", IdentityUsername = "KcarrickAdmin", Address = "20 Jack avenue, 2001", PhoneNumber = "0675249849", Role = "Administrator" });
			context.EfUsers.Add(new EfUser() { EfUserId = 11, FirstName = "Jordan", LastName = "Khalid", Email = "Jkhalid@gmail.com", IdentityUsername = "Jkhalid", Address = "25 Zuma avenue, 2001", PhoneNumber = "074396748", Role = "Employee" });
			context.EfUsers.Add(new EfUser() { EfUserId = 12, FirstName = "Elga", LastName = "Cantopher", Email = "ecantopher0@biblegateway.com", IdentityUsername = "ecantopher0", Address = "8 Merchant Point", PhoneNumber = "689-657-2674", Role = "Employee" });
			context.EfUsers.Add(new EfUser() { EfUserId = 13, FirstName = "Gasparo", LastName = "Turpin", Email = "gturpin1@si.edu", IdentityUsername = "gturpin1", Address = "24 Amoth Street", PhoneNumber = "196-950-8155", Role = "Employee" });
			context.EfUsers.Add(new EfUser() { EfUserId = 14, FirstName = "Harlin", LastName = "Venny", Email = "hvenny2@hao123.com", IdentityUsername = "hvenny2", Address = "290571 Sunbrook Alley", PhoneNumber = "196-950-8155", Role = "Employee" });
			context.EfUsers.Add(new EfUser() { EfUserId = 15, FirstName = "Roshelle", LastName = "Maiden", Email = "rmaiden3@vimeo.com", IdentityUsername = "rmaiden3", Address = "207 Delladonna Drive", PhoneNumber = "310-145-9782", Role = "Employee" });
			context.EfUsers.Add(new EfUser() { EfUserId = 16, FirstName = "Kesley", LastName = "Abatelli", Email = "kabatelli4@canalblog.com", IdentityUsername = "kabatelli4", Address = "14 Mayer Junction", PhoneNumber = "597-725-9204", Role = "Employee" });
			context.EfUsers.Add(new EfUser() { EfUserId = 17, FirstName = "Merridie", LastName = "Bucky", Email = "mbucky5@house.gov", IdentityUsername = "mbucky5", Address = "06 Paget Crossing", PhoneNumber = "597-725-9204", Role = "Employee" });
			context.EfUsers.Add(new EfUser() { EfUserId = 18, FirstName = "Lawry", LastName = "Purdom", Email = "lpurdom6@kickstarter.com", IdentityUsername = "lpurdom6", Address = "0 Prairieview Point", PhoneNumber = "866-316-2624", Role = "Employee" });
			context.EfUsers.Add(new EfUser() { EfUserId = 19, FirstName = "Bekki", LastName = "Chavrin", Email = "bchavrin7@gmail.com", IdentityUsername = "bchavrin7", Address = "35347 Eliot Place", PhoneNumber = "405-920-9719", Role = "Employee" });
			context.EfUsers.Add(new EfUser() { EfUserId = 20, FirstName = "Orazio", LastName = "Littley", Email = "olittley8@gmail.com", IdentityUsername = "olittley8", Address = "5268 Division Parkway", PhoneNumber = "268-407-0285", Role = "Employee" });
			context.EfUsers.Add(new EfUser() { EfUserId = 21, FirstName = "Zack", LastName = "Efron", Email = "Efronz@gmail.com", IdentityUsername = "Efronz", Address = "15 Zuma avenue, 2001", PhoneNumber = "074396748", Role = "Customer" });
			context.EfUsers.Add(new EfUser() { EfUserId = 22, FirstName = "Katharyn", LastName = "Maybey", Email = "kmaybey0@gmail.com", IdentityUsername = "kmaybey0", Address = "22310 Schmedeman Parkway", PhoneNumber = "368-346-0788", Role = "Customer" });
			context.EfUsers.Add(new EfUser() { EfUserId = 23, FirstName = "Lanni", LastName = "Measom", Email = "lmeasom1@networkadvertising.org", IdentityUsername = "lmeasom1", Address = "6 Kim Place", PhoneNumber = "991-219-0321", Role = "Customer" });
			context.EfUsers.Add(new EfUser() { EfUserId = 24, FirstName = "Laying", LastName = "Woolaston", Email = "mwoolaston2@soup.io", IdentityUsername = "mwoolaston2", Address = "8 Saint Paul Pass", PhoneNumber = "404-725-6084", Role = "Customer" });
			context.EfUsers.Add(new EfUser() { EfUserId = 25, FirstName = "Linda", LastName = "Hamby", Email = "lhamby3@a8.net", IdentityUsername = "lhamby3", Address = "09060 Hanover Trail", PhoneNumber = "879-880-7372", Role = "Customer" });
			context.EfUsers.Add(new EfUser() { EfUserId = 26, FirstName = "Ernesto", LastName = "Bartaloni", Email = "ebartaloni4@intel.com", IdentityUsername = "ebartaloni4", Address = "0 Garrison Crossing", PhoneNumber = "799-505-6110", Role = "Customer" });
			context.EfUsers.Add(new EfUser() { EfUserId = 27, FirstName = "Tarra", LastName = "MacGinney", Email = "tmacginney5@goodreads.com", IdentityUsername = "tmacginney5", Address = "36383 Hallows Place", PhoneNumber = "122-564-5165", Role = "Customer" });
			context.EfUsers.Add(new EfUser() { EfUserId = 28, FirstName = "Cacilia", LastName = "Rhodef", Email = "crhodef6@shutterfly.com", IdentityUsername = "crhodef6", Address = "0148 Little Fleur Hill", PhoneNumber = "640-914-6862", Role = "Customer" });
			context.EfUsers.Add(new EfUser() { EfUserId = 29, FirstName = "Kerr", LastName = "Ivashkin", Email = "kivashkin7@salon.com", IdentityUsername = "kivashkin7", Address = "3113 Clyde Gallagher Crossing", PhoneNumber = "733-331-0434", Role = "Customer" });
			context.EfUsers.Add(new EfUser() { EfUserId = 30, FirstName = "Emelda", LastName = "Guinnane", Email = "eguinnane8@discovery.com", IdentityUsername = "eguinnane8", Address = "656 Menomonie Hill", PhoneNumber = "656-188-2034", Role = "Customer" });

			context.Discounts.Add(new Discount() { DiscountId = 1, Name = "Summer Sale", Rate = 0.10m });
			context.Discounts.Add(new Discount() { DiscountId = 2, Name = "Black Friday", Rate = 0.20m });
			context.Discounts.Add(new Discount() { DiscountId = 3, Name = "Cyber Monday", Rate = 0.15m });
			context.Discounts.Add(new Discount() { DiscountId = 4, Name = "New Year Sale", Rate = 0.25m });
			context.Discounts.Add(new Discount() { DiscountId = 5, Name = "Clearance", Rate = 0.30m });
			context.Discounts.Add(new Discount() { DiscountId = 6, Name = "Valentine's Day", Rate = 0.12m });
			context.Discounts.Add(new Discount() { DiscountId = 7, Name = "Spring Sale", Rate = 0.18m });
			context.Discounts.Add(new Discount() { DiscountId = 8, Name = "Easter Sale", Rate = 0.22m });
			context.Discounts.Add(new Discount() { DiscountId = 9, Name = "Back to School", Rate = 0.15m });
			context.Discounts.Add(new Discount() { DiscountId = 10, Name = "Holiday Sale", Rate = 0.20m });


			context.Invoices.Add(new Invoice() { InvoiceId = 1, EfUserId = 21, InvoiceDate = new DateTime(2024, 3, 4, 09, 34, 00), DueDate = new DateTime(2024, 3, 29, 09, 34, 00), Subtotal = 834.34, Tax = 12, DiscountId = 1, TotalAmount = 492.96, Status = "Pending" });
			context.Invoices.Add(new Invoice() { InvoiceId = 2, EfUserId = 22, InvoiceDate = new DateTime(2024, 3, 5, 09, 34, 00), DueDate = new DateTime(2024, 3, 25, 09, 34, 00), Subtotal = 511.64, Tax = 10, DiscountId = 2, TotalAmount = 635.8, Status = "Pending" });
			context.Invoices.Add(new Invoice() { InvoiceId = 3, EfUserId = 23, InvoiceDate = new DateTime(2024, 3, 3, 09, 34, 00), DueDate = new DateTime(2024, 3, 5, 2, 09, 34, 00), Subtotal = 591.95, Tax = 3, DiscountId = 3, TotalAmount = 728.52, Status = "Pending" });
			context.Invoices.Add(new Invoice() { InvoiceId = 4, EfUserId = 24, InvoiceDate = new DateTime(2024, 3, 3, 03, 34, 00), DueDate = new DateTime(2024, 3, 25, 09, 34, 00), Subtotal = 424.03, Tax = 4, DiscountId = 4, TotalAmount = 465.82, Status = "Paid" });
			context.Invoices.Add(new Invoice() { InvoiceId = 5, EfUserId = 25, InvoiceDate = new DateTime(2024, 3, 3, 06, 34, 00), DueDate = new DateTime(2024, 3, 5, 09, 34, 00), Subtotal = 267.98, Tax = 9, DiscountId = 5, TotalAmount = 850.92, Status = "Paid" });
			context.Invoices.Add(new Invoice() { InvoiceId = 6, EfUserId = 26, InvoiceDate = new DateTime(2024, 3, 3, 07, 34, 00), DueDate = new DateTime(2024, 3, 25, 09, 34, 00), Subtotal = 997.89, Tax = 8, DiscountId = 6, TotalAmount = 247.52, Status = "Paid" });
			context.Invoices.Add(new Invoice() { InvoiceId = 7, EfUserId = 27, InvoiceDate = new DateTime(2024, 3, 3, 08, 34, 00), DueDate = new DateTime(2024, 3, 25, 09, 34, 00), Subtotal = 543.86, Tax = 8, DiscountId = 7, TotalAmount = 555.31, Status = "Paid" });
			context.Invoices.Add(new Invoice() { InvoiceId = 8, EfUserId = 28, InvoiceDate = new DateTime(2024, 3, 3, 01, 34, 00), DueDate = new DateTime(2024, 3, 25, 09, 34, 00), Subtotal = 449.51, Tax = 12, DiscountId = 8, TotalAmount = 163.18, Status = "Overdue" });
			context.Invoices.Add(new Invoice() { InvoiceId = 9, EfUserId = 29, InvoiceDate = new DateTime(2024, 3, 3, 02, 34, 00), DueDate = new DateTime(2024, 3, 25, 09, 34, 00), Subtotal = 472.01, Tax = 9, DiscountId = 9, TotalAmount = 107.9, Status = "Overdue" });
			context.Invoices.Add(new Invoice(){ InvoiceId = 10, EfUserId = 30, InvoiceDate = new DateTime(2024, 3, 5, 09, 34, 00), DueDate = new DateTime(2024, 3, 25, 09, 34, 00),	Subtotal = 410.66, Tax = 14, DiscountId = 10, TotalAmount = 322.18, Status = "Overdue" });



			context.Products.Add(new Product() { ProductId = 1, Name = "Laptop", StockQuantity = 50, Price = 899.99M, Description = "Full HD, Intel Core i5, 8GB RAM, 512GB SSD", ModifiedDate = new DateTime(2018, 11, 12, 12, 45, 00) });
			context.Products.Add(new Product() { ProductId = 2, Name = "Smartphone", StockQuantity = 100, Price = 899.99M, Description = "6.5 AMOLED Display, Snapdragon 888, 128GB, 5G", ModifiedDate = new DateTime(2017, 1, 22, 15, 35, 10) });
			context.Products.Add(new Product() { ProductId = 3, Name = "Smartwatch", StockQuantity = 80, Price = 899.99M, Description = "Waterproof Fitness Tracker, Heart Rate Monitor", ModifiedDate = new DateTime(2019, 10, 12, 12, 45, 00) });
			context.Products.Add(new Product() { ProductId = 4, Name = "Headphones", StockQuantity = 150, Price = 899.99M, Description = "Wireless Over-Ear Headphones, Active Noise Cancellation", ModifiedDate = new DateTime(2018, 1, 12, 12, 45, 00) });
			context.Products.Add(new Product() { ProductId = 5, Name = "Tablet", StockQuantity = 70, Price = 899.99M, Description = "Retina Display, A13 Bionic Chip, 256GB", ModifiedDate = new DateTime(2018, 2, 12, 21, 47, 50) });
			context.Products.Add(new Product() { ProductId = 6, Name = "Computer", StockQuantity = 150, Price = 1099.99M, Description = "QHD Monitor, Intel Core i7, 16GB RAM, 1TB SSD", ModifiedDate = new DateTime(2019, 9, 13, 8, 5, 40) });
			context.Products.Add(new Product() { ProductId = 7, Name = "Gaming Console", StockQuantity = 30, Price = 499.99M, Description = "4K HDR, 1TB Storage, DualSense Wireless Controller", ModifiedDate = new DateTime(2020, 1, 2, 13, 35, 00) });
			context.Products.Add(new Product() { ProductId = 8, Name = "Wireless Router", StockQuantity = 90, Price = 79.99M, Description = "Dual-Band Gigabit Wi-Fi Router, Parental Controls", ModifiedDate = new DateTime(2021, 11, 12, 12, 45, 00) });
			context.Products.Add(new Product() { ProductId = 9, Name = "External Hard Drive", StockQuantity = 520, Price = 129.99M, Description = "2TB Portable USB 3.0 External HDD, Backup Solution", ModifiedDate = new DateTime(2022, 11, 10, 10, 40, 00) });
			context.Products.Add(new Product() { ProductId = 10, Name = "Wireless Mouse", StockQuantity = 200, Price = 29.99M, Description = "Ergonomic Design, Silent Click, USB Receiver", ModifiedDate = new DateTime(2019, 3, 22, 16, 5, 20) });

			context.Payments.Add(new Payment() { PaymentId = 1, InvoiceId = 1, PaymentDate = new DateTime(2024, 01, 04, 11, 13, 12), Amount = 6233.92, PaymentMethod = "PayPal" });
			context.Payments.Add(new Payment() { PaymentId = 2, InvoiceId = 2, PaymentDate = new DateTime(2024, 01, 05, 12, 14, 15), Amount = 5678.71, PaymentMethod = "debit card" });
			context.Payments.Add(new Payment() { PaymentId = 3, InvoiceId = 3, PaymentDate = new DateTime(2024, 05, 24, 10, 15, 12), Amount = 487.14, PaymentMethod = "PayPal" });
			context.Payments.Add(new Payment() { PaymentId = 4, InvoiceId = 4, PaymentDate = new DateTime(2024, 07, 14, 09, 13, 52), Amount = 7333.71, PaymentMethod = "credit card" });
			context.Payments.Add(new Payment() { PaymentId = 5, InvoiceId = 5, PaymentDate = new DateTime(2023, 11, 12, 10, 25, 45), Amount = 9807.81, PaymentMethod = "PayPal" });
			context.Payments.Add(new Payment() { PaymentId = 6, InvoiceId = 6, PaymentDate = new DateTime(2024, 01, 11, 05, 25, 30), Amount = 8308.03, PaymentMethod = "debit card" });
			context.Payments.Add(new Payment() { PaymentId = 7, InvoiceId = 7, PaymentDate = new DateTime(2024, 01, 13, 13, 33, 10), Amount = 7112.68, PaymentMethod = "credit card" });
			context.Payments.Add(new Payment() { PaymentId = 8, InvoiceId = 8, PaymentDate = new DateTime(2024, 02, 10, 12, 38, 45), Amount = 7739.86, PaymentMethod = "PayPal" });
			context.Payments.Add(new Payment() { PaymentId = 9, InvoiceId = 9, PaymentDate = new DateTime(2024, 04, 05, 10, 40, 46), Amount = 9815.18, PaymentMethod = "PayPal" });
			context.Payments.Add(new Payment() { PaymentId = 10, InvoiceId = 10, PaymentDate = new DateTime(2024, 06, 10, 11, 55, 10), Amount = 5878.71, PaymentMethod = "credit card" });


			context.InvoiceItems.Add(new InvoiceItem() { InvoiceItemId = 1, InvoiceId = 1, ProductId = 1, Quantity = 2, UnitPrice = 50.00m, TotalPrice = 100.00m });
			context.InvoiceItems.Add(new InvoiceItem() { InvoiceItemId = 2, InvoiceId = 1, ProductId = 2, Quantity = 1, UnitPrice = 75.00m, TotalPrice = 75.00m });
			context.InvoiceItems.Add(new InvoiceItem() { InvoiceItemId = 3, InvoiceId = 2, ProductId = 1, Quantity = 1, UnitPrice = 50.00m, TotalPrice = 50.00m });
			context.InvoiceItems.Add(new InvoiceItem() { InvoiceItemId = 4, InvoiceId = 3, ProductId = 3, Quantity = 3, UnitPrice = 20.00m, TotalPrice = 60.00m });
			context.InvoiceItems.Add(new InvoiceItem() { InvoiceItemId = 5, InvoiceId = 2, ProductId = 4, Quantity = 2, UnitPrice = 30.00m, TotalPrice = 60.00m });
			context.InvoiceItems.Add(new InvoiceItem() { InvoiceItemId = 6, InvoiceId = 4, ProductId = 5, Quantity = 4, UnitPrice = 25.00m, TotalPrice = 100.00m });
			context.InvoiceItems.Add(new InvoiceItem() { InvoiceItemId = 7, InvoiceId = 5, ProductId = 6, Quantity = 1, UnitPrice = 150.00m, TotalPrice = 150.00m });
			context.InvoiceItems.Add(new InvoiceItem() { InvoiceItemId = 8, InvoiceId = 5, ProductId = 2, Quantity = 2, UnitPrice = 75.00m, TotalPrice = 150.00m });
			context.InvoiceItems.Add(new InvoiceItem() { InvoiceItemId = 9, InvoiceId = 6, ProductId = 3, Quantity = 5, UnitPrice = 20.00m, TotalPrice = 100.00m });
			context.InvoiceItems.Add(new InvoiceItem() { InvoiceItemId = 10, InvoiceId = 6, ProductId = 1, Quantity = 3, UnitPrice = 50.00m, TotalPrice = 150.00m });

			context.Notes.Add(new Note() { NoteId = 1, InvoiceId = 1, InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 1 is currently on hold pending approval.", CreatedDate = new DateTime(2024, 3, 23, 9, 34, 0) });
			context.Notes.Add(new Note() { NoteId = 2, InvoiceId = 2, InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 2 is currently on hold pending approval. ", CreatedDate = new DateTime(2024, 4, 23, 0, 34, 0) });
			context.Notes.Add(new Note() { NoteId = 3, InvoiceId = 3, InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 3 is currently on hold pending approval. ", CreatedDate = new DateTime(2024, 4, 23, 8, 34, 0) });
			context.Notes.Add(new Note() { NoteId = 4, InvoiceId = 4, InvoiceNotes = "We wanted to express our gratitude for your recent payment of invoice 4. Your promptness is greatly appreciated.", CreatedDate = new DateTime(2024, 4, 23, 10, 34, 0) });
			context.Notes.Add(new Note() { NoteId = 5, InvoiceId = 5, InvoiceNotes = "We wanted to express our gratitude for your recent payment of invoice 5. Your promptness is greatly appreciated.", CreatedDate = new DateTime(2024, 5, 13, 12, 34, 0) });
			context.Notes.Add(new Note() { NoteId = 6, InvoiceId = 6, InvoiceNotes = "We wanted to express our gratitude for your recent payment of invoice 6. Your promptness is greatly appreciated.", CreatedDate = new DateTime(2024, 3, 13, 13, 34, 0) });
			context.Notes.Add(new Note() { NoteId = 7, InvoiceId = 7, InvoiceNotes = "We wanted to express our gratitude for your recent payment of invoice 7. Your promptness is greatly appreciated.", CreatedDate = new DateTime(2024, 4, 23, 12, 34, 0) });
			context.Notes.Add(new Note() { NoteId = 8, InvoiceId = 8, InvoiceNotes = "We hope this message finds you well. We're reaching out to remind you that invoice 8 is now overdue.", CreatedDate = new DateTime(2024, 4, 3, 12, 24, 0) });
			context.Notes.Add(new Note() { NoteId = 9, InvoiceId = 9, InvoiceNotes = "We hope this message finds you well. We're reaching out to remind you that invoice 9 is now overdue.", CreatedDate = new DateTime(2024, 4, 1, 11, 14, 0) });
			context.Notes.Add(new Note() { NoteId = 10, InvoiceId = 10, InvoiceNotes = "We hope this message finds you well. We're reaching out to remind you that invoice", CreatedDate = new DateTime(2024, 4, 1, 11, 54, 0) });

			context.SaveChanges();
		}

		public static Object GeneratedAuthDB()
		{

			var _contextOptions = new DbContextOptionsBuilder<AuthenticationContext>()
				.UseInMemoryDatabase("ControllerTest")
				.ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
				.Options;

			var context = new AuthenticationContext(_contextOptions);

			// Seed data
			SeedData(context);

			return context;
		}

		private static void SeedData(AuthenticationContext context)
		{
			// Add users
			var roleId_1 = Guid.NewGuid().ToString();
			var userId_1 = Guid.NewGuid().ToString();

			var roleId_2 = Guid.NewGuid().ToString();
			var userId_2 = Guid.NewGuid().ToString();

			var roleId_3 = Guid.NewGuid().ToString();
			var userId_3 = Guid.NewGuid().ToString();
			context.Roles.Add(
			   new IdentityRole { Id = roleId_1, Name = "Administrator", NormalizedName = "ADMINISTRATOR" });
			context.Roles.Add(
			 new IdentityRole { Id = roleId_2, Name = "Employee", NormalizedName = "SELLER" });
			context.Roles.Add(
			new IdentityRole { Id = roleId_3, Name = "Customer", NormalizedName = "CUSTOMER" }
			   );




			//create Administrator user
			var AdminUser = new ApplicationUser
			{
				Id = userId_1,
				Email = "Zzimela@gmail.com",
				EmailConfirmed = true,
				Address = "18 Jack avenue, 2001",
				PhoneNumber = "0743244345",
				FirstName = "Zandile",
				LastName = "Zimela",
				UserName = "ZzimelaAdmin",
				NormalizedUserName = "ZZIMELAADMIN"
			};

			//set user password
			PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
			AdminUser.PasswordHash = ph.HashPassword(AdminUser, "zimelaZ@1234");

			//seed user
			context.ApplicationUsers.Add(AdminUser);

			//set user role to Administrator
			context.UserRoles.Add(new IdentityUserRole<string>
			{
				RoleId = roleId_1,
				UserId = userId_1
			});

			//create Employee user
			var EmployeeUser = new ApplicationUser
			{
				Id = userId_2,
				Email = "Jkhalid@gmail.com",
				EmailConfirmed = true,
				Address = "25 Zuma avenue, 2001",
				PhoneNumber = "074396748",
				FirstName = "Jordan",
				LastName = "Khalid",
				UserName = "Jkhalid",
				NormalizedUserName = "JKHALID"
			};

			//set user password
			PasswordHasher<ApplicationUser> cstph = new PasswordHasher<ApplicationUser>();
			EmployeeUser.PasswordHash = cstph.HashPassword(EmployeeUser, "Khalidj@1234");

			//seed user
			context.ApplicationUsers.Add(EmployeeUser);

			//set user role to admin
			context.UserRoles.Add(new IdentityUserRole<string>
			{
				RoleId = roleId_2,
				UserId = userId_2
			});

			//create Customer user
			var CustomerUser = new ApplicationUser
			{
				Id = userId_3,
				Email = "Efronz@gmail.com",
				EmailConfirmed = true,
				Address = "15 Zuma avenue, 2001",
				PhoneNumber = "074396748",
				FirstName = "Zack",
				LastName = "Efron",
				UserName = "Efronz",
				NormalizedUserName = "EFRONZ"
			};

			//set user password
			PasswordHasher<ApplicationUser> lgcph = new PasswordHasher<ApplicationUser>();
			CustomerUser.PasswordHash = lgcph.HashPassword(CustomerUser, "Efron@123456");

			//seed user
			context.ApplicationUsers.Add(CustomerUser);

			//set user role to admin
			context.UserRoles.Add(new IdentityUserRole<string>
			{
				RoleId = roleId_3,
				UserId = userId_3
			});

			context.SaveChanges();
		}
	}
}
