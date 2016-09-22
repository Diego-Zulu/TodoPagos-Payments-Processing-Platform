using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace Tests
{
    [TestClass]
    public class PaymentShould
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveANullPaymentMethod()
        {
            PayMethod paymentMethod = null;
            Payment newPayment = new Payment(paymentMethod);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HavePositiveAmountPayed()
        {
            int amountPayed = -1000;
            PayMethod paymentMethod = new DebitPayMethod(DateTime.Now);
            Payment newPayment = new Payment(paymentMethod, amountPayed);
        }
    }
}
