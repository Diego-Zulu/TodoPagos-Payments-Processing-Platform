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

        public CashPayMethod(DateTime date)
        {
            CheckIfDateIsNotInTheFuture(date);
            this.PayDate = date;
        }

        private void CheckIfDateIsNotInTheFuture(DateTime date)
        {
            if (DateTime.Now < date)
            {
                throw new ArgumentException();
            }
        }

        public override double PayAndReturnChange(double total, double payedWith)
        {
            CheckIfAmountPayedIsMoreThanOrEqualsTotal(payedWith, total);
            CheckIfTotalIsPositive(total);
            return payedWith - total;
        }

        private void CheckIfAmountPayedIsMoreThanOrEqualsTotal(double amountPayed, double total)
        {
            if (amountPayed < total)
            {
                throw new InvalidOperationException();
            }
        }

        private void CheckIfTotalIsPositive(double total)
        {
            if (total < 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
