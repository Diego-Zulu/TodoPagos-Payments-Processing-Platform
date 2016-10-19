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
        public double PaidWith { get; set; }
        public double Change { get; set; }
        private double PaymentTotal { get; set; }
        public virtual ICollection<Receipt> Receipts { get; set; }
        public int ID { get; set; }

        public Payment()
        {
            Receipts = new List<Receipt>();
        }

        public Payment(PayMethod aPayMethod, double theAmountPaid, ICollection<Receipt> paymentReceipts)
        {
            CheckAttributeCorrectness(aPayMethod, theAmountPaid, paymentReceipts);
            PaymentMethod = aPayMethod;
            PaidWith = theAmountPaid;
            Receipts = paymentReceipts;
            PaymentTotal = CalculatePaymentTotal();
            Change = PaymentMethod.PayAndReturnChange(this.PaymentTotal, this.PaidWith);
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
            CheckIfAmountPaidIsPositive(theAmountPayed);
            CheckIfReceiptsAreNotNull(paymentReceipts);
            CheckIfPaymentHasAtLeastOneReceipt(paymentReceipts);
            CheckIfAllReceiptsAreComplete(paymentReceipts);
        }

        private void CheckIfPaymentHasAtLeastOneReceipt(ICollection<Receipt> paymentReceipts)
        {
            if (paymentReceipts.Count < 1)
            {
                throw new ArgumentException();
            }
        }

        private void CheckIfAllReceiptsAreComplete(ICollection<Receipt> paymentReceipts)
        {
            foreach (Receipt oneReceipt in paymentReceipts)
            {
                if (!oneReceipt.IsComplete())
                {
                    throw new ArgumentException();
                }
            }
        }

        private void CheckIfReceiptsAreNotNull(ICollection<Receipt> paymentReceipts)
        {
            if (paymentReceipts == null)
            {
                throw new ArgumentException();
            }
        }

        private void CheckIfAmountPaidIsPositive(double theAmountPaid)
        {
            if (theAmountPaid < 0)
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
                CheckAttributeCorrectness(this.PaymentMethod, this.PaidWith, this.Receipts);
                CheckThatPaymentTotalIsStillTheSumOfTheAmountsOfAllReceipts(this.PaymentTotal);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        private void CheckThatPaymentTotalIsStillTheSumOfTheAmountsOfAllReceipts(double payment)
        {
            if (payment != this.CalculatePaymentTotal())
            {
                throw new ArgumentException();
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

        public override bool Equals(object obj)
        {
            try
            {
                if (IsNull(obj)) return false;
                Payment otherPayment = (Payment)obj;
                return (TheTwoReceiptsListsAreEqual(this.Receipts, otherPayment.Receipts) || this.ID == otherPayment.ID);
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }

        private bool IsNull(object objectToCheck)
        {
            return objectToCheck == null;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        private bool TheTwoReceiptsListsAreEqual(ICollection<Receipt> firstList, ICollection<Receipt> secondList)
        {
            return firstList.All(x => secondList.Contains(x)) && secondList.All(x => firstList.Contains(x));
        }
    }
}
