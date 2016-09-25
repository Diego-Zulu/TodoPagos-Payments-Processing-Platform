using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class PaymentShould
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveANullPaymentMethod()
        {
            int amountPayed = 5000;
            PayMethod paymentMethod = null;
            Payment newPayment = new Payment(paymentMethod, amountPayed);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HavePositiveAmountPayed()
        {
            int amountPayed = -1000;
            PayMethod paymentMethod = new DebitPayMethod(DateTime.Now);
            Payment newPayment = new Payment(paymentMethod, amountPayed);
        }

        [TestMethod]
        public void NotHaveNullReceipts()
        {
            int amountPayed = 500;
            PayMethod paymentMethod = new DebitPayMethod(DateTime.Now);
            ICollection<Receipt> receipts = null;

            Payment newPayment = new Payment(paymentMethod, amountPayed, receipts);
        }
    }
}
