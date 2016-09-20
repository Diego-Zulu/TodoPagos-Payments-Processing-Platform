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

            IField newNumberField = numberField.FillAndClone("15");
            string expectedResult = "15";

            Assert.AreEqual(expectedResult, newNumberField.GetData());
        }

        [TestMethod]
        public void ReturnANewNumberFieldWhenFilledTest()
        {
            NumberField numberField = new NumberField();

            IField newNumberField = numberField.FillAndClone("15");

            Assert.AreNotSame(numberField, newNumberField);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void FailWhenDataToBeFilledWithIsNotNumericTest()
        {
            NumberField numberField = new NumberField();

            IField newNumberField = numberField.FillAndClone("hello");
        }

        [TestMethod]
        public void TellItIsValidWhenItStoresANumberGreaterThanZeroTest()
        {
            NumberField numberField = new NumberField();

            IField newNumberField = numberField.FillAndClone("1");

            Assert.IsTrue(newNumberField.IsValid());
        }

        [TestMethod]
        public void TellItIsNotValidWhenItStoresANumberSmallerOrEqualToZeroTest()
        {
            NumberField numberField = new NumberField();

            IField newNumberField = numberField.FillAndClone("0");

            Assert.IsFalse(newNumberField.IsValid());
        }
    }
}
