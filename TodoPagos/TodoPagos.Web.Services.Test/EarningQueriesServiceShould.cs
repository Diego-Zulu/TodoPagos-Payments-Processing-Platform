using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain.Repository;
using Moq;
using System.Collections.Generic;
using TodoPagos.Domain;

namespace TodoPagos.Web.Services.Test
{
    [TestClass]
    public class EarningQueriesServiceShould
    {
        [TestMethod]
        public void ReceiveAUnitOfWorkOnCreation()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            EarningQueriesService service = new EarningQueriesService(mockUnitOfWork.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfUnitOfWorkOnCreationIsNull()
        {
            IUnitOfWork mockUnitOfWork = null;

            EarningQueriesService service = new EarningQueriesService(mockUnitOfWork);
        }

        [TestMethod]
        public void BeAbleToGetEarningsPerProvider()
        {
            KeyValuePair<Payment, Provider> pair = CreateNewPaymentAndProvider();
            Payment payment = pair.Key;
            IDictionary<Provider, double> expectedDictionary = new Dictionary<Provider, double>();
            expectedDictionary.Add(pair.Value, 3);
            List<Payment> paymentsList = new List<Payment>();
            paymentsList.Add(payment);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.PaymentRepository.Get(null, null, "")).Returns(paymentsList);
            EarningQueriesService earningQueries = new EarningQueriesService(mockUnitOfWork.Object);

            IDictionary<Payment, double> resultingDictionary = 
                (IDictionary<Payment, double>) earningQueries.GetEarningsPerProvider(DateTime.Today, DateTime.Today);

            mockUnitOfWork.VerifyAll();
        }

        private KeyValuePair<Payment,Provider> CreateNewPaymentAndProvider()
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
            return new KeyValuePair<Payment, Provider>
                (new Payment(new CashPayMethod(100, DateTime.Now), 100, list), provider);
        }
    }
}
