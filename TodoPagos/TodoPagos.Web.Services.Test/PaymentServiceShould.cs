﻿using System;
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
            List<IField> emptyFields = new List<IField>();
            NumberField field = new NumberField("Monto");
            emptyFields.Add(field);
            IField filledField = field.FillAndClone("100");
            List<IField> fullFields = new List<IField>();
            fullFields.Add(filledField);
            Provider provider = new Provider("Antel", 3, emptyFields);
            Receipt receipt = new Receipt(provider, fullFields, 100);
            List<Receipt> list = new List<Receipt>();
            list.Add(receipt);
            Payment payment = new Payment(new CashPayMethod(100, DateTime.Now), 100, list);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.PaymentRepository.GetByID(payment.ID)).Returns(payment);
            PaymentService paymentService = new PaymentService(mockUnitOfWork.Object);

            Payment returnedPayment = paymentService.GetSinglePayment(payment.ID);

            mockUnitOfWork.VerifyAll();
            Assert.AreSame(payment, returnedPayment);
        }
    }
}
