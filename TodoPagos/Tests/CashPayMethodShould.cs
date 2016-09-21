using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace Tests
{
    [TestClass]
    public class CashPayMethodShould
    {
        [TestMethod]
        public void BeAbleToPay()
        {
            PayMethod payMethod = new CashPayMethod();
            int paymentTotal = 1000;
            int moneyPayedWith = 2500;

            Assert.AreEqual(moneyPayedWith - paymentTotal, payMethod.Change);
        }
    }
}
