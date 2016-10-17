using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoPagos.Domain
{
    public class DebitPayMethod : PayMethod
    {

        const int NO_CHANGE = 0;

        protected DebitPayMethod() { }

        public DebitPayMethod(DateTime date)
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
            CheckIfTotalIsPositive(total);
            CheckThatTotalAndPayedWithAreSame(total, payedWith);
            return NO_CHANGE;
        }

        private void CheckThatTotalAndPayedWithAreSame(double total, double payedWith)
        {
            if (!total.Equals(payedWith))
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