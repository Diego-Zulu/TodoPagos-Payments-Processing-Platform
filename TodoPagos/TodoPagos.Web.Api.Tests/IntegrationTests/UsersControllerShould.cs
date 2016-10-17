using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.WebApi;
using Moq;
using TodoPagos.Web.Services;
using TodoPagos.Web.Api.Controllers;
using System.Web.Http;
using System.Web.Http.Results;
using TodoPagos.UserAPI;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace TodoPagos.WebApi.Tests.IntegrationTests
{
    [TestClass]
    public class UsersControllerShould
    {
        static string ADMIN_USER_USEREMAIL = "diego@bruno.com";
        static User ADMIN_USER;

        static string CASHIER_USER_USEREMAIL = "nacho@gabriel.com";
        static User CASHIER_USER;

        [ClassInitialize()]
        public static void SetAdminAndCashierUsersInfoForTests(TestContext testContext)
        {
            ADMIN_USER = new User("Brulu", ADMIN_USER_USEREMAIL, "HOLA1234", AdminRole.GetInstance());
            ADMIN_USER.ID = 1;

            CASHIER_USER = new User("Nariel", CASHIER_USER_USEREMAIL, "HOLA1234", CashierRole.GetInstance());
            CASHIER_USER.ID = 2;

            //UsersController controller = new UsersController("bla");

            //controller.PostUser(ADMIN_USER);
            //controller.PostUser(CASHIER_USER);

            //int bla = 0;
        }

        [TestMethod]
        public void ReceiveAUserServiceAndASignedInUsernameOnCreation()
        {
            string username = "TestUser";

            UsersController controller = new UsersController(username);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfUserNameIsNullOnCreation()
        {
            string nullUsername = null;

            UsersController controller = new UsersController(nullUsername);
        }

        [TestMethod]
        public void BeAbleToReturnAllUsersInRepository()
        {

            var allUsers = new[]
            {
                ADMIN_USER,
                CASHIER_USER,
                new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance()),
                new User("Ignacio", "valle@gmail.com", "#designPatternsLover123", AdminRole.GetInstance())                
            };
            UsersController controller = new UsersController(ADMIN_USER_USEREMAIL);

            controller.PostUser(allUsers[2]);
            controller.PostUser(allUsers[3]);

            IHttpActionResult actionResult = controller.GetUsers();
            OkNegotiatedContentResult<IEnumerable<User>> contentResult = (OkNegotiatedContentResult<IEnumerable<User>>)actionResult;

            Assert.IsTrue(contentResult.Content.All(x=> allUsers.Contains(x)) 
                && allUsers.All(x => contentResult.Content.Contains(x)));

            controller.DeleteUser(3);
            controller.DeleteUser(4);
        }

        [TestMethod]
        public void NotReturnAnyPasswordsOrSaltsWhenAllUsersAreReturned()
        {
            var allUsersWithoutPasswordsAndSalts = GetAllUsersWithoutPasswordsAndSalts();
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.GetAllUsers()).Returns(allUsersWithoutPasswordsAndSalts);
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.GetUsers();
            OkNegotiatedContentResult<IEnumerable<User>> contentResult = (OkNegotiatedContentResult<IEnumerable<User>>)actionResult;

            Assert.AreSame(contentResult.Content, allUsersWithoutPasswordsAndSalts);
            Assert.IsTrue(NoUserInTargetListHasPasswordsOrSalts(contentResult.Content));
        }

        private bool NoUserInTargetListHasPasswordsOrSalts(IEnumerable<User> targetList)
        {
            foreach(User oneUser in targetList)
            {
                if (oneUser.HasPassword() || oneUser.HasSalt())
                {
                    return false;
                }
            }
            return true;
        }

        private User[] GetAllUsersWithoutPasswordsAndSalts()
        {
            User firstUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
            User secondUser = new User("Ignacio", "valle@gmail.com", "#designPatternsLover123", AdminRole.GetInstance());
            firstUser.ClearPassword();
            firstUser.ClearSalt();
            secondUser.ClearPassword();
            secondUser.ClearSalt();
            return new[]
            {
                firstUser,
                secondUser
            };

        }

        [TestMethod]
        public void BeAbleToReturnSingleUserInRepository()
        {
            User singleUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.GetSingleUser(singleUser.ID)).Returns(singleUser);
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.GetUser(singleUser.ID);
            OkNegotiatedContentResult<User> contentResult = (OkNegotiatedContentResult<User>)actionResult;

            Assert.AreSame(contentResult.Content, singleUser);
        }

        [TestMethod]
        public void NotReturnThePasswordNeitherTheSaltWhenSingleUserIsReturned()
        {
            User singleUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
            singleUser.ClearPassword();
            singleUser.ClearSalt();
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.GetSingleUser(singleUser.ID)).Returns(singleUser);
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.GetUser(singleUser.ID);
            OkNegotiatedContentResult<User> contentResult = (OkNegotiatedContentResult<User>)actionResult;

            Assert.AreSame(contentResult.Content, singleUser);
            Assert.IsFalse(contentResult.Content.HasPassword());
            Assert.IsFalse(contentResult.Content.HasSalt());
        }

        [TestMethod]
        public void FailWithNotFoundIfSingleUserIdDoesntExistInRepository()
        {
            User singleUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.GetSingleUser(singleUser.ID + 1)).Throws(new ArgumentOutOfRangeException());
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.GetUser(singleUser.ID + 1);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void BeAbleToPostNewUserIntoRepository()
        {
            User singleUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.CreateUser(singleUser, It.IsAny<String>())).Returns(1);
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.PostUser(singleUser);
            CreatedAtRouteNegotiatedContentResult<User> contentResult = (CreatedAtRouteNegotiatedContentResult<User>)actionResult;

            Assert.AreSame(contentResult.Content, singleUser);
        }

        [TestMethod]
        public void FailWithBadRequestIfPostedNewUserIsAlreadyInRepository()
        {
            User singleUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.CreateUser(singleUser, It.IsAny<string>())).Throws(new InvalidOperationException());
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.PostUser(singleUser);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void FailWithBadRequestIfPostedNewUserIsNotCompleteInRepository()
        {
            User incompleteUser = new User();
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.CreateUser(incompleteUser, It.IsAny<string>())).Throws(new ArgumentException());
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.PostUser(incompleteUser);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void FailWithBadRequestIfPostedNewUserIsNull()
        {
            User nullUser = null;
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.CreateUser(nullUser, It.IsAny<string>())).Throws(new ArgumentNullException());
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.PostUser(nullUser);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void BeAbleToUpdateAnUserInTheRepository()
        {
            User singleUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.UpdateUser(singleUser.ID, singleUser, It.IsAny<string>())).Returns(true);
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.PutUser(singleUser.ID, singleUser);
            StatusCodeResult contentResult = (StatusCodeResult)actionResult;

            Assert.AreEqual(contentResult.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void FailWithBadRequestIfUpdatedUserIdAndSuppliedIdAreDifferent()
        {
            User singleUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.UpdateUser(singleUser.ID + 1, singleUser, It.IsAny<string>())).Returns(false);
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.PutUser(singleUser.ID + 1, singleUser);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void FailWithBadRequestIfUpdatedUserIsNull()
        {
            User nullUser = null;
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.UpdateUser(1, nullUser, It.IsAny<string>())).Returns(false);
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.PutUser(1, nullUser);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void FailWithNotFoundIfServiceCantFindToBeUpdatedUserInRepository()
        {
            User singleUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.UpdateUser(singleUser.ID, singleUser, It.IsAny<string>())).Returns(false);
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.PutUser(singleUser.ID, singleUser);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void BeAbleToDeleteAnUser()
        {
            User singleUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.DeleteUser(singleUser.ID, It.IsAny<string>())).Returns(true);
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.DeleteUser(singleUser.ID);
            StatusCodeResult contentResult = (StatusCodeResult)actionResult;

            Assert.AreEqual(contentResult.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void FailWithNotFoundIfToBeDeletedUserDoesntExistInRepository()
        {
            User singleUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.DeleteUser(singleUser.ID, It.IsAny<string>())).Returns(false);
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.DeleteUser(singleUser.ID);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}
