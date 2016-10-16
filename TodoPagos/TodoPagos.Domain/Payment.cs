﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoPagos.Domain
{
    public class Payment
    {
        public virtual PayMethod PaymentMethod { get; set; }
        public double PayedWith { get; set; }
        public double Change { get; set; }
        private double PaymentTotal { get; set; }
        public virtual ICollection<Receipt> Receipts { get; set; }
        public virtual int ID { get; set; }

        public Payment()
        {
            Receipts = new List<Receipt>();
        }

        public Payment(PayMethod aPayMethod, double theAmountPayed, ICollection<Receipt> paymentReceipts)
        {
            CheckAttributeCorrectness(aPayMethod, theAmountPayed, paymentReceipts);
            PaymentMethod = aPayMethod;
            PayedWith = theAmountPayed;
            Receipts = paymentReceipts;
            PaymentTotal = CalculatePaymentTotal();
            Change = PaymentMethod.PayAndReturnChange(this.PaymentTotal, this.PayedWith);
        }

        private double CalculatePaymentTotal()
        {
            double total = 0;
            foreach (Receipt oneReceipt in this.Receipts)
            {
                total += oneReceipt.Amount;
            }
            return total;
        }

        private void CheckAttributeCorrectness(PayMethod aPayMethod, double theAmountPayed, ICollection<Receipt> paymentReceipts)
        {
            CheckIfPayMethodIsNotNull(aPayMethod);
            CheckIfAmountPayedIsPositive(theAmountPayed);
            CheckIfReceiptsAreNotNull(paymentReceipts);
            CheckIfItHasOneOrMoreReceipts(paymentReceipts);
        }

        private void CheckIfItHasOneOrMoreReceipts(ICollection<Receipt> paymentReceipts)
        {
            if (paymentReceipts.Count < 1)
            {
                throw new ArgumentException();
            }
        }

        private void CheckIfReceiptsAreNotNull(ICollection<Receipt> paymentReceipts)
        {
            if (paymentReceipts == null)
            {
                throw new ArgumentException();
            }
        }

        private void CheckIfAmountPayedIsPositive(double theAmountPayed)
        {
            if (theAmountPayed < 0)
            {
                throw new ArgumentException();
            }
        }

        private void CheckIfPayMethodIsNotNull(PayMethod aPayMethod)
        {
            if (aPayMethod == null)
            {
                throw new ArgumentException();
            }
        }

        public bool IsComplete()
        {
            try
            {
                CheckAttributeCorrectness(this.PaymentMethod, this.PayedWith, this.Receipts);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public void AddThisPaymentsEarningsToDictionary
            (IDictionary<Provider, double> earningsPerProvider, DateTime from, DateTime to)
        {
            if(PaymentMethod.PayDate >= from && PaymentMethod.PayDate <= to)
            {
                IterateOverAllReceiptsForDictionary(earningsPerProvider);
            }
        }

        private void IterateOverAllReceiptsForDictionary
            (IDictionary<Provider, double> earningsPerProvider)
        {
            foreach (Receipt receipt in Receipts)
            {
                double actualValue;
                earningsPerProvider.TryGetValue(receipt.ReceiptProvider, out actualValue);
                earningsPerProvider.Add(receipt.ReceiptProvider, actualValue + receipt.CalculateEarnings());
            }
        }

        public void AddThisPaymentsEarningsToOverallValue(ref double overallValue, DateTime from, DateTime to)
        {
            if (PaymentMethod.PayDate >= from && PaymentMethod.PayDate <= to)
            {
                IterateOverAllReceiptsForOverallValue(ref overallValue);
            }
        }

        private void IterateOverAllReceiptsForOverallValue(ref double overallValue)
        {
            foreach (Receipt receipt in Receipts)
            {
                overallValue += receipt.CalculateEarnings();
            }
        }
    }
}
