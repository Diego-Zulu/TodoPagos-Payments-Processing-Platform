using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace Tests
{
    [TestClass]
    public class NumberFieldShould
    {
        [TestMethod]
        public void BeAbleToReturnItsDataTest()
        {
            NumberField numberField = new NumberField();

            string expectedResult = "0";

            Assert.AreEqual(expectedResult, numberField.GetData());
        }
    }
}
