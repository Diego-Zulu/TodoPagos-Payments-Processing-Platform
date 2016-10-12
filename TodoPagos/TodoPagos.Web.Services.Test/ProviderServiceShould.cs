using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain.Repository;
using Moq;
using System.Collections.Generic;
using TodoPagos.Domain;

namespace TodoPagos.Web.Services.Test
{
    [TestClass]
    public class ProviderServiceShould
    {
        [TestMethod]
        public void ReceiveAUnitOfWorkOnCreation()
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
            mockUnitOfWork.Setup(un => un.ProviderRepository.Get(null, null, ""));
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            IEnumerable<Provider> returnedProviders = providerService.GetAllProviders();

            mockUnitOfWork.VerifyAll();
        }

        [TestMethod]
        public void BeAbleToReturnSingleProvicerFromRepository()
        {
            Provider singleProvider = new Provider("UTE", 60, new List<IField>());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.ProviderRepository.GetByID(singleProvider.ID)).Returns(singleProvider);
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            Provider returnedProvider = providerService.GetSingleProvider(singleProvider.ID);

            mockUnitOfWork.VerifyAll();
            Assert.AreSame(singleProvider, returnedProvider);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfSingleProviderdDoesntExistInRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.ProviderRepository.GetByID(It.IsAny<int>()));
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            Provider returnedProvider = providerService.GetSingleProvider(5);
        }

        [TestMethod]
        public void BeAbleToCreateNewProviderInRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.ProviderRepository.Insert(It.IsAny<Provider>()));
            mockUnitOfWork.Setup(un => un.Save());
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            Provider oneProvider = new Provider("UTE", 60, new List<IField>());
            int id = providerService.CreateProvider(oneProvider);

            mockUnitOfWork.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfToBeCreatedNewProviderIsNotComplete()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.ProviderRepository.Insert(It.IsAny<Provider>()));
            mockUnitOfWork.Setup(un => un.Save());
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            Provider incompleteProvider = new Provider();
            int id = providerService.CreateProvider(incompleteProvider);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FailWithNullArgumentExceptionIfToBeCreatedNewProviderIsNull()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.ProviderRepository.Insert(It.IsAny<Provider>()));
            mockUnitOfWork.Setup(un => un.Save());
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            Provider nullProvider = null;
            int id = providerService.CreateProvider(nullProvider);
        }

        [TestMethod]
        public void BeAbleToUpdateExistingProvider()
        {
            Provider toBeUpdatedProvider = new Provider("AntelData", 60, new List<IField>());
            Provider updatedProvider = new Provider("Antel", 20, new List<IField>());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine1(mockUnitOfWork, toBeUpdatedProvider);
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            bool updated = providerService.UpdateProvider(toBeUpdatedProvider.ID, updatedProvider);

            mockUnitOfWork.Verify(un => un.ProviderRepository.Update(It.IsAny<Provider>()), Times.Exactly(1));
            mockUnitOfWork.Verify(un => un.Save(), Times.Exactly(1));
            Assert.IsTrue(updated);
        }

        private void SetMockUpdateRoutine1(Mock<IUnitOfWork> mockUnitOfWork, Provider toBeUpdatedProvider)
        {
            mockUnitOfWork
                .Setup(un => un.ProviderRepository.GetByID(It.IsAny<int>()))
                .Returns(() => toBeUpdatedProvider);
            mockUnitOfWork.Setup(un => un.ProviderRepository.Update(It.IsAny<Provider>()));
            mockUnitOfWork.Setup(un => un.Save());
        }

        [TestMethod]
        public void NotUpdateNotFilledInformation()
        {
            Provider toBeUpdatedProvider = new Provider("AntelData", 60, new List<IField>());
            Provider updatedProvider = new Provider("AntelData", 20, new List<IField>());
            updatedProvider.Name = "";
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine2(mockUnitOfWork, toBeUpdatedProvider);
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            bool updated = providerService.UpdateProvider(toBeUpdatedProvider.ID, updatedProvider);

            mockUnitOfWork.Verify(un => un.ProviderRepository.Update(It.IsAny<Provider>()), Times.Exactly(1));
            mockUnitOfWork.Verify(un => un.Save(), Times.Exactly(1));
            Assert.IsTrue(updated);
        }

        private void SetMockUpdateRoutine2(Mock<IUnitOfWork> mockUnitOfWork, Provider toBeUpdatedProvider)
        {
            mockUnitOfWork
                .Setup(un => un.ProviderRepository.GetByID(It.IsAny<int>()))
                .Returns(() => toBeUpdatedProvider);
            mockUnitOfWork.Setup(un => un.ProviderRepository.Update(It.IsAny<Provider>()));
            mockUnitOfWork.Setup(un => un.Save());
        }

        [TestMethod]
        public void NotUpdateNonExistingProvider()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine3(mockUnitOfWork);
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            bool updated = providerService.UpdateProvider(0, new Provider());

            mockUnitOfWork.Verify(un => un.ProviderRepository.Update(It.IsAny<Provider>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            Assert.IsFalse(updated);
        }

        private void SetMockUpdateRoutine3(Mock<IUnitOfWork> mockUnitOfWork)
        {
            mockUnitOfWork
                .Setup(un => un.ProviderRepository.GetByID(It.IsAny<int>()))
                .Returns(() => null);
            mockUnitOfWork.Setup(un => un.ProviderRepository.Update(It.IsAny<Provider>()));
            mockUnitOfWork.Setup(un => un.Save());
        }

        [TestMethod]
        public void NotUpdateNullProvider()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine4(mockUnitOfWork);
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            bool updated = providerService.UpdateProvider(0, null);

            mockUnitOfWork.Verify(un => un.ProviderRepository.Update(It.IsAny<Provider>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            Assert.IsFalse(updated);
        }

        private void SetMockUpdateRoutine4(Mock<IUnitOfWork> mockUnitOfWork)
        {
            mockUnitOfWork
                .Setup(un => un.ProviderRepository.GetByID(It.IsAny<int>()))
                .Returns(() => null);
            mockUnitOfWork.Setup(un => un.ProviderRepository.Update(It.IsAny<Provider>()));
            mockUnitOfWork.Setup(un => un.Save());
        }

        [TestMethod]
        public void NotUpdateProviderWithDifferentIdThanSupplied()
        {
            Provider toBeUpdatedProvider = new Provider("AntelData", 60, new List<IField>());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine5(mockUnitOfWork);
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            bool updated = providerService.UpdateProvider(toBeUpdatedProvider.ID + 1, toBeUpdatedProvider);

            mockUnitOfWork.Verify(un => un.ProviderRepository.Update(It.IsAny<Provider>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            Assert.IsFalse(updated);
        }

        private void SetMockUpdateRoutine5(Mock<IUnitOfWork> mockUnitOfWork)
        {
            mockUnitOfWork
                .Setup(un => un.ProviderRepository.GetByID(It.IsAny<int>()))
                .Returns(() => new Provider());
            mockUnitOfWork.Setup(un => un.ProviderRepository.Update(It.IsAny<Provider>()));
            mockUnitOfWork.Setup(un => un.Save());
        }

        [TestMethod]
        public void BeAbleToMarkAsDeletedAProviderFromRepository()
        {
            Provider toBeDeletedProvider = new Provider("AntelData", 60, new List<IField>());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockDeleteRoutine1(mockUnitOfWork, toBeDeletedProvider);
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            bool deleted = providerService.MarkProviderAsDeleted(2);

            mockUnitOfWork.Verify(un => un.ProviderRepository.Update(It.IsAny<Provider>()), Times.Exactly(1));
            mockUnitOfWork.Verify(un => un.Save(), Times.Exactly(1));
            mockUnitOfWork.Verify(un => un.ProviderRepository.Delete(It.IsAny<int>()), Times.Never());
            Assert.IsTrue(deleted);
            Assert.IsFalse(toBeDeletedProvider.Active);
        }

        private void SetMockDeleteRoutine1(Mock<IUnitOfWork> mockUnitOfWork, Provider toBeDeletedProvider)
        {
            mockUnitOfWork
                .Setup(un => un.ProviderRepository.GetByID(It.IsAny<int>()))
                .Returns(() => toBeDeletedProvider);
            mockUnitOfWork.Setup(un => un.ProviderRepository.Update(toBeDeletedProvider));
            mockUnitOfWork.Setup(un => un.Save());
        }

        [TestMethod]
        public void NotModifyAnythingIfProviderIdDoesntExistInRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockDeleteRoutine2(mockUnitOfWork);
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            bool deleted = providerService.MarkProviderAsDeleted(2);

            mockUnitOfWork.Verify(un => un.ProviderRepository.Update(It.IsAny<Provider>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            mockUnitOfWork.Verify(un => un.ProviderRepository.Delete(It.IsAny<int>()), Times.Never());
            Assert.IsFalse(deleted);
        }

        private void SetMockDeleteRoutine2(Mock<IUnitOfWork> mockUnitOfWork)
        {
            mockUnitOfWork
                .Setup(un => un.ProviderRepository.GetByID(It.IsAny<int>()))
                .Returns(() => null);
        }

        [TestMethod]
        public void NotModifyAnythingIfProviderInRepositoryIsAlreadyMarkedAsDeleted()
        {
            Provider alreadyDeletedProvider = new Provider("AntelData", 60, new List<IField>());
            alreadyDeletedProvider.MarkAsInactiveToShowItIsDeleted();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockDeleteRoutine3(mockUnitOfWork, alreadyDeletedProvider);
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            bool deleted = providerService.MarkProviderAsDeleted(2);

            mockUnitOfWork.Verify(un => un.ProviderRepository.Update(It.IsAny<Provider>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            mockUnitOfWork.Verify(un => un.ProviderRepository.Delete(It.IsAny<int>()), Times.Never());
            Assert.IsFalse(deleted);
        }

        private void SetMockDeleteRoutine3(Mock<IUnitOfWork> mockUnitOfWork, Provider alreadyDeletedProvider)
        {
            mockUnitOfWork
                .Setup(un => un.ProviderRepository.GetByID(It.IsAny<int>()))
                .Returns(() => alreadyDeletedProvider);
        }
    }
}
