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
            int moneyPayedWith = 2500;
            DateTime futureDate = DateTime.Now.AddYears(1);
            PayMethod payMethod = new CashPayMethod(moneyPayedWith, futureDate);
        }

        [TestMethod]
        public void BeAbleToPayAndReturnChange()
        {
            int paymentTotal = 1000;
            int moneyPayedWith = 2500;
            DateTime todaysDate = DateTime.Now;
            PayMethod payMethod = new CashPayMethod(moneyPayedWith, todaysDate);

            int change = payMethod.PayAndReturnChange(paymentTotal);

            Assert.AreEqual(moneyPayedWith - paymentTotal, change);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RefusePaymentWhenTotalIsAboveAmountPayed()
        {
            int paymentTotal = 3000;
            int moneyPayedWith = 2500;
            DateTime todaysDate = DateTime.Now;
            PayMethod payMethod = new CashPayMethod(moneyPayedWith, todaysDate);

            payMethod.PayAndReturnChange(paymentTotal);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RefusePaymentWhenTotalIsNegative()
        {
            int paymentTotal = -3000;
            int moneyPayedWith = 2500;
            DateTime todaysDate = DateTime.Now;
            PayMethod payMethod = new CashPayMethod(moneyPayedWith, todaysDate);

            payMethod.PayAndReturnChange(paymentTotal);
        }

        [TestMethod]
        public void KnowWhenItWasUsedToPaid()
        {
            int paymentTotal = 1000;
            int moneyPayedWith = 2500;
            DateTime oneDate = DateTime.Parse("10/10/2010");
            PayMethod payMethod = new CashPayMethod(moneyPayedWith, oneDate);

            Assert.AreEqual(oneDate, payMethod.PayDate);
        }
    }
}
