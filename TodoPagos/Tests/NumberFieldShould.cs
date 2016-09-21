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
            NumberField numberField = new NumberField("Monto");

            string expectedResult = "0";

            Assert.AreEqual(expectedResult, numberField.GetData());
        }


        [TestMethod]
        public void ReturnANewNumberFieldWhenFilledTest()
        {
            NumberField numberField = new NumberField("Monto");

            IField newNumberField = numberField.FillAndClone("15");

            Assert.AreNotSame(numberField, newNumberField);
        }

        [TestMethod]
        public void AllowToBeFilledTest()
        {
            NumberField numberField = new NumberField("Monto");

            IField newNumberField = numberField.FillAndClone("15");
            string expectedResult = "15";

            Assert.AreEqual(expectedResult, newNumberField.GetData());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWhenDataToBeFilledWithIsNotNumericTest()
        {
            NumberField numberField = new NumberField("Monto");

            IField newNumberField = numberField.FillAndClone("hello");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWhenDataToBeFilledWithIsNullTest()
        {
            NumberField numberField = new NumberField("Monto");

            IField newNumberField = numberField.FillAndClone(null);
        }

        [TestMethod]
        public void TellItIsValidWhenItStoresANumberGreaterThanZeroTest()
        {
            NumberField numberField = new NumberField("Monto");

            IField newNumberField = numberField.FillAndClone("1");

            Assert.IsTrue(newNumberField.IsValid());
        }

        [TestMethod]
        public void TellItIsNotValidWhenItStoresANumberSmallerOrEqualToZeroTest()
        {
            NumberField numberField = new NumberField("Monto");

            IField newNumberField = numberField.FillAndClone("0");

            Assert.IsFalse(newNumberField.IsValid());
        }

        [TestMethod]
        public void BeAbleToTellIfItIsEqualToAnotherNumberFieldTest()
        {
            NumberField firstNumberField = new NumberField("Monto");
            NumberField secondNumberField = new NumberField("Monto");

            IField firstNewNumberField = firstNumberField.FillAndClone("2");
            IField secondNewNumberField = secondNumberField.FillAndClone("2");

            Assert.IsTrue(firstNewNumberField.Equals(secondNewNumberField));
        }

        [TestMethod]
        public void BeAbleToTellItIsNotEqualToANullObjectTest()
        {
            NumberField firstNumberField = new NumberField("Monto");

            IField firstNewNumberField = firstNumberField.FillAndClone("2");

            Assert.IsFalse(firstNewNumberField.Equals(null));
        }

        [TestMethod]
        public void BeAbleToTellItIsNotEqualToAnotherTypeOfFieldTest()
        {
            NumberField aNumberField = new NumberField("Monto");

            DateField aDateField = new DateField("Fecha");

            Assert.IsFalse(aNumberField.Equals(aDateField));
        }
    }
}
