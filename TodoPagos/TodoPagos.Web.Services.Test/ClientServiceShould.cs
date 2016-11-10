using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain.Repository;
using Moq;
using TodoPagos.UserAPI;
using TodoPagos.Domain;
using System.Collections.Generic;
using System.Linq.Expressions;

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
                It.IsAny<string>(), ClientManagementPrivilege.GetInstance()))
            .Returns(true);
            mockUnitOfWork.Setup(un => un.ClientRepository.Get(null, null, ""));
            ClientService clientService = new ClientService(mockUnitOfWork.Object);

            clientService.GetAllClients(It.IsAny<string>());
            mockUnitOfWork.VerifyAll();
        }

        [TestMethod]
        public void BeAbleToReturnSingleClientsInRepository()
        {
            Client singleClient = new Client("Ruben Rada", "11111111", "26666666", "1112 28th NE");
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
            .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(
                It.IsAny<string>(), ClientManagementPrivilege.GetInstance()))
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
                It.IsAny<string>(), ClientManagementPrivilege.GetInstance()))
            .Returns(true);
            ClientService clientService = new ClientService(mockUnitOfWork.Object);

            Client returnedClient = clientService.GetSingleClient(5, It.IsAny<string>());
        }

        [TestMethod]
        public void BeAbleToCreateNewClientInRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ClientManagementPrivilege.GetInstance())).Returns(true);
            mockUnitOfWork.Setup(un => un.ClientRepository.Insert(It.IsAny<Client>()));
            mockUnitOfWork.Setup(un => un.Save());
            ClientService clientService = new ClientService(mockUnitOfWork.Object);

            Client singleClient = new Client("Ruben Rada", "11111111", "26666666", "1112 28th NE");
            int id = clientService.CreateClient(singleClient, It.IsAny<string>());

            mockUnitOfWork.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FailWithInvalidOperationExceptionIfToBeCreatedNewClientIsNotComplete()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ClientManagementPrivilege.GetInstance())).Returns(true);
            mockUnitOfWork.Setup(un => un.ClientRepository.Get(
               It.IsAny<System.Linq.Expressions.Expression<Func<Client, bool>>>(), null, "")).Returns(new List<Client>());
            ClientService clientService = new ClientService(mockUnitOfWork.Object);

            Client singleClient = new Client("Ruben Rada", "11111111", "26666666", "1112 28th NE");
            singleClient.Name = "";
            int id = clientService.CreateClient(singleClient, It.IsAny<string>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FailWithArgumentNullExceptionIfToBeCreatedNewClientIsNull()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ClientManagementPrivilege.GetInstance())).Returns(true);
            ClientService clientService = new ClientService(mockUnitOfWork.Object);

            Client nullClient = null;
            int id = clientService.CreateClient(nullClient, It.IsAny<string>());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FailWithInvalidOperationExceptionIfToBeCreatedNewClientIsAlreadyInRepository()
        {
            Client repeatedClient = new Client("Ruben Rada", "11111111", "26666666", "1112 28th NE");
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ClientManagementPrivilege.GetInstance())).Returns(true);
            mockUnitOfWork
                .Setup(un => un.ClientRepository.Get(
                It.IsAny<System.Linq.Expressions.Expression<Func<Client, bool>>>(), null, ""))
                .Returns(new[] { repeatedClient });

            ClientService clientService = new ClientService(mockUnitOfWork.Object);
            int id = clientService.CreateClient(repeatedClient, It.IsAny<string>());
        }

        [TestMethod]
        public void BeAbleToUpdateExistingClient()
        {
            Client toBeUpdatedClient = new Client("Ruben Rada", "11111111", "26666666", "1112 28th NE");
            Client updatedClient = new Client("Rada", "49018830", "26666667", "1112 28th NE");
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine1(mockUnitOfWork, toBeUpdatedClient);
            ClientService clientService = new ClientService(mockUnitOfWork.Object);

            bool updated = clientService.UpdateClient(toBeUpdatedClient.ID, updatedClient, It.IsAny<string>());

            mockUnitOfWork.Verify(un => un.ClientRepository.Update(It.IsAny<Client>()), Times.Exactly(1));
            mockUnitOfWork.Verify(un => un.Save(), Times.Exactly(1));
            Assert.IsTrue(updated);
        }

        private void SetMockUpdateRoutine1(Mock<IUnitOfWork> mockUnitOfWork, Client toBeUpdatedClient)
        {
            mockUnitOfWork
                .Setup(un => un.ClientRepository.GetByID(It.IsAny<int>()))
                .Returns(() => toBeUpdatedClient);
            mockUnitOfWork
            .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ClientManagementPrivilege.GetInstance()))
            .Returns(true);
            mockUnitOfWork.Setup(un => un.ClientRepository.Update(It.IsAny<Client>()));
            mockUnitOfWork.Setup(un => un.Save());
        }

        [TestMethod]
        public void NotUpdateNotFilledInformation()
        {
            Client toBeUpdatedClient = new Client("Ruben Rada", "11111111", "26666666", "1112 28th NE");
            Client updatedClient = new Client("Ruben Rada", "49018830", "26666667", "1112 28th NE");
            updatedClient.Name = "";
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine2(mockUnitOfWork, toBeUpdatedClient);
            ClientService clientService = new ClientService(mockUnitOfWork.Object);

            bool updated = clientService.UpdateClient(toBeUpdatedClient.ID, updatedClient, It.IsAny<string>());

            mockUnitOfWork.Verify(un => un.ClientRepository.Update(It.IsAny<Client>()), Times.Exactly(1));
            mockUnitOfWork.Verify(un => un.Save(), Times.Exactly(1));
            Assert.AreEqual(toBeUpdatedClient.Name, "Ruben Rada");
        }

        private void SetMockUpdateRoutine2(Mock<IUnitOfWork> mockUnitOfWork, Client toBeUpdatedClient)
        {
            mockUnitOfWork
                .Setup(un => un.ClientRepository.GetByID(It.IsAny<int>()))
                .Returns(() => toBeUpdatedClient);
            mockUnitOfWork
                .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ClientManagementPrivilege.GetInstance()))
                .Returns(true);
        }

        [TestMethod]
        public void NotUpdateNonExistingClient()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine3(mockUnitOfWork);
            ClientService clientService = new ClientService(mockUnitOfWork.Object);

            bool updated = clientService.UpdateClient(0, new Client() { }, It.IsAny<string>());

            mockUnitOfWork.Verify(un => un.ClientRepository.Update(It.IsAny<Client>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            Assert.IsFalse(updated);
        }

        private void SetMockUpdateRoutine3(Mock<IUnitOfWork> mockUnitOfWork)
        {
            mockUnitOfWork
                .Setup(un => un.ClientRepository.GetByID(It.IsAny<int>()))
                .Returns(() => null);
            mockUnitOfWork
                .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ClientManagementPrivilege.GetInstance()))
                .Returns(true);
        }

        [TestMethod]
        public void NotUpdateNullClient()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine4(mockUnitOfWork);
            ClientService clientService = new ClientService(mockUnitOfWork.Object);

            bool updated = clientService.UpdateClient(0, null, It.IsAny<string>());

            mockUnitOfWork.Verify(un => un.ClientRepository.Update(It.IsAny<Client>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            Assert.IsFalse(updated);
        }

        private void SetMockUpdateRoutine4(Mock<IUnitOfWork> mockUnitOfWork)
        {
            mockUnitOfWork
                .Setup(un => un.ClientRepository.GetByID(It.IsAny<int>()))
                .Returns(() => null);
            mockUnitOfWork
                .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ClientManagementPrivilege.GetInstance()))
                .Returns(true);
        }

        [TestMethod]
        public void NotUpdateClientWithDifferentIdThanSupplied()
        {
            Client updatedClientInfo = new Client("Ruben Rada", "11111111", "26666666", "1112 28th NE");
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine5(mockUnitOfWork);
            ClientService clientService = new ClientService(mockUnitOfWork.Object);

            bool updated = clientService.UpdateClient(updatedClientInfo.ID + 1, updatedClientInfo, It.IsAny<string>());

            mockUnitOfWork.Verify(un => un.ClientRepository.Update(It.IsAny<Client>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            Assert.IsFalse(updated);
        }

        private void SetMockUpdateRoutine5(Mock<IUnitOfWork> mockUnitOfWork)
        {
            mockUnitOfWork
                .Setup(un => un.ClientRepository.GetByID(It.IsAny<int>()))
                .Returns(() => new Client());
            mockUnitOfWork
                .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ClientManagementPrivilege.GetInstance()))
                .Returns(true);
        }

        [TestMethod]
        public void NotUpdateClientWithIDCardOfAnotherClientInRepository()
        {
            Client updatedClientInfo = new Client("Ruben Rada", "11111111", "26666666", "1112 28th NE");
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine6(mockUnitOfWork);
            ClientService clientService = new ClientService(mockUnitOfWork.Object);

            bool updated = clientService.UpdateClient(updatedClientInfo.ID + 1, updatedClientInfo, It.IsAny<string>());

            mockUnitOfWork.Verify(un => un.ClientRepository.Update(It.IsAny<Client>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            Assert.IsFalse(updated);
        }

        private void SetMockUpdateRoutine6(Mock<IUnitOfWork> mockUnitOfWork)
        {
            Client clientWithSameIDCard = new Client("Rada", "11111111", "26666667", "1112 28th NE");
            clientWithSameIDCard.ID = 5;
            mockUnitOfWork
                .Setup(un => un.ClientRepository.GetByID(It.IsAny<int>()))
                .Returns(() => new Client());
            mockUnitOfWork
                .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ClientManagementPrivilege.GetInstance()))
                .Returns(true);
            mockUnitOfWork
               .Setup(un => un.ClientRepository.Get(
               It.IsAny<System.Linq.Expressions.Expression<Func<Client, bool>>>(), null, ""))
               .Returns(new[] { clientWithSameIDCard });
        }

        [TestMethod]
        public void BeAbleToDeleteClientFromRepository()
        {
            Client singleClient = new Client("Rada", "11111111", "26666667", "1112 28th NE");
            singleClient.ID = 1;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockDeleteRoutine1(mockUnitOfWork, singleClient);
            ClientService clientService = new ClientService(mockUnitOfWork.Object);

            bool deleted = clientService.DeleteClient(1, "diego_i_zuluaga@outlook.com");

            mockUnitOfWork.Verify(un => un.ClientRepository.Delete(It.IsAny<int>()), Times.Exactly(1));
            mockUnitOfWork.Verify(un => un.Save(), Times.Exactly(1));
            Assert.IsTrue(deleted);
        }

        private void SetMockDeleteRoutine1(Mock<IUnitOfWork> mockUnitOfWork, Client singleClient)
        {
            mockUnitOfWork
                .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(
                    "diego_i_zuluaga@outlook.com", ClientManagementPrivilege.GetInstance()))
                .Returns(true);
            mockUnitOfWork
                .Setup(un => un.ClientRepository.GetByID(It.IsAny<int>()))
                .Returns(() => singleClient);
            mockUnitOfWork.Setup(un => un.ClientRepository.Delete(It.IsAny<int>()));
            mockUnitOfWork.Setup(un => un.Save());
        }

        [TestMethod]
        public void NotDeleteAnythingIfClientIdDoesntExistInRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockDeleteRoutine2(mockUnitOfWork);
            ClientService clientService = new ClientService(mockUnitOfWork.Object);

            bool deleted = clientService.DeleteClient(2, It.IsAny<string>());

            mockUnitOfWork.Verify(un => un.ClientRepository.Delete(It.IsAny<int>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            Assert.IsFalse(deleted);
        }

        private void SetMockDeleteRoutine2(Mock<IUnitOfWork> mockUnitOfWork)
        {
            mockUnitOfWork
                .Setup(un => un.ClientRepository.GetByID(It.IsAny<int>()))
                .Returns(() => null);
            mockUnitOfWork
            .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ClientManagementPrivilege.GetInstance()))
            .Returns(true);
            mockUnitOfWork.Setup(un => un.ClientRepository.Delete(It.IsAny<int>()));
            mockUnitOfWork.Setup(un => un.Save());
        }


        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void FailWithUnauthorizedAccessExceptionIfUserTriesToPostANewClientWithoutHavingClientManagementPrivilege()
        {
            Client singleClient = new Client("Rada", "11111111", "26666667", "1112 28th NE");
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ClientManagementPrivilege.GetInstance()))
                .Returns(false);
            ClientService clientService = new ClientService(mockUnitOfWork.Object);

            int id = clientService.CreateClient(singleClient, It.IsAny<string>());
        }
    }
}
