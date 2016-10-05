using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TodoPagos.Web.Api.Controllers;
using TodoPagos.Web.Services;

namespace TodoPagos.Web.Api.Tests
{
    class ProvidersControllerShould
    {
        [TestMethod]
        public void RecieveAProviderServiceOnCreation()
        {
            var mockProviderService = new Mock<IProviderService>();

            ProvidersController controller = new ProvidersController(mockProviderService.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfProviderServiceIsNullOnCreation()
        {
            IProviderService nullProviderService = null;

            ProvidersController controller = new ProvidersController(nullProviderService);
        }
    }
}
