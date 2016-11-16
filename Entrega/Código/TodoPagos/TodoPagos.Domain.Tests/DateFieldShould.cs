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
            string expectedResult = date.ToString("yyyy-MM-ddTHH:mm:ssZ");

            Assert.AreEqual(expectedResult, dateField.GetData());
        }

        [TestMethod]
        public void ReturnANewIFieldWhenFilled()
        {
            DateField dateField = new DateField("Fecha");

            IField newDateField = dateField.FillAndClone("2008-09-22T14:01:54Z");

            Assert.AreNotSame(dateField, newDateField);
        }

        [TestMethod]
        public void AllowToBeFilled()
        {
            DateField dateField = new DateField("Fecha");

            IField newDateField = dateField.FillAndClone("2008-09-22T14:01:54Z");
            DateTime expectedResult = DateTime.Parse("2008-09-22T14:01:54Z");

            Assert.AreEqual(expectedResult.ToString("yyyy-MM-ddTHH:mm:ssZ"), newDateField.GetData());
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

            IField firstNewDateField = firstDateField.FillAndClone("2015-10-26T14:01:54Z");
            IField secondNewDateField = secondDateField.FillAndClone("2015-10-26T14:01:54Z");

            Assert.IsTrue(firstNewDateField.Equals(secondNewDateField));
        }

        [TestMethod]
        public void BeAbleToTellItIsNotEqualToAnotherDateFieldWithSameNameAndDifferentData()
        {
            DateField firstDateField = new DateField("Fecha");
            DateField secondDateField = new DateField("Fecha");

            IField firstNewDateField = firstDateField.FillAndClone("2008-09-22T14:01:54Z");
            IField secondNewDateField = secondDateField.FillAndClone("2015-10-26T14:01:54Z");

            Assert.IsFalse(firstNewDateField.Equals(secondNewDateField));
        }

        [TestMethod]
        public void BeAbleToTellItIsNotEqualToANullObject()
        {
            DateField firstDateField = new DateField("Fecha");

            IField firstNewDateField = firstDateField.FillAndClone("2008-09-22T14:01:54Z");

            Assert.IsFalse(firstNewDateField.Equals(null));
        }

        [TestMethod]
        public void BeAbleToTellItIsNotEqualToAnotherTypeOfField()
        {
            DateField aDateField = new DateField("Fecha");

            IField aNewDateField = aDateField.FillAndClone("2008-09-22T14:01:54Z");

            NumberField aNumberField = new NumberField("Monto");

            Assert.IsFalse(aNewDateField.Equals(aNumberField));
        }
    }
}
