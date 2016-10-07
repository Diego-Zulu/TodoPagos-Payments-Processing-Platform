using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
