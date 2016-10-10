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
            IProviderService providerService = new ProviderService(mockUnitOfWork.Object);

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
            IProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            Provider returnedProvider = providerService.GetSingleProvider(5);
        }

        [TestMethod]
        public void BeAbleToCreateNewProviderInRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.ProviderRepository.Insert(It.IsAny<Provider>()));
            mockUnitOfWork.Setup(un => un.Save());
            IProviderService providerService = new ProviderService(mockUnitOfWork.Object);

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
            IProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            Provider nullProvider = null;
            int id = providerService.CreateProvider(nullProvider);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FailWithNullArgumentExceptionIfToBeCreatedNewProviderIsNull()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.ProviderRepository.Insert(It.IsAny<Provider>()));
            mockUnitOfWork.Setup(un => un.Save());
            IProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            Provider incompleteProvider = new Provider();
            int id = providerService.CreateProvider(incompleteProvider);
        }
    }
}
