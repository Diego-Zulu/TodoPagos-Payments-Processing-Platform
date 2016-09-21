using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class PayMethod
    {
        public virtual int PayedWith { get; set; }
        public virtual int Change { get; set; }

        public abstract int PayAndReturnChange(int amountPayed, int total);
    }
}
