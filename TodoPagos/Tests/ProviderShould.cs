using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class ProviderShould
    {
        [TestMethod]
        public void BeAbleToModifyHisCommission()
        {
            Provider provider = new Provider("Antel", 20);

            provider.ChangeCommission(15);

            Assert.AreEqual(15, provider.Commission);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWhenChangingCommissionToANegativeValue()
        {
            Provider provider = new Provider("Antel", 20);

            provider.ChangeCommission(-15);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWhenCreatingNewInstanceWithNegativeCommissionValue()
        {
            Provider provider = new Provider("Antel", -20);
        }

        [TestMethod]
        public void BeAbleToAddNewFields()
        {
            Provider provider = new Provider("Antel", 20);
            NumberField numericField = new NumberField("Monto");

            provider.AddField(numericField);

            Assert.IsTrue(provider.ContainsField(numericField));
        }

        [TestMethod]
        public void BeAbleToRemoveAField()
        {
            Provider provider = new Provider("Antel", 20);
            NumberField numericField = new NumberField("Monto");

            provider.AddField(numericField);
            provider.RemoveField(numericField);

            Assert.IsFalse(provider.ContainsField(numericField));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfUserTriesToRemoveAFieldThatIsNotInFieldsList()
        {
            Provider provider = new Provider("Antel", 20);
            NumberField numericField = new NumberField("Monto");
            DateField dateField = new DateField("Fecha");

            provider.AddField(numericField);
            provider.RemoveField(dateField);
        }

        [TestMethod]
        public void BeAbleToTellIfItIsEqualToAnotherProvider()
        {
            Provider firstProvider = new Provider("Antel", 20);
            Provider secondProvider = new Provider("Antel", 10);

            Assert.AreEqual(firstProvider, secondProvider);
        }
    }
}
