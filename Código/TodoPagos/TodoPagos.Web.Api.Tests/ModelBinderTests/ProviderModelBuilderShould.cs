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
    public class ProviderModelBuilderShould
    {
        static ProviderModelBinder BINDER;
        static HttpControllerContext HTTP_CONTROLLER_CONTEXT;
        static ModelBindingContext BINDING_CONTEXT;

        [ClassInitialize()]
        public static void SetDataForUserModelBinderTests(TestContext testContext)
        {
            BINDER = new ProviderModelBinder();
            HTTP_CONTROLLER_CONTEXT = new HttpControllerContext();
            HTTP_CONTROLLER_CONTEXT.Request = new HttpRequestMessage(HttpMethod.Put, "http://localhost/providers");

            BINDING_CONTEXT = new ModelBindingContext();

            var data = new DataAnnotationsModelMetadataProvider();

            var modelMetadata = data.GetMetadataForType(null, typeof(Provider));

            BINDING_CONTEXT.ModelMetadata = modelMetadata;
        }

        [TestMethod]
        public void BeAbleToCastJsonToProvider()
        {
            IField firstProviderEmptyField = new NumberField("Total");
            Provider processedProvider = new Provider("Antel", 2, new[] { firstProviderEmptyField });

            JObject providerInJson = JObject.Parse("{\"ID\" : 1, \"Commission\" : 2, \"Name\" : \"Antel\", " +
			"\"Active\" : true, \"Fields\" : [{\"Type\" : \"NumberField\", \"Name\" : \"Total\"}]}");

            HTTP_CONTROLLER_CONTEXT.Request.Content = new ObjectContent(typeof(JObject), providerInJson, new JsonMediaTypeFormatter());
            var httpActionContext = new HttpActionContext();
            httpActionContext.ControllerContext = HTTP_CONTROLLER_CONTEXT;

            var result = BINDER.BindModel(httpActionContext, BINDING_CONTEXT);

            Assert.AreEqual(processedProvider, BINDING_CONTEXT.Model);

            BINDING_CONTEXT.Model = null;
        }

        [TestMethod]
        public void FailToCastJsonIfIncomplete()
        {
            JObject providerInJson = JObject.Parse("{\"ID\" : 1, \"Name\" : \"Antel\", " +
            "\"Fields\" : [{\"Type\" : \"NumberField\", \"Name\" : \"Total\"}]}");

            HTTP_CONTROLLER_CONTEXT.Request.Content = new ObjectContent(typeof(JObject), providerInJson, new JsonMediaTypeFormatter());
            var httpActionContext = new HttpActionContext();
            httpActionContext.ControllerContext = HTTP_CONTROLLER_CONTEXT;

            var result = BINDER.BindModel(httpActionContext, BINDING_CONTEXT);

            Assert.IsNull(BINDING_CONTEXT.Model);
        }
    }
}
