using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DebitPayMethod : PayMethod
    {

        public DebitPayMethod(DateTime date)
        {
            this.Change = 0;
            this.PaidWith = 0;
            this.payDate = date;
        }

        public override int PayAndReturnChange(int total)
        {
            throw new NotImplementedException();
        }
    }
}