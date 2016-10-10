using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoPagos.Domain;
using TodoPagos.Domain.Repository;

namespace TodoPagos.Web.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork unitOfWork;

        public PaymentService(IUnitOfWork oneUnitOfWork)
        {
            if (oneUnitOfWork == null) throw new ArgumentException();
            unitOfWork = oneUnitOfWork;
        }

        public int CreatePayment(Payment newPayment)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Payment> GetAllPayments()
        {
            throw new NotImplementedException();
        }

        public Payment GetSinglePayment(int id)
        {
            throw new NotImplementedException();
        }
    }
}
