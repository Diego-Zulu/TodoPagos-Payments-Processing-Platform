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

            IField newDateField = dateField.FillAndClone("15/02/2015");

            Assert.AreNotSame(dateField, newDateField);
        }
    }
}
