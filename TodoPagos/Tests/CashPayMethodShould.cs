using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace Tests
{
    [TestClass]
    public class CashPayMethodShould
    {
        [TestMethod]
        public void BeAbleToPayAndReturnChange()
        {
            PayMethod payMethod = new CashPayMethod();
            int paymentTotal = 1000;
            int moneyPayedWith = 2500;

            int change = payMethod.PayAndReturnChange(moneyPayedWith, paymentTotal);

            Assert.AreEqual(moneyPayedWith - paymentTotal, change);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RefusePaymentWhenTotalIsAboveAmountPayed()
        {
            PayMethod payMethod = new CashPayMethod();
            int paymentTotal = 3000;
            int moneyPayedWith = 2500;

            payMethod.PayAndReturnChange(moneyPayedWith, paymentTotal);
        }

        [TestMethod]
        public void KnowIfPaymentWasCompleted()
        {
            PayMethod payMethod = new CashPayMethod();
            int paymentTotal = 1000;
            int moneyPayedWith = 2500;

            payMethod.PayAndReturnChange(moneyPayedWith, paymentTotal);

            Assert.IsTrue(payMethod.PaymentComplete);
        }
    }
}
