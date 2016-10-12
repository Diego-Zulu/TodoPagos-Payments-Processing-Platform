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

        [TestMethod]
        public void BeAbleToReturnSinglePaymentFromRepository()
        {

            Payment payment = CreateNewPayment();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.PaymentRepository.GetByID(payment.ID)).Returns(payment);
            PaymentService paymentService = new PaymentService(mockUnitOfWork.Object);

            Payment returnedPayment = paymentService.GetSinglePayment(payment.ID);

            mockUnitOfWork.VerifyAll();
            Assert.AreSame(payment, returnedPayment);
        }

        private Payment CreateNewPayment()
        {
            Receipt receipt = CreateNewReceipt();
            List<Receipt> list = new List<Receipt>();
            list.Add(receipt);
            return new Payment(new CashPayMethod(100, DateTime.Now), 100, list);
        }

        private Receipt CreateNewReceipt()
        {
            List<IField> emptyFields = new List<IField>();
            NumberField field = new NumberField("Monto");
            emptyFields.Add(field);
            IField filledField = field.FillAndClone("100");
            List<IField> fullFields = new List<IField>();
            fullFields.Add(filledField);
            Provider provider = new Provider("Antel", 3, emptyFields);
            return new Receipt(provider, fullFields, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfSinglePaymentDoesntExistInRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.PaymentRepository.GetByID(It.IsAny<int>()));
            PaymentService paymentService = new PaymentService(mockUnitOfWork.Object);

            Payment payment = paymentService.GetSinglePayment(5);
        }

        [TestMethod]
        public void BeAbleToCreateNewPaymentInRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            List<Receipt> receipts = new List<Receipt>();
            mockUnitOfWork.Setup(x => x.PaymentRepository.Insert(It.IsAny<Payment>()));
            mockUnitOfWork.Setup(x => x.ReceiptRepository.Get(null, null, "")).Returns(receipts);
            mockUnitOfWork.Setup(x => x.Save());
            PaymentService paymentService = new PaymentService(mockUnitOfWork.Object);
            Payment payment = CreateNewPayment();

            int id = paymentService.CreatePayment(payment);

            mockUnitOfWork.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfToBeCreatedNewPaymentIsNotComplete()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            PaymentService paymentService = new PaymentService(mockUnitOfWork.Object);

            Payment payment = new Payment();

            int id = paymentService.CreatePayment(payment);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FailWithArgumentNullExceptionIfToBeCreatedNewPaymentIsNull()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            PaymentService paymentService = new PaymentService(mockUnitOfWork.Object);

            Payment payment = null;

            int id = paymentService.CreatePayment(payment);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfAnyOfTheNewPaymentsReceiptsAlreadyIsInTheReceiptsRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            List<Receipt> receipts = new List<Receipt>();
            Receipt receipt = CreateNewReceipt();
            receipts.Add(receipt);
            mockUnitOfWork.Setup(x => x.PaymentRepository.Insert(It.IsAny<Payment>()));
            mockUnitOfWork.Setup(x => x.ReceiptRepository.Get(null, null, "")).Returns(receipts);
            mockUnitOfWork.Setup(x => x.Save());
            PaymentService paymentService = new PaymentService(mockUnitOfWork.Object);

            Payment firstPayment = CreateNewPayment();
            Payment secondPayment = CreateNewPayment();

            paymentService.CreatePayment(firstPayment);
            paymentService.CreatePayment(secondPayment);
        }
    }
}
