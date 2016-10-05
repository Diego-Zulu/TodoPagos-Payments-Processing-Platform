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

namespace TodoPagos.WebApi.Tests
{
    [TestClass]
    public class UserControllerShould
    {
        [TestMethod]
        public void RecieveAUserServiceOnCreation()
        {
            var mockUserService = new Mock<IUserService>();

            UserController controller = new UserController(mockUserService.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfUserServiceIsNullOnCreation()
        {
            IUserService nullUserService = null;

            UserController controller = new UserController(nullUserService);
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
            UserController controller = new UserController(mockUserService.Object);

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
            UserController controller = new UserController(mockUserService.Object);

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
            UserController controller = new UserController(mockUserService.Object);

            IHttpActionResult actionResult = controller.GetUser(singleUser.ID + 1);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void BeAbleToPostNewUserIntoRepository()
        {
            User singleUser = new User("Gabriel", "gpiffaretti@gmail.com", "Wololo1234!", CashierRole.GetInstance());
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.CreateUser(singleUser)).Returns(1);
            UserController controller = new UserController(mockUserService.Object);

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
            UserController controller = new UserController(mockUserService.Object);

            IHttpActionResult actionResult = controller.PostUser(singleUser);
            
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }
    }
}
