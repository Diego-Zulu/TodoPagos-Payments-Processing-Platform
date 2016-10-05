﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

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
    }
}