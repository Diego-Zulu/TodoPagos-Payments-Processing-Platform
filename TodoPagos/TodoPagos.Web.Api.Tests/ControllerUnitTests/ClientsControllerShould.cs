using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Web.Services;
using TodoPagos.Web.Api.Controllers;
using Moq;
using TodoPagos.Domain;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using System.Linq;

namespace TodoPagos.Web.Api.Tests.ControllerUnitTests
{
    [TestClass]
    public class ClientsControllerShould
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void FailWithNullReferenceExceptionWhenNoUserIsLogedIn()
        {
            ClientsController controller = new ClientsController();
        }

        [TestMethod]
        public void ReceiveAClientServiceOnCreation()
        {
            var mockClientService = new Mock<IClientService>();

            ClientsController controller = new ClientsController(mockClientService.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfClientServiceIsNullOnCreation()
        {
            IClientService nullClientService = null;

            ClientsController controller = new ClientsController(nullClientService);
        }

        [TestMethod]
        public void BeAbleToReturnAllClientsInRepository()
        {
            List<Client> allClients = new List<Client>
            {
                new Client("Manzana", "12345672", "26666666"),
                new Client("Ruben Rada", "11111118", "26666666")
            };
            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(x => x.GetAllClients(It.IsAny<string>())).Returns(allClients);
            ClientsController controller = new ClientsController(mockClientService.Object);

            IHttpActionResult actionResult = controller.GetClients();
            OkNegotiatedContentResult<IEnumerable<Client>> contentResult = (OkNegotiatedContentResult<IEnumerable<Client>>)actionResult;

            Assert.IsTrue(contentResult.Content.All(x => allClients.Contains(x)) &&
                        allClients.All(x => contentResult.Content.Contains(x)));
        }

        [TestMethod]
        public void BeAbleToReturnSingleClientInRepository()
        {
            Client singleClient = new Client("Ruben Rada", "11111118", "26666666");
            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(x => x.GetSingleClient(singleClient.ID, It.IsAny<string>())).Returns(singleClient);
            ClientsController controller = new ClientsController(mockClientService.Object);

            IHttpActionResult actionResult = controller.GetClient(singleClient.ID);
            OkNegotiatedContentResult<Client> contentResult = (OkNegotiatedContentResult<Client>)actionResult;

            Assert.AreEqual(contentResult.Content, singleClient);
        }

        [TestMethod]
        public void FailWithNotFoundIfSingleUserIdDoesntExistInRepository()
        {
            Client singleClient = new Client("Ruben Rada", "11111118", "26666666");
            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(x => x.GetSingleClient(singleClient.ID, It.IsAny<string>())).Returns(singleClient);
            ClientsController controller = new ClientsController(mockClientService.Object);

            IHttpActionResult actionResult = controller.GetClient(singleClient.ID + 1);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}
