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
            if (oneUnitOfWork == null) throw new ArgumentException();
            unitOfWork = oneUnitOfWork;
        }

        public int GetAllEarnings(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public IDictionary<Provider, int> GetEarningsPerProvider(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }
    }
}
