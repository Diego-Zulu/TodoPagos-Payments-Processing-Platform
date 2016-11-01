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

namespace TodoPagos.Web.Api.Tests.ControllerIntegrationTests
{
    [TestClass]
    public class ProvidersControllerShould
    {
        static User ADMIN_USER;
        static string ADMIN_USER_USEREMAIL = "diego@bruno.com";
        static Provider RESERVED_PROVIDER;
        static Provider MODIFICABLE_PROVIDER;
        static ICollection<Provider> ALL_PROVIDERS_IN_REPOSITORY;
        static int MODIFICABLE_PROVIDER_ID;
        static int RESERVED_PROVIDER_ID;

        static ProvidersController CONTROLLER;

        [ClassInitialize()]
        public static void SetReservedProviderInfoForTests(TestContext testContext)
        {
            ADMIN_USER = new User("Brulu", ADMIN_USER_USEREMAIL, "HOLA1234", AdminRole.GetInstance());

            RESERVED_PROVIDER = new Provider("Claro", 5, new List<IField>());

            MODIFICABLE_PROVIDER = new Provider("Movistar", 10, new List<IField>());

            CONTROLLER = new ProvidersController(ADMIN_USER.Email);
            CONTROLLER.PostProvider(MODIFICABLE_PROVIDER);
            CONTROLLER.PostProvider(RESERVED_PROVIDER);
            CONTROLLER.Dispose();
        }

        [TestInitialize()]
        public void InsertTestsProviderInfoForTest()
        {
            CONTROLLER = new ProvidersController(ADMIN_USER.Email);
            IHttpActionResult actionResult = CONTROLLER.GetProviders();
            OkNegotiatedContentResult<IEnumerable<Provider>> contentResult = (OkNegotiatedContentResult<IEnumerable<Provider>>)actionResult;
            ALL_PROVIDERS_IN_REPOSITORY = contentResult.Content.ToList();
            IEnumerable<Provider> providers = contentResult.Content;
            foreach (Provider provider in providers)
            {
                if (provider.Name.Equals(MODIFICABLE_PROVIDER.Name) && provider.Active)
                {
                    MODIFICABLE_PROVIDER_ID = provider.ID;
                    MODIFICABLE_PROVIDER.ID = provider.ID;
                }
                if (provider.Name.Equals(RESERVED_PROVIDER.Name) && provider.Active)
                {
                    RESERVED_PROVIDER_ID = provider.ID;
                    RESERVED_PROVIDER.ID = provider.ID;
                }
            }
        }

        [TestCleanup()]
        public void FinalizeTest()
        {
            CONTROLLER.Dispose();
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

            ICollection<Provider> activeProviders = new List<Provider>();
            foreach(Provider provider in ALL_PROVIDERS_IN_REPOSITORY)
            {
                if (provider.Active) activeProviders.Add(provider);
            }

            CollectionAssert.AreEquivalent((ICollection)contentResult.Content, (ICollection) activeProviders);
        }

        [TestMethod]
        public void BeAbleToReturnASingleProviderFromRepository()
        {
            IHttpActionResult actionResult = CONTROLLER.GetProvider(RESERVED_PROVIDER_ID);
            OkNegotiatedContentResult<Provider> contentResult = (OkNegotiatedContentResult<Provider>)actionResult;

            Assert.AreEqual(contentResult.Content, RESERVED_PROVIDER);
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
            Provider update = new Provider("UTE", 10, new List<IField>());
            update.ID = MODIFICABLE_PROVIDER_ID;
            IHttpActionResult actionResult = CONTROLLER.PutProvider(MODIFICABLE_PROVIDER_ID, update);
            StatusCodeResult contentResult = (StatusCodeResult)actionResult;

            Assert.AreEqual(contentResult.StatusCode, HttpStatusCode.NoContent);
            Provider anotherUpdate = new Provider("Movistar", 10, new List<IField>());
            IHttpActionResult actionResultAfter = CONTROLLER.GetProviders();
            OkNegotiatedContentResult<IEnumerable<Provider>> contentResultAfter = (OkNegotiatedContentResult<IEnumerable<Provider>>)actionResultAfter;
            IEnumerable<Provider> providers = contentResultAfter.Content;
            foreach (Provider provider in providers)
            {
                if (provider.IsCompletelyEqualTo(update)) anotherUpdate.ID = provider.ID;
            }
            CONTROLLER.PutProvider(anotherUpdate.ID, anotherUpdate);
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
            Provider oneProvider = new Provider("Microsoft", 10, new List<IField>());

            IHttpActionResult actionResult = CONTROLLER.PostProvider(oneProvider);
            CreatedAtRouteNegotiatedContentResult<Provider> contentResult = (CreatedAtRouteNegotiatedContentResult<Provider>)actionResult;

            Assert.AreEqual(contentResult.Content, oneProvider);
            CONTROLLER.DeleteProvider(contentResult.Content.ID);
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
            IHttpActionResult actionResult = CONTROLLER.DeleteProvider(MODIFICABLE_PROVIDER_ID);
            StatusCodeResult contentResult = (StatusCodeResult)actionResult;

            Assert.AreEqual(contentResult.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void FailWithNotFoundIfToBeDeletedProviderDoesntExistInRepository()
        {
            Provider oneProvider = new Provider("Facebook", 10, new List<IField>());

            IHttpActionResult actionResult = CONTROLLER.DeleteProvider(oneProvider.ID);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}
