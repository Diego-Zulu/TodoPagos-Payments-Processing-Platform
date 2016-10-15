using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoPagos.Domain;

namespace TodoPagos.Web.Services
{
    public interface IPaymentService
    {
        IEnumerable<Payment> GetAllPayments();

        Payment GetSinglePayment(int id);

        int CreatePayment(Payment newPayment);

        void Dispose();
    }
}
