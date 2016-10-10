using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TodoPagos.Domain.Repository;
using System.Collections.Generic;
using TodoPagos.Domain;

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

        [TestMethod]
        public void BeAbleToGetAllPaymentsFromRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.PaymentRepository.Get(null, null, ""));
            PaymentService paymentService = new PaymentService(mockUnitOfWork.Object);

            IEnumerable<Payment> payments = paymentService.GetAllPayments();

            mockUnitOfWork.VerifyAll();
        }
    }
}
