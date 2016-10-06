using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoPagos.Domain;

namespace TodoPagos.Web.Services
{
    public interface IProviderService
    {
        IEnumerable<Provider> GetAllProviders();

        Provider GetSingleProvider(int providerId);
    }
}
