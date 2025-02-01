namespace Pocos.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Pocos.Model1>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Pocos.Model1 context)
        {
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method
			//  to avoid creating duplicate seed data.
			context.EFUsers.AddOrUpdate(c => c.EFUserID,
				new EFUser() { EFUserID = 1, FirstName = "Zandile", LastName = "Zimela", Email = "Zzimela@gmail.com", IdentityUserName = "ZzimelaAdmin", Address = "18 Jack avenue, 2001", PhoneNumber = "0743244345", Role = "Administrator" },
				new EFUser() { EFUserID = 2, FirstName = "Andile", LastName = "Zuma", Email = "Azuma22@gmail.com", IdentityUserName = "Azuma22Admin", Address = "2003 Field street, 4001", PhoneNumber = "0844544278", Role = "Administrator" },
				new EFUser() { EFUserID = 3, FirstName = "Jack", LastName = "Harrison", Email = "Harrionj01@gmail.com", IdentityUserName = "Harrison01Admin", Address = "230 West street, 4001", PhoneNumber = "0675647385", Role = "Administrator" },
				new EFUser() { EFUserID = 4, FirstName = "Hannah", LastName = "Sancho", Email = "Sanchoh@gmail.com", IdentityUserName = "SanchohAdmin", Address = "15 Zimbali avenue, 4501", PhoneNumber = "0673004545", Role = "Administrator" },
				new EFUser() { EFUserID = 5, FirstName = "Zack", LastName = "Wake", Email = "Wakez007@gmail.com", IdentityUserName = "Wakez007Admin", Address = "28 Sandton avenue, 2001", PhoneNumber = "0745667386", Role = "Administrator" },
				new EFUser() { EFUserID = 6, FirstName = "Bruno", LastName = "Sterling", Email = "Bruno22@gmail.com", IdentityUserName = "Brunos22Admin", Address = "20 King Edwards avenue, 3201", PhoneNumber = "0843778347", Role = "Administrator" },
				new EFUser() { EFUserID = 7, FirstName = "Luis", LastName = "Diaz", Email = "Luisd7@gmail.com", IdentityUserName = "Luis7Admin", Address = "19 Jack avenue, 2001", PhoneNumber = "0742687829", Role = "Administrator" },
				new EFUser() { EFUserID = 8, FirstName = "Cristiano", LastName = "Walker", Email = "Walkerc07@gmail.com", IdentityUserName = "Walkerc07Admin", Address = "15 Harrison avenue, 3001", PhoneNumber = "0743264748", Role = "Administrator" },
				new EFUser() { EFUserID = 9, FirstName = "Phil", LastName = "Foden", Email = "Fodenp47@gmail.com", IdentityUserName = "Fodenp47Admin", Address = "100 Everton street, 6001", PhoneNumber = "0746277885", Role = "Administrator" },
				new EFUser() { EFUserID = 10, FirstName = "Kylian", LastName = "Carrick", Email = "Kcarrick@gmail.com", IdentityUserName = "KcarrickAdmin", Address = "20 Jack avenue, 2001", PhoneNumber = "0675249849", Role = "Administrator" },
				new EFUser() { EFUserID = 11, FirstName = "Jordan", LastName = "Khalid", Email = "Jkhalid@gmail.com", IdentityUserName = "Jkhalid", Address = "25 Zuma avenue, 2001", PhoneNumber = "074396748", Role = "Employee" },
				new EFUser() { EFUserID = 12, FirstName = "Elga", LastName = "Cantopher", Email = "ecantopher0@biblegateway.com", IdentityUserName = "ecantopher0", Address = "8 Merchant Point", PhoneNumber = "689-657-2674", Role = "Employee" },
				new EFUser() { EFUserID = 13, FirstName = "Gasparo", LastName = "Turpin", Email = "gturpin1@si.edu", IdentityUserName = "gturpin1", Address = "24 Amoth Street", PhoneNumber = "196-950-8155", Role = "Employee" },
				new EFUser() { EFUserID = 14, FirstName = "Harlin", LastName = "Venny", Email = "hvenny2@hao123.com", IdentityUserName = "hvenny2", Address = "290571 Sunbrook Alley", PhoneNumber = "196-950-8155", Role = "Employee" },
				new EFUser() { EFUserID = 15, FirstName = "Roshelle", LastName = "Maiden", Email = "rmaiden3@vimeo.com", IdentityUserName = "rmaiden3", Address = "207 Delladonna Drive", PhoneNumber = "310-145-9782", Role = "Employee" },
				new EFUser() { EFUserID = 16, FirstName = "Kesley", LastName = "Abatelli", Email = "kabatelli4@canalblog.com", IdentityUserName = "kabatelli4", Address = "14 Mayer Junction", PhoneNumber = "597-725-9204", Role = "Employee" },
				new EFUser() { EFUserID = 17, FirstName = "Merridie", LastName = "Bucky", Email = "mbucky5@house.gov", IdentityUserName = "mbucky5", Address = "06 Paget Crossing", PhoneNumber = "597-725-9204", Role = "Employee" },
				new EFUser() { EFUserID = 18, FirstName = "Lawry", LastName = "Purdom", Email = "lpurdom6@kickstarter.com", IdentityUserName = "lpurdom6", Address = "0 Prairieview Point", PhoneNumber = "866-316-2624", Role = "Employee" },
				new EFUser() { EFUserID = 19, FirstName = "Bekki", LastName = "Chavrin", Email = "bchavrin7@gmail.com", IdentityUserName = "bchavrin7", Address = "35347 Eliot Place", PhoneNumber = "405-920-9719", Role = "Employee" },
				new EFUser() { EFUserID = 20, FirstName = "Orazio", LastName = "Littley", Email = "olittley8@gmail.com", IdentityUserName = "olittley8", Address = "5268 Division Parkway", PhoneNumber = "268-407-0285", Role = "Employee" },
				new EFUser() { EFUserID = 21, FirstName = "Zack", LastName = "Efron", Email = "Efronz@gmail.com", IdentityUserName = "Efronz", Address = "15 Zuma avenue, 2001", PhoneNumber = "074396748", Role = "Customer" },
				new EFUser() { EFUserID = 22, FirstName = "Katharyn", LastName = "Maybey", Email = "kmaybey0@gmail.com", IdentityUserName = "kmaybey0", Address = "22310 Schmedeman Parkway", PhoneNumber = "368-346-0788", Role = "Customer" },
				new EFUser() { EFUserID = 23, FirstName = "Lanni", LastName = "Measom", Email = "lmeasom1@networkadvertising.org", IdentityUserName = "lmeasom1", Address = "6 Kim Place", PhoneNumber = "991-219-0321", Role = "Customer" },
				new EFUser() { EFUserID = 24, FirstName = "Laying", LastName = "Woolaston", Email = "mwoolaston2@soup.io", IdentityUserName = "mwoolaston2", Address = "8 Saint Paul Pass", PhoneNumber = "404-725-6084", Role = "Customer" },
				new EFUser() { EFUserID = 25, FirstName = "Linda", LastName = "Hamby", Email = "lhamby3@a8.net", IdentityUserName = "lhamby3", Address = "09060 Hanover Trail", PhoneNumber = "879-880-7372", Role = "Customer" },
				new EFUser() { EFUserID = 26, FirstName = "Ernesto", LastName = "Bartaloni", Email = "ebartaloni4@intel.com", IdentityUserName = "ebartaloni4", Address = "0 Garrison Crossing", PhoneNumber = "799-505-6110", Role = "Customer" },
				new EFUser() { EFUserID = 27, FirstName = "Tarra", LastName = "MacGinney", Email = "tmacginney5@goodreads.com", IdentityUserName = "tmacginney5", Address = "36383 Hallows Place", PhoneNumber = "122-564-5165", Role = "Customer" },
				new EFUser() { EFUserID = 28, FirstName = "Cacilia", LastName = "Rhodef", Email = "crhodef6@shutterfly.com", IdentityUserName = "crhodef6", Address = "0148 Little Fleur Hill", PhoneNumber = "640-914-6862", Role = "Customer" },
				new EFUser() { EFUserID = 29, FirstName = "Kerr", LastName = "Ivashkin", Email = "kivashkin7@salon.com", IdentityUserName = "kivashkin7", Address = "3113 Clyde Gallagher Crossing", PhoneNumber = "733-331-0434", Role = "Customer" },
				new EFUser() { EFUserID = 30, FirstName = "Emelda", LastName = "Guinnane", Email = "eguinnane8@discovery.com", IdentityUserName = "eguinnane8", Address = "656 Menomonie Hill", PhoneNumber = "656-188-2034", Role = "Customer" }
				);

			context.Discounts.AddOrUpdate(d => d.DiscountID,
				new Discount() { DiscountID = 1, Name = "Summer Sale", Rate = 0.10m },
				new Discount() { DiscountID = 2, Name = "Black Friday", Rate = 0.20m },
				new Discount() { DiscountID = 3, Name = "Cyber Monday", Rate = 0.15m},
				new Discount() { DiscountID = 4, Name = "New Year Sale", Rate = 0.25m },
				new Discount() { DiscountID = 5, Name = "Clearance", Rate = 0.30m},
				new Discount() { DiscountID = 6, Name = "Valentine's Day", Rate = 0.12m},
				new Discount() { DiscountID = 7, Name = "Spring Sale", Rate = 0.18m},
				new Discount() { DiscountID = 8, Name = "Easter Sale", Rate = 0.22m},
				new Discount() { DiscountID = 9, Name = "Back to School", Rate = 0.15m},
				new Discount() { DiscountID = 10, Name = "Holiday Sale", Rate = 0.20m}
			);


            context.Invoices.AddOrUpdate(c => c.InvoiceID,
                new Invoice() { InvoiceID = 1, EFUserID = 21, InvoiceDate = new DateTime(2024, 3, 4, 09, 34, 00), DueDate = new DateTime(2024, 3, 29, 09, 34, 00), Subtotal = 2549.98, Tax = 12, TaxAmount = 305.10, DiscountID = 1, DiscountAmount=254.10, TotalAmount = 2600.98, Status = "Paid" },
                new Invoice() { InvoiceID = 2, EFUserID = 22, InvoiceDate = new DateTime(2024, 3, 5, 09, 34, 00), DueDate = new DateTime(2024, 3, 25, 09, 34, 00), Subtotal = 2060.00, Tax = 10, TaxAmount = 206,DiscountID = 2, DiscountAmount = 412,TotalAmount = 1854.00, Status = "Paid" },
                new Invoice() { InvoiceID = 3, EFUserID = 23, InvoiceDate = new DateTime(2024, 3, 3, 09, 34, 00), DueDate = new DateTime(2024, 3, 5, 2, 09, 34, 00), Subtotal = 500.00, Tax = 3, TaxAmount = 15,DiscountID = 3, DiscountAmount = 75, TotalAmount = 440.00, Status = "Paid" },
                new Invoice() { InvoiceID = 4, EFUserID = 24, InvoiceDate = new DateTime(2024, 3, 3, 03, 34, 00), DueDate = new DateTime(2024, 3, 25, 09, 34, 00), Subtotal = 1770.00, Tax = 4, TaxAmount = 70.8,DiscountID = 4, DiscountAmount = 442.5, TotalAmount = 1398.3, Status = "Paid" },
                new Invoice() { InvoiceID = 5, EFUserID = 25, InvoiceDate = new DateTime(2024, 3, 3, 06, 34, 00), DueDate = new DateTime(2024, 3, 5, 09, 34, 00), Subtotal = 2180.00, Tax = 9, TaxAmount = 196.2, DiscountID = 5, DiscountAmount = 654, TotalAmount = 1722.2, Status = "Paid" },
                new Invoice() { InvoiceID = 6, EFUserID = 26, InvoiceDate = new DateTime(2024, 3, 3, 07, 34, 00), DueDate = new DateTime(2024, 3, 25, 09, 34, 00), Subtotal = 2680.00, Tax = 8, TaxAmount = 214.4, DiscountID = 6, DiscountAmount = 321.6, TotalAmount = 2572.8, Status = "Paid" },
                new Invoice() { InvoiceID = 7, EFUserID = 27, InvoiceDate = new DateTime(2024, 3, 3, 08, 34, 00), DueDate = new DateTime(2024, 3, 25, 09, 34, 00), Subtotal = 600.00, Tax = 8, TaxAmount = 48, DiscountID = 7, DiscountAmount = 108, TotalAmount = 540, Status = "Paid" },
                new Invoice() { InvoiceID = 8, EFUserID = 28, InvoiceDate = new DateTime(2024, 3, 3, 01, 34, 00), DueDate = new DateTime(2024, 3, 25, 09, 34, 00), Subtotal = 790.00, Tax = 12, TaxAmount = 94.8, DiscountID = 8, DiscountAmount = 173.8, TotalAmount = 711, Status = "Pending" },
                new Invoice() { InvoiceID = 9, EFUserID = 29, InvoiceDate = new DateTime(2024, 3, 3, 02, 34, 00), DueDate = new DateTime(2024, 3, 25, 09, 34, 00), Subtotal = 200.00, Tax = 9, TaxAmount =18, DiscountID = 9, DiscountAmount = 30.00, TotalAmount = 188, Status = "Overdue" },
                new Invoice() { InvoiceID = 10, EFUserID = 30, InvoiceDate = new DateTime(2024, 3, 5, 09, 34, 00), DueDate = new DateTime(2024, 3, 25, 09, 34, 00), Subtotal = 560.00, Tax = 14, TaxAmount = 78.4, DiscountID = 10, DiscountAmount = 112, TotalAmount = 526.4, Status = "Overdue" },

                new Invoice() { InvoiceID = 11, EFUserID = 21, InvoiceDate = new DateTime(2024, 3, 4, 09, 34, 00), DueDate = new DateTime(2024, 3, 29, 09, 34, 00), Subtotal = 2549.98, Tax = 12, TaxAmount = 305.10, DiscountID = 1, DiscountAmount = 254.10, TotalAmount = 2600.98, Status = "Pending" },
                new Invoice() { InvoiceID = 12, EFUserID = 22, InvoiceDate = new DateTime(2024, 3, 5, 09, 34, 00), DueDate = new DateTime(2024, 3, 25, 09, 34, 00), Subtotal = 2060.00, Tax = 10, TaxAmount = 206, DiscountID = 2, DiscountAmount = 412, TotalAmount = 1854.00, Status = "Pending" },
                new Invoice() { InvoiceID = 13, EFUserID = 23, InvoiceDate = new DateTime(2024, 3, 3, 09, 34, 00), DueDate = new DateTime(2024, 3, 5, 2, 09, 34, 00), Subtotal = 500.00, Tax = 3, TaxAmount = 15, DiscountID = 3, DiscountAmount = 75, TotalAmount = 440.00, Status = "Pending" },
                new Invoice() { InvoiceID = 14, EFUserID = 24, InvoiceDate = new DateTime(2024, 3, 3, 03, 34, 00), DueDate = new DateTime(2024, 3, 25, 09, 34, 00), Subtotal = 1770.00, Tax = 4, TaxAmount = 70.8, DiscountID = 4, DiscountAmount = 442.5, TotalAmount = 1398.3, Status = "Pending" },
                new Invoice() { InvoiceID = 15, EFUserID = 25, InvoiceDate = new DateTime(2024, 3, 3, 06, 34, 00), DueDate = new DateTime(2024, 3, 5, 09, 34, 00), Subtotal = 2180.00, Tax = 9, TaxAmount = 196.2, DiscountID = 5, DiscountAmount = 654, TotalAmount = 1722.2, Status = "Pending" },
                new Invoice() { InvoiceID = 16, EFUserID = 26, InvoiceDate = new DateTime(2024, 3, 3, 07, 34, 00), DueDate = new DateTime(2024, 3, 25, 09, 34, 00), Subtotal = 2680.00, Tax = 8, TaxAmount = 214.4, DiscountID = 6, DiscountAmount = 321.6, TotalAmount = 2572.8, Status = "Pending" },
                new Invoice() { InvoiceID = 17, EFUserID = 27, InvoiceDate = new DateTime(2024, 3, 3, 08, 34, 00), DueDate = new DateTime(2024, 3, 25, 09, 34, 00), Subtotal = 600.00, Tax = 8, TaxAmount = 48, DiscountID = 7, DiscountAmount = 108, TotalAmount = 540, Status = "Pending" },
                new Invoice() { InvoiceID = 18, EFUserID = 28, InvoiceDate = new DateTime(2024, 3, 3, 01, 34, 00), DueDate = new DateTime(2024, 3, 25, 09, 34, 00), Subtotal = 790.00, Tax = 12, TaxAmount = 94.8, DiscountID = 8, DiscountAmount = 173.8, TotalAmount = 711, Status = "Pending" },
                new Invoice() { InvoiceID = 19, EFUserID = 29, InvoiceDate = new DateTime(2024, 3, 3, 02, 34, 00), DueDate = new DateTime(2024, 3, 25, 09, 34, 00), Subtotal = 200.00, Tax = 9, TaxAmount = 18, DiscountID = 9, DiscountAmount = 30.00, TotalAmount = 188, Status = "Overdue" },
                new Invoice() { InvoiceID = 20, EFUserID = 30, InvoiceDate = new DateTime(2024, 3, 5, 09, 34, 00), DueDate = new DateTime(2024, 3, 25, 09, 34, 00), Subtotal = 560.00, Tax = 14, TaxAmount = 78.4, DiscountID = 10, DiscountAmount = 112, TotalAmount = 526.4, Status = "Overdue" }
            );



            context.Products.AddOrUpdate(p => p.ProductID,

				new Product() { ProductID = 1, Name = "Laptop", StockQuantity = 50, Price = 899.99M, Description = "Full HD, Intel Core i5, 8GB RAM, 512GB SSD", ModifiedDate = new DateTime(2018, 11, 12, 12, 45, 00) },
				new Product() { ProductID = 2, Name = "Smartphone", StockQuantity = 100, Price = 750.00M, Description = "6.5 AMOLED Display, Snapdragon 888, 128GB, 5G", ModifiedDate = new DateTime(2017, 1, 22, 15, 35, 10) },
				new Product() { ProductID = 3, Name = "Smartwatch", StockQuantity = 80, Price = 500.00M, Description = "Waterproof Fitness Tracker, Heart Rate Monitor", ModifiedDate = new DateTime(2019, 10, 12, 12, 45, 00) },
				new Product() { ProductID = 4, Name = "Headphones", StockQuantity = 150, Price = 590.00M, Description = "Wireless Over-Ear Headphones, Active Noise Cancellation", ModifiedDate = new DateTime(2018, 1, 12, 12, 45, 00) },
				new Product() { ProductID = 5, Name = "Tablet", StockQuantity = 70, Price = 780.00M, Description = "Retina Display, A13 Bionic Chip, 256GB", ModifiedDate = new DateTime(2018, 2, 12, 21, 47, 50) },
				new Product() { ProductID = 6, Name = "Computer", StockQuantity = 150, Price = 1345.00M, Description = "QHD Monitor, Intel Core i7, 16GB RAM, 1TB SSD", ModifiedDate = new DateTime(2019, 9, 13, 8, 5, 40) },
				new Product() { ProductID = 7, Name = "Gaming Console", StockQuantity = 30, Price = 600.00M, Description = "4K HDR, 1TB Storage, DualSense Wireless Controller", ModifiedDate = new DateTime(2020, 1, 2, 13, 35, 00) },
				new Product() { ProductID = 8, Name = "Wireless Router", StockQuantity = 90, Price = 790.00M, Description = "Dual-Band Gigabit Wi-Fi Router, Parental Controls", ModifiedDate = new DateTime(2021, 11, 12, 12, 45, 00) },
				new Product() { ProductID = 9, Name = "External Hard Drive", StockQuantity = 520, Price = 200.00M, Description = "2TB Portable USB 3.0 External HDD, Backup Solution", ModifiedDate = new DateTime(2022, 11, 10, 10, 40, 00) },
				new Product() { ProductID = 10, Name = "Wireless Mouse", StockQuantity = 200, Price = 560.00M, Description = "Ergonomic Design, Silent Click, USB Receiver", ModifiedDate = new DateTime(2019, 3, 22, 16, 5, 20) }
				);


            context.Payments.AddOrUpdate(p => p.PaymentID,
				new Payment() { PaymentID = 1, InvoiceID = 1, PaymentDate = new DateTime(2024, 01, 04, 11, 13, 12), Amount = 2600.98, PaymentMethod = "PayPal" },
				new Payment() { PaymentID = 2, InvoiceID = 2, PaymentDate = new DateTime(2024, 01, 05, 12, 14, 15), Amount = 1854.00, PaymentMethod = "debit card" },
				new Payment() { PaymentID = 3, InvoiceID = 3, PaymentDate = new DateTime(2024, 05, 24, 10, 15, 12), Amount = 440.00, PaymentMethod = "PayPal" },
				new Payment() { PaymentID = 4, InvoiceID = 4, PaymentDate = new DateTime(2024, 07, 14, 09, 13, 52), Amount = 1398.3, PaymentMethod = "credit card" },
				new Payment() { PaymentID = 5, InvoiceID = 5, PaymentDate = new DateTime(2023, 11, 12, 10, 25, 45), Amount = 1722.2, PaymentMethod = "PayPal" },
				new Payment() { PaymentID = 6, InvoiceID = 6, PaymentDate = new DateTime(2024, 01, 11, 05, 25, 30), Amount = 2572.8, PaymentMethod = "debit card" },
				new Payment() { PaymentID = 7, InvoiceID = 7, PaymentDate = new DateTime(2024, 01, 13, 13, 33, 10), Amount = 540, PaymentMethod = "credit card" }
			);


            context.InvoiceItems.AddOrUpdate(d => d.InvoiceItemID,
                 new InvoiceItem() { InvoiceItemID = 1, InvoiceID = 1, ProductID = 1, Quantity = 2, UnitPrice = 899.99m, TotalPrice = 1799.98m },
                 new InvoiceItem() { InvoiceItemID = 2, InvoiceID = 1, ProductID = 2, Quantity = 1, UnitPrice = 750.00m, TotalPrice = 750.00m },
                 new InvoiceItem() { InvoiceItemID = 3, InvoiceID = 2, ProductID = 1, Quantity = 1, UnitPrice = 500.00m, TotalPrice = 500.00m },
                 new InvoiceItem() { InvoiceItemID = 4, InvoiceID = 3, ProductID = 3, Quantity = 3, UnitPrice = 590.00m, TotalPrice = 1770.00m },
                 new InvoiceItem() { InvoiceItemID = 5, InvoiceID = 2, ProductID = 4, Quantity = 2, UnitPrice = 780.00m, TotalPrice = 1560.00m },
                 new InvoiceItem() { InvoiceItemID = 6, InvoiceID = 4, ProductID = 5, Quantity = 4, UnitPrice = 1345.00m, TotalPrice = 5380.00m },
                 new InvoiceItem() { InvoiceItemID = 7, InvoiceID = 5, ProductID = 6, Quantity = 1, UnitPrice = 600.00m, TotalPrice = 600.00m },
                 new InvoiceItem() { InvoiceItemID = 8, InvoiceID = 5, ProductID = 2, Quantity = 2, UnitPrice = 790.00m, TotalPrice = 1580.00m },
                 new InvoiceItem() { InvoiceItemID = 9, InvoiceID = 6, ProductID = 3, Quantity = 5, UnitPrice = 200.00m, TotalPrice = 1000.00m },
                 new InvoiceItem() { InvoiceItemID = 10, InvoiceID = 6, ProductID = 1, Quantity = 3, UnitPrice = 560.00m, TotalPrice = 1680.00m },
                 new InvoiceItem() { InvoiceItemID = 11, InvoiceID = 7, ProductID = 2, Quantity = 2, UnitPrice = 790.00m, TotalPrice = 1580.00m },
                 new InvoiceItem() { InvoiceItemID = 12, InvoiceID = 8, ProductID = 2, Quantity = 1, UnitPrice = 790.00m, TotalPrice = 790.00m },
                 new InvoiceItem() { InvoiceItemID = 13, InvoiceID = 9, ProductID = 3, Quantity = 1, UnitPrice = 200.00m, TotalPrice = 200.00m },
                 new InvoiceItem() { InvoiceItemID = 14, InvoiceID = 10, ProductID = 1, Quantity = 1, UnitPrice = 560.00m, TotalPrice = 560.00m },

                 new InvoiceItem() { InvoiceItemID = 15, InvoiceID = 11, ProductID = 1, Quantity = 2, UnitPrice = 899.99m, TotalPrice = 1799.98m },
                 new InvoiceItem() { InvoiceItemID = 16, InvoiceID = 11, ProductID = 2, Quantity = 1, UnitPrice = 750.00m, TotalPrice = 750.00m },
                 new InvoiceItem() { InvoiceItemID = 17, InvoiceID = 12, ProductID = 1, Quantity = 1, UnitPrice = 500.00m, TotalPrice = 500.00m },
                 new InvoiceItem() { InvoiceItemID = 18, InvoiceID = 13, ProductID = 3, Quantity = 3, UnitPrice = 590.00m, TotalPrice = 1770.00m },
                 new InvoiceItem() { InvoiceItemID = 19, InvoiceID = 12, ProductID = 4, Quantity = 2, UnitPrice = 780.00m, TotalPrice = 1560.00m },
                 new InvoiceItem() { InvoiceItemID = 20, InvoiceID = 14, ProductID = 5, Quantity = 4, UnitPrice = 1345.00m, TotalPrice = 5380.00m },
                 new InvoiceItem() { InvoiceItemID = 21, InvoiceID = 15, ProductID = 6, Quantity = 1, UnitPrice = 600.00m, TotalPrice = 600.00m },
                 new InvoiceItem() { InvoiceItemID = 22, InvoiceID = 15, ProductID = 2, Quantity = 2, UnitPrice = 790.00m, TotalPrice = 1580.00m },
                 new InvoiceItem() { InvoiceItemID = 23, InvoiceID = 16, ProductID = 3, Quantity = 5, UnitPrice = 200.00m, TotalPrice = 1000.00m },
                 new InvoiceItem() { InvoiceItemID = 24, InvoiceID = 16, ProductID = 1, Quantity = 3, UnitPrice = 560.00m, TotalPrice = 1680.00m },
                 new InvoiceItem() { InvoiceItemID = 25, InvoiceID = 17, ProductID = 2, Quantity = 2, UnitPrice = 790.00m, TotalPrice = 1580.00m },
                 new InvoiceItem() { InvoiceItemID = 26, InvoiceID = 18, ProductID = 2, Quantity = 1, UnitPrice = 790.00m, TotalPrice = 790.00m },
                 new InvoiceItem() { InvoiceItemID = 27, InvoiceID = 19, ProductID = 3, Quantity = 1, UnitPrice = 200.00m, TotalPrice = 200.00m },
                 new InvoiceItem() { InvoiceItemID = 28, InvoiceID = 20, ProductID = 1, Quantity = 1, UnitPrice = 560.00m, TotalPrice = 560.00m }

            );

            context.Notes.AddOrUpdate(c => c.NoteID,
				new Note() { NoteID = 1, InvoiceID = 1, InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 1 is currently on hold pending approval.", CreatedDate = new DateTime(2024, 3, 23, 9, 34, 0) },
				new Note() { NoteID = 2, InvoiceID = 2, InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 2 is currently on hold pending approval. ", CreatedDate = new DateTime(2024, 4, 23, 0, 34, 0) },
				new Note() { NoteID = 3, InvoiceID = 3, InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 3 is currently on hold pending approval. ", CreatedDate = new DateTime(2024, 4, 23, 8, 34, 0) },
				new Note() { NoteID = 4, InvoiceID = 4, InvoiceNotes = "We wanted to express our gratitude for your recent payment of invoice 4. Your promptness is greatly appreciated.", CreatedDate = new DateTime(2024, 4, 23, 10, 34, 0) },
				new Note() { NoteID = 5, InvoiceID = 5, InvoiceNotes = "We wanted to express our gratitude for your recent payment of invoice 5. Your promptness is greatly appreciated.", CreatedDate = new DateTime(2024, 5, 13, 12, 34, 0) },
				new Note() { NoteID = 6, InvoiceID = 6, InvoiceNotes = "We wanted to express our gratitude for your recent payment of invoice 6. Your promptness is greatly appreciated.", CreatedDate = new DateTime(2024, 3, 13, 13, 34, 0) },
				new Note() { NoteID = 7, InvoiceID = 7, InvoiceNotes = "We wanted to express our gratitude for your recent payment of invoice 7. Your promptness is greatly appreciated.", CreatedDate = new DateTime(2024, 4, 23, 12, 34, 0) },
				new Note() { NoteID = 8, InvoiceID = 8, InvoiceNotes = "We hope this message finds you well. We're reaching out to remind you that invoice 8 is now overdue.", CreatedDate = new DateTime(2024, 4, 3, 12, 24, 0) },
				new Note() { NoteID = 9, InvoiceID = 9, InvoiceNotes = "We hope this message finds you well. We're reaching out to remind you that invoice 9 is now overdue.", CreatedDate = new DateTime(2024, 4, 1, 11, 14, 0) },
				new Note() { NoteID = 10, InvoiceID = 10, InvoiceNotes = "We hope this message finds you well. We're reaching out to remind you that invoice", CreatedDate = new DateTime(2024, 4, 1, 11, 54, 0) }
				);
		}
	}
}
