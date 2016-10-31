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

namespace TodoPagos.Web.Api.Tests.ControllerUnitTests
{
    [TestClass]
    public class EarningQueriesControllerShould
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void FailWithNullReferenceExceptionWhenNoUserIsLogedIn()
        {
            EarningQueriesController controller = new EarningQueriesController();
        }

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
            DateTime from = DateTime.ParseExact("2012-09-17T22:02:51Z",
                "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
            DateTime to = DateTime.Today;
            IDictionary<Provider, double> result = new Dictionary<Provider, double>();
            result.Add(new Provider("Antel", 10, new List<IField>()), 100);
            result.Add(new Provider("Tienda Inglesa", 7, new List<IField>()), 200);
            var mockEarningQueriesService = new Mock<IEarningQueriesService>();
            mockEarningQueriesService.Setup(x => x.GetEarningsPerProvider(from, to, It.IsAny<string>())).Returns(result);
            EarningQueriesController controller = new EarningQueriesController(mockEarningQueriesService.Object);

            IHttpActionResult actionResult = controller.GetEarningsPerProvider(from.ToString("yyyy-MM-ddTHH:mm:ssZ"), to.ToString("yyyy-MM-ddTHH:mm:ssZ"));
            OkNegotiatedContentResult<IDictionary<Provider, double>> contentResult = 
                (OkNegotiatedContentResult<IDictionary<Provider, double>>)actionResult;

            Assert.AreSame(contentResult.Content, result);
        }

        [TestMethod]
        public void BeAbleToReturnEarningsPerProviderWithDefaultDates()
        {
            DateTime from = DateTime.MinValue;
            DateTime to = DateTime.Today;
            IDictionary<Provider, double> result = new Dictionary<Provider, double>();
            result.Add(new Provider("Antel", 10, new List<IField>()), 100);
            result.Add(new Provider("Tienda Inglesa", 7, new List<IField>()), 200);
            var mockEarningQueriesService = new Mock<IEarningQueriesService>();
            mockEarningQueriesService.Setup(x => x.GetEarningsPerProvider(from, to, It.IsAny<string>())).Returns(result);
            EarningQueriesController controller = new EarningQueriesController(mockEarningQueriesService.Object);

            IHttpActionResult actionResult = controller.GetEarningsPerProvider();
            OkNegotiatedContentResult<IDictionary<Provider, double>> contentResult =
                (OkNegotiatedContentResult<IDictionary<Provider, double>>)actionResult;

            Assert.AreSame(contentResult.Content, result);
        }

        [TestMethod]
        public void BeAbleToReturnEarningsInACertainTimePeriod()
        {
            DateTime from = DateTime.ParseExact("2007-09-17T22:02:51Z",
                "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
            DateTime to = DateTime.Today;
            int earnings = 1000;
            var mockEarningQueriesService = new Mock<IEarningQueriesService>();
            mockEarningQueriesService.Setup(x => x.GetAllEarnings(from, to, It.IsAny<string>())).Returns(earnings);
            EarningQueriesController controller = new EarningQueriesController(mockEarningQueriesService.Object);

            IHttpActionResult actionResult = controller.GetAllEarnings(from.ToString("yyyy-MM-ddTHH:mm:ssZ"), to.ToString("yyyy-MM-ddTHH:mm:ssZ"));
            OkNegotiatedContentResult<double> contentResult = (OkNegotiatedContentResult<double>)actionResult;

            Assert.AreEqual(contentResult.Content, earnings);
        }

        [TestMethod]
        public void BeAbleToReturnAllEarningsWithDefaultDates()
        {
            DateTime from = DateTime.MinValue;
            DateTime to = DateTime.Today;
            int earnings = 1000;
            var mockEarningQueriesService = new Mock<IEarningQueriesService>();
            mockEarningQueriesService.Setup(x => x.GetAllEarnings(from, to, It.IsAny<string>())).Returns(earnings);
            EarningQueriesController controller = new EarningQueriesController(mockEarningQueriesService.Object);

            IHttpActionResult actionResult = controller.GetAllEarnings();
            OkNegotiatedContentResult<double> contentResult = (OkNegotiatedContentResult<double>)actionResult;

            Assert.AreEqual(contentResult.Content, earnings);
        }
    }
}
