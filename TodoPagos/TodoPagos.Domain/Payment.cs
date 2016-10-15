using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoPagos.Domain
{
    public class Payment
    {
        public virtual PayMethod PaymentMethod { get; set; }
        public double AmountPaid { get; set; }
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
            AmountPaid = theAmountPaid;
            Receipts = paymentReceipts;
        }

        private void CheckAttributeCorrectness(PayMethod aPayMethod, double theAmountPayed, ICollection<Receipt> paymentReceipts)
        {
            CheckIfPayMethodIsNotNull(aPayMethod);
            CheckIfAmountPaidIsPositive(theAmountPayed);
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
                CheckAttributeCorrectness(this.PaymentMethod, this.AmountPaid, this.Receipts);
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

        public bool IsValid()
        {
            foreach(Receipt receipt in Receipts)
            {
                if (!receipt.IsValid())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
