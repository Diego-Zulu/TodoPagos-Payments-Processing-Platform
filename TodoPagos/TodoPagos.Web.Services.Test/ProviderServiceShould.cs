using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain.Repository;
using Moq;
using System.Collections.Generic;
using TodoPagos.UserAPI;

namespace TodoPagos.Web.Services.Test
{
    [TestClass]
    public class ProviderServiceShould
    {
        [TestMethod]
        public void RecieveAUnitOfWorkOnCreation()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            ProviderService service = new ProviderService(mockUnitOfWork.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfUnitOfWorkOnCreationIsNull()
        {
            IUnitOfWork mockUnitOfWork = null;

            ProviderService service = new ProviderService(mockUnitOfWork);
        }

        [TestMethod]
        public void BeAbleToGetAllProvidersFromRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.UserRepository.Get(null, null, ""));
            ProviderService userService = new ProviderService(mockUnitOfWork.Object);

            IEnumerable<Provider> returnedUsers = userService.GetAllUsers();

            mockUnitOfWork.VerifyAll();
        }
    }
}
