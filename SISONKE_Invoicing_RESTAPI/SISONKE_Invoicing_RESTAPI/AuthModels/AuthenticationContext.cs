using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SISONKE_Invoicing_RESTAPI.AuthModels
{
	public class AuthenticationContext : IdentityDbContext
	{
		public AuthenticationContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<ApplicationUser> ApplicationUsers { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			var roleId_1 = Guid.NewGuid().ToString();
			var userId_1 = Guid.NewGuid().ToString();

			var roleId_2 = Guid.NewGuid().ToString();
			var userId_2 = Guid.NewGuid().ToString();

			var roleId_3 = Guid.NewGuid().ToString();
			var userId_3 = Guid.NewGuid().ToString();

			#region "Seed Data"
			builder.Entity<IdentityRole>().HasData(
				new { Id = roleId_1, Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
				new { Id = roleId_2, Name = "Employee", NormalizedName = "EMPLOYEE" },
				new { Id = roleId_3, Name = "Customer", NormalizedName = "CUSTOMER" }
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
			builder.Entity<ApplicationUser>().HasData(AdminUser);

			//set user role to Administrator
			builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
			{
				RoleId = roleId_1,
				UserId = userId_1
			});

            var AdminUser2 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "Harrionj01@gmail.com",
                EmailConfirmed = true,
                Address = "230 West street, 4001",
                PhoneNumber = "0675647385",
                FirstName = "Jack",
                LastName = "Harrison",
                UserName = "Harrison01Admin",
                NormalizedUserName = "HARRISON01ADMIN"
            };
            //set user password
            PasswordHasher<ApplicationUser> ph2 = new PasswordHasher<ApplicationUser>();
            AdminUser2.PasswordHash = ph2.HashPassword(AdminUser2, "Harrionj01@1234");

            //seed user
            builder.Entity<ApplicationUser>().HasData(AdminUser2);

            //set user role to Administrator
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_1,
                UserId = AdminUser2.Id
            });


            var AdminUser3 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "Sanchoh@gmail.com",
                EmailConfirmed = true,
                Address = "15 Zimbali avenue, 4501",
                PhoneNumber = "0673004545",
                FirstName = "Hannah",
                LastName = "Sancho",
                UserName = "SanchohAdmin",
                NormalizedUserName = "SANCHOHADMIN"
            };
            //set user password
            PasswordHasher<ApplicationUser> ph3 = new PasswordHasher<ApplicationUser>();
            AdminUser3.PasswordHash = ph3.HashPassword(AdminUser3, "Sanchoh@1234");
            //seed user
            builder.Entity<ApplicationUser>().HasData(AdminUser3);
            //set user role to Administrator
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_1,
                UserId = AdminUser3.Id
            });

            var AdminUser4 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "Wakez007@gmail.com",
                EmailConfirmed = true,
                Address = "28 Sandton avenue, 2001",
                PhoneNumber = "0745667386",
                FirstName = "Zack",
                LastName = "Wake",
                UserName = "Wakez007Admin",
                NormalizedUserName = "WAKEZ007ADMIN"
            };
            //set user password
            PasswordHasher<ApplicationUser> ph4 = new PasswordHasher<ApplicationUser>();
            AdminUser4.PasswordHash = ph4.HashPassword(AdminUser4, "Wakez007@1234");
            //seed user
            builder.Entity<ApplicationUser>().HasData(AdminUser4);
            //set user role to Administrator
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_1,
                UserId = AdminUser4.Id
            });


            var AdminUser5 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "Bruno22@gmail.com",
                EmailConfirmed = true,
                Address = "20 King Edwards avenue, 3201",
                PhoneNumber = "0843778347",
                FirstName = "Bruno",
                LastName = "Sterling",
                UserName = "Brunos22Admin",
                NormalizedUserName = "BRUNOS22ADMIN"
            };
            //set user password
            PasswordHasher<ApplicationUser> ph5 = new PasswordHasher<ApplicationUser>();
            AdminUser5.PasswordHash = ph5.HashPassword(AdminUser5, "Bruno22@1234");
            //seed user
            builder.Entity<ApplicationUser>().HasData(AdminUser5);
            //set user role to Administrator
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_1,
                UserId = AdminUser5.Id
            });


            var AdminUser6 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "Luisd7@gmail.com",
                EmailConfirmed = true,
                Address = "19 Jack avenue, 2001",
                PhoneNumber = "0742687829",
                FirstName = "Luis",
                LastName = "Diaz",
                UserName = "Luis7Admin",
                NormalizedUserName = "LUIS7ADMIN"
            };

            //set user password
            PasswordHasher<ApplicationUser> ph6 = new PasswordHasher<ApplicationUser>();
            AdminUser6.PasswordHash = ph6.HashPassword(AdminUser6, "Luisd7@1234");
            //seed user
            builder.Entity<ApplicationUser>().HasData(AdminUser6);
            //set user role to Administrator
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_1,
                UserId = AdminUser6.Id
            });


            var AdminUser7 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "Walkerc07@gmail.com",
                EmailConfirmed = true,
                Address = "15 Harrison avenue, 3001",
                PhoneNumber = "0743264748",
                FirstName = "Cristiano",
                LastName = "Walker",
                UserName = "Walkerc07Admin",
                NormalizedUserName = "WALKERC07ADMIN"
            };

            //set user password
            PasswordHasher<ApplicationUser> ph7 = new PasswordHasher<ApplicationUser>();
            AdminUser7.PasswordHash = ph7.HashPassword(AdminUser7, "Walkerc07@1234");
            //seed user
            builder.Entity<ApplicationUser>().HasData(AdminUser7);
            //set user role to Administrator
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_1,
                UserId = AdminUser7.Id
            });


            var AdminUser8 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "Fodenp47@gmail.com",
                EmailConfirmed = true,
                Address = "100 Everton street, 6001",
                PhoneNumber = "0746277885",
                FirstName = "Phil",
                LastName = "Foden",
                UserName = "Fodenp47Admin",
                NormalizedUserName = "FODENP47ADMIN"
            };
            //set user password
            PasswordHasher<ApplicationUser> ph8 = new PasswordHasher<ApplicationUser>();
            AdminUser8.PasswordHash = ph8.HashPassword(AdminUser8, "Fodenp47@1234");
            //seed user
            builder.Entity<ApplicationUser>().HasData(AdminUser8);
            //set user role to Administrator
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_1,
                UserId = AdminUser8.Id
            });


            var AdminUser9 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "Kcarrick@gmail.com",
                EmailConfirmed = true,
                Address = "20 Jack avenue, 2001",
                PhoneNumber = "0675249849",
                FirstName = "Kylian",
                LastName = "Carrick",
                UserName = "KcarrickAdmin",
                NormalizedUserName = "KCARRICKADMIN"
            };
            //set user password
            PasswordHasher<ApplicationUser> ph9 = new PasswordHasher<ApplicationUser>();
            AdminUser9.PasswordHash = ph9.HashPassword(AdminUser9, "Kcarrick@1234");
            //seed user
            builder.Entity<ApplicationUser>().HasData(AdminUser9);
            //set user role to Administrator
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_1,
                UserId = AdminUser9.Id
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
			builder.Entity<ApplicationUser>().HasData(EmployeeUser);

			//set user role to admin
			builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
			{
				RoleId = roleId_2,
				UserId = userId_2
			});


            var EmployeeUser2 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "ecantopher0@biblegateway.com",
                EmailConfirmed = true,
                Address = "8 Merchant Point",
                PhoneNumber = "0896572674",
                FirstName = "Elga",
                LastName = "Cantopher",
                UserName = "ecantopher0",
                NormalizedUserName = "ECANTOPHER0"
            };
            // Set user password
            PasswordHasher<ApplicationUser> empph2 = new PasswordHasher<ApplicationUser>();
            EmployeeUser2.PasswordHash = empph2.HashPassword(EmployeeUser2, "PaSSward123!");
            // Seed user
            builder.Entity<ApplicationUser>().HasData(EmployeeUser2);
            // Set user role to Employee
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_2,
                UserId = EmployeeUser2.Id
            });

            var EmployeeUser3 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "gturpin1@si.edu",
                EmailConfirmed = true,
                Address = "24 Amoth Street",
                PhoneNumber = "0969508155",
                FirstName = "Gasparo",
                LastName = "Turpin",
                UserName = "gturpin1",
                NormalizedUserName = "GTURPIN1"
            };
            // Set user password
            PasswordHasher<ApplicationUser> empph3 = new PasswordHasher<ApplicationUser>();
            EmployeeUser3.PasswordHash = empph3.HashPassword(EmployeeUser3, "Gasparii#123!");
            // Seed user
            builder.Entity<ApplicationUser>().HasData(EmployeeUser3);
            // Set user role to Employee
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_2,
                UserId = EmployeeUser3.Id
            });

            var EmployeeUser4 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "hvenny2@hao123.com",
                EmailConfirmed = true,
                Address = "290571 Sunbrook Alley",
                PhoneNumber = "0969508155",
                FirstName = "Harlin",
                LastName = "Venny",
                UserName = "hvenny2",
                NormalizedUserName = "HVENNY2"
            };
            // Set user password
            PasswordHasher<ApplicationUser> empph4 = new PasswordHasher<ApplicationUser>();
            EmployeeUser4.PasswordHash = ph4.HashPassword(EmployeeUser4, "Gomes*&123!");
            // Seed user
            builder.Entity<ApplicationUser>().HasData(EmployeeUser4);
            // Set user role to Employee
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_2,
                UserId = EmployeeUser4.Id
            });

            var EmployeeUser5 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "rmaiden3@vimeo.com",
                EmailConfirmed = true,
                Address = "207 Delladonna Drive",
                PhoneNumber = "0101459782",
                FirstName = "Roshelle",
                LastName = "Maiden",
                UserName = "rmaiden3",
                NormalizedUserName = "RMAIDEN3"
            };
            // Set user password
            PasswordHasher<ApplicationUser> empph5 = new PasswordHasher<ApplicationUser>();
            EmployeeUser5.PasswordHash = empph5.HashPassword(EmployeeUser5, "dellamin&23!");
            // Seed user
            builder.Entity<ApplicationUser>().HasData(EmployeeUser5);
            // Set user role to Employee
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_2,
                UserId = EmployeeUser5.Id
            });

            var EmployeeUser6 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "kabatelli4@canalblog.com",
                EmailConfirmed = true,
                Address = "14 Mayer Junction",
                PhoneNumber = "0977259204",
                FirstName = "Kesley",
                LastName = "Abatelli",
                UserName = "kabatelli4",
                NormalizedUserName = "KABATELLI4"
            };
            // Set user password
            PasswordHasher<ApplicationUser> empph6 = new PasswordHasher<ApplicationUser>();
            EmployeeUser6.PasswordHash = empph6.HashPassword(EmployeeUser6, "Abatleli@92");
            // Seed user
            builder.Entity<ApplicationUser>().HasData(EmployeeUser6);
            // Set user role to Employee
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_2,
                UserId = EmployeeUser6.Id
            });

            var EmployeeUser7 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "mbucky5@house.gov",
                EmailConfirmed = true,
                Address = "06 Paget Crossing",
                PhoneNumber = "0977259204",
                FirstName = "Merridie",
                LastName = "Bucky",
                UserName = "mbucky5",
                NormalizedUserName = "MBUCKY5"
            };
            // Set user password
            PasswordHasher<ApplicationUser> empph7 = new PasswordHasher<ApplicationUser>();
            EmployeeUser7.PasswordHash = empph7.HashPassword(EmployeeUser7, "Jimi@989");
            // Seed user
            builder.Entity<ApplicationUser>().HasData(EmployeeUser7);
            // Set user role to Employee
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_2,
                UserId = EmployeeUser7.Id
            });

            var EmployeeUser8 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "lpurdom6@kickstarter.com",
                EmailConfirmed = true,
                Address = "0 Prairieview Point",
                PhoneNumber = "0669797870",
                FirstName = "Lawry",
                LastName = "Purdom",
                UserName = "lpurdom6",
                NormalizedUserName = "LPURDOM6"
            };
            // Set user password
            PasswordHasher<ApplicationUser> empph8 = new PasswordHasher<ApplicationUser>();
            EmployeeUser8.PasswordHash = empph8.HashPassword(EmployeeUser8, "LetsDoIt@3!");
            // Seed user
            builder.Entity<ApplicationUser>().HasData(EmployeeUser8);
            // Set user role to Employee
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_2,
                UserId = EmployeeUser8.Id
            });

            var EmployeeUser9 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "bchavrin7@gmail.com",
                EmailConfirmed = true,
                Address = "35347 Eliot Place",
                PhoneNumber = "0059209719",
                FirstName = "Bekki",
                LastName = "Chavrin",
                UserName = "bchavrin7",
                NormalizedUserName = "BCHAVRIN7"
            };
            // Set user password
            PasswordHasher<ApplicationUser> empph9 = new PasswordHasher<ApplicationUser>();
            EmployeeUser9.PasswordHash = empph9.HashPassword(EmployeeUser9, "MaryJane55#!");
            // Seed user
            builder.Entity<ApplicationUser>().HasData(EmployeeUser9);
            // Set user role to Employee
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_2,
                UserId = EmployeeUser9.Id
            });

            var EmployeeUser10 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "olittley8@gmail.com",
                EmailConfirmed = true,
                Address = "5268 Division Parkway",
                PhoneNumber = "2684070285",
                FirstName = "Orazio",
                LastName = "Littley",
                UserName = "olittley8",
                NormalizedUserName = "OLITTLEY8"
            };
            // Set user password
            PasswordHasher<ApplicationUser> empph10 = new PasswordHasher<ApplicationUser>();
            EmployeeUser10.PasswordHash = empph10.HashPassword(EmployeeUser10, "Passwo**9!");
            // Seed user
            builder.Entity<ApplicationUser>().HasData(EmployeeUser10);
            // Set user role to Employee
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_2,
                UserId = EmployeeUser10.Id
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
			builder.Entity<ApplicationUser>().HasData(CustomerUser);

			//set user role to admin
			builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
			{
				RoleId = roleId_3,
				UserId = userId_3
			});

            var userId_32 = Guid.NewGuid().ToString();
            var userId_33 = Guid.NewGuid().ToString();
            var userId_34 = Guid.NewGuid().ToString();
            var userId_35 = Guid.NewGuid().ToString();
            var userId_36 = Guid.NewGuid().ToString();
            var userId_37 = Guid.NewGuid().ToString();
            var userId_38 = Guid.NewGuid().ToString();
            var userId_39 = Guid.NewGuid().ToString();
            var userId_310 = Guid.NewGuid().ToString();


            var CustomerUser2 = new ApplicationUser
            {
                Id = userId_32,
                Email = "kmaybey0@gmail.com",
                EmailConfirmed = true,
                Address = "22310 Schmedeman Parkway",
                PhoneNumber = "368-346-0788",
                FirstName = "Katharyn",
                LastName = "Maybey",
                UserName = "kmaybey0",
                NormalizedUserName = "KMAYBEY0"
            };
            // Set user password
            PasswordHasher<ApplicationUser> lgcph2 = new PasswordHasher<ApplicationUser>();
            CustomerUser2.PasswordHash = lgcph2.HashPassword(CustomerUser2, "Customer@1234");
            // Seed user
            builder.Entity<ApplicationUser>().HasData(CustomerUser2);
            // Set user role to Customer
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_3,
                UserId = userId_32
            });

            var CustomerUser3 = new ApplicationUser
            {
                Id = userId_33,
                Email = "lmeasom1@networkadvertising.org",
                EmailConfirmed = true,
                Address = "6 Kim Place",
                PhoneNumber = "991-219-0321",
                FirstName = "Lanni",
                LastName = "Measom",
                UserName = "lmeasom1",
                NormalizedUserName = "LMEASOM1"
            };
            // Set user password
            PasswordHasher<ApplicationUser> lgcph3 = new PasswordHasher<ApplicationUser>();
            CustomerUser3.PasswordHash = lgcph3.HashPassword(CustomerUser3, "Customer@1234");
            // Seed user
            builder.Entity<ApplicationUser>().HasData(CustomerUser3);
            // Set user role to Customer
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_3,
                UserId = userId_33
            });

            var CustomerUser4 = new ApplicationUser
            {
                Id = userId_34,
                Email = "mwoolaston2@soup.io",
                EmailConfirmed = true,
                Address = "8 Saint Paul Pass",
                PhoneNumber = "404-725-6084",
                FirstName = "Laying",
                LastName = "Woolaston",
                UserName = "mwoolaston2",
                NormalizedUserName = "MWOOLASTON2"
            };
            // Set user password
            PasswordHasher<ApplicationUser> lgcph4 = new PasswordHasher<ApplicationUser>();
            CustomerUser4.PasswordHash = lgcph4.HashPassword(CustomerUser4, "Customer@1234");
            // Seed user
            builder.Entity<ApplicationUser>().HasData(CustomerUser4);
            // Set user role to Customer
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_3,
                UserId = userId_34
            });

            var CustomerUser5 = new ApplicationUser
            {
                Id = userId_35,
                Email = "lhamby3@a8.net",
                EmailConfirmed = true,
                Address = "09060 Hanover Trail",
                PhoneNumber = "879-880-7372",
                FirstName = "Linda",
                LastName = "Hamby",
                UserName = "lhamby3",
                NormalizedUserName = "LHAMBY3"
            };
            // Set user password
            PasswordHasher<ApplicationUser> lgcph5 = new PasswordHasher<ApplicationUser>();
            CustomerUser5.PasswordHash = lgcph5.HashPassword(CustomerUser5, "Customer@1234");
            // Seed user
            builder.Entity<ApplicationUser>().HasData(CustomerUser5);
            // Set user role to Customer
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_3,
                UserId = userId_35
            });

            var CustomerUser6 = new ApplicationUser
            {
                Id = userId_36,
                Email = "ebartaloni4@intel.com",
                EmailConfirmed = true,
                Address = "0 Garrison Crossing",
                PhoneNumber = "799-505-6110",
                FirstName = "Ernesto",
                LastName = "Bartaloni",
                UserName = "ebartaloni4",
                NormalizedUserName = "EBARTALONI4"
            };
            // Set user password
            PasswordHasher<ApplicationUser> lgcph6 = new PasswordHasher<ApplicationUser>();
            CustomerUser6.PasswordHash = lgcph6.HashPassword(CustomerUser6, "Customer@1234");
            // Seed user
            builder.Entity<ApplicationUser>().HasData(CustomerUser6);
            // Set user role to Customer
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_3,
                UserId = userId_36
            });

            var CustomerUser7 = new ApplicationUser
            {
                Id = userId_37,
                Email = "tmacginney5@goodreads.com",
                EmailConfirmed = true,
                Address = "36383 Hallows Place",
                PhoneNumber = "122-564-5165",
                FirstName = "Tarra",
                LastName = "MacGinney",
                UserName = "tmacginney5",
                NormalizedUserName = "TMACGINNEY5"
            };
            // Set user password
            PasswordHasher<ApplicationUser> lgcph7 = new PasswordHasher<ApplicationUser>();
            CustomerUser7.PasswordHash = lgcph7.HashPassword(CustomerUser7, "Customer@1234");
            // Seed user
            builder.Entity<ApplicationUser>().HasData(CustomerUser7);
            // Set user role to Customer
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_3,
                UserId = userId_37
            });

            var CustomerUser8 = new ApplicationUser
            {
                Id = userId_38,
                Email = "crhodef6@shutterfly.com",
                EmailConfirmed = true,
                Address = "0148 Little Fleur Hill",
                PhoneNumber = "640-914-6862",
                FirstName = "Cacilia",
                LastName = "Rhodef",
                UserName = "crhodef6",
                NormalizedUserName = "CRHODEF6"
            };
            // Set user password
            PasswordHasher<ApplicationUser> lgcph8 = new PasswordHasher<ApplicationUser>();
            CustomerUser8.PasswordHash = lgcph8.HashPassword(CustomerUser8, "Customer@1234");
            // Seed user
            builder.Entity<ApplicationUser>().HasData(CustomerUser8);
            // Set user role to Customer
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_3,
                UserId = userId_38
            });

            var CustomerUser9 = new ApplicationUser
            {
                Id = userId_39,
                Email = "kivashkin7@salon.com",
                EmailConfirmed = true,
                Address = "3113 Clyde Gallagher Crossing",
                PhoneNumber = "733-331-0434",
                FirstName = "Kerr",
                LastName = "Ivashkin",
                UserName = "kivashkin7",
                NormalizedUserName = "KIVASHKIN7"
            };
            // Set user password
            PasswordHasher<ApplicationUser> lgcph9 = new PasswordHasher<ApplicationUser>();
            CustomerUser9.PasswordHash = lgcph9.HashPassword(CustomerUser9, "Customer@1234");
            // Seed user
            builder.Entity<ApplicationUser>().HasData(CustomerUser9);
            // Set user role to Customer
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_3,
                UserId = userId_39
            });

            var CustomerUser10 = new ApplicationUser
            {
                Id = userId_310,
                Email = "eguinnane8@discovery.com",
                EmailConfirmed = true,
                Address = "656 Menomonie Hill",
                PhoneNumber = "656-188-2034",
                FirstName = "Emelda",
                LastName = "Guinnane",
                UserName = "eguinnane8",
                NormalizedUserName = "EGUINNANE8"
            };
            // Set user password
            PasswordHasher<ApplicationUser> lgcph10 = new PasswordHasher<ApplicationUser>();
            CustomerUser10.PasswordHash = lgcph10.HashPassword(CustomerUser10, "Customer@1234");
            // Seed user
            builder.Entity<ApplicationUser>().HasData(CustomerUser10);
            // Set user role to Customer
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_3,
                UserId = userId_310
            });

            #endregion
        }
	}
}
