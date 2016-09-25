using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace Tests
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
        public void ReturnANewDateFieldWhenFilled()
        {
            DateField dateField = new DateField("Fecha");

            IField newDateField = dateField.FillAndClone("10/02/2015");

            Assert.AreNotSame(dateField, newDateField);
        }

        [TestMethod]
        public void AllowToBeFilled()
        {
            DateField dateField = new DateField("Fecha");

            IField newDateField = dateField.FillAndClone("18/12/2015");
            string expectedResult = "18/12/2015";

            Assert.AreEqual(expectedResult, newDateField.GetData());
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
        public void TellItIsValidWhenItStoresADateLaterThan2013()
        {
            DateField dateField = new DateField("Fecha");

            IField newDateField = dateField.FillAndClone("1/2/2014");

            Assert.IsTrue(newDateField.IsValid());
        }

        [TestMethod]
        public void TellItIsNotValidWhenItStoresADateBeforeThan2014()
        {
            DateField dateField = new DateField("Fecha");

            IField newDateField = dateField.FillAndClone("31/12/2013");

            Assert.IsFalse(newDateField.IsValid());
        }

        [TestMethod]
        public void BeAbleToTellItIsEqualToAnotherDateFieldWithSameNameAndData()
        {
            DateField firstDateField = new DateField("Fecha");
            DateField secondDateField = new DateField("Fecha");

            IField firstNewDateField = firstDateField.FillAndClone("31/12/2013");
            IField secondNewDateField = secondDateField.FillAndClone("31/12/2013");

            Assert.IsTrue(firstNewDateField.Equals(secondNewDateField));
        }

        [TestMethod]
        public void BeAbleToTellItIsNotEqualToAnotherDateFieldWithSameNameAndDifferentData()
        {
            DateField firstDateField = new DateField("Fecha");
            DateField secondDateField = new DateField("Fecha");

            IField firstNewDateField = firstDateField.FillAndClone("31/12/2013");
            IField secondNewDateField = secondDateField.FillAndClone("30/11/2013");

            Assert.IsFalse(firstNewDateField.Equals(secondNewDateField));
        }

        [TestMethod]
        public void BeAbleToTellItIsNotEqualToANullObject()
        {
            DateField firstDateField = new DateField("Fecha");

            IField firstNewDateField = firstDateField.FillAndClone("31/12/2013");

            Assert.IsFalse(firstNewDateField.Equals(null));
        }

        [TestMethod]
        public void BeAbleToTellItIsNotEqualToAnotherTypeOfField()
        {
            DateField aDateField = new DateField("Fecha");

            IField aNewDateField = aDateField.FillAndClone("31/12/2013");

            NumberField aNumberField = new NumberField("Monto");

            Assert.IsFalse(aNewDateField.Equals(aNumberField));
        }
    }
}
