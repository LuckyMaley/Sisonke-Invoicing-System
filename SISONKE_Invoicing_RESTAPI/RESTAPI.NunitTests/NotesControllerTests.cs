
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
using Microsoft.AspNetCore.Http;
using FluentAssertions.Equivalency;

namespace RESTApi.NunitTests
{
    
    public class NotesControllerTests
    {
        private SISONKE_Invoicing_System_EFDBContext _sisonkeContext;
        private NotesController _controllerUnderTest;
        private List<Note> _noteList;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private AuthenticationContext _authenticationContext;
        NoteVM _noteVM;
        Note _note;
        IdentityUser identityUser;
        ClaimsPrincipal principal;

        [SetUp]
        public void Initialiser()
        {
            _sisonkeContext = (SISONKE_Invoicing_System_EFDBContext)InMemoryContext.GeneratedDB();
            var prod = _sisonkeContext.Notes.Count();
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
            _controllerUnderTest = new NotesController(_sisonkeContext, _userManager, _roleManager, _authenticationContext)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = principal }
                }
            };

            _noteList = new List<Note>();
            _note = new Note()

            {
                NoteId = 1,
                InvoiceId = 1,
                InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 1 is currently on hold pending approval.",
                CreatedDate = new DateTime(2024, 3, 23, 9, 34, 0)
            };

            _noteVM = new NoteVM()

            {
                NoteId = 1,
                InvoiceId = 1,
                InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 1 is currently on hold pending approval.",
                CreatedDate = new DateTime(2024, 3, 23, 9, 34, 0)
            };

        }



        [TearDown]
        public void CleanUpObject()
        {
            _sisonkeContext.Database.EnsureDeleted();
            _controllerUnderTest = null;
            _noteList = null;
            _note = null;
            _userManager = null;

            _roleManager = null;
            _authenticationContext.Database.EnsureDeleted();
        }

        [Test]
        public async Task _01Test_GetAllNote_ReturnsListWithValidCount0()
        {
            // Arrange


            // Act
            var result = await _controllerUnderTest.GetNotes();

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            var noteList = okResult.Value as List<Note>;
            Assert.NotNull(noteList);
            Assert.AreEqual(10, noteList.Count);
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
        public async Task _03Test_GetNoteByID_ReturnsaListwithCount1_WhenCorrectNoteIDEntered(int idPassedIn)
        {
            //Arrange
            int id = idPassedIn;

            //Act            
            var result = await _controllerUnderTest.GetNotes(id);
            var okResult = (OkObjectResult)result.Result;
            var actual = (Note)okResult.Value;
            var expected = _sisonkeContext.Notes.FirstOrDefault(c => c.NoteId == id);

            //Assert
            Assert.IsInstanceOf<ActionResult<Note>>(result);
            Assert.AreEqual(okResult.StatusCode, 200);
            Assert.AreEqual(actual.NoteId, expected.NoteId);

        }


        
        [Test]
        public async Task _06Test_GetNoteByID_ReturnsaBackActionResult_WhenNoteIDEnteredDoesNotExist()
        {
            //Arrange
            int id = 20;

            //Act            
            var result = await _controllerUnderTest.GetNotes(id);
            var badResult = (NotFoundObjectResult)result.Result;
            var actual = badResult.Value.ToString();
            var expected = "No Note with that ID exists, please try again";

            //Assert
            Assert.IsTrue(actual.Contains(expected));
            Assert.AreEqual(badResult.StatusCode, 404);

        }

        [Test]
        public async Task _07Test_PostAnote_ReturnsBadRequest_WhenEmptyNoteAdded()
        {
            //Arrange 

            //Act            
            var result = await _controllerUnderTest.PostNotes(new NoteVM());
            var badResult = (BadRequestObjectResult)result.Result;
            var actual = badResult.Value.ToString();
            var expected = "Cannot Add an empty note";

            //Assert
            Assert.AreEqual(badResult.StatusCode, 400);
            Assert.AreEqual(10, _sisonkeContext.Notes.Count());

        }


        [Test]
        public async Task _09Test_PutNotes_ReturnsNotUpdated_WhenEmployee()
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
            _controllerUnderTest = new NotesController(_sisonkeContext, _userManager, _roleManager, _authenticationContext)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = principal }
                }
            };

            var note1 = new Note()
            {
                NoteId = 11,
                InvoiceId = 1,
                InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 1 is currently on hold pending approval.",
                CreatedDate = new DateTime(2024, 3, 23, 9, 34, 0)
            };
            var notesVM1 = new NoteVM()
            {
                NoteId = note1.NoteId,
                InvoiceId = note1.InvoiceId,
                InvoiceNotes = note1.InvoiceNotes,
                CreatedDate = new DateTime(2023, 3, 25, 9, 18, 0),
            };

            var note2 = new Note()
            {
                NoteId = 12,
                InvoiceId = 1,
                InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 1 is currently on hold pending approval.",
                CreatedDate = new DateTime(2024, 3, 23, 9, 34, 0)
            };
            var notesVM2 = new NoteVM()
            {
                NoteId = note1.NoteId,
                InvoiceId = note1.InvoiceId,
                InvoiceNotes = note1.InvoiceNotes,
                CreatedDate = new DateTime(2023, 3, 25, 9, 18, 0),
            };

            var note3 = new Note()
            {
                NoteId = 14,
                InvoiceId = 1,
                InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 1 is currently on hold pending approval.",
                CreatedDate = new DateTime(2024, 3, 23, 9, 34, 0)
            };
            var notesVM3 = new NoteVM()
            {
                NoteId = note1.NoteId,
                InvoiceId = note1.InvoiceId,
                InvoiceNotes = note1.InvoiceNotes,
                CreatedDate = new DateTime(2023, 3, 25, 9, 18, 0),
            };

            //Act            
            var result1 = await _controllerUnderTest.PutNotes(1, notesVM1);
            var result2 = await _controllerUnderTest.PutNotes(2, notesVM2);
            var result3 = await _controllerUnderTest.PutNotes(3, notesVM3);

            var badResult1 = (BadRequestObjectResult)result1;
            var actual1 = badResult1.Value.ToString();
            var expected1 = "Not authorised to update notes";

            var badResult2 = (BadRequestObjectResult)result2;
            var actual2 = badResult2.Value.ToString();
            var expected2 = "Not authorised to update notes";

            var badResult3 = (BadRequestObjectResult)result3;
            var actual3 = badResult3.Value.ToString();
            var expected3 = "Not authorised to update notes";

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
        public async Task _10Test_PutNotes_ReturnsNotFoundResult()
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
            _controllerUnderTest = new NotesController(_sisonkeContext, _userManager, _roleManager, _authenticationContext)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = principal }
                }
            };
            var note1 = new Note()
            {
                NoteId = 1,
                InvoiceId = 1,
                InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 1 is currently on hold pending approval.",
                CreatedDate = new DateTime(2024, 3, 23, 9, 34, 0)
            };
            var notesVM1 = new NoteVM()
            {
                NoteId = note1.NoteId,
                InvoiceId = note1.InvoiceId,
                InvoiceNotes = note1.InvoiceNotes,
                CreatedDate = new DateTime(2023, 3, 25, 9, 18, 0),
            };

            var note2 = new Note()
            {
                NoteId = 1,
                InvoiceId = 1,
                InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 1 is currently on hold pending approval.",
                CreatedDate = new DateTime(2024, 3, 23, 9, 34, 0)
            };
            var notesVM2 = new NoteVM()
            {
                NoteId = note1.NoteId,
                InvoiceId = note1.InvoiceId,
                InvoiceNotes = note1.InvoiceNotes,
                CreatedDate = new DateTime(2023, 3, 25, 9, 18, 0),
            };

            var note3 = new Note()
            {
                NoteId = 1,
                InvoiceId = 1,
                InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 1 is currently on hold pending approval.",
                CreatedDate = new DateTime(2024, 3, 23, 9, 34, 0)
            };
            var notesVM3 = new NoteVM()
            {
                NoteId = note1.NoteId,
                InvoiceId = note1.InvoiceId,
                InvoiceNotes = note1.InvoiceNotes,
                CreatedDate = new DateTime(2023, 3, 25, 9, 18, 0),
            };



            //Act            
            var result1 = await _controllerUnderTest.PutNotes(11, notesVM1);
            var result2 = await _controllerUnderTest.PutNotes(12, notesVM2);
            var result3 = await _controllerUnderTest.PutNotes(13, notesVM3);

            var badResult1 = (NotFoundObjectResult)result1;
            var actual1 = badResult1.Value.ToString();
            var expected1 = "No Note with that ID exists, please try again";

            var badResult2 = (NotFoundObjectResult)result2;
            var actual2 = badResult2.Value.ToString();
            var expected2 = "No Note with that ID exists, please try again";

            var badResult3 = (NotFoundObjectResult)result3;
            var actual3 = badResult3.Value.ToString();
            var expected3 = "No Note with that ID exists, please try again";

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
        public async Task _12Test_GetNoteByID_ReturnsWithCorrectType_WhenPassedInID(int id)
        {
            //Arrange 
            var note1 = new Note()
            {
                NoteId = 11,
                InvoiceId = 1,
                InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 1 is currently on hold pending approval.",
                CreatedDate = new DateTime(2024, 3, 23, 9, 34, 0)
            };
            var notesVM1 = new NoteVM()
            {
                NoteId = note1.NoteId,
                InvoiceId = note1.InvoiceId,
                InvoiceNotes = note1.InvoiceNotes,
                CreatedDate = new DateTime(2023, 3, 25, 9, 18, 0),
            };

            var note2 = new Note()
            {
                NoteId = 12,
                InvoiceId = 1,
                InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 1 is currently on hold pending approval.",
                CreatedDate = new DateTime(2024, 3, 23, 9, 34, 0)
            };
            var notesVM2 = new NoteVM()
            {
                NoteId = note1.NoteId,
                InvoiceId = note1.InvoiceId,
                InvoiceNotes = note1.InvoiceNotes,
                CreatedDate = new DateTime(2023, 3, 25, 9, 18, 0),
            };

            var note3 = new Note()
            {
                NoteId = 14,
                InvoiceId = 1,
                InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 1 is currently on hold pending approval.",
                CreatedDate = new DateTime(2024, 3, 23, 9, 34, 0)
            };
            var notesVM3 = new NoteVM()
            {
                NoteId = note1.NoteId,
                InvoiceId = note1.InvoiceId,
                InvoiceNotes = note1.InvoiceNotes,
                CreatedDate = new DateTime(2023, 3, 25, 9, 18, 0),
            };


            await _controllerUnderTest.PostNotes(notesVM1);
            await _controllerUnderTest.PostNotes(notesVM2);

            //Act            
            var actionResult = await _controllerUnderTest.GetNotes(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsInstanceOf<ActionResult<Note>>(actionResult);
        }



        [Test]
        public async Task _12Test_GetAllnote_ReturnsWithCorrectTypeAndCount()
        {
            //Arrange 
            var note1 = new Note()
            {
                NoteId = 11,
                InvoiceId = 1,
                InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 1 is currently on hold pending approval.",
                CreatedDate = new DateTime(2024, 3, 23, 9, 34, 0)
            };
            var notesVM1 = new NoteVM()
            {
                NoteId = note1.NoteId,
                InvoiceId = note1.InvoiceId,
                InvoiceNotes = note1.InvoiceNotes,
                CreatedDate = new DateTime(2023, 3, 25, 9, 18, 0),
            };

            var note2 = new Note()
            {
                NoteId = 13,
                InvoiceId = 1,
                InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 1 is currently on hold pending approval.",
                CreatedDate = new DateTime(2024, 3, 23, 9, 34, 0)
            };
            var notesVM2 = new NoteVM()
            {
                NoteId = note1.NoteId,
                InvoiceId = note1.InvoiceId,
                InvoiceNotes = note1.InvoiceNotes,
                CreatedDate = new DateTime(2023, 3, 25, 9, 18, 0),
            };

            var note3 = new Note()
            {
                NoteId = 15,
                InvoiceId = 1,
                InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 1 is currently on hold pending approval.",
                CreatedDate = new DateTime(2024, 3, 23, 9, 34, 0)
            };
            var notesVM3 = new NoteVM()
            {
                NoteId = note1.NoteId,
                InvoiceId = note1.InvoiceId,
                InvoiceNotes = note1.InvoiceNotes,
                CreatedDate = new DateTime(2023, 3, 25, 9, 18, 0),
            };


            await _controllerUnderTest.PostNotes(notesVM1);
            await _controllerUnderTest.PostNotes(notesVM2);
            await _controllerUnderTest.PostNotes(notesVM3);

            //Act            
            var actionResult = await _controllerUnderTest.GetNotes();

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsInstanceOf<ActionResult<IEnumerable<Note>>>(actionResult);
            var result = (OkObjectResult)actionResult.Result;
            var value = (List<Note>)result.Value;
            Assert.AreEqual(_sisonkeContext.Notes.Count(), value.Count);
        }

        [Test]
        public async Task _13Test_GetnoteById_ReturnsWithCorrectType()
        {
            //Arrange 
            var note1 = new Note()
            {
                NoteId = 1,
                InvoiceId = 1,
                InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 1 is currently on hold pending approval.",
                CreatedDate = new DateTime(2024, 3, 23, 9, 34, 0)
            };
            var notesVM1 = new NoteVM()
            {
                NoteId = note1.NoteId,
                InvoiceId = note1.InvoiceId,
                InvoiceNotes = note1.InvoiceNotes,
                CreatedDate = new DateTime(2023, 3, 25, 9, 18, 0),
            };

            var note2 = new Note()
            {
                NoteId = 1,
                InvoiceId = 1,
                InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 1 is currently on hold pending approval.",
                CreatedDate = new DateTime(2024, 3, 23, 9, 34, 0)
            };
            var notesVM2 = new NoteVM()
            {
                NoteId = note1.NoteId,
                InvoiceId = note1.InvoiceId,
                InvoiceNotes = note1.InvoiceNotes,
                CreatedDate = new DateTime(2023, 3, 25, 9, 18, 0),
            };

            var note3 = new Note()
            {
                NoteId = 1,
                InvoiceId = 1,
                InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 1 is currently on hold pending approval.",
                CreatedDate = new DateTime(2024, 3, 23, 9, 34, 0)
            };
            var notesVM3 = new NoteVM()
            {
                NoteId = note1.NoteId,
                InvoiceId = note1.InvoiceId,
                InvoiceNotes = note1.InvoiceNotes,
                CreatedDate = new DateTime(2023, 3, 25, 9, 18, 0),
            };



            await _controllerUnderTest.PostNotes(_noteVM);
            await _controllerUnderTest.PostNotes(notesVM2);

            //Act            
            var actionResult = await _controllerUnderTest.GetNotes(11);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsInstanceOf<ActionResult<Note>>(actionResult);
        }


        [Test]
        public async Task _14Test_PostNotes_NotAddedAndShowsInContextCount_WhenUserIsAEmployee()
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
            _controllerUnderTest = new NotesController(_sisonkeContext, _userManager, _roleManager, _authenticationContext)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = principal }
                }
            };


            //Act            
            var actionResult = await _controllerUnderTest.PostNotes(_noteVM);
            //Assert
            Assert.IsFalse(_sisonkeContext.Notes.Where(c => c.NoteId == 11).Count() > 0);
            Assert.IsTrue(_sisonkeContext.Notes.Count() == 10);

        }


        [Test]
        public async Task _15Test_PostNotes_ReturnsBadObjectResult_WhenUserIsACustomer()
        {
            //Arrange

            //Act            
            var actionResult = await _controllerUnderTest.PostNotes(_noteVM);

            //Assert
            var result = (ActionResult<Note>)actionResult.Result;
            var badResult = (BadRequestObjectResult)actionResult.Result;
            var expected = "Not authorised to add notes as a customer";
            var actual = badResult.Value.ToString();

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsInstanceOf<ActionResult<Note>>(actionResult);
            Assert.AreEqual(badResult.StatusCode, 400);
            Assert.IsFalse(actual.Contains(expected));
            Assert.AreEqual(10, _sisonkeContext.Notes.Count());
        }


        [Test]
        public async Task _16Test_DeleteNotes_ReturnsMessageThatEmployeeCannotDeleteNote()
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
            _controllerUnderTest = new NotesController(_sisonkeContext, _userManager, _roleManager, _authenticationContext)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = principal }
                }
            };

            var note1 = new Note()
            {
                NoteId = 1,
                InvoiceId = 1,
                InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 1 is currently on hold pending approval.",
                CreatedDate = new DateTime(2024, 3, 23, 9, 34, 0)
            };
            var notesVM1 = new NoteVM()
            {
                NoteId = note1.NoteId,
                InvoiceId = note1.InvoiceId,
                InvoiceNotes = note1.InvoiceNotes,
                CreatedDate = new DateTime(2023, 3, 25, 9, 18, 0),
            };

            var note2 = new Note()
            {
                NoteId = 1,
                InvoiceId = 1,
                InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 1 is currently on hold pending approval.",
                CreatedDate = new DateTime(2024, 3, 23, 9, 34, 0)
            };
            var notesVM2 = new NoteVM()
            {
                NoteId = note1.NoteId,
                InvoiceId = note1.InvoiceId,
                InvoiceNotes = note1.InvoiceNotes,
                CreatedDate = new DateTime(2023, 3, 25, 9, 18, 0),
            };

            var note3 = new Note()
            {
                NoteId = 1,
                InvoiceId = 1,
                InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 1 is currently on hold pending approval.",
                CreatedDate = new DateTime(2024, 3, 23, 9, 34, 0)
            };
            var notesVM3 = new NoteVM()
            {
                NoteId = note1.NoteId,
                InvoiceId = note1.InvoiceId,
                InvoiceNotes = note1.InvoiceNotes,
                CreatedDate = new DateTime(2023, 3, 25, 9, 18, 0),
            };


            //Act

            var actionResultDeleted = await _controllerUnderTest.DeleteNotes(8);
            var result = (ActionResult<Note>)actionResultDeleted.Result;
            var badResult = (BadRequestObjectResult)actionResultDeleted.Result;
            var expected = "Not authorised to delete notes";
            var actual = badResult.Value.ToString();

            //Assert
            Assert.IsInstanceOf<ActionResult<Note>>(actionResultDeleted);
            Assert.AreEqual(badResult.StatusCode, 400);
            Assert.IsTrue(actual.Contains(expected));
            Assert.AreEqual(10, _sisonkeContext.Notes.Count());
        }



        [Test]
        public async Task _17Test_DeleteNotes_DeleteSuccessfullyReturnsWithCorrectTypeAndShowsInContextCount_WhenAdministrator()
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
            _controllerUnderTest = new NotesController(_sisonkeContext, _userManager, _roleManager, _authenticationContext)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = principal }
                }
            };

            var note2 = new Note()
            {
                NoteId = 13,
                InvoiceId = 1,
                InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 1 is currently on hold pending approval.",
                CreatedDate = new DateTime(2024, 3, 23, 9, 34, 0)
            };
            var notesVM2 = new NoteVM()
            {
                NoteId = note2.NoteId,
                InvoiceId = note2.InvoiceId,
                InvoiceNotes = note2.InvoiceNotes,
                CreatedDate = new DateTime(2023, 3, 25, 9, 18, 0),
            };


            //Act
            var actionResult = await _controllerUnderTest.PostNotes(notesVM2);
            var actionResultDeleted = await _controllerUnderTest.DeleteNotes(note2.NoteId);

            //Assert
            Assert.NotNull(actionResultDeleted);
            Assert.IsInstanceOf<ActionResult<Note>>(actionResultDeleted);
            Assert.AreEqual(10, _sisonkeContext.Notes.Count());
        }


        [Test]
        public async Task _18Test_DeleteNotes_AddMultipleDeleteOne_DeleteSuccessfullyReturnsWithCorrectTypeAndShowsInContextCount_WhenAdministrator()
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
            _controllerUnderTest = new NotesController(_sisonkeContext, _userManager, _roleManager, _authenticationContext)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = principal }
                }
            };

            var note2 = new Note()
            {
                NoteId = 1,
                InvoiceId = 1,
                InvoiceNotes = "We hope you're doing well. This message is to inform you that invoice 1 is currently on hold pending approval.",
                CreatedDate = new DateTime(2024, 3, 23, 9, 34, 0)
            };
            var notesVM2 = new NoteVM()
            {
                NoteId = note2.NoteId,
                InvoiceId = note2.InvoiceId,
                InvoiceNotes = note2.InvoiceNotes,
                CreatedDate = new DateTime(2023, 3, 25, 9, 18, 0),
            };

            //Act
            await _controllerUnderTest.PostNotes(_noteVM);
            await _controllerUnderTest.PostNotes(notesVM2);

            var actionResultDeleted = await _controllerUnderTest.DeleteNotes(_note.NoteId);
            var actionResultDeleted2 = await _controllerUnderTest.DeleteNotes(note2.NoteId);

            //Assert
            Assert.NotNull(actionResultDeleted);
            Assert.IsInstanceOf<ActionResult<Note>>(actionResultDeleted);
            Assert.NotNull(actionResultDeleted2);
            Assert.IsInstanceOf<ActionResult<Note>>(actionResultDeleted2);
            Assert.AreEqual(10, _sisonkeContext.Notes.Count());
        }

        [TestCase("2024-03-23")]
        [TestCase("2024-04-23")]
        [TestCase("2024-04-23")]
        [TestCase("2024-04-23")]
        [TestCase("2024-05-13")]
        [TestCase("2024-03-13")]
        [TestCase("2024-04-23")]
        public async Task _19Test_GetNoteByDate_ReturnsaListwithCount1_WhenCorrectCreatedDateEntered(DateTime datePassedIn)
        {
            //Arrange
            DateTime date = datePassedIn;

            //Act            
            var result = await _controllerUnderTest.GetNoteByDate(date);
            var okResult = (OkObjectResult)result.Result;
            var actual = (List<Note>)okResult.Value;
            var expected = _sisonkeContext.Notes.FirstOrDefault(c => c.CreatedDate == date);

            //Assert
            Assert.IsInstanceOf<ActionResult<List<Note>>>(result);
            Assert.AreEqual(okResult.StatusCode, 200);
            Assert.IsTrue(actual.Count() > 0);

        }

        [TestCase("2015-12-12")]
        [TestCase("2016-05-22")]
        [TestCase("2016-11-22")]
        [TestCase("2014-03-22")]
        [TestCase("2015-03-02")]
        [TestCase("2013-10-13")]
        [TestCase("2014-02-02")]
        [TestCase("2013-04-22")]
        [TestCase("2013-12-12")]
        [TestCase("2013-12-10")]
        [TestCase("2020-01-01")]
        public async Task _20Test_GetNoteByDate_ReturnsaListwithCount0_WhenWrongCreatedDateEntered(DateTime datePassedIn)
        {
            //Arrange
            DateTime date = datePassedIn;

            //Act            
            var result = await _controllerUnderTest.GetNoteByDate(date);
            var notFoundResult = (NotFoundObjectResult)result.Result;
            var actual = "No Note with that date exists, please try again";
            var expected = "No Note with that date exists, please try again";

            //Assert
            Assert.AreEqual(notFoundResult.StatusCode, 404);
            Assert.IsTrue(actual.Contains(expected));

        }


     
        [TestCase("2024-01-01", "2024-04-12")]
        [TestCase("2024-02-06", "2024-03-16")]
        [TestCase("2024-02-06", "2024-05-19")]
        public async Task _21Test_GetNoteByDate_ReturnsaListwithCount1_WhenCorrectCreatedDateEntered(DateTime date1PassedIn, DateTime date2PassedIn)
        {
            //Arrange
            DateTime date1 = date1PassedIn;
            DateTime date2 = date2PassedIn;

            //Act            
            var result = await _controllerUnderTest.GetNoteByBetweenDates(date1, date2);
            var okResult = (OkObjectResult)result.Result;
            var actual = (List<Note>)okResult.Value;

            //Assert
            Assert.IsInstanceOf<ActionResult<List<Note>>>(result);
            Assert.AreEqual(okResult.StatusCode, 200);
            Assert.IsTrue(actual.Count() > 0);

        }

        [TestCase("2012-01-01", "2012-12-12")]
        [TestCase("2013-01-01", "2013-12-12")]
        [TestCase("2014-01-01", "2014-12-12")]
        [TestCase("2015-01-01", "2015-12-12")]
        [TestCase("2016-01-01", "2016-12-12")]
        public async Task _22Test_GetNoteByDate_ReturnsaListwithCount0_WhenWrongCreatedDateEntered(DateTime date1PassedIn, DateTime date2PassedIn)
        {
            //Arrange
            DateTime date1 = date1PassedIn;
            DateTime date2 = date2PassedIn;

            //Act            
            var result = await _controllerUnderTest.GetNoteByBetweenDates(date1, date2);
            var notFoundResult = (NotFoundObjectResult)result.Result;
            var actual = "No Note with that date range exists, please try again";
            var expected = "No Note with that date range exists, please try again";

            //Assert
            Assert.AreEqual(notFoundResult.StatusCode, 404);
            Assert.IsTrue(actual.Contains(expected));

        }
    }
}
