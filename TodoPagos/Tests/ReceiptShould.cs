using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class ReceiptShould
    {

        [TestMethod]
        public void BeAbleToTellItsProvider()
        {
            Provider provider = new Provider("Antel", 20);

            Receipt receipt = new Receipt(provider);

            Assert.AreEqual(provider, receipt.ReceiptProvider);
        }

        [TestMethod]
        public void HaveTheNecessaryCompletedFields()
        {
            DateField datefield = new DateField("Fecha");
            NumberField numberField = new NumberField("Monto");
            IField completedDateField = datefield.FillAndClone("01/02/2014");
            IField completedNumberField = numberField.FillAndClone("8000");
            List<IField> completedFields = new List<IField>();
            completedFields.Add(completedDateField);
            completedFields.Add(completedNumberField);
        
            Receipt receipt = new Receipt(provider, completedFields);

            Assert.IsTrue(completedFields.ForEach(field => receipt.CompletedFields.contains(field)));
        }
    }
}
