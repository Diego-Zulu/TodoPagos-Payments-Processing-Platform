using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace Tests
{
    [TestClass]
    public class DateFieldShould
    {
        [TestMethod]
        public void BeAbleToReturnItsDataTest()
        {
            DateField dateField = new DateField();

            DateTime date = DateTime.Today;
            dateField.Data = date;
            string expectedResult = date.ToShortDateString();

            Assert.AreEqual(expectedResult, dateField.GetData());
        }

        [TestMethod]
        public void ReturnANewDateFieldWhenFilledTest()
        {
            DateField dateField = new DateField();

            IField newDateField = dateField.FillAndClone("15/2/2015");

            Assert.AreNotSame(dateField, newDateField);
        }

        [TestMethod]
        public void AllowToBeFilledTest()
        {
            DateField dateField = new DateField();

            IField newDateField = dateField.FillAndClone("15/2/2015");
            string expectedResult = "15/2/2015";

            Assert.AreEqual(expectedResult, newDateField.GetData());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWhenDataToBeFilledWithIsNullTest()
        {
            DateField dateField = new DateField();

            IField newDateField = dateField.FillAndClone(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWhenDataToBeFilledWithIsNotValidDateTimeTest()
        {
            DateField dateField = new DateField();

            IField newDateField = dateField.FillAndClone("hola");
        }

        [TestMethod]
        public void TellItIsValidWhenItStoresADateLaterThan2013Test()
        {
            DateField dateField = new DateField();

            IField newDateField = dateField.FillAndClone("1/1/2014");

            Assert.IsTrue(newDateField.IsValid());
        }

        [TestMethod]
        public void TellItIsNotValidWhenItStoresADateBeforeThan2014Test()
        {
            DateField dateField = new DateField();

            IField newDateField = dateField.FillAndClone("31/12/2013");

            Assert.IsFalse(newDateField.IsValid());
        }

        [TestMethod]
        public void BeAbleToTellIfItIsEqualToAnotherDateFieldTest()
        {
            DateField firstDateField = new DateField();
            DateField secondDateField = new DateField();

            IField firstNewDateField = firstDateField.FillAndClone("31/12/2013");
            IField secondNewDateField = secondDateField.FillAndClone("31/12/2013");

            Assert.IsTrue(firstNewDateField.Equals(secondNewDateField));
        }
    }
}
