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

        [TestMethod]
        public void AllowToBeFilledTest()
        {
            NumberField numberField = new NumberField();

            numberField.FillAndClone("15");
            string expectedResult = "15";

            Assert.AreEqual(expectedResult, numberField.GetData());
        }
    }
}
