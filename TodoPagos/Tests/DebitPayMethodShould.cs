using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace Tests
{
    [TestClass]
    public class DebitPayMethodShould
    {
        [TestMethod]
        public void BeAbleToPayAndReturn0Change()
        {
            int paymentTotal = 1000;
            DateTime todaysDate = DateTime.Now;
            PayMethod payMethod = new DebitPayMethod(todaysDate);

            int change = payMethod.PayAndReturnChange(paymentTotal);

            Assert.AreEqual(0, change);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RefusePaymentWhenTotalIsNegative()
        {
            int paymentTotal = -1;
            DateTime oneDate = DateTime.Parse("12/12/2009");
            PayMethod payMethod = new DebitPayMethod(oneDate);

            payMethod.PayAndReturnChange(paymentTotal);
        }

        [TestMethod]
        public void KnowWhenItWasUsedToPaid()
        {
            int paymentTotal = 1100;
            DateTime oneDate = DateTime.Parse("10/10/2010");
            PayMethod payMethod = new DebitPayMethod(oneDate);

            Assert.AreEqual(oneDate, payMethod.payDate);
        }
    }

}
