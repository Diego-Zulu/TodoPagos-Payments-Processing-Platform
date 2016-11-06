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
using System.Net;

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
                new Client("Ruben Rada", "11111111", "26666666")
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
            Client singleClient = new Client("Ruben Rada", "11111111", "26666666");
            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(x => x.GetSingleClient(singleClient.ID, It.IsAny<string>())).Returns(singleClient);
            ClientsController controller = new ClientsController(mockClientService.Object);

            IHttpActionResult actionResult = controller.GetClient(singleClient.ID);
            OkNegotiatedContentResult<Client> contentResult = (OkNegotiatedContentResult<Client>)actionResult;

            Assert.AreEqual(contentResult.Content, singleClient);
        }

        [TestMethod]
        public void FailWithNotFoundIfSingleClientIdDoesntExistInRepository()
        {
            Client singleClient = new Client("Ruben Rada", "11111111", "26666666");
            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(x => x.GetSingleClient(singleClient.ID + 1, It.IsAny<string>())).Throws(new ArgumentException());
            ClientsController controller = new ClientsController(mockClientService.Object);

            IHttpActionResult actionResult = controller.GetClient(singleClient.ID + 1);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void BeAbleToPostNewClientIntoRepository()
        {
            Client singleClient = new Client("Ruben Rada", "11111111", "26666666");
            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(x => x.CreateClient(singleClient, It.IsAny<String>())).Returns(1);
            ClientsController controller = new ClientsController(mockClientService.Object);

            IHttpActionResult actionResult = controller.PostClient(singleClient);
            CreatedAtRouteNegotiatedContentResult<Client> contentResult = (CreatedAtRouteNegotiatedContentResult<Client>)actionResult;

            Assert.AreEqual(contentResult.Content, singleClient);
        }

        [TestMethod]
        public void FailWithBadRequestIfPostedNewClientIsAlreadyInRepository()
        {
            Client singleClient = new Client("Ruben Rada", "11111111", "26666666");
            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(x => x.CreateClient(singleClient, It.IsAny<String>())).Throws(new InvalidOperationException());
            ClientsController controller = new ClientsController(mockClientService.Object);

            IHttpActionResult actionResult = controller.PostClient(singleClient);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void FailWithBadRequestIfPostedNewClientIsNotCompleteInRepository()
        {
            Client incompleteClient = new Client("Ruben Rada", "11111111", "26666666");
            incompleteClient.Name = "";
            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(x => x.CreateClient(incompleteClient, It.IsAny<String>())).Throws(new ArgumentException());
            ClientsController controller = new ClientsController(mockClientService.Object);

            IHttpActionResult actionResult = controller.PostClient(incompleteClient);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void FailWithBadRequestIfPostedNewClientIsNull()
        {
            Client nullClient = null;
            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(x => x.CreateClient(nullClient, It.IsAny<String>())).Throws(new ArgumentNullException());
            ClientsController controller = new ClientsController(mockClientService.Object);

            IHttpActionResult actionResult = controller.PostClient(nullClient);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void BeAbleToUpdateAnClientInTheRepository()
        {
            Client singleClient = new Client("Ruben Rada", "11111111", "26666666");
            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(x => x.UpdateClient(singleClient.ID, singleClient, It.IsAny<string>())).Returns(true);
            ClientsController controller = new ClientsController(mockClientService.Object);

            IHttpActionResult actionResult = controller.PutClient(singleClient.ID, singleClient);
            StatusCodeResult contentResult = (StatusCodeResult)actionResult;

            Assert.AreEqual(contentResult.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void FailWithBadRequestIfUpdatedClientIdAndSuppliedIdAreDifferent()
        {
            Client singleClient = new Client("Ruben Rada", "11111111", "26666666");
            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(x => x.UpdateClient(singleClient.ID + 1, singleClient, It.IsAny<string>())).Returns(false);
            ClientsController controller = new ClientsController(mockClientService.Object);

            IHttpActionResult actionResult = controller.PutClient(singleClient.ID + 1, singleClient);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void FailWithBadRequestIfUpdatedClientIsNull()
        {
            Client nullClient = null;
            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(x => x.UpdateClient(1, nullClient, It.IsAny<string>())).Returns(false);
            ClientsController controller = new ClientsController(mockClientService.Object);

            IHttpActionResult actionResult = controller.PutClient(1, nullClient);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void FailWithNotFoundIfServiceCantFindToBeUpdatedClientInRepository()
        {
            Client singleClient = new Client("Ruben Rada", "11111111", "26666666");
            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(x => x.UpdateClient(singleClient.ID, singleClient, It.IsAny<string>())).Returns(false);
            ClientsController controller = new ClientsController(mockClientService.Object);

            IHttpActionResult actionResult = controller.PutClient(singleClient.ID, singleClient);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}
