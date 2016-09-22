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
        public int amountPayed { get; set; }

        public Payment(PayMethod aPayMethod, int theAmountPayed)
        {
            CheckIfPayMethodIsNotNull(aPayMethod);
            CheckIfAmountPayedIsPositive(theAmountPayed);
            PaymentMethod = aPayMethod;
            amountPayed = theAmountPayed;
        }

        private void CheckIfAmountPayedIsPositive(int theAmountPayed)
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
