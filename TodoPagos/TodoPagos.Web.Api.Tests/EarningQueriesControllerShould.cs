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

namespace TodoPagos.Web.Api.Tests
{
    [TestClass]
    public class EarningQueriesControllerShould
    {
        [TestMethod]
        public void ReceiveAnEarningQueriesServiceOnCreation()
        {
            var mockEarningQueriesService = new Mock<IEarningQueriesService>();

            EarningQueriesController controller = new EarningQueriesController(mockEarningQueriesService.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailCreationIfServiceIsNull()
        {
            IEarningQueriesService service = null;

            EarningQueriesController controller = new EarningQueriesController(service);
        }

        [TestMethod]
        public void BeAbleToReturnEarningsPerProviderInACertainTimePeriod()
        {
            DateTime from = DateTime.ParseExact("Mon, 15 Sep 2008 09:30:41 GMT", 
                "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'", CultureInfo.InvariantCulture);
            DateTime to = DateTime.Today;
            IDictionary<Provider, int> result = new Dictionary<Provider, int>();
            result.Add(new Provider("Antel", 10, new List<IField>()), 100);
            result.Add(new Provider("Tienda Inglesa", 7, new List<IField>()), 200);
            var mockEarningQueriesService = new Mock<IEarningQueriesService>();
            mockEarningQueriesService.Setup(x => x.GetEarningsPerProvider(from, to)).Returns(result);
            EarningQueriesController controller = new EarningQueriesController(mockEarningQueriesService.Object);

            IHttpActionResult actionResult = controller.GetEarningsPerProvider(from, to);
            OkNegotiatedContentResult<IDictionary<Provider, int>> contentResult = 
                (OkNegotiatedContentResult<IDictionary<Provider, int>>)actionResult;

            Assert.AreSame(contentResult.Content, result);
        }

        [TestMethod]
        public void BeAbleToReturnEarningsPerProviderWithDefaultDates()
        {
            DateTime from = DateTime.ParseExact("Wed, 29 Aug 1962 00:00:00 GMT",
                 "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'", CultureInfo.InvariantCulture);
            DateTime to = DateTime.Today;
            IDictionary<Provider, int> result = new Dictionary<Provider, int>();
            result.Add(new Provider("Antel", 10, new List<IField>()), 100);
            result.Add(new Provider("Tienda Inglesa", 7, new List<IField>()), 200);
            var mockEarningQueriesService = new Mock<IEarningQueriesService>();
            mockEarningQueriesService.Setup(x => x.GetEarningsPerProvider(from, to)).Returns(result);
            EarningQueriesController controller = new EarningQueriesController(mockEarningQueriesService.Object);

            IHttpActionResult actionResult = controller.GetEarningsPerProvider();
            OkNegotiatedContentResult<IDictionary<Provider, int>> contentResult =
                (OkNegotiatedContentResult<IDictionary<Provider, int>>)actionResult;

            Assert.AreSame(contentResult.Content, result);
        }

        [TestMethod]
        public void BeAbleToReturnEarningsInACertainTimePeriod()
        {
            DateTime from = DateTime.ParseExact("Mon, 15 Sep 2008 09:30:41 GMT",
                "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'", CultureInfo.InvariantCulture);
            DateTime to = DateTime.Today;
            int earnings = 1000;
            var mockEarningQueriesService = new Mock<IEarningQueriesService>();
            mockEarningQueriesService.Setup(x => x.GetAllEarnings(from, to)).Returns(earnings);
            EarningQueriesController controller = new EarningQueriesController(mockEarningQueriesService.Object);

            IHttpActionResult actionResult = controller.GetAllEarnings(from, to);
            OkNegotiatedContentResult<int> contentResult = (OkNegotiatedContentResult<int>)actionResult;

            Assert.AreEqual(contentResult.Content, earnings);
        }

        [TestMethod]
        public void BeAbleToReturnAllEarningsWithDefaultDates()
        {
            DateTime from = DateTime.ParseExact("Wed, 29 Aug 1962 00:00:00 GMT",
                 "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'", CultureInfo.InvariantCulture);
            DateTime to = DateTime.Today;
            int earnings = 1000;
            var mockEarningQueriesService = new Mock<IEarningQueriesService>();
            mockEarningQueriesService.Setup(x => x.GetAllEarnings(from, to)).Returns(earnings);
            EarningQueriesController controller = new EarningQueriesController(mockEarningQueriesService.Object);

            IHttpActionResult actionResult = controller.GetAllEarnings();
            OkNegotiatedContentResult<int> contentResult = (OkNegotiatedContentResult<int>)actionResult;

            Assert.AreEqual(contentResult.Content, earnings);
        }
    }
}
