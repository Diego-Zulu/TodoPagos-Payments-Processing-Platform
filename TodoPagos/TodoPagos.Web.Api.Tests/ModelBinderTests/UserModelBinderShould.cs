using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Web.Api.Models;
using TodoPagos.UserAPI;
using Moq;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Linq;
using System.Web.Http.Metadata.Providers;

namespace TodoPagos.Web.Api.Tests.ModelBinderTests
{
    [TestClass]
    public class UserModelBinderShould
    {
        static UserModelBinder BINDER;
        static HttpControllerContext HTTP_CONTROLLER_CONTEXT;
        static ModelBindingContext BINDING_CONTEXT;

        [ClassInitialize()]
        public static void SetDataForUserModelBinderTests(TestContext testContext)
        {
            BINDER = new UserModelBinder();
            HTTP_CONTROLLER_CONTEXT = new HttpControllerContext();
            HTTP_CONTROLLER_CONTEXT.Request = new HttpRequestMessage(HttpMethod.Put, "http://localhost/users");

            BINDING_CONTEXT = new ModelBindingContext();

            var data = new DataAnnotationsModelMetadataProvider();

            var modelMetadata = data.GetMetadataForType(null, typeof(User));

            BINDING_CONTEXT.ModelMetadata = modelMetadata;
        }

        [TestMethod]
        public void BeAbleToCastJsonToUser()
        {
            JObject userInJson = JObject.Parse("{\"ID\" : 1, \"Name\" : \"Diego\", \"Email\" : \"dizg2695@gmail.com\", "
                    + "\"Password\" : \"Holi1234!\", \"Roles\" : [\"AdminRole\"] }");
            User processedUser = new User("Diego", "dizg2695@gmail.com", "Holi1234!", AdminRole.GetInstance());

            HTTP_CONTROLLER_CONTEXT.Request.Content = new ObjectContent(typeof(JObject), userInJson, new JsonMediaTypeFormatter());
            var httpActionContext = new HttpActionContext();
            httpActionContext.ControllerContext = HTTP_CONTROLLER_CONTEXT;

            var result = BINDER.BindModel(httpActionContext, BINDING_CONTEXT);

            Assert.AreEqual(processedUser, BINDING_CONTEXT.Model);

            BINDING_CONTEXT.Model = null;
        }

        [TestMethod]
        public void FailToCastJsonIfIncomplete()
        {
            JObject userInJson = JObject.Parse("{\"Email\" : \"dizg2695@gmail.com\", "
                    + "\"Password\" : \"Holi1234!\", \"Roles\" : [\"AdminRole\"] }");
            User processedUser = new User("Diego", "dizg2695@gmail.com", "Holi1234!", AdminRole.GetInstance());

            HTTP_CONTROLLER_CONTEXT.Request.Content = new ObjectContent(typeof(JObject), userInJson, new JsonMediaTypeFormatter());
            var httpActionContext = new HttpActionContext();
            httpActionContext.ControllerContext = HTTP_CONTROLLER_CONTEXT;

            var result = BINDER.BindModel(httpActionContext, BINDING_CONTEXT);

            Assert.IsNull(BINDING_CONTEXT.Model);
        }
    }
}
