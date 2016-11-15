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
using TodoPagos.UserAPI;

namespace TodoPagos.Web.Api.Tests.ControllerIntegrationTests
{
    [TestClass]
    public class ClientsControllerShould
    {

        static string ADMIN_USER_USEREMAIL = "diego@bruno.com";

        static ICollection<Client> TEST_CLIENTS;

        ClientsController CONTROLLER;

        [TestInitialize()]
        public void InsertTestsUserInfoForTest()
        {
            CONTROLLER = new ClientsController(ADMIN_USER_USEREMAIL);

            TEST_CLIENTS = new[]
            {
                new Client("Shigeru Miyamoto", "12345672", "26666666", "1112 28th NE"),
            new Client("Ruben Rada", "11111111", "26666666", "1112 28th NE")
        };

            foreach (Client aTestClient in TEST_CLIENTS)
            {
                CONTROLLER.PostClient(aTestClient);
            }
        }

        [TestCleanup()]
        public void DeleteTestUserInfoForTest()
        {

            foreach (Client aTestClient in TEST_CLIENTS)
            {
                CONTROLLER.DeleteClient(aTestClient.ID);
            }

            CONTROLLER.Dispose();
        }


        [TestMethod]
        public void ReceiveASignedInUsernameOnCreation()
        {
            string username = "TestUser";

            ClientsController controller = new ClientsController(username);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfUserNameIsNullOnCreation()
        {
            string nullUsername = null;

            ClientsController controller = new ClientsController(nullUsername);
        }

        [TestMethod]
        public void BeAbleToReturnAllClientsInRepository()
        {
            IHttpActionResult actionResult = CONTROLLER.GetClients();
            OkNegotiatedContentResult<IEnumerable<Client>> contentResult = (OkNegotiatedContentResult<IEnumerable<Client>>)actionResult;

            Assert.IsTrue(contentResult.Content.All(x => TEST_CLIENTS.Contains(x)) &&
                        TEST_CLIENTS.All(x => contentResult.Content.Contains(x)));
        }

        [TestMethod]
        public void BeAbleToReturnSingleClientInRepository()
        {
            Client singleClient = TEST_CLIENTS.First();
            IHttpActionResult actionResult = CONTROLLER.GetClient(singleClient.ID);
            OkNegotiatedContentResult<Client> contentResult = (OkNegotiatedContentResult<Client>)actionResult;

            Assert.AreEqual(contentResult.Content, singleClient);
        }

        [TestMethod]
        public void FailWithNotFoundIfSingleClientIdDoesntExistInRepository()
        {
            IHttpActionResult actionResult = CONTROLLER.GetClient(0);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void BeAbleToPostNewClientIntoRepository()
        {
            Client singleClient = new Client("Super Mega", "49018830", "26666666", "1112 28th NE");
            IHttpActionResult actionResult = CONTROLLER.PostClient(singleClient);
            CreatedAtRouteNegotiatedContentResult<Client> contentResult = (CreatedAtRouteNegotiatedContentResult<Client>)actionResult;

            Assert.AreEqual(contentResult.Content, singleClient);

            CONTROLLER.DeleteClient(singleClient.ID);
        }

        [TestMethod]
        public void FailWithBadRequestIfPostedNewClientIsAlreadyInRepository()
        {
            Client repeatedClient = TEST_CLIENTS.First();

            IHttpActionResult actionResult = CONTROLLER.PostClient(repeatedClient);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void FailWithBadRequestIfPostedNewClientIsNotCompleteInRepository()
        {
            Client incompleteClient = new Client("Lala", "49018830", "26666666", "1112 28th NE");
            incompleteClient.Name = "";

            IHttpActionResult actionResult = CONTROLLER.PostClient(incompleteClient);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void FailWithBadRequestIfPostedNewClientIsNull()
        {
            Client nullClient = null;

            IHttpActionResult actionResult = CONTROLLER.PostClient(nullClient);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void BeAbleToUpdateAClientInTheRepository()
        {
            Client toBeUpdatedClient = TEST_CLIENTS.First();
            Client updatedClient = new Client("Gunpei Yokoi", toBeUpdatedClient.IDCard, toBeUpdatedClient.PhoneNumber, "1112 28th NE");
            updatedClient.ID = toBeUpdatedClient.ID;

            IHttpActionResult actionResult = CONTROLLER.PutClient(toBeUpdatedClient.ID, updatedClient);
            StatusCodeResult contentResult = (StatusCodeResult)actionResult;

            Assert.AreEqual(contentResult.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void FailWithBadRequestIfUpdatedClientIdAndSuppliedIdAreDifferent()
        {
            Client toBeUpdatedClient = TEST_CLIENTS.First();

            IHttpActionResult actionResult = CONTROLLER.PutClient(toBeUpdatedClient.ID + 1, toBeUpdatedClient);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void FailWithBadRequestIfUpdatedClientIsNull()
        {
            Client nullClient = null;

            IHttpActionResult actionResult = CONTROLLER.PutClient(TEST_CLIENTS.First().ID, nullClient);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void FailWithNotFoundIfServiceCantFindToBeUpdatedClientInRepository()
        {
            Client updatedClient = new Client("Super Mega", "49018830", "26666666", "1112 28th NE");

            IHttpActionResult actionResult = CONTROLLER.PutClient(0, updatedClient);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void BeAbleToDeleteAClient()
        {
            Client toBeDeletedClient = TEST_CLIENTS.First();

            IHttpActionResult actionResult = CONTROLLER.DeleteClient(toBeDeletedClient.ID);
            StatusCodeResult contentResult = (StatusCodeResult)actionResult;

            Assert.AreEqual(contentResult.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void FailWithNotFoundIfToBeDeletedClientDoesntExistInRepository()
        {
            IHttpActionResult actionResult = CONTROLLER.DeleteClient(0);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}
