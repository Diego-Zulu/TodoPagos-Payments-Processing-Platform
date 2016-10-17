using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain;

namespace TodoPagos.Domain.Tests
{
    [TestClass]
    public class DebitPayMethodShould
    {
        [TestMethod]
        public void BeAbleToPayAndReturn0Change()
        {
            int paymentTotal = 1000;
            int payedWith = 1000;
            DateTime todaysDate = DateTime.Now;
            PayMethod payMethod = new DebitPayMethod(todaysDate);

            double change = payMethod.PayAndReturnChange(paymentTotal, payedWith);

            Assert.AreEqual(0, change);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RefusePaymentWhenTotalIsNegative()
        {
            int paymentTotal = -1;
            int payedWith = 0;
            DateTime oneDate = DateTime.Parse("12/12/2009");
            PayMethod payMethod = new DebitPayMethod(oneDate);

            payMethod.PayAndReturnChange(paymentTotal, payedWith);
        }

        [TestMethod]
        public void KnowWhenItWasUsedToPaid()
        {
            int paymentTotal = 1100;
            DateTime oneDate = DateTime.Parse("10/10/2010");
            PayMethod payMethod = new DebitPayMethod(oneDate);

            Assert.AreEqual(oneDate, payMethod.PayDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveAFutureDate()
        {
            DateTime futureDate = DateTime.Now.AddYears(1);
            PayMethod payMethod = new DebitPayMethod(futureDate);
        }
    }

}
