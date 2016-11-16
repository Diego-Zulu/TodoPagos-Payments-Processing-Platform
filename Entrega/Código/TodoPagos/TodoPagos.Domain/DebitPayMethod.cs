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
            this.Type = "DebitPayMethod";
        }

        private void CheckIfDateIsNotInTheFuture(DateTime date)
        {
            if (DateTime.Now < date)
            {
                throw new ArgumentException("No se puede pagar con una fecha futura");
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
                throw new InvalidOperationException("El monto pago debe ser igual al total");
            }
        }

        private void CheckIfTotalIsPositive(double total)
        {
            if (total < 0)
            {
                throw new InvalidOperationException("El total debe ser positivo");
            }
        }
    }
}