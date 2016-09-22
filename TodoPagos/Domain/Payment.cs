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

        public Payment(PayMethod aPayMethod)
        {
            CheckIfPayMethodIsNotNull(aPayMethod);
            PaymentMethod = aPayMethod;
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
