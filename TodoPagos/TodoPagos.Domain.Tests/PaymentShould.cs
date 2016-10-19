using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain;
using System.Collections.Generic;
using System.Linq;

namespace TodoPagos.Domain.Tests
{
    [TestClass]
    public class PaymentShould
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveANullPaymentMethod()
        {
            PayMethod paymentMethod = null;
            List<IField> emptyFieldList = new List<IField>();
            NumberField aNumberField = new NumberField("Numerito");
            emptyFieldList.Add(aNumberField);

            Provider provider = new Provider("Antel", 20, emptyFieldList);

            IField aCompleteNumberField = aNumberField.FillAndClone("1234");
            List<IField> completeFieldList = new List<IField>();
            completeFieldList.Add(aCompleteNumberField);
            double amount = 10000;
            Receipt receipt = new Receipt(provider, completeFieldList, amount);

            List<Receipt> receipts = new List<Receipt>();
            receipts.Add(receipt);

            int amountPayed = 500;
            Payment newPayment = new Payment(paymentMethod, amountPayed, receipts);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HavePositiveAmountPayed()
        {
            PayMethod paymentMethod = new DebitPayMethod(DateTime.Now);
            List<IField> emptyFieldList = new List<IField>();
            NumberField aNumberField = new NumberField("Numerito");
            emptyFieldList.Add(aNumberField);

            Provider provider = new Provider("Antel", 20, emptyFieldList);

            IField aCompleteNumberField = aNumberField.FillAndClone("1234");
            List<IField> completeFieldList = new List<IField>();
            completeFieldList.Add(aCompleteNumberField);
            double amount = 10000;
            Receipt receipt = new Receipt(provider, completeFieldList, amount);

            List<Receipt> receipts = new List<Receipt>();
            receipts.Add(receipt);

            int amountPayed = -1000;
            Payment newPayment = new Payment(paymentMethod, amountPayed, receipts);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveNullReceipts()
        {
            int amountPayed = 500;
            PayMethod paymentMethod = new DebitPayMethod(DateTime.Now);
            ICollection<Receipt> receipts = null;

            Payment newPayment = new Payment(paymentMethod, amountPayed, receipts);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HaveMinimumOneReceipt()
        {
            PayMethod paymentMethod = new DebitPayMethod(DateTime.Now);
            List<Receipt> receipts = new List<Receipt>();

            int amountPayed = 7500;
            Payment newPayment = new Payment(paymentMethod, amountPayed, receipts);
        }

        [TestMethod]
        public void BeAbleToAddAllEarningsToExistingDictionaryOfEarningsPerProviderGivenFromAndToDates()
        {
            IDictionary<Provider, double> earningsPerProvider = new Dictionary<Provider, double>();
            PayMethod paymentMethod = new DebitPayMethod(DateTime.Today);
            List<IField> emptyFieldList = new List<IField>();
            NumberField aNumberField = new NumberField("Numerito");
            emptyFieldList.Add(aNumberField);
            Provider provider = new Provider("Antel", 20, emptyFieldList);
            IField aCompleteNumberField = aNumberField.FillAndClone("1234");
            List<IField> completeFieldList = new List<IField>();
            completeFieldList.Add(aCompleteNumberField);
            double amount = 10000;
            Receipt receipt = new Receipt(provider, completeFieldList, amount);
            List<Receipt> receipts = new List<Receipt>();
            receipts.Add(receipt);
            int amountPayed = 10000;
            Payment newPayment = new Payment(paymentMethod, amountPayed, receipts);
            IDictionary<Provider, double> expectedDictionary = new Dictionary<Provider, double>();
            expectedDictionary.Add(provider, 2000);

            newPayment.AddThisPaymentsEarningsToDictionary(earningsPerProvider, DateTime.Today, DateTime.Today);

            bool result = true;
            foreach(KeyValuePair<Provider, double> pair in expectedDictionary)
            {
                if (!earningsPerProvider.Contains(pair)) result = false;
            }
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BeAbleToAddItsEarningsToExistingEarningsValueGivenFromAndToDates()
        {
            IDictionary<Provider, double> earningsPerProvider = new Dictionary<Provider, double>();
            Payment newPayment = CreatePayment();
            double expectedValue = 2000;
            double overallValue = 0;

            newPayment.AddThisPaymentsEarningsToOverallValue(ref overallValue, DateTime.Today, DateTime.Today);

            Assert.AreEqual(expectedValue, overallValue);
        }

        [TestMethod]
        public void BeAbleToTellIfItsReceiptsAreValid()
        {
            Payment payment = CreatePayment();

            Assert.IsTrue(payment.IsComplete());
        }

        private Payment CreatePayment()
        {
            PayMethod paymentMethod = new DebitPayMethod(DateTime.Today);
            List<IField> emptyFieldList = new List<IField>();
            NumberField aNumberField = new NumberField("Numerito");
            emptyFieldList.Add(aNumberField);
            Provider provider = new Provider("Antel", 20, emptyFieldList);
            IField aCompleteNumberField = aNumberField.FillAndClone("1234");
            List<IField> completeFieldList = new List<IField>();
            completeFieldList.Add(aCompleteNumberField);
            double amount = 10000;
            Receipt receipt = new Receipt(provider, completeFieldList, amount);
            List<Receipt> receipts = new List<Receipt>();
            receipts.Add(receipt);
            int amountPayed = 10000;
            return new Payment(paymentMethod, amountPayed, receipts);
        }

        [TestMethod]
        public void BeAbleToTellIfItIsEqualToAnotherPayment()
        {
            Payment firstPayment = CreatePayment();
            Payment secondPayment = CreatePayment();

            Assert.IsTrue(firstPayment.Equals(secondPayment));
        }

        [TestMethod]
        public void BeAbleToTellItIsNotEqualToANullObject()
        {
            Payment payment = CreatePayment();

            Assert.IsFalse(payment.Equals(null));
        }

        [TestMethod]
        public void BeAbleToTellItIsNotEqualToAnotherTypeOfObject()
        {
            Provider provider = new Provider("Antel", 20, new List<IField>());
            Payment payment = CreatePayment();

            Assert.IsFalse(payment.Equals(provider));
        }

        [TestMethod]
        public void TellItIsNotCompleteWhenPaymentTotalIsNotEqualToTheSumOfItsReceipts()
        {

            Payment payment = CreatePayment();
            payment.Receipts.Add(CreateReceipt());

            Assert.IsFalse(payment.IsComplete());
        }

        private Receipt CreateReceipt()
        {
            List<IField> emptyFieldList = new List<IField>();
            NumberField aNumberField = new NumberField("Numerito");
            emptyFieldList.Add(aNumberField);

            Provider provider = new Provider("Antel", 20, emptyFieldList);

            IField aCompleteNumberField = aNumberField.FillAndClone("1234");
            List<IField> completeFieldList = new List<IField>();
            completeFieldList.Add(aCompleteNumberField);
            double amount = 1500;
            return new Receipt(provider, completeFieldList, amount);
        }
    }
}
