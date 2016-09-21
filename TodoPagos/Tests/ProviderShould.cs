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
        public void BeAbleToModifyHisCommissionTest()
        {
            Provider provider = new Provider("Antel", 20);

            provider.ChangeCommission(15);

            Assert.AreEqual(15, provider.Commission);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWhenChangingCommissionToANegativeValueTest()
        {
            Provider provider = new Provider("Antel", 20);

            provider.ChangeCommission(-15);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWhenCreatingNewInstanceWithNegativeCommissionValueTest()
        {
            Provider provider = new Provider("Antel", -20);
        }

        [TestMethod]
        public void BeAbleToAddNewFieldsTest()
        {
            Provider provider = new Provider("Antel", 20);
            NumberField numericField = new NumberField();

            provider.AddField(numericField);

            Assert.IsTrue(provider.ContainsField(numericField));
        }

        [TestMethod]
        public void BeAbleToRemoveAFieldTest()
        {
            Provider provider = new Provider("Antel", 20);
            NumberField numericField = new NumberField();

            provider.AddField(numericField);
            provider.RemoveField(numericField);

            Assert.IsFalse(provider.ContainsField(numericField));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfUserTriesToRemoveAFieldThatIsNotInFieldsListTest()
        {
            Provider provider = new Provider("Antel", 20);
            NumberField numericField = new NumberField();
            DateField dateField = new DateField();

            provider.AddField(numericField);
            provider.RemoveField(dateField);
        }
    }
}
