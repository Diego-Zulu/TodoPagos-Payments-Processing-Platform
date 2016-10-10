using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TodoPagos.Domain.Repository;

namespace TodoPagos.Web.Services.Test
{
    [TestClass]
    public class PaymentServiceShould
    {
        [TestMethod]
        public void ReceiveAUnitOfWorkOnCreation()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            PaymentService service = new PaymentService(mockUnitOfWork.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfUnitOfWorkOnCreationIsNull()
        {
            IUnitOfWork mockUnitOfWork = null;

            PaymentService service = new PaymentService(mockUnitOfWork);
        }
    }
}
