using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Web.Api.Controllers;
using Moq;
using TodoPagos.Web.Services;
using TodoPagos.Domain;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using System.Linq;
using System.Collections;
using TodoPagos.UserAPI;

namespace TodoPagos.Web.Api.Tests.ControllerIntegrationTests
{
    [TestClass]
    public class PaymentsControllerShould
    {
        static string ADMIN_USER_USEREMAIL = "diego@bruno.com";
        static User ADMIN_USER;
        static PaymentsController CONTROLLER;
        static Payment FIRST_PAYMENT;
        static Payment SECOND_PAYMENT;
        static ICollection<Payment> ALL_PAYMENTS;
        static Provider DESIGNED_PROVIDER;

        [ClassInitialize()]
        public static void SetReservedProviderInfoForTests(TestContext testContext)
        {
            ADMIN_USER = new User("Brulu", ADMIN_USER_USEREMAIL, "HOLA1234", AdminRole.GetInstance());
            List<IField> emptyFields = new List<IField>();
            TextField field = new TextField("NumeroCliente");
            emptyFields.Add(field);
            DESIGNED_PROVIDER = new Provider("Apple", 3, emptyFields);
            ProvidersController providerController = new ProvidersController(ADMIN_USER.Email);
            providerController.PostProvider(DESIGNED_PROVIDER);
            IHttpActionResult actionResultProvider = providerController.GetProviders();
            OkNegotiatedContentResult<IEnumerable<Provider>> contentResult = (OkNegotiatedContentResult<IEnumerable<Provider>>)actionResultProvider;
            IEnumerable<Provider> providers = contentResult.Content;
            foreach (Provider provider in providers)
            {
                if (provider.Name.Equals(DESIGNED_PROVIDER.Name))
                {
                    DESIGNED_PROVIDER.ID = provider.ID;
                }
            }
            providerController.Dispose();
            FIRST_PAYMENT = CreateFirstPayment();
            SECOND_PAYMENT = CreateNewRandomPayment();
            CONTROLLER = new PaymentsController(ADMIN_USER_USEREMAIL);
            CONTROLLER.PostPayment(FIRST_PAYMENT);
            CONTROLLER.PostPayment(SECOND_PAYMENT);
            CONTROLLER.Dispose();
        }

        [TestInitialize()]
        public void InsertTestsProviderInfoForTest()
        {
            CONTROLLER = new PaymentsController(ADMIN_USER_USEREMAIL);
            IHttpActionResult actionResult = CONTROLLER.GetPayments();
            OkNegotiatedContentResult<IEnumerable<Payment>> contentResult = 
                (OkNegotiatedContentResult<IEnumerable<Payment>>)actionResult;
            ALL_PAYMENTS = contentResult.Content.ToList();
            foreach(Payment payment in ALL_PAYMENTS)
            {
                if(payment.Equals(FIRST_PAYMENT)) FIRST_PAYMENT.ID = payment.ID;
                if (payment.Equals(SECOND_PAYMENT)) SECOND_PAYMENT.ID = payment.ID;
            }
        }

        [TestCleanup()]
        public void FinalizeTest()
        {
            CONTROLLER.Dispose();
        }

        private static Payment CreateFirstPayment()
        {
            TextField field = new TextField("NumeroCliente");
            IField firstFilledField = field.FillAndClone(Guid.NewGuid().ToString());
            List<IField> firstFullFields = new List<IField>();
            firstFullFields.Add(firstFilledField);
            IField secondFilledField = field.FillAndClone(Guid.NewGuid().ToString());
            List<IField> secondFullFields = new List<IField>();
            secondFullFields.Add(secondFilledField);
            Receipt firstReceipt = new Receipt(DESIGNED_PROVIDER, firstFullFields, 100);
            Receipt secondReceipt = new Receipt(DESIGNED_PROVIDER, secondFullFields, 100);
            List<Receipt> firstList = new List<Receipt>();
            firstList.Add(firstReceipt);
            firstList.Add(secondReceipt);
            return new Payment(new CashPayMethod(DateTime.Now), 200, firstList);
        }

        [TestMethod]
        public void BeAbleToReturnAllPaymentsInRepository()
        {
            IHttpActionResult actionResult = CONTROLLER.GetPayments();
            OkNegotiatedContentResult<IEnumerable<Payment>> contentResult = (OkNegotiatedContentResult<IEnumerable<Payment>>)actionResult;

            CollectionAssert.AreEquivalent((ICollection) contentResult.Content, (ICollection) ALL_PAYMENTS);
        }

        [TestMethod]
        public void BeAbleToReturnASinglePaymentInRepository()
        {
            IHttpActionResult actionResult = CONTROLLER.GetPayment(FIRST_PAYMENT.ID);
            OkNegotiatedContentResult<Payment> contentResult = (OkNegotiatedContentResult<Payment>)actionResult;

            Assert.AreEqual(contentResult.Content, FIRST_PAYMENT);
        }

        [TestMethod]
        public void FailIfSinglePaymentIdDoesntExistInRepository()
        {
            IHttpActionResult actionResult = CONTROLLER.GetPayment(0);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void BeAbleToPostNewPaymentIntoRepository()
        {
            Payment payment = CreateNewRandomPayment();

            IHttpActionResult actionResult = CONTROLLER.PostPayment(payment);
            CreatedAtRouteNegotiatedContentResult<Payment> contentResult = (CreatedAtRouteNegotiatedContentResult<Payment>)actionResult;

            Assert.AreEqual(contentResult.Content, payment);
        }

        private static Payment CreateNewRandomPayment()
        {
            TextField field = new TextField("NumeroCliente");
            IField filledField = field.FillAndClone(Guid.NewGuid().ToString());
            List<IField> fullFields = new List<IField>();
            fullFields.Add(filledField);
            Receipt firstReceipt = new Receipt(DESIGNED_PROVIDER, fullFields, 100);
            List<Receipt> list = new List<Receipt>();
            list.Add(firstReceipt);
            return new Payment(new CashPayMethod(DateTime.Now), 100, list);
        }

        [TestMethod]
        public void FailIfPostedNewPaymentIsAlreadyInRepository()
        {
            IHttpActionResult actionResult = CONTROLLER.PostPayment(FIRST_PAYMENT);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }
    }
}
