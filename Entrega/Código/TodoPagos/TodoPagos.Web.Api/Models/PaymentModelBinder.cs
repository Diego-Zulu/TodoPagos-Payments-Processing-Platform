using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
    public class PaymentModelBinder : IModelBinder
    {
        private readonly string[] ACCEPTED_DATE_FORMATS = new[]{"yyyy-MM-ddTHH:mm:ssZ"};

        public PaymentModelBinder()
        {

        }

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            try
            {
                dynamic jsonParameters = ParseJsonObjectAndReturnAsDynamic(actionContext);
                Payment parsedPayment = ParsePaymentFromJsonParameters(jsonParameters);

                bindingContext.Model = parsedPayment;
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
            JsonReader reader = new JsonTextReader(new StringReader(body));
            reader.DateParseHandling = DateParseHandling.None;
            return JObject.Load(reader);
        }

        private Payment ParsePaymentFromJsonParameters(dynamic jsonParameters)
        {
            double amountPaid = jsonParameters.AmountPaid;
            PayMethod payMethod = ParsePayMethodFromJsonParameters(jsonParameters.PayMethod);

            JArray receiptsJsonArray = (JArray)jsonParameters.Receipts;
            ICollection<Receipt> receipts = ParseReceiptsFromJsonArray(receiptsJsonArray);

            Payment parsedPayment = new Payment();
            parsedPayment.PaymentMethod = payMethod;
            parsedPayment.Receipts = receipts;
            parsedPayment.SetPaidWithAndCalculateChange(amountPaid);
            return parsedPayment;
        }

        private PayMethod ParsePayMethodFromJsonParameters(dynamic payMethodJsonParameters)
        {
            string className = payMethodJsonParameters.Type;
            string payDateString = payMethodJsonParameters.PayDate;
            DateTime payDate = ParseToISO8061Date(payDateString);

            Type payMethodType = Type.GetType("TodoPagos.Domain." + className + ",Domain");
            ConstructorInfo payMethodConstructor = payMethodType.GetConstructor(new[] { typeof(DateTime) });
            dynamic payMethodInstance = payMethodConstructor.Invoke(new object[] { payDate });

            return payMethodInstance;
        }
        private DateTime ParseToISO8061Date(string dataToBeFilledWith)
        {
            return DateTime.ParseExact(dataToBeFilledWith, ACCEPTED_DATE_FORMATS,
                CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        private ICollection<Receipt> ParseReceiptsFromJsonArray(JArray receiptsJsonArray)
        {
            ICollection<Receipt> receipts = new List<Receipt>();

            foreach (dynamic oneJsonObject in receiptsJsonArray)
            {
                double amount = oneJsonObject.Amount;
                Provider receiptProvider = ParseReceiptProviderFromJsonParameters(oneJsonObject);

                JArray completedFieldsJsonArray = (JArray)oneJsonObject.CompletedFields;
                ICollection<IField> completedFields = ParseCompletedFieldsFromJsonArray(completedFieldsJsonArray);

                Receipt oneReceipt = ReturnNewReceiptWithFilledProperties(completedFields, amount, receiptProvider);
                receipts.Add(oneReceipt);
            }

            return receipts;
        }

        private Receipt ReturnNewReceiptWithFilledProperties(ICollection<IField> completedFields, double amount, Provider receiptProvider)
        {
            Receipt oneReceipt = new Receipt();
            oneReceipt.CompletedFields = completedFields;
            oneReceipt.Amount = amount;
            oneReceipt.ReceiptProvider = receiptProvider;

            return oneReceipt;
        }

        private Provider ParseReceiptProviderFromJsonParameters(dynamic oneJsonObject)
        {
            int id = oneJsonObject.ReceiptProviderID;

            Provider parsedProvider = new Provider();
            parsedProvider.ID = id;

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

        private ICollection<IField> ParseCompletedFieldsFromJsonArray(JArray fieldsJsonArray)
        {
            ICollection<IField> fields = new List<IField>();

            foreach (dynamic oneJsonObject in fieldsJsonArray)
            {
                string name = oneJsonObject.Name;
                string data = oneJsonObject.Data;
                string className = oneJsonObject.Type;

                Type fieldType = Type.GetType("TodoPagos.Domain." + className + ",Domain");
                ConstructorInfo fieldConstructor = fieldType.GetConstructor(new[] { typeof(string) });
                IField fieldInstance = (IField)fieldConstructor.Invoke(new object[] { name });

                IField filledFieldInstance = fieldInstance.FillAndClone(data);
                fields.Add(filledFieldInstance);
            }

            return fields;
        }

    }
}