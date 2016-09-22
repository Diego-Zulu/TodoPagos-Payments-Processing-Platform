using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CashPayMethod : PayMethod
    {
        public CashPayMethod()
        {
            this.Change = 0;
            this.PayedWith = 0;
            this.PaymentComplete = false;
        }

        public override int PayAndReturnChange(int amountPayed, int total)
        {
            CheckIfAmountPayedIsMoreThanOrEqualsTotal(amountPayed, total);
            PayedWith = amountPayed;
            Change = amountPayed - total;
            PaymentComplete = true;

            return Change;
        }

        private void CheckIfAmountPayedIsMoreThanOrEqualsTotal(int amountPayed, int total)
        {
            if (amountPayed < total)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
