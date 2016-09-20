using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class NumberFieldShould
    {
        [TestMethod]
        public void BeAbleToReturnItsDataTest()
        {
            NumberField numberField = new NumberField();

            numberField.data = 15;
            string expectedResult = "15";

            Assert.AreEqual(expectedResult, numberField.GetData());
        }
    }
}
