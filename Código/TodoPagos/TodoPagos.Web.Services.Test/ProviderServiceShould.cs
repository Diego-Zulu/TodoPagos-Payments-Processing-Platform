using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain.Repository;
using Moq;
using System.Collections.Generic;
using TodoPagos.Domain;
using TodoPagos.UserAPI;

namespace TodoPagos.Web.Services.Tests
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
        public void BeAbleToGetAllActiveProvidersFromRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.ProviderRepository.Get(
                It.IsAny<System.Linq.Expressions.Expression<Func<Provider, bool>>>(), null, ""));
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            bool getActiveProviders = true;
            IEnumerable<Provider> returnedProviders = providerService.GetAllProvidersAcoordingToState(getActiveProviders);

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
        public void FailWithArgumentExceptionIfSingleProviderDoesntExistInRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.ProviderRepository.GetByID(It.IsAny<int>()));
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            Provider returnedProvider = providerService.GetSingleProvider(5);
        }

        [TestMethod]
        public void BeAbleToCreateNewProviderInRepository()
        {
            User receivedUser = new User("Bruno", "bferr42@gmail.com", "#ElBizagra1996", AdminRole.GetInstance());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(receivedUser.Email, ProviderManagementPrivilege.GetInstance())).Returns(true);
            mockUnitOfWork.Setup(un => un.ProviderRepository.Insert(It.IsAny<Provider>()));
            mockUnitOfWork.Setup(un => un.Save());
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            Provider oneProvider = new Provider("UTE", 60, new List<IField>());
            int id = providerService.CreateProvider(oneProvider, receivedUser.Email);

            mockUnitOfWork.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfToBeCreatedNewProviderIsNotComplete()
        {
            Provider incompleteProvider = new Provider();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
            .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ProviderManagementPrivilege.GetInstance()))
            .Returns(true);
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);
            
            int id = providerService.CreateProvider(incompleteProvider, It.IsAny<string>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FailWithNullArgumentExceptionIfToBeCreatedNewProviderIsNull()
        {
            Provider nullProvider = null;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
            .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ProviderManagementPrivilege.GetInstance()))
            .Returns(true);
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);
    
            int id = providerService.CreateProvider(nullProvider, It.IsAny<string>());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FailWithInvalidOperationExceptionIfToBeCreatedNewProviderHasTheNameOfAnActiveProviderInRepository()
        {
            Provider providerWithSameName = new Provider("UTE", 60, new List<IField>());
            Provider providerWithRepeatedName = new Provider("UTE", 25, new List<IField>());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.ProviderRepository.Get(
                It.IsAny<System.Linq.Expressions.Expression<Func<Provider, bool>>>(), null, "")).Returns(new[] { providerWithSameName });
            mockUnitOfWork
            .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ProviderManagementPrivilege.GetInstance()))
            .Returns(true);
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            int id = providerService.CreateProvider(providerWithRepeatedName, It.IsAny<string>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfToBeCreatedNewProviderIsMarkedAsDeleted()
        {

            Provider deletedProvider = new Provider("UTE", 60, new List<IField>());
            deletedProvider.MarkAsInactiveToShowItIsDeleted();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
            .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ProviderManagementPrivilege.GetInstance()))
            .Returns(true);
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);
            
            int id = providerService.CreateProvider(deletedProvider, It.IsAny<string>());
        }

        [TestMethod]
        public void BeAbleToInsertUpdatedProviderAsMarkOldProviderAsDeletedWhenUpdatingExistingProvider()
        {
            User receivedUser = new User("Bruno", "bferr42@gmail.com", "#ElBizagra1996", AdminRole.GetInstance());
            Provider toBeUpdatedProvider = new Provider("AntelData", 60, new List<IField>());
            Provider updatedProvider = new Provider("Antel", 20, new List<IField>());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine1(mockUnitOfWork, toBeUpdatedProvider);
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            bool updated = providerService.UpdateProvider(toBeUpdatedProvider.ID, updatedProvider, receivedUser.Email);

            mockUnitOfWork.Verify(un => un.ProviderRepository.Insert(updatedProvider), Times.Exactly(1));
            mockUnitOfWork.Verify(un => un.ProviderRepository.Update(toBeUpdatedProvider), Times.Exactly(1));
            mockUnitOfWork.Verify(un => un.Save(), Times.Exactly(1));
            Assert.IsTrue(updated);
            Assert.IsFalse(toBeUpdatedProvider.Active);
        }

        private void SetMockUpdateRoutine1(Mock<IUnitOfWork> mockUnitOfWork, Provider toBeUpdatedProvider)
        {
            mockUnitOfWork
                .Setup(un => un.ProviderRepository.GetByID(It.IsAny<int>()))
                .Returns(() => toBeUpdatedProvider);
            mockUnitOfWork.Setup(un => un.CurrentSignedInUserHasRequiredPrivilege
            (It.IsAny<string>(), ProviderManagementPrivilege.GetInstance())).Returns(true);
            mockUnitOfWork.Setup(un => un.ProviderRepository.Update(It.IsAny<Provider>()));
            mockUnitOfWork.Setup(un => un.Save());
        }

        [TestMethod]
        public void NotUpdateProviderIfNotComplete()
        {
            Provider toBeUpdatedProvider = new Provider("AntelData", 60, new List<IField>());
            Provider updatedProvider = new Provider("AntelData", 20, new List<IField>());
            updatedProvider.Name = "";
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine2(mockUnitOfWork, toBeUpdatedProvider);
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            bool updated = providerService.UpdateProvider(toBeUpdatedProvider.ID, updatedProvider, It.IsAny<string>());

            mockUnitOfWork.Verify(un => un.ProviderRepository.Update(It.IsAny<Provider>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            Assert.IsFalse(updated);
        }

        private void SetMockUpdateRoutine2(Mock<IUnitOfWork> mockUnitOfWork, Provider toBeUpdatedProvider)
        {
            mockUnitOfWork
                .Setup(un => un.ProviderRepository.GetByID(It.IsAny<int>()))
                .Returns(() => toBeUpdatedProvider);
            mockUnitOfWork
                .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ProviderManagementPrivilege.GetInstance()))
                .Returns(true);
            mockUnitOfWork.Setup(un => un.ProviderRepository.Update(It.IsAny<Provider>()));
            mockUnitOfWork.Setup(un => un.Save());
        }

        [TestMethod]
        public void NotUpdateNonExistingProvider()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine3(mockUnitOfWork);
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            bool updated = providerService.UpdateProvider(0, new Provider(), It.IsAny<string>());

            mockUnitOfWork.Verify(un => un.ProviderRepository.Update(It.IsAny<Provider>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            Assert.IsFalse(updated);
        }

        private void SetMockUpdateRoutine3(Mock<IUnitOfWork> mockUnitOfWork)
        {
            mockUnitOfWork
                .Setup(un => un.ProviderRepository.GetByID(It.IsAny<int>()))
                .Returns(() => null);
            mockUnitOfWork
                .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ProviderManagementPrivilege.GetInstance()))
                .Returns(true);
            mockUnitOfWork.Setup(un => un.ProviderRepository.Update(It.IsAny<Provider>()));
            mockUnitOfWork.Setup(un => un.Save());
        }

        [TestMethod]
        public void NotUpdateNullProvider()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockUpdateRoutine4(mockUnitOfWork);
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            bool updated = providerService.UpdateProvider(0, null, It.IsAny<string>());

            mockUnitOfWork.Verify(un => un.ProviderRepository.Update(It.IsAny<Provider>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            Assert.IsFalse(updated);
        }

        private void SetMockUpdateRoutine4(Mock<IUnitOfWork> mockUnitOfWork)
        {
            mockUnitOfWork
                .Setup(un => un.ProviderRepository.GetByID(It.IsAny<int>()))
                .Returns(() => null);
            mockUnitOfWork
                .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ProviderManagementPrivilege.GetInstance()))
                .Returns(true);
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

            bool updated = providerService.UpdateProvider(toBeUpdatedProvider.ID + 1, toBeUpdatedProvider, It.IsAny<string>());

            mockUnitOfWork.Verify(un => un.ProviderRepository.Update(It.IsAny<Provider>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            Assert.IsFalse(updated);
        }

        private void SetMockUpdateRoutine5(Mock<IUnitOfWork> mockUnitOfWork)
        {
            mockUnitOfWork
                .Setup(un => un.ProviderRepository.GetByID(It.IsAny<int>()))
                .Returns(() => new Provider());
            mockUnitOfWork
                .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ProviderManagementPrivilege.GetInstance()))
                .Returns(true);
            mockUnitOfWork.Setup(un => un.ProviderRepository.Update(It.IsAny<Provider>()));
            mockUnitOfWork.Setup(un => un.Save());
        }

        [TestMethod]
        public void NotUpdateProviderIfUpdatedInfoHasTheNameOfAnActiveProviderInRepositoryDiferentThanIt()
        {
            Provider providerWithSameName = new Provider("UTE", 60, new List<IField>());
            Provider providerWithUpdatedInfo = new Provider("UTE", 25, new List<IField>());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(un => un.ProviderRepository.GetByID(It.IsAny<int>()))
                .Returns(() => new Provider());
            mockUnitOfWork
                .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ProviderManagementPrivilege.GetInstance()))
                .Returns(true);
            mockUnitOfWork.Setup(un => un.ProviderRepository.Get(
                It.IsAny<System.Linq.Expressions.Expression<Func<Provider, bool>>>(), null, "")).Returns(new[] { providerWithSameName });
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            bool updated = providerService.UpdateProvider(0, new Provider(), It.IsAny<string>());

            mockUnitOfWork.Verify(un => un.ProviderRepository.Update(It.IsAny<Provider>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            Assert.IsFalse(updated);
        }

        [TestMethod]
        public void NotUpdateIfUpdatedProviderIsMarkedAsDeleted()
        {

            Provider deletedProvider = new Provider("UTE", 60, new List<IField>());
            deletedProvider.MarkAsInactiveToShowItIsDeleted();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ProviderManagementPrivilege.GetInstance()))
                .Returns(true);
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            bool updated = providerService.UpdateProvider(0, new Provider(), It.IsAny<string>());

            mockUnitOfWork.Verify(un => un.ProviderRepository.Update(It.IsAny<Provider>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            Assert.IsFalse(updated);
        }

        [TestMethod]
        public void NotUpdateIfToBeUpdatedProviderInRepositoryIsMarkedAsDeleted()
        {
            Provider providerWithUpdatedInfo = new Provider("UTE", 20, new List<IField>());
            Provider markedAsDeletedInRepositoryProvider = new Provider("UTE", 60, new List<IField>());
            markedAsDeletedInRepositoryProvider.MarkAsInactiveToShowItIsDeleted();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(un => un.ProviderRepository.GetByID(It.IsAny<int>()))
                .Returns(() => markedAsDeletedInRepositoryProvider);
            mockUnitOfWork
                .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ProviderManagementPrivilege.GetInstance()))
                .Returns(true);
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            bool updated = providerService.UpdateProvider(markedAsDeletedInRepositoryProvider.ID, providerWithUpdatedInfo, It.IsAny<string>());

            mockUnitOfWork.Verify(un => un.ProviderRepository.Update(It.IsAny<Provider>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            Assert.IsFalse(updated);
        }

        [TestMethod]
        public void BeAbleToMarkAsDeletedAProviderFromRepository()
        {
            User receivedUser = new User("Bruno", "bferr42@gmail.com", "#ElBizagra1996", AdminRole.GetInstance());
            Provider toBeDeletedProvider = new Provider("AntelData", 60, new List<IField>());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockDeleteRoutine1(mockUnitOfWork, toBeDeletedProvider);
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            bool deleted = providerService.MarkProviderAsDeleted(2, receivedUser.Email);

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
            mockUnitOfWork.Setup(un => un.CurrentSignedInUserHasRequiredPrivilege
            (It.IsAny<string>(), ProviderManagementPrivilege.GetInstance())).Returns(true);
            mockUnitOfWork.Setup(un => un.ProviderRepository.Update(toBeDeletedProvider));
            mockUnitOfWork.Setup(un => un.Save());
        }

        [TestMethod]
        public void NotModifyAnythingIfProviderIdDoesntExistInRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockDeleteRoutine2(mockUnitOfWork);
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            bool deleted = providerService.MarkProviderAsDeleted(2, It.IsAny<string>());

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
            mockUnitOfWork
                .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ProviderManagementPrivilege.GetInstance()))
                .Returns(true);
        }

        [TestMethod]
        public void NotModifyAnythingAndNotFailIfProviderInRepositoryIsAlreadyMarkedAsDeleted()
        {
            Provider alreadyDeletedProvider = new Provider("AntelData", 60, new List<IField>());
            alreadyDeletedProvider.MarkAsInactiveToShowItIsDeleted();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            SetMockDeleteRoutine3(mockUnitOfWork, alreadyDeletedProvider);
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            bool deleted = providerService.MarkProviderAsDeleted(2, It.IsAny<string>());

            mockUnitOfWork.Verify(un => un.ProviderRepository.Update(It.IsAny<Provider>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            mockUnitOfWork.Verify(un => un.ProviderRepository.Delete(It.IsAny<int>()), Times.Never());
            Assert.IsTrue(deleted);
        }

        private void SetMockDeleteRoutine3(Mock<IUnitOfWork> mockUnitOfWork, Provider alreadyDeletedProvider)
        {
            mockUnitOfWork
                .Setup(un => un.ProviderRepository.GetByID(It.IsAny<int>()))
                .Returns(() => alreadyDeletedProvider);
            mockUnitOfWork
                .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ProviderManagementPrivilege.GetInstance()))
                .Returns(true);
        }

        [TestMethod]
        public void ReturnTrueWhenUpdatingAProviderAndToBeUpdatedProviderIsCompletelyEqualToModifiedProviderButNotModifyAnything()
        {
            Provider providerWithUpdatedInfo = new Provider("UTE", 20, new List<IField>());
            Provider providerInRepository = new Provider("UTE", 20, new List<IField>());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(un => un.ProviderRepository.GetByID(It.IsAny<int>()))
                .Returns(() => providerInRepository);
            mockUnitOfWork
                .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ProviderManagementPrivilege.GetInstance()))
                .Returns(true);
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            bool updated = providerService.UpdateProvider(providerInRepository.ID, providerWithUpdatedInfo, It.IsAny<string>());

            mockUnitOfWork.Verify(un => un.ProviderRepository.Update(It.IsAny<Provider>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            Assert.IsTrue(updated);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void FailWithUnauthorizedAccessExceptionIfUserTriesToAccessAnythingWithoutProviderManagementPrivilege()
        {
            Provider providerToCreate = new Provider("Antel", 20, new List<IField>());
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(un => un.CurrentSignedInUserHasRequiredPrivilege(It.IsAny<string>(), ProviderManagementPrivilege.GetInstance()))
                .Returns(false);
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            int id = providerService.CreateProvider(providerToCreate, It.IsAny<string>());
        }
    }
}
