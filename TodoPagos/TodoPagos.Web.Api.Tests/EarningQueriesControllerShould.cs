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
            DateTime to = DateTime.ParseExact("Tue, 16 Sep 2008 09:30:41 GMT",
                "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'", CultureInfo.InvariantCulture);
            Dictionary<Provider, int> result = new Dictionary<Provider, int>();
            result.Add(new Provider("Antel", 10, new List<IField>()), 100);
            result.Add(new Provider("Tienda Inglesa", 7, new List<IField>()), 200);
            var mockEarningQueriesService = new Mock<IEarningQueriesService>();
            mockEarningQueriesService.Setup(x => x.GetEarningsPerProvider(from, to)).Returns(result);
            EarningQueriesController controller = new EarningQueriesController(mockEarningQueriesService.Object);

            IHttpActionResult actionResult = controller.GetEarningsPerProvider(from, to);
            OkNegotiatedContentResult<Dictionary<Provider, int>> contentResult = 
                (OkNegotiatedContentResult<Dictionary<Provider, int>>)actionResult;

            Assert.AreSame(contentResult.Content, result);
        }
    }
}
