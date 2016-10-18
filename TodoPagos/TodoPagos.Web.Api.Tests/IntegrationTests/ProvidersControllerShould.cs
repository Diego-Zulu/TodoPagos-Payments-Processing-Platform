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
using System.Net;
using System.Collections;
using TodoPagos.UserAPI;

namespace TodoPagos.Web.Api.Tests.IntegrationTests
{
    [TestClass]
    public class ProvidersControllerShould
    {
        static User ADMIN_USER;
        static ICollection<Provider> TESTS_PROVIDERS;
        static Provider RESERVED_PROVIDER;
        static Provider MODIFICABLE_PROVIDER;
        static ICollection<Provider> ALL_PROVIDERS_IN_REPOSITORY;

        static ProvidersController CONTROLLER;

        [ClassInitialize()]
        public static void SetReservedProviderInfoForTests(TestContext testContext)
        {
            ADMIN_USER = new User("Hola", "hola@hola.com", "HolaHola11", AdminRole.GetInstance());

            RESERVED_PROVIDER = new Provider("Claro", 5, new List<IField>());

            MODIFICABLE_PROVIDER = new Provider("Movistar", 10, new List<IField>());

            TESTS_PROVIDERS = new[]
{
                new Provider("Antel", 20, new List<IField>() { new NumberField("Monto")}),
                new Provider("OSE", 10, new List<IField>())
            };

            CONTROLLER = new ProvidersController(ADMIN_USER.Email);
            CONTROLLER.PostProvider(MODIFICABLE_PROVIDER);
            CONTROLLER.PostProvider(RESERVED_PROVIDER);
            foreach (Provider aProvider in TESTS_PROVIDERS)
            {
                CONTROLLER.PostProvider(aProvider);
            }
            //UsersController uController = new UsersController("bla");
            //uController.PostUser(ADMIN_USER);
            //int bla = 0;
        }

        [TestInitialize()]
        public void InsertTestsProviderInfoForTest()
        {
            ICollection<Provider> reservedProviders = new[] { RESERVED_PROVIDER, MODIFICABLE_PROVIDER };
            ALL_PROVIDERS_IN_REPOSITORY = reservedProviders.Concat(TESTS_PROVIDERS).ToList();
        }

        [TestMethod]
        public void ReceiveAProviderServiceOnCreation()
        {
            string username = "Test User";

            ProvidersController controller = new ProvidersController(username);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfUsernameIsNullOnCreation()
        {
            string username = null;

            ProvidersController controller = new ProvidersController(username);
        }

        [TestMethod]
        public void BeAbleToReturnAllProvidersInRepository()
        {
            IHttpActionResult actionResult = CONTROLLER.GetProviders();
            OkNegotiatedContentResult<IEnumerable<Provider>> contentResult = (OkNegotiatedContentResult<IEnumerable<Provider>>)actionResult;

            CollectionAssert.AreEquivalent((ICollection)contentResult.Content, (ICollection)ALL_PROVIDERS_IN_REPOSITORY);
        }

        [TestMethod]
        public void BeAbleToReturnAllActiveProvidersInRepository()
        {
            IHttpActionResult actionResult = CONTROLLER.GetProviders(true);
            OkNegotiatedContentResult<IEnumerable<Provider>> contentResult = (OkNegotiatedContentResult<IEnumerable<Provider>>)actionResult;

            CollectionAssert.AreEquivalent((ICollection)contentResult.Content, (ICollection)ALL_PROVIDERS_IN_REPOSITORY);
        }

        [TestMethod]
        public void BeAbleToReturnASingleProviderFromRepository()
        {
            IHttpActionResult actionResult = CONTROLLER.GetProvider(RESERVED_PROVIDER.ID);
            OkNegotiatedContentResult<IEnumerable<Provider>> contentResult = (OkNegotiatedContentResult<IEnumerable<Provider>>)actionResult;

            CollectionAssert.AreEquivalent((ICollection)contentResult.Content, (ICollection)RESERVED_PROVIDER);
        }

        [TestMethod]
        public void FailWithNotFoundIfProviderIdIsNotInRepository()
        {
            IHttpActionResult actionResult = CONTROLLER.GetProvider(0);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void BeAbleToUpdateProviderInRepositoryAndReturnNoContent()
        {
            MODIFICABLE_PROVIDER.Name = "UTE";

            IHttpActionResult actionResult = CONTROLLER.PutProvider(MODIFICABLE_PROVIDER.ID, MODIFICABLE_PROVIDER);
            StatusCodeResult contentResult = (StatusCodeResult)actionResult;

            Assert.AreEqual(contentResult.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void FailWithBadRequestIfToBeUpdatedProviderIdAndSuppliedIdDontMatch()
        {
            MODIFICABLE_PROVIDER.Name = "Movistar";

            IHttpActionResult actionResult = CONTROLLER.PutProvider(MODIFICABLE_PROVIDER.ID + 1, MODIFICABLE_PROVIDER);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void FailWithBadRequestIfToBeUpdatedProviderIsNull()
        {
            Provider nullProvider = null;

            IHttpActionResult actionResult = CONTROLLER.PutProvider(1, nullProvider);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void FailWithNotFoundIfToBeUpdatedProviderIsNotInRepository()
        {
            Provider oneProvider = new Provider("Google", 10, new List<IField>());

            IHttpActionResult actionResult = CONTROLLER.PutProvider(oneProvider.ID, oneProvider);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void BeAbleToPostNewProviderIntoRepository()
        {
            Provider oneProvider = new Provider("Antel", 10, new List<IField>());

            IHttpActionResult actionResult = CONTROLLER.PostProvider(oneProvider);
            CreatedAtRouteNegotiatedContentResult<User> contentResult = (CreatedAtRouteNegotiatedContentResult<User>)actionResult;

            Assert.AreEqual(contentResult.Content, oneProvider);

            CONTROLLER.DeleteProvider(oneProvider.ID);
        }

        [TestMethod]
        public void FailWithBadRequestIfPostedNewProviderIsAlreadyInRepository()
        {
            IHttpActionResult actionResult = CONTROLLER.PostProvider(RESERVED_PROVIDER);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void FailWithBadRequestIfPostedNewProviderIsNull()
        {
            Provider nullProvider = null;

            IHttpActionResult actionResult = CONTROLLER.PostProvider(nullProvider);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void BeAbleToDeleteAProvider()
        {
            IHttpActionResult actionResult = CONTROLLER.DeleteProvider(TESTS_PROVIDERS.First().ID);
            StatusCodeResult contentResult = (StatusCodeResult)actionResult;

            Assert.AreEqual(contentResult.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void FailWithNotFoundIfToBeDeletedProviderDoesntExistInRepository()
        {
            Provider oneProvider = new Provider("Antel", 10, new List<IField>());

            IHttpActionResult actionResult = CONTROLLER.DeleteProvider(oneProvider.ID);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}
