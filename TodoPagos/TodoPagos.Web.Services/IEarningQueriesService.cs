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
        IDictionary<Provider, double> GetEarningsPerProvider(DateTime from, DateTime to);

        double GetAllEarnings(DateTime from, DateTime to);
    }
}
