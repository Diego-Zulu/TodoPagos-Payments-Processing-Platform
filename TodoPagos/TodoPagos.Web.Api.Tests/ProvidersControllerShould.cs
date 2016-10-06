using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TodoPagos.Web.Api.Controllers;
using TodoPagos.Web.Services;
using TodoPagos.Domain;
using System.Web.Http;
using System.Web.Http.Results;

namespace TodoPagos.Web.Api.Tests
{
    [TestClass]
    public class ProvidersControllerShould
    {
        [TestMethod]
        public void RecieveAProviderServiceOnCreation()
        {
            var mockProviderService = new Mock<IProviderService>();

            ProvidersController controller = new ProvidersController(mockProviderService.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfProviderServiceIsNullOnCreation()
        {
            IProviderService nullProviderService = null;

            ProvidersController controller = new ProvidersController(nullProviderService);
        }

        [TestMethod]
        public void BeAbleToReturnAllProvidersInRepository()
        {
            var allProviders = new[]
            {
                new Provider("Antel", 10, new List<IField>()),
            new Provider("Devoto", 15, new List<IField>())
        };
            var mockProviderService = new Mock<IProviderService>();
            mockProviderService.Setup(x => x.GetAllProviders()).Returns(allProviders);
            ProvidersController controller = new ProvidersController(mockProviderService.Object);

            IHttpActionResult actionResult = controller.GetProviders();
            OkNegotiatedContentResult<IEnumerable<Provider>> contentResult = (OkNegotiatedContentResult<IEnumerable<Provider>>)actionResult;

            Assert.AreSame(contentResult.Content, allProviders);
        }

        [TestMethod]
        public void BeAbleToReturnASingleProviderFromRepository()
        {
            Provider singleProvider = new Provider("Antel", 10, new List<IField>());
            var mockProviderService = new Mock<IProviderService>();
            mockProviderService.Setup(x => x.GetSingleProvider(singleProvider.ID)).Returns(singleProvider);
            ProvidersController controller = new ProvidersController(mockProviderService.Object);

            IHttpActionResult actionResult = controller.GetProvider(singleProvider.ID);
            OkNegotiatedContentResult<Provider> contentResult = (OkNegotiatedContentResult<Provider>)actionResult;

            Assert.AreSame(contentResult.Content, singleProvider);
        }

        [TestMethod]
        public void FailWithNotFoundIfUserIdIsNotInRepository()
        {
            var mockProviderService = new Mock<IProviderService>();
            mockProviderService.Setup(x => x.GetSingleProvider(1)).Throws(new ArgumentOutOfRangeException());
            ProvidersController controller = new ProvidersController(mockProviderService.Object);

            IHttpActionResult actionResult = controller.GetProvider(1);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}
