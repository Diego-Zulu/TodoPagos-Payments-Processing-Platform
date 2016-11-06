using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain.Repository;
using Moq;
using TodoPagos.UserAPI;
using TodoPagos.Domain;
using System.Collections.Generic;

namespace TodoPagos.Web.Services.Test
{
    [TestClass]
    public class ClientServiceShould
    {
        [TestMethod]
        public void ReceiveAUnitOfWorkOnCreation()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            ClientService service = new ClientService(mockUnitOfWork.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfUnitOfWorkOnCreationIsNull()
        {
            IUnitOfWork mockUnitOfWork = null;

            ClientService service = new ClientService(mockUnitOfWork);
        }

        [TestMethod]
        public void BeAbleToGetAllClientsFromRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
            .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(
                It.IsAny<string>(), UserManagementPrivilege.GetInstance()))
            .Returns(true);
            mockUnitOfWork.Setup(un => un.ClientRepository.Get(null, null, ""));
            ClientService clientService = new ClientService(mockUnitOfWork.Object);

            clientService.GetAllClients(It.IsAny<string>());
            mockUnitOfWork.VerifyAll();
        }

        [TestMethod]
        public void BeAbleToReturnSingleClientsInRepository()
        {
            Client singleClient = new Client("Ruben Rada", "11111111", "26666666");
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
            .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(
                It.IsAny<string>(), UserManagementPrivilege.GetInstance()))
            .Returns(true);
            mockUnitOfWork.Setup(un => un.ClientRepository.GetByID(It.IsAny<int>())).Returns(singleClient);
            ClientService clientService = new ClientService(mockUnitOfWork.Object);

            Client returnedClient = clientService.GetSingleClient(singleClient.ID, It.IsAny<string>());

            mockUnitOfWork.VerifyAll();
            Assert.AreSame(singleClient, returnedClient);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfSingleClientsIdDoesntExistInRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.ClientRepository.GetByID(It.IsAny<int>()));
            mockUnitOfWork
            .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(
                It.IsAny<string>(), UserManagementPrivilege.GetInstance()))
            .Returns(true);
            ClientService clientService = new ClientService(mockUnitOfWork.Object);

            Client returnedClient = clientService.GetSingleClient(5, It.IsAny<string>());
        }
    }
}
