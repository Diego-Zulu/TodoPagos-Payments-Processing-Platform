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

        IEnumerable<Provider> GetAllProvidersAcoordingToState(bool isActive);

        Provider GetSingleProvider(int providerId);

        bool UpdateProvider(int providerId, Provider targetProvider);

        int CreateProvider(Provider targetProvider);

        bool MarkProviderAsDeleted(int providerId);

        void Dispose();
    }
}
