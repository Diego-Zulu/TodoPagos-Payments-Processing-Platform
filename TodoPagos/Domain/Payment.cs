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

        public Payment(PayMethod aPayMethod, double theAmountPayed, ICollection<Receipt> paymentReciepts)
        {
            CheckIfPayMethodIsNotNull(aPayMethod);
            CheckIfAmountPayedIsPositive(theAmountPayed);
            PaymentMethod = aPayMethod;
            amountPayed = theAmountPayed;
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
