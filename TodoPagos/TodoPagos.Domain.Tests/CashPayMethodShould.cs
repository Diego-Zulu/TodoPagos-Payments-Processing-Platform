using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain;

namespace TodoPagos.Domain.Tests
{
    [TestClass]
    public class CashPayMethodShould
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveAFutureDate()
        {
            DateTime futureDate = DateTime.Now.AddYears(1);
            PayMethod payMethod = new CashPayMethod(futureDate);
        }

        [TestMethod]
        public void BeAbleToPayAndReturnChange()
        {
            int paymentTotal = 1000;
            int moneyPayedWith = 2500;
            DateTime todaysDate = DateTime.Now;
            PayMethod payMethod = new CashPayMethod(todaysDate);

            double change = payMethod.PayAndReturnChange(paymentTotal, moneyPayedWith);

            Assert.AreEqual(moneyPayedWith - paymentTotal, change);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RefusePaymentWhenTotalIsAboveAmountPayed()
        {
            int paymentTotal = 3000;
            int moneyPayedWith = 2500;
            DateTime todaysDate = DateTime.Now;
            PayMethod payMethod = new CashPayMethod(todaysDate);

            payMethod.PayAndReturnChange(paymentTotal, moneyPayedWith);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RefusePaymentWhenTotalIsNegative()
        {
            int paymentTotal = -3000;
            int moneyPayedWith = 2500;
            DateTime todaysDate = DateTime.Now;
            PayMethod payMethod = new CashPayMethod(todaysDate);

            payMethod.PayAndReturnChange(paymentTotal, moneyPayedWith);
        }

        [TestMethod]
        public void KnowWhenItWasUsedToPaid()
        {
            int paymentTotal = 1000;
            int moneyPayedWith = 2500;
            DateTime oneDate = DateTime.Parse("Sun, 25 Oct 2015 10:30:41 GMT");
            PayMethod payMethod = new CashPayMethod(oneDate);

            Assert.AreEqual(oneDate, payMethod.PayDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWhenTheProvidedDateIsNotInGMTFormat()
        {
            int paymentTotal = 1000;
            int moneyPayedWith = 2500;
            DateTime oneDate = DateTime.Parse("05/05/2010");

            PayMethod payMethod = new CashPayMethod(oneDate);
        }
    }
}
