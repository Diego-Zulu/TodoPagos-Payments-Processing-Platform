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

        private DebitPayMethod() { }

        public DebitPayMethod(DateTime date)
        {
            CheckIfDateIsNotInTheFuture(date);
            this.Change = 0;
            this.PaidWith = 0;
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
            CheckIfTotalIsPositive(total);
            this.PaidWith = total;

            return NO_CHANGE;
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