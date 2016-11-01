using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using TodoPagos.Domain;

namespace TodoPagos.Web.Api.Models
{
    public class ProviderModelBinder : IModelBinder
    {
        public ProviderModelBinder()
        {

        }

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            try
            {
                dynamic jsonParameters = ParseJsonObjectAndReturnAsDynamic(actionContext);
                Provider parsedProvider = ParseProviderFromJsonParameters(jsonParameters);

                AddIDToParsedProviderIfNeeded(actionContext, jsonParameters, parsedProvider);

                bindingContext.Model = parsedProvider;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private dynamic ParseJsonObjectAndReturnAsDynamic(HttpActionContext actionContext)
        {
            Task<string> content = actionContext.Request.Content.ReadAsStringAsync();
            string body = content.Result;
            return JObject.Parse(body);
        }

        private Provider ParseProviderFromJsonParameters(dynamic providerJsonParameters)
        {
            double commission = providerJsonParameters.Commission;
            string name = providerJsonParameters.Name;

            JArray providerFieldsJsonArray = (JArray)providerJsonParameters.Fields;
            ICollection<IField> providerFields = ParseProviderFieldsFromJsonArray(providerFieldsJsonArray);

            Provider parsedProvider = new Provider(name, commission, providerFields);

            return parsedProvider;
        }

        private ICollection<IField> ParseProviderFieldsFromJsonArray(JArray fieldsJsonArray)
        {
            ICollection<IField> fields = new List<IField>();

            foreach (dynamic oneJsonObject in fieldsJsonArray)
            {
                string name = oneJsonObject.Name;
                string className = oneJsonObject.Type;

                Type fieldType = Type.GetType("TodoPagos.Domain." + className + ",Domain");
                ConstructorInfo fieldConstructor = fieldType.GetConstructor(new[] { typeof(string) });
                dynamic fieldInstance = fieldConstructor.Invoke(new object[] { name });

                fields.Add(fieldInstance);
            }

            return fields;
        }

        private void AddIDToParsedProviderIfNeeded(HttpActionContext actionContext, dynamic jsonParameters, Provider parsedProvider)
        {
            if (actionContext.Request.Method == HttpMethod.Put
                    || actionContext.Request.Method == HttpMethod.Delete)
            {
                int id = jsonParameters.ID;
                parsedProvider.ID = id;
            }
        }
    }
}