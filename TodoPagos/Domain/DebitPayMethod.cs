using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DebitPayMethod : PayMethod
    {

        const int NO_CHANGE = 0;

        public DebitPayMethod(DateTime date)
        {
            this.Change = 0;
            this.PaidWith = 0;
            this.payDate = date;
        }

        public override int PayAndReturnChange(int total)
        {
            this.PaidWith = total;

            return NO_CHANGE;
        }
    }
}