using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TodoPagos.Web.Services;
using TodoPagos.Web.Api.Controllers;
using System.Globalization;
using System.Collections.Generic;
using TodoPagos.Domain;
using System.Web.Http;
using System.Web.Http.Results;
using TodoPagos.UserAPI;

namespace TodoPagos.Web.Api.Tests.ControllerIntegrationTests
{
    [TestClass]
    public class EarningQueriesControllerShould
    {
        static string ADMIN_USER_USEREMAIL = "diego@bruno.com";
        static User ADMIN_USER;

        EarningQueriesController EARNINGS_CONTROLLER;

        static Provider FIRST_TEST_PROVIDER;
        static Provider SECOND_TEST_PROVIDER;

        [ClassInitialize()]
        public static void SetAdminInfoForTests(TestContext testContext)
        {
            ADMIN_USER = new User("Brulu", ADMIN_USER_USEREMAIL, "HOLA1234", AdminRole.GetInstance());
            ADMIN_USER.ID = 1;

            ProvidersController providersController = new ProvidersController(ADMIN_USER_USEREMAIL);

            OkNegotiatedContentResult<Provider> result =
                (OkNegotiatedContentResult<Provider>)providersController.GetProvider(1);

            FIRST_TEST_PROVIDER = result.Content;

            result = (OkNegotiatedContentResult<Provider>)providersController.GetProvider(2);

            SECOND_TEST_PROVIDER = result.Content;
        }

        [TestInitialize()]
        public void CreateEarningsController()
        {

            EARNINGS_CONTROLLER = new EarningQueriesController(ADMIN_USER_USEREMAIL);
        }

        [TestCleanup()]
        public void DisposeOfEarningsController()
        {

            EARNINGS_CONTROLLER.Dispose();
        }

        [TestMethod]
        public void ReceiveASignedInUsernameOnCreation()
        {
            string username = "TestUser";

            EarningQueriesController controller = new EarningQueriesController(username);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfUserNameIsNullOnCreation()
        {
            string nullUsername = null;

            EarningQueriesController controller = new EarningQueriesController(nullUsername);
        }

        [TestMethod]
        public void BeAbleToReturnEarningsPerProviderInACertainTimePeriod()
        {
            
            DateTime from = DateTime.ParseExact("Mon, 15 Sep 2008 09:30:41 GMT", 
                "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'", CultureInfo.InvariantCulture);
            DateTime to = DateTime.Today;    

            IHttpActionResult actionResult = EARNINGS_CONTROLLER.GetEarningsPerProvider(from, to);
            OkNegotiatedContentResult<IDictionary<Provider, double>> contentResult = 
                (OkNegotiatedContentResult<IDictionary<Provider, double>>)actionResult;

            Assert.AreEqual(contentResult.Content[FIRST_TEST_PROVIDER], 100);
            Assert.AreEqual(contentResult.Content[SECOND_TEST_PROVIDER], 200);
        }

        [TestMethod]
        public void BeAbleToReturnEarningsPerProviderWithDefaultDates()
        {
            IHttpActionResult actionResult = EARNINGS_CONTROLLER.GetEarningsPerProvider();
            OkNegotiatedContentResult<IDictionary<Provider, double>> contentResult =
                (OkNegotiatedContentResult<IDictionary<Provider, double>>)actionResult;

            Assert.AreEqual(contentResult.Content[FIRST_TEST_PROVIDER], 100);
            Assert.AreEqual(contentResult.Content[SECOND_TEST_PROVIDER], 200);
        }

        [TestMethod]
        public void BeAbleToReturnEarningsInACertainTimePeriod()
        {
            DateTime from = DateTime.ParseExact("Mon, 15 Sep 2008 09:30:41 GMT",
                "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'", CultureInfo.InvariantCulture);
            DateTime to = DateTime.Today;
            int earnings = 300;
           
            IHttpActionResult actionResult = EARNINGS_CONTROLLER.GetAllEarnings(from, to);
            OkNegotiatedContentResult<double> contentResult = (OkNegotiatedContentResult<double>)actionResult;

            Assert.AreEqual(contentResult.Content, earnings);
        }

        [TestMethod]
        public void BeAbleToReturnAllEarningsWithDefaultDates()
        {
            int earnings = 300;

            IHttpActionResult actionResult = EARNINGS_CONTROLLER.GetAllEarnings();
            OkNegotiatedContentResult<double> contentResult = (OkNegotiatedContentResult<double>)actionResult;

            Assert.AreEqual(contentResult.Content, earnings);
        }
    }
}
