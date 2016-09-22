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
    }

}
