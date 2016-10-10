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
            mockUnitOfWork.Setup(un => un.ProviderRepository.Get(null, null, ""));
            ProviderService providerService = new ProviderService(mockUnitOfWork.Object);

            IEnumerable<Provider> returnedProviders = providerService.GetAllProviders();

            mockUnitOfWork.VerifyAll();
        }

        [TestMethod]
        public void BeAbleToReturnSingleProviderFromRepository()
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
    }
}
