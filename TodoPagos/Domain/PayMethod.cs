using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class PayMethod
    {
        public int PaidWith { get; set; }
        public int Change { get; set; }
        public virtual DateTime payDate {get; set;}

        public abstract int PayAndReturnChange(int total);
    }
}
