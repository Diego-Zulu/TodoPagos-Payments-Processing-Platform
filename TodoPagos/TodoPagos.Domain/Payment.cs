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
        public double amountPayed { get; set; }
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
            amountPayed = theAmountPayed;
            Receipts = paymentReceipts;
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
                CheckAttributeCorrectness(this.PaymentMethod, this.amountPayed, this.Receipts);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public IDictionary<Provider, double> AddThisPaymentsEarningsToDictionary
            (IDictionary<Provider, double> earningsPerProvider, DateTime from, DateTime to)
        {
            if(PaymentMethod.payDate >= from && PaymentMethod.payDate <= to)
            {
                earningsPerProvider = IterateOverAllReceipts(earningsPerProvider);
            }
            return earningsPerProvider;
        }

        private IDictionary<Provider, double> IterateOverAllReceipts
            (IDictionary<Provider, double> earningsPerProvider)
        {
            foreach (Receipt receipt in Receipts)
            {
                double actualValue;
                earningsPerProvider.TryGetValue(receipt.ReceiptProvider, out actualValue);
                earningsPerProvider.Add(receipt.ReceiptProvider, actualValue + receipt.CalculateEarnings());
            }
            return earningsPerProvider;
        }
    }
}
