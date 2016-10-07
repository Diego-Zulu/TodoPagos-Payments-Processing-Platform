using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Web.Api.Controllers;
using Moq;
using TodoPagos.Web.Services;
using TodoPagos.Domain;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace TodoPagos.Web.Api.Tests
{
    [TestClass]
    public class PaymentsControllerShould
    {
        [TestMethod]
        public void RecieveAPaymentServiceOnCreation()
        {
            var mockPaymentService = new Mock<IPaymentService>();

            PaymentsController controller = new PaymentsController(mockPaymentService.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailCreationIfServiceIsNull()
        {
            IPaymentService service = null;

            PaymentsController controller = new PaymentsController(service);
        }

        [TestMethod]
        public void BeAbleToReturnAllPaymentsInRepository()
        {
            var allPayments = new[]
            {
                new Payment(new CashPayMethod(100, DateTime.Now), 100, new List<Receipt>()),
            new Payment(new DebitPayMethod(DateTime.Now), 100, new List<Receipt>())
            };
            var mockPaymentService = new Mock<IPaymentService>();
            mockPaymentService.Setup(x => x.GetAllPayments()).Returns(allPayments);
            PaymentsController controller = new PaymentsController(mockPaymentService.Object);

            IHttpActionResult actionResult = controller.GetPayments();
            OkNegotiatedContentResult<IEnumerable<Payment>> contentResult = (OkNegotiatedContentResult<IEnumerable<Payment>>)actionResult;

            Assert.AreSame(contentResult.Content, allPayments);
        }
    }
}
