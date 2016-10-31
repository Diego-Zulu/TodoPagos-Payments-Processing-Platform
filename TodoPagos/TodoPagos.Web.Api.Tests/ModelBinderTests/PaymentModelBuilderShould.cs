using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Web.Api.Models;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Linq;
using System.Web.Http.Metadata.Providers;
using TodoPagos.Domain;

namespace TodoPagos.Web.Api.Tests.ModelBinderTests
{
    [TestClass]
    public class PaymentModelBuilderShould
    {
        static PaymentModelBinder BINDER;
        static HttpControllerContext HTTP_CONTROLLER_CONTEXT;
        static ModelBindingContext BINDING_CONTEXT;

        [ClassInitialize()]
        public static void SetDataForUserModelBinderTests(TestContext testContext)
        {
            BINDER = new PaymentModelBinder();
            HTTP_CONTROLLER_CONTEXT = new HttpControllerContext();
            HTTP_CONTROLLER_CONTEXT.Request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/payments");

            BINDING_CONTEXT = new ModelBindingContext();

            var data = new DataAnnotationsModelMetadataProvider();

            var modelMetadata = data.GetMetadataForType(null, typeof(Payment));

            BINDING_CONTEXT.ModelMetadata = modelMetadata;
        }

        [TestMethod]
        public void BeAbleToCastJsonToPayment()
        {
            JObject paymentInJson = JObject.Parse("{\"AmountPaid\" : 1000, \"PayMethod\" : {\"Type\" : \"DebitPayMethod\", " +
            "\"PayDate\" : \"Mon, 15 Sep 2008 09:30:41 GMT\"}, \"Receipts\" : [{\"Amount\" :    1000, \"ReceiptProvider\" : {\"ID\" : 1, " +
            "\"Commission\" : 2, \"Name\" : \"Antel\", \"Active\" : true, \"Fields\" : [{ \"Type\" : \"NumberField\", " +
            "\"Name\" : \"Total\"}]},\"CompletedFields\" : [{\"Type\" : \"NumberField\",\"Data\" : \"123456\",\"Name\" : \"Total\" " +
            "}]}]}");

            Payment processedPayment = CreatePayment();

            HTTP_CONTROLLER_CONTEXT.Request.Content = new ObjectContent(typeof(JObject), paymentInJson, new JsonMediaTypeFormatter());
            var httpActionContext = new HttpActionContext();
            httpActionContext.ControllerContext = HTTP_CONTROLLER_CONTEXT;

            var result = BINDER.BindModel(httpActionContext, BINDING_CONTEXT);

            Assert.AreEqual(processedPayment, BINDING_CONTEXT.Model);

            BINDING_CONTEXT.Model = null;
        }

        private Payment CreatePayment()
        {
            IField providerEmptyField = new NumberField("Total");

            Provider oneprovider = new Provider("Antel", 10, new[] { providerEmptyField });

            IField providerCompleteField = providerEmptyField.FillAndClone("123456");

            Receipt providerReceipt = new Receipt(
                oneprovider, new[] { providerCompleteField }, 1000);


            return new Payment(new DebitPayMethod(DateTime.Today), 1000, new[] { providerReceipt });
        }

        [TestMethod]
        public void FailToCastJsonIfIncomplete()
        {
            JObject paymentInJson = JObject.Parse("{\"PayMethod\" : {\"Type\" : \"DebitPayMethod\", " +
           "\"PayDate\" : \"Mon, 15 Sep 2008 09:30:41 GMT\"}, \"Receipts\" : [{\"Amount\" :    1000, \"ReceiptProvider\" : {\"ID\" : 1, " +
           "\"Commission\" : 2, \"Name\" : \"Antel\", \"Active\" : true, \"Fields\" : [{ \"Type\" : \"NumberField\", " +
           "\"Name\" : \"Total\"}]},\"CompletedFields\" : [{\"Type\" : \"NumberField\",\"Data\" : \"123456\",\"Name\" : \"Total\" " +
           "}]}]}");

            HTTP_CONTROLLER_CONTEXT.Request.Content = new ObjectContent(typeof(JObject), paymentInJson, new JsonMediaTypeFormatter());
            var httpActionContext = new HttpActionContext();
            httpActionContext.ControllerContext = HTTP_CONTROLLER_CONTEXT;

            var result = BINDER.BindModel(httpActionContext, BINDING_CONTEXT);

            Assert.IsNull(BINDING_CONTEXT.Model);
        }
    }
}
