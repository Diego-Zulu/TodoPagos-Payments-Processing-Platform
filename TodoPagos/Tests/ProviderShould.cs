using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

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
    }
}
