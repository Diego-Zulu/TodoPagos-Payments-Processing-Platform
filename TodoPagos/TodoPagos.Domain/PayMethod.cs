using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoPagos.Domain
{
    public abstract class PayMethod
    {
        public virtual DateTime PayDate {get; set;}

        public int ID { get; set; }

        public abstract double PayAndReturnChange(double total, double payedWith);
    }
}
