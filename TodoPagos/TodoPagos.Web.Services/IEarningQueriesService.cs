using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoPagos.Domain;

namespace TodoPagos.Web.Services
{
    public interface IEarningQueriesService
    {
        IDictionary<Provider, int> GetEarningsPerProvider(DateTime from, DateTime to);

        int GetAllEarnings(DateTime from, DateTime to);
    }
}
