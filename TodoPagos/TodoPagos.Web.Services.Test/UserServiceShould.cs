using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain.Repository;
using Moq;
using System.Collections.Generic;
using TodoPagos.UserAPI;

namespace TodoPagos.Web.Services.Test
{
    [TestClass]
    public class UserServiceShould
    {
        [TestMethod]
        public void RecieveAUnitOfWorkOnCreation()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            UserService service = new UserService(mockUnitOfWork.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfUnitOfWorkOnCreationIsNull()
        {
            IUnitOfWork mockUnitOfWork = null;

            UserService service = new UserService(mockUnitOfWork);
        }

        [TestMethod]
        public void BeAbleToGetAllUsersFromRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.UserRepository.Get(null, null, ""));
            UserService userService = new UserService(mockUnitOfWork.Object);

            IEnumerable<User> returnedUsers = userService.GetAllUsers();

            mockUnitOfWork.VerifyAll();
        }
    }
}
