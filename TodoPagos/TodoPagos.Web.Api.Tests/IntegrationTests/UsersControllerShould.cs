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
using System.Collections;

namespace TodoPagos.WebApi.Tests.IntegrationTests
{
    [TestClass]
    public class UsersControllerShould
    {
        static string ADMIN_USER_USEREMAIL = "diego@bruno.com";
        static User ADMIN_USER;

        static string CASHIER_USER_USEREMAIL = "nacho@gabriel.com";
        static User CASHIER_USER;

        static ICollection<User> TESTS_USERS;
        static ICollection<User> ALL_USERS_IN_REPOSITORY;

        UsersController CONTROLLER;



        [ClassInitialize()]
        public static void SetAdminAndCashierUsersInfoForTests(TestContext testContext)
        {
            ADMIN_USER = new User("Brulu", ADMIN_USER_USEREMAIL, "HOLA1234", AdminRole.GetInstance());
            CASHIER_USER = new User("Nariel", CASHIER_USER_USEREMAIL, "HOLA1234", CashierRole.GetInstance());

            ADMIN_USER.ID = 1;
            CASHIER_USER.ID = 2;

            //UsersController controller = new UsersController("bla");

            //controller.PostUser(ADMIN_USER);
            //controller.PostUser(CASHIER_USER);

            //int bla = 0;
        }

        [TestInitialize()]
        public void InsertTestsUserInfoForTest() {

            CONTROLLER = new UsersController(ADMIN_USER_USEREMAIL);

            TESTS_USERS = new[]
            {
                new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance()),
                new User("Ignacio", "valle@gmail.com", "#designPatternsLover123", AdminRole.GetInstance())
            };

            foreach (User aTestUser in TESTS_USERS)
            {
                CONTROLLER.PostUser(aTestUser);
            }

            ICollection<User> reservedUsers = new[] { ADMIN_USER, CASHIER_USER};
            ALL_USERS_IN_REPOSITORY = reservedUsers.Concat(TESTS_USERS).ToList();
        }

        [TestCleanup()]
        public void DeleteTestUserInfoForTest() {

            foreach (User aTestUser in TESTS_USERS)
            {
                CONTROLLER.DeleteUser(aTestUser.ID);
            }

            CONTROLLER.Dispose();
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

            IHttpActionResult actionResult = CONTROLLER.GetUsers();
            OkNegotiatedContentResult<IEnumerable<User>> contentResult = (OkNegotiatedContentResult<IEnumerable<User>>)actionResult;

            CollectionAssert.AreEqual((ICollection)contentResult.Content, (ICollection)ALL_USERS_IN_REPOSITORY);
        }

        [TestMethod]
        public void NotReturnAnyPasswordsOrSaltsWhenAllUsersAreReturned()
        {

            IHttpActionResult actionResult = CONTROLLER.GetUsers();
            OkNegotiatedContentResult<IEnumerable<User>> contentResult = (OkNegotiatedContentResult<IEnumerable<User>>)actionResult;

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

        //private User[] GetAllUsersWithoutPasswordsAndSalts()
        //{
        //    User firstUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
        //    User secondUser = new User("Ignacio", "valle@gmail.com", "#designPatternsLover123", AdminRole.GetInstance());
        //    firstUser.ClearPassword();
        //    firstUser.ClearSalt();
        //    secondUser.ClearPassword();
        //    secondUser.ClearSalt();
        //    return new[]
        //    {
        //        firstUser,
        //        secondUser
        //    };

        //}

        [TestMethod]
        public void BeAbleToReturnSingleUserInRepository()
        { 
            IHttpActionResult actionResult = CONTROLLER.GetUser(ADMIN_USER.ID);
            OkNegotiatedContentResult<User> contentResult = (OkNegotiatedContentResult<User>)actionResult;

            Assert.AreEqual(contentResult.Content, ADMIN_USER);
        }

        [TestMethod]
        public void NotReturnThePasswordNeitherTheSaltWhenSingleUserIsReturned()
        {

            IHttpActionResult actionResult = CONTROLLER.GetUser(CASHIER_USER.ID);
            OkNegotiatedContentResult<User> contentResult = (OkNegotiatedContentResult<User>)actionResult;

            Assert.AreEqual(contentResult.Content, CASHIER_USER);
            Assert.IsFalse(contentResult.Content.HasPassword());
            Assert.IsFalse(contentResult.Content.HasSalt());
        }

        [TestMethod]
        public void FailWithNotFoundIfSingleUserIdDoesntExistInRepository()
        {
            IHttpActionResult actionResult = CONTROLLER.GetUser(-1);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void BeAbleToPostNewUserIntoRepository()
        {
            User newUser = new User("Gonzalo", "laguna@gmail.com", "OtraPass123!", CashierRole.GetInstance());

            IHttpActionResult actionResult = CONTROLLER.PostUser(newUser);
            CreatedAtRouteNegotiatedContentResult<User> contentResult = (CreatedAtRouteNegotiatedContentResult<User>)actionResult;

            Assert.AreEqual(contentResult.Content, newUser);

            CONTROLLER.DeleteUser(newUser.ID);
        }

        [TestMethod]
        public void FailWithBadRequestIfPostedNewUserIsAlreadyInRepository()
        {
            User repeteadUser = ADMIN_USER;

            IHttpActionResult actionResult = CONTROLLER.PostUser(repeteadUser);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void FailWithBadRequestIfPostedNewUserIsNotCompleteInRepository()
        {
            User incompleteUser = new User();

            IHttpActionResult actionResult = CONTROLLER.PostUser(incompleteUser);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void FailWithBadRequestIfPostedNewUserIsNull()
        {
            User nullUser = null;

            IHttpActionResult actionResult = CONTROLLER.PostUser(nullUser);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void BeAbleToUpdateAnUserInTheRepository()
        {
            User updatedUser = new User(TESTS_USERS.Last());
            updatedUser.Name = "Nuevo Nombre";
            IHttpActionResult actionResult = CONTROLLER.PutUser(updatedUser.ID, updatedUser);
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
