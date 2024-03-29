﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain;
using System.Collections.Generic;

namespace TodoPagos.Domain.Tests
{
    [TestClass]
    public class ReceiptShould
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailCreationIfProviderIsDeactivated()
        {
            List<IField> list = new List<IField>();
            NumberField aNumberField = new NumberField("ID");
            list.Add(aNumberField);
            Provider provider = new Provider("Antel", 20, list);
            provider.Active = false;
            IField completedNumberField = aNumberField.FillAndClone("8000");
            List<IField> completedFields = new List<IField>();
            completedFields.Add(completedNumberField);

            Receipt receipt = new Receipt(provider, completedFields, 0);
        }

        [TestMethod]
        public void BeAbleToTellItsProvider()
        {
            List<IField> list = new List<IField>();
            NumberField aNumberField = new NumberField("ID");
            list.Add(aNumberField);
            Provider provider = new Provider("Antel", 20, list);
            IField completedNumberField = aNumberField.FillAndClone("8000");
            List<IField> completedFields = new List<IField>();
            completedFields.Add(completedNumberField);

            Receipt receipt = new Receipt(provider, completedFields, 0);

            Assert.AreEqual(provider, receipt.ReceiptProvider);
        }

        [TestMethod]
        public void HaveTheNecessaryCompletedFields()
        {
            List<IField> list = new List<IField>();
            DateField datefield = new DateField("Fecha");
            NumberField numberField = new NumberField("Monto");
            list.Add(numberField);
            list.Add(datefield);
            Provider provider = new Provider("Antel", 20, list);
            IField completedDateField = datefield.FillAndClone("2012-09-17T22:02:51Z");
            IField completedNumberField = numberField.FillAndClone("8000");
            List<IField> completedFields = new List<IField>();
            completedFields.Add(completedDateField);
            completedFields.Add(completedNumberField);
        
            Receipt receipt = new Receipt(provider, completedFields, 0);
        }

        [TestMethod]
        public void KnowTheAmountToBePaid()
        {
            List<IField> list = new List<IField>();
            NumberField aNumberField = new NumberField("Coordenada X");
            list.Add(aNumberField);
            Provider provider = new Provider("Antel", 20, list);
            double amount = 10000;
            IField completedNumberField = aNumberField.FillAndClone("8000");
            List<IField> completedFields = new List<IField>();
            completedFields.Add(completedNumberField);

            Receipt receipt = new Receipt(provider, completedFields, amount);

            Assert.AreEqual(amount, receipt.Amount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfAmountIsLessThanZero()
        {
            List<IField> list = new List<IField>();
            TextField aTextField = new TextField("Apellido");
            list.Add(aTextField);
            Provider provider = new Provider("Antel", 20, list);
            double amount = -1;

            Receipt receipt = new Receipt(provider, new List<IField>(), amount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfNullFields()
        {
            List<IField> list = new List<IField>();
            DateField aDateField = new DateField("Fecha");
            list.Add(aDateField);
            Provider provider = new Provider("Antel", 20, list);
            double amount = 10000;

            Receipt receipt = new Receipt(provider, null, amount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfOneFieldIsEmpty()
        {
            List<IField> emptyFieldsList = new List<IField>();
            DateField aDateField = new DateField("Fecha");
            emptyFieldsList.Add(aDateField);
            Provider provider = new Provider("Antel", 20, emptyFieldsList);
            double amount = 10000;

            List<IField> completeFieldsList = new List<IField>();
            completeFieldsList.Add(aDateField);

            Receipt receipt = new Receipt(provider, completeFieldsList, amount);
        }

        [TestMethod]
        public void BeAbleToCalculateHowMuchTodoPagosEarnsFromIt()
        {
            List<IField> list = new List<IField>();
            NumberField aNumberField = new NumberField("Coordenada X");
            list.Add(aNumberField);
            Provider provider = new Provider("Antel", 20, list);
            double amount = 10000;
            IField completedNumberField = aNumberField.FillAndClone("8000");
            List<IField> completedFields = new List<IField>();
            completedFields.Add(completedNumberField);
            Receipt receipt = new Receipt(provider, completedFields, amount);
            double expectedEarning = 2000;

            double obtainedEarning = receipt.CalculateEarnings();

            Assert.AreEqual(expectedEarning, obtainedEarning);
        }

        [TestMethod]
        public void BeAbleToTellItIsEqualToAnotherReceiptWithTheSameFieldsAndData()
        {
            List<IField> list = new List<IField>();
            NumberField aNumberField = new NumberField("Coordenada X");
            list.Add(aNumberField);
            Provider provider = new Provider("Antel", 20, list);
            double amount = 10000;
            IField completedNumberField = aNumberField.FillAndClone("8000");
            List<IField> completedFields = new List<IField>();
            completedFields.Add(completedNumberField);
            Receipt firstReceipt = CreateReceipt(provider, completedFields, amount);
            Receipt secondReceipt = CreateReceipt(provider, completedFields, amount);

            Assert.AreNotSame(firstReceipt, secondReceipt);
            Assert.IsTrue(firstReceipt.Equals(secondReceipt));
        }

        private Receipt CreateReceipt(Provider provider, List<IField> completedFields, double amount)
        {
            return new Receipt(provider, completedFields, amount);
        }

        [TestMethod]
        public void BeAbleToTellItIsNotEqualToAnotherReceiptWithDifferentAmount()
        {
            List<IField> list = new List<IField>();
            NumberField aNumberField = new NumberField("Coordenada X");
            list.Add(aNumberField);
            Provider provider = new Provider("Antel", 20, list);
            double firstAmount = 10000;
            double secondAmount = 1000;
            IField completedNumberField = aNumberField.FillAndClone("8000");
            List<IField> completedFields = new List<IField>();
            completedFields.Add(completedNumberField);
            Receipt firstReceipt = CreateReceipt(provider, completedFields, firstAmount);
            Receipt secondReceipt = CreateReceipt(provider, completedFields, secondAmount);

            Assert.AreNotSame(firstReceipt, secondReceipt);
            Assert.IsFalse(firstReceipt.Equals(secondReceipt));
        }

        [TestMethod]
        public void BeAbleToTellItIsNotEqualToAnotherReceiptWithDifferentProvider()
        {
            List<IField> list = new List<IField>();
            NumberField aNumberField = new NumberField("Coordenada X");
            list.Add(aNumberField);
            Provider firstProvider = new Provider("Antel", 20, list);
            Provider secondProvider = new Provider("UTE", 20, list);
            secondProvider.ID = 1;
            double amount = 1000;
            IField completedNumberField = aNumberField.FillAndClone("8000");
            List<IField> completedFields = new List<IField>();
            completedFields.Add(completedNumberField);
            Receipt firstReceipt = CreateReceipt(firstProvider, completedFields, amount);
            Receipt secondReceipt = CreateReceipt(secondProvider, completedFields, amount);

            Assert.AreNotSame(firstReceipt, secondReceipt);
            Assert.IsFalse(firstReceipt.Equals(secondReceipt));
        }

        [TestMethod]
        public void BeAbleToTellItIsNotEqualToAnotherReceiptWithDifferentCompletedFieldsList()
        {
            List<IField> list = new List<IField>();
            NumberField aNumberField = new NumberField("Coordenada X");
            list.Add(aNumberField);
            Provider provider = new Provider("Antel", 20, list);
            double amount = 1000;
            IField firstCompletedNumberField = aNumberField.FillAndClone("8000");
            IField secondCompletedNumberField = aNumberField.FillAndClone("5000");
            List<IField> firstCompletedFields = new List<IField>();
            firstCompletedFields.Add(firstCompletedNumberField);
            List<IField> secondCompletedFields = new List<IField>();
            secondCompletedFields.Add(secondCompletedNumberField);
            Receipt firstReceipt = CreateReceipt(provider, firstCompletedFields, amount);
            Receipt secondReceipt = CreateReceipt(provider, secondCompletedFields, amount);

            Assert.AreNotSame(firstReceipt, secondReceipt);
            Assert.IsFalse(firstReceipt.Equals(secondReceipt));
        }

        [TestMethod]
        public void BeAbleToTellItIsNotEqualToANullObject()
        {
            List<IField> list = new List<IField>();
            NumberField aNumberField = new NumberField("Coordenada X");
            list.Add(aNumberField);
            Provider provider = new Provider("Antel", 20, list);
            double amount = 10000;
            IField completedNumberField = aNumberField.FillAndClone("8000");
            List<IField> completedFields = new List<IField>();
            completedFields.Add(completedNumberField);
            Receipt firstReceipt = CreateReceipt(provider, completedFields, amount);
            Receipt secondReceipt = null;

            Assert.IsFalse(firstReceipt.Equals(secondReceipt));
        }

        [TestMethod]
        public void BeAbleToTellItIsNotEqualToAnotherTypeOfObject()
        {
            List<IField> list = new List<IField>();
            NumberField aNumberField = new NumberField("Coordenada X");
            list.Add(aNumberField);
            Provider provider = new Provider("Antel", 20, list);
            double amount = 10000;
            IField completedNumberField = aNumberField.FillAndClone("8000");
            List<IField> completedFields = new List<IField>();
            completedFields.Add(completedNumberField);
            Receipt firstReceipt = CreateReceipt(provider, completedFields, amount);

            Assert.IsFalse(firstReceipt.Equals(provider));
        }

        [TestMethod]
        public void BeAbleToTellItsFieldsAreCompleteAndValid()
        {
            List<IField> list = new List<IField>();
            NumberField aNumberField = new NumberField("Coordenada X");
            list.Add(aNumberField);
            Provider provider = new Provider("Antel", 20, list);
            double amount = 10000;
            IField completedNumberField = aNumberField.FillAndClone("8000");
            List<IField> completedFields = new List<IField>();
            completedFields.Add(completedNumberField);
            Receipt receipt = CreateReceipt(provider, completedFields, amount);

            Assert.IsTrue(receipt.IsComplete());

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfCompletedFieldsAreNotTheSameAsProvidersFieldsIgnoringData()
        {
            List<IField> list = new List<IField>();
            NumberField aNumberField = new NumberField("Coordenada X");
            NumberField anotherNumberField = new NumberField("Coordenada Y");
            list.Add(aNumberField);
            Provider provider = new Provider("Antel", 20, list);
            double amount = 10000;
            IField completedNumberField = anotherNumberField.FillAndClone("8000");
            List<IField> completedFields = new List<IField>();
            completedFields.Add(completedNumberField);

            Receipt receipt = CreateReceipt(provider, completedFields, amount);
        }

        [TestMethod]
        public void BeAbleToTellIfItContainsCertainField()
        {
            List<IField> list = new List<IField>();
            NumberField aNumberField = new NumberField("Coordenada X");
            list.Add(aNumberField);
            Provider provider = new Provider("Antel", 20, list);
            double amount = 10000;
            IField completedNumberField = aNumberField.FillAndClone("8000");
            List<IField> completedFields = new List<IField>();
            completedFields.Add(completedNumberField);
            Receipt receipt = CreateReceipt(provider, completedFields, amount);

            Assert.IsTrue(receipt.ContainsField(completedNumberField));
        }

        [TestMethod]
        public void BeAbleToTellItNotComplete()
        {
            List<IField> list = new List<IField>();
            NumberField aNumberField = new NumberField("Coordenada X");
            list.Add(aNumberField);
            Provider provider = new Provider("Antel", 20, list);
            double amount = 10000;
            IField completedNumberField = aNumberField.FillAndClone("8000");
            List<IField> completedFields = new List<IField>();
            completedFields.Add(completedNumberField);
            Receipt receipt = CreateReceipt(provider, completedFields, amount);

            receipt.Amount = -10;

            Assert.IsFalse(receipt.IsComplete());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfProviderIsNull()
        {
            List<IField> list = new List<IField>();
            NumberField aNumberField = new NumberField("Coordenada X");
            NumberField anotherNumberField = new NumberField("Coordenada Y");
            list.Add(aNumberField);
            double amount = 10000;
            IField completedNumberField = anotherNumberField.FillAndClone("8000");
            List<IField> completedFields = new List<IField>();
            completedFields.Add(completedNumberField);

            Receipt receipt = CreateReceipt(null, completedFields, amount);
        }
    }
}
