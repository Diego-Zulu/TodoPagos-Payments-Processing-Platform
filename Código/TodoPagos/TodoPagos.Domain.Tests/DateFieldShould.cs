using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain;

namespace TodoPagos.Domain.Tests
{
    [TestClass]
    public class DateFieldShould
    {
        [TestMethod]
        public void BeAbleToReturnItsData()
        {
            DateField dateField = new DateField("Fecha");

            DateTime date = DateTime.Today;
            dateField.Data = date;
            string expectedResult = date.ToShortDateString();

            Assert.AreEqual(expectedResult, dateField.GetData());
        }

        [TestMethod]
        public void ReturnANewIFieldWhenFilled()
        {
            DateField dateField = new DateField("Fecha");

            IField newDateField = dateField.FillAndClone("Mon, 15 Sep 2008 09:30:41 GMT");

            Assert.AreNotSame(dateField, newDateField);
        }

        [TestMethod]
        public void AllowToBeFilled()
        {
            DateField dateField = new DateField("Fecha");

            IField newDateField = dateField.FillAndClone("Mon, 15 Sep 2008 09:30:41 GMT");
            DateTime expectedResult = DateTime.Parse("Mon, 15 Sep 2008 09:30:41 GMT");

            Assert.AreEqual(expectedResult.ToShortDateString(), newDateField.GetData());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWhenDataToBeFilledWithIsNull()
        {
            DateField dateField = new DateField("Fecha");

            IField newDateField = dateField.FillAndClone(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWhenDataToBeFilledWithIsNotValidDateTime()
        {
            DateField dateField = new DateField("Fecha");

            IField newDateField = dateField.FillAndClone("hola");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWhenTheProvidedDateIsNotInGMTFormat()
        {
            DateField dateField = new DateField("Fecha");

            IField newDateField = dateField.FillAndClone("05/09/2016");
        }

        [TestMethod]
        public void BeAbleToTellItIsEqualToAnotherDateFieldWithSameNameAndData()
        {
            DateField firstDateField = new DateField("Fecha");
            DateField secondDateField = new DateField("Fecha");

            IField firstNewDateField = firstDateField.FillAndClone("Sun, 25 Oct 2015 10:30:41 GMT");
            IField secondNewDateField = secondDateField.FillAndClone("Sun, 25 Oct 2015 10:30:41 GMT");

            Assert.IsTrue(firstNewDateField.Equals(secondNewDateField));
        }

        [TestMethod]
        public void BeAbleToTellItIsNotEqualToAnotherDateFieldWithSameNameAndDifferentData()
        {
            DateField firstDateField = new DateField("Fecha");
            DateField secondDateField = new DateField("Fecha");

            IField firstNewDateField = firstDateField.FillAndClone("Mon, 15 Sep 2008 09:30:41 GMT");
            IField secondNewDateField = secondDateField.FillAndClone("Sun, 25 Oct 2015 10:30:41 GMT");

            Assert.IsFalse(firstNewDateField.Equals(secondNewDateField));
        }

        [TestMethod]
        public void BeAbleToTellItIsNotEqualToANullObject()
        {
            DateField firstDateField = new DateField("Fecha");

            IField firstNewDateField = firstDateField.FillAndClone("Mon, 11 Jan 2010 15:31:15 GMT");

            Assert.IsFalse(firstNewDateField.Equals(null));
        }

        [TestMethod]
        public void BeAbleToTellItIsNotEqualToAnotherTypeOfField()
        {
            DateField aDateField = new DateField("Fecha");

            IField aNewDateField = aDateField.FillAndClone("Sun, 25 Oct 2015 10:30:41 GMT");

            NumberField aNumberField = new NumberField("Monto");

            Assert.IsFalse(aNewDateField.Equals(aNumberField));
        }
    }
}
