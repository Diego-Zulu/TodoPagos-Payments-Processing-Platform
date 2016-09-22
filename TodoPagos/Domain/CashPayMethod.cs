using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CashPayMethod : PayMethod
    {
        public CashPayMethod(int amountPayed, DateTime date)
        {
            this.Change = 0;
            this.PaidWith = amountPayed;
            this.payDate = date;
        }

        public override int PayAndReturnChange(int total)
        {
            CheckIfAmountPayedIsMoreThanOrEqualsTotal(PaidWith, total);
            Change = PaidWith - total;

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
