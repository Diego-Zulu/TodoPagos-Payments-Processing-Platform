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
            CheckForNullUnitOfWork(oneUnitOfWork);
            unitOfWork = oneUnitOfWork;
        }

        private void CheckForNullUnitOfWork(IUnitOfWork oneUnitOfWork)
        {
            if (oneUnitOfWork == null) throw new ArgumentException();
        }

        public int CreatePayment(Payment newPayment)
        {
            CheckForIncompletePayment(newPayment);
            unitOfWork.PaymentRepository.Insert(newPayment);
            unitOfWork.Save();
            return newPayment.ID;
        }

        private void CheckForIncompletePayment(Payment newPayment)
        {
            if (!newPayment.IsComplete()) throw new ArgumentException();
        }

        public IEnumerable<Payment> GetAllPayments()
        {
            return unitOfWork.PaymentRepository.Get(null, null, "");
        }

        public Payment GetSinglePayment(int paymentId)
        {
            Payment payment = unitOfWork.PaymentRepository.GetByID(paymentId);
            CheckIfPaymentExisted(payment);
            return payment;
        }

        private void CheckIfPaymentExisted(Payment payment)
        {
            if (payment == null) throw new ArgumentException();
        }
    }
}
