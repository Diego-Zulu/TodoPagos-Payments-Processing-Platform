using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoPagos.Domain;
using TodoPagos.Domain.Repository;

namespace TodoPagos.Web.Services
{
    public class EarningQueriesService : IEarningQueriesService
    {
        private readonly IUnitOfWork unitOfWork;

        public EarningQueriesService(IUnitOfWork oneUnitOfWork)
        {
            CheckForNullUnitOfWork(oneUnitOfWork);
            unitOfWork = oneUnitOfWork;
        }

        private void CheckForNullUnitOfWork(IUnitOfWork oneUnitOfWork)
        {
            if (oneUnitOfWork == null) throw new ArgumentException();
        }

        public int GetAllEarnings(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public IDictionary<Provider, double> GetEarningsPerProvider(DateTime from, DateTime to)
        {
            IEnumerable<Payment> allPayments = unitOfWork.PaymentRepository.Get(null, null, "");
            IDictionary<Provider, double> dictionary = new Dictionary<Provider, double>();
            foreach(Payment payment in allPayments)
            {
                payment.AddThisPaymentsEarningsToDictionary(dictionary, from, to);
            }
            return dictionary;
        }
    }
}
