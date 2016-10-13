using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoPagos.Domain
{
    public class CashPayMethod : PayMethod
    {
        private CashPayMethod() { }

        public CashPayMethod(int amountPayed, DateTime date)
        {
            CheckIfDateIsNotInTheFuture(date);
            this.Change = 0;
            this.PaidWith = amountPayed;
            this.PayDate = date;
        }

        private void CheckIfDateIsNotInTheFuture(DateTime date)
        {
            if (DateTime.Now < date)
            {
                throw new ArgumentException();
            }
        }

        public override int PayAndReturnChange(int total)
        {
            CheckIfAmountPayedIsMoreThanOrEqualsTotal(PaidWith, total);
            CheckIfTotalIsPositive(total);
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

        private void CheckIfTotalIsPositive(int total)
        {
            if (total < 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
