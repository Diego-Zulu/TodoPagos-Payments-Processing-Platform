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

        [TestMethod]
        public void BeAbleToCreateNewClientInRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), UserManagementPrivilege.GetInstance())).Returns(true);
            mockUnitOfWork.Setup(un => un.ClientRepository.Insert(It.IsAny<Client>()));
            mockUnitOfWork.Setup(un => un.Save());
            ClientService clientService = new ClientService(mockUnitOfWork.Object);

            Client singleClient = new Client("Ruben Rada", "11111111", "26666666");
            int id = clientService.CreateClient(singleClient, It.IsAny<string>());

            mockUnitOfWork.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FailWithInvalidOperationExceptionIfToBeCreatedNewClientIsNotComplete()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), UserManagementPrivilege.GetInstance())).Returns(true);
            mockUnitOfWork.Setup(un => un.ClientRepository.Get(
               It.IsAny<System.Linq.Expressions.Expression<Func<Client, bool>>>(), null, "")).Returns(new List<Client>());
            ClientService clientService = new ClientService(mockUnitOfWork.Object);

            Client singleClient = new Client("Ruben Rada", "11111111", "26666666");
            singleClient.Name = "";
            int id = clientService.CreateClient(singleClient, It.IsAny<string>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FailWithArgumentNullExceptionIfToBeCreatedNewClientIsNull()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), UserManagementPrivilege.GetInstance())).Returns(true);
            ClientService clientService = new ClientService(mockUnitOfWork.Object);

            Client nullClient = null;
            int id = clientService.CreateClient(nullClient, It.IsAny<string>());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FailWithInvalidOperationExceptionIfToBeCreatedNewUserIsAlreadyInRepository()
        {
            Client repeatedClient = new Client("Ruben Rada", "11111111", "26666666");
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), UserManagementPrivilege.GetInstance())).Returns(true);
            mockUnitOfWork
                .Setup(un => un.ClientRepository.Get(
                It.IsAny<System.Linq.Expressions.Expression<Func<Client, bool>>>(), null, ""))
                .Returns(new[] { repeatedClient });

            ClientService clientService = new ClientService(mockUnitOfWork.Object);
            int id = clientService.CreateClient(repeatedClient, It.IsAny<string>());
        }
    }
}
