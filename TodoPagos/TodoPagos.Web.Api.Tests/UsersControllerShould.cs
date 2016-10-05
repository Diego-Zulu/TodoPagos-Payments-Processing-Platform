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

namespace TodoPagos.WebApi.Tests
{
    [TestClass]
    public class UsersControllerShould
    {
        [TestMethod]
        public void RecieveAUserServiceOnCreation()
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
            var allUsers = new[]
            {
                new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance()),
            new User("Ignacio", "valle@gmail.com", "#designPatternsLover123", AdminRole.GetInstance())
        };
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.GetAllUsers()).Returns(allUsers);
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.GetUsers();
            OkNegotiatedContentResult<IEnumerable<User>> contentResult = (OkNegotiatedContentResult<IEnumerable<User>>)actionResult;

            Assert.AreSame(contentResult.Content, allUsers);
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
        public void FailIfSingleUserIdDoesntExistInRepository()
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
            mockUserService.Setup(x => x.CreateUser(singleUser)).Returns(1);
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.PostUser(singleUser);
            CreatedAtRouteNegotiatedContentResult<User> contentResult = (CreatedAtRouteNegotiatedContentResult<User>)actionResult;

            Assert.AreSame(contentResult.Content, singleUser);
        }

        [TestMethod]
        public void FailIfPostedNewUserIsAlreadyInRepository()
        {
            User singleUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.CreateUser(singleUser)).Throws(new InvalidOperationException());
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.PostUser(singleUser);
            
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void BeAbleToUpdateAnUserInTheRepository()
        {
            User singleUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.UpdateUser(singleUser.ID, singleUser)).Returns(true);
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.PutUser(singleUser.ID, singleUser);
            StatusCodeResult contentResult = (StatusCodeResult)actionResult;

            Assert.AreEqual(contentResult.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void FailIfUpdatedUserIdAndSuppliedIdAreDifferent()
        {
            User singleUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
            var mockUserService = new Mock<IUserService>();
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.PutUser(singleUser.ID + 1, singleUser);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void FailIfServiceCantFindToBeUpdatedUserInRepository()
        {
            User singleUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.UpdateUser(singleUser.ID, singleUser)).Returns(false);
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.PutUser(singleUser.ID, singleUser);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void BeAbleToDeleteAnUser()
        {
            User singleUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.DeleteUser(singleUser.ID)).Returns(true);
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.DeleteUser(singleUser.ID);
            StatusCodeResult contentResult = (StatusCodeResult)actionResult;

            Assert.AreEqual(contentResult.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void FailIfToBeDeletedUserDoesntExistInRepository()
        {
            User singleUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.DeleteUser(singleUser.ID)).Returns(false);
            UsersController controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.DeleteUser(singleUser.ID);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}
