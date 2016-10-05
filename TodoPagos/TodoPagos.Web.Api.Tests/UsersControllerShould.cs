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
    }
}
