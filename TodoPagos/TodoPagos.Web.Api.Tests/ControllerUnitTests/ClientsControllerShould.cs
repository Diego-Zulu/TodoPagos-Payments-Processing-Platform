using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
