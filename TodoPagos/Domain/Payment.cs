using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Payment
    {
        public virtual PayMethod PaymentMethod { get; set; }
        public double amountPayed { get; set; }
        public virtual ICollection<Receipt> Receipts { get; set; }

        public Payment(PayMethod aPayMethod, double theAmountPayed, ICollection<Receipt> paymentReceipts)
        {
            CheckAttributeCorrectness(aPayMethod, theAmountPayed, paymentReceipts);
            PaymentMethod = aPayMethod;
            amountPayed = theAmountPayed;
        }

        private void CheckAttributeCorrectness(PayMethod aPayMethod, double theAmountPayed, ICollection<Receipt> paymentReceipts)
        {
            CheckIfPayMethodIsNotNull(aPayMethod);
            CheckIfAmountPayedIsPositive(theAmountPayed);
            CheckIfReceiptsAreNotNull(paymentReceipts);
            CheckIfItHasMoreThanOneReceipt(paymentReceipts);
        }

        private void CheckIfItHasMoreThanOneReceipt(ICollection<Receipt> paymentReceipts)
        {
            if (paymentReceipts.Count < 1)
            {
                throw new ArgumentException();
            }
        }

        private void CheckIfReceiptsAreNotNull(ICollection<Receipt>  paymentReceipts)
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
    }
}
