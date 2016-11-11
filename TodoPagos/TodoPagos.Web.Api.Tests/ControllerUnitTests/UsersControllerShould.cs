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
using System.Linq;
using System.Collections;

namespace TodoPagos.WebApi.Tests.ControllerUnitTests
{
    [TestClass]
    public class UsersControllerShould
    {

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void FailWithNullReferenceExceptionWhenNoUserIsLogedIn()
        {
            UsersController controller = new UsersController();
        }

        [TestMethod]
        public void ReceiveAUserServiceOnCreation()
        {
            var mockUserService = new Mock<IUserService>();

            UsersController controller = new UsersController(mockUserService.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfUserServiceIsNullOnCreation()
        {
            IUserService nullUserService = null;

            UsersController controller = new UsersController(nullUserService);
        }

        [TestMethod]
        public void BeAbleToReturnAllUsersInRepository()
        {
            List<User> allUsers = new List<User>
            {
                new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance()),
                new User("Ignacio", "valle@gmail.com", "#designPatternsLover123", AdminRole.GetInstance())
            };
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.GetAllUsers(It.IsAny<string>())).Returns(allUsers);
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.GetUsers();
            OkNegotiatedContentResult<IEnumerable<User>> contentResult = (OkNegotiatedContentResult<IEnumerable<User>>)actionResult;

            Assert.IsTrue(contentResult.Content.All(x => allUsers.Contains(x)) &&
                        allUsers.All(x => contentResult.Content.Contains(x)));
        }

        [TestMethod]
        public void NotReturnAnyPasswordsOrSaltsWhenAllUsersAreReturned()
        {
            List<User> allUsersWithPasswordsAndSalts = GetAllUsersWithPasswordsAndSalts();
            List<User> allUsersWithoutPasswordsAndSalts = GetAllUsersWithoutPasswordsAndSalts();
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.GetAllUsers(It.IsAny<string>())).Returns(allUsersWithPasswordsAndSalts);
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.GetUsers();
            OkNegotiatedContentResult<IEnumerable<User>> contentResult = (OkNegotiatedContentResult<IEnumerable<User>>)actionResult;

            Assert.IsTrue(allUsersWithoutPasswordsAndSalts.All(x => contentResult.Content.Contains(x)) && 
                        contentResult.Content.All(x => allUsersWithoutPasswordsAndSalts.Contains(x)));
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

        private List<User> GetAllUsersWithoutPasswordsAndSalts()
        {
            User firstUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
            User secondUser = new User("Ignacio", "valle@gmail.com", "#designPatternsLover123", AdminRole.GetInstance());
            firstUser.ClearPassword();
            firstUser.ClearSalt();
            secondUser.ClearPassword();
            secondUser.ClearSalt();
            return new List<User>
            {
                firstUser,
                secondUser
            };

        }

        private List<User> GetAllUsersWithPasswordsAndSalts()
        {
            User firstUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
            User secondUser = new User("Ignacio", "valle@gmail.com", "#designPatternsLover123", AdminRole.GetInstance());
            return new List<User>
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
            mockUserService.Setup(x => x.GetSingleUser(singleUser.ID, It.IsAny<string>())).Returns(singleUser);
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.GetUser(singleUser.ID);
            OkNegotiatedContentResult<User> contentResult = (OkNegotiatedContentResult<User>)actionResult;

            Assert.AreEqual(contentResult.Content, singleUser);
        }

        [TestMethod]
        public void NotReturnThePasswordNeitherTheSaltWhenSingleUserIsReturned()
        {
            User singleUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.GetSingleUser(singleUser.ID, It.IsAny<string>())).Returns(singleUser);
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.GetUser(singleUser.ID);
            OkNegotiatedContentResult<User> contentResult = (OkNegotiatedContentResult<User>)actionResult;

            Assert.AreEqual(contentResult.Content, singleUser);
            Assert.IsFalse(contentResult.Content.HasPassword());
            Assert.IsFalse(contentResult.Content.HasSalt());
        }

        [TestMethod]
        public void FailWithNotFoundIfSingleUserIdDoesntExistInRepository()
        {
            User singleUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.GetSingleUser(singleUser.ID + 1, It.IsAny<string>())).Throws(new ArgumentOutOfRangeException());
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.GetUser(singleUser.ID + 1);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void BeAbleToReturnAllRolesOfASpecificUser()
        {
            User singleUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
            var mockUserService = new Mock<IUserService>();
            IEnumerable<string> rolesToReturn = new List<string>() { "Cashier" };
            mockUserService.Setup(x => x.GetRolesOfUser(singleUser.Email, It.IsAny<string>())).Returns(rolesToReturn);
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.GetRolesOfUser(singleUser.Email);
            OkNegotiatedContentResult<IEnumerable<string>> contentResult = (OkNegotiatedContentResult<IEnumerable<string>>)actionResult;

            CollectionAssert.AreEqual((ICollection) contentResult.Content, (ICollection) rolesToReturn);
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

            Assert.AreEqual(contentResult.Content, singleUser);
        }

        [TestMethod]
        public void FailWithBadRequestIfPostedNewUserIsAlreadyInRepository()
        {
            User singleUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.CreateUser(singleUser, It.IsAny<string>())).Throws(new InvalidOperationException());
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.PostUser(singleUser);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void FailWithBadRequestIfPostedNewUserIsNotCompleteInRepository()
        {
            User incompleteUser = new User();
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.CreateUser(incompleteUser, It.IsAny<string>())).Throws(new ArgumentException());
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.PostUser(incompleteUser);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void FailWithBadRequestIfPostedNewUserIsNull()
        {
            User nullUser = null;
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.CreateUser(nullUser, It.IsAny<string>())).Throws(new ArgumentNullException());
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.PostUser(nullUser);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
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
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void FailWithBadRequestIfUpdatedUserIsNull()
        {
            User nullUser = null;
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.UpdateUser(1, nullUser, It.IsAny<string>())).Returns(false);
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.PutUser(1, nullUser);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
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
