using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Web.Api.Controllers;
using Moq;
using TodoPagos.Web.Services;
using TodoPagos.Domain;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace TodoPagos.Web.Api.Tests.ControllerUnitTests
{
    [TestClass]
    public class PaymentsControllerShould
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void FailWithNullReferenceExceptionWhenNoUserIsLogedIn()
        {
            PaymentsController controller = new PaymentsController();
        }

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
            List<IField> emptyFields = new List<IField>();
            NumberField field = new NumberField("Monto");
            emptyFields.Add(field);
            IField firstFilledField = field.FillAndClone("100");
            List<IField> firstFullFields = new List<IField>();
            firstFullFields.Add(firstFilledField);
            IField secondFilledField = field.FillAndClone("101");
            List<IField> secondFullFields = new List<IField>();
            secondFullFields.Add(secondFilledField);
            Provider provider = new Provider("Antel", 3, emptyFields);
            Receipt firstReceipt = new Receipt(provider, firstFullFields, 100);
            Receipt secondReceipt = new Receipt(provider, secondFullFields, 100);
            List<Receipt> firstList = new List<Receipt>();
            List<Receipt> secondList = new List<Receipt>();
            firstList.Add(firstReceipt);
            secondList.Add(secondReceipt);
            var allPayments = new[]
            {
                new Payment(new CashPayMethod(DateTime.Now), 100, firstList),
                new Payment(new DebitPayMethod(DateTime.Now), 100, secondList)
            };
            var mockPaymentService = new Mock<IPaymentService>();
            mockPaymentService.Setup(x => x.GetAllPayments()).Returns(allPayments);
            PaymentsController controller = new PaymentsController(mockPaymentService.Object);

            IHttpActionResult actionResult = controller.GetPayments();
            OkNegotiatedContentResult<IEnumerable<Payment>> contentResult = (OkNegotiatedContentResult<IEnumerable<Payment>>)actionResult;

            Assert.AreSame(contentResult.Content, allPayments);
        }

        [TestMethod]
        public void BeAbleToReturnASinglePaymentInRepository()
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
            Payment payment = new Payment(new CashPayMethod(DateTime.Now), 100, list);
            var mockPaymentService = new Mock<IPaymentService>();
            mockPaymentService.Setup(x => x.GetSinglePayment(payment.ID)).Returns(payment);
            PaymentsController controller = new PaymentsController(mockPaymentService.Object);

            IHttpActionResult actionResult = controller.GetPayment(payment.ID);
            OkNegotiatedContentResult<Payment> contentResult = (OkNegotiatedContentResult<Payment>)actionResult;

            Assert.AreSame(contentResult.Content, payment);
        }

        [TestMethod]
        public void FailIfSinglePaymentIdDoesntExistInRepository()
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
            Payment payment = new Payment(new CashPayMethod(DateTime.Now), 100, list);
            var mockPaymentService = new Mock<IPaymentService>();
            mockPaymentService.Setup(x => x.GetSinglePayment(payment.ID + 1)).Throws(new ArgumentOutOfRangeException());
            PaymentsController controller = new PaymentsController(mockPaymentService.Object);

            IHttpActionResult actionResult = controller.GetPayment(payment.ID + 1);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void BeAbleToPostNewPaymentIntoRepository()
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
            Payment payment = new Payment(new CashPayMethod(DateTime.Now), 100, list);
            var mockPaymentService = new Mock<IPaymentService>();
            mockPaymentService.Setup(x => x.CreatePayment(payment)).Returns(1);
            PaymentsController controller = new PaymentsController(mockPaymentService.Object);

            IHttpActionResult actionResult = controller.PostPayment(payment);
            CreatedAtRouteNegotiatedContentResult<Payment> contentResult = (CreatedAtRouteNegotiatedContentResult<Payment>)actionResult;

            Assert.AreSame(contentResult.Content, payment);
        }

        [TestMethod]
        public void FailIfPostedNewPaymentIsAlreadyInRepository()
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
            Payment payment = new Payment(new CashPayMethod(DateTime.Now), 100, list);
            var mockPaymentService = new Mock<IPaymentService>();
            mockPaymentService.Setup(x => x.CreatePayment(payment)).Throws(new ArgumentException());
            PaymentsController controller = new PaymentsController(mockPaymentService.Object);

            IHttpActionResult actionResult = controller.PostPayment(payment);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }
    }
}
