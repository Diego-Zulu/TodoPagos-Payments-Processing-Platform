using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TodoPagos.Web.Services;
using TodoPagos.Web.Api.Controllers;

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
    }
}
