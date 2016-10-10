using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoPagos.Domain;
using TodoPagos.Domain.Repository;

namespace TodoPagos.Web.Services
{
    public class ProviderService : IProviderService
    {

        private readonly IUnitOfWork unitOfWork;

        public ProviderService(IUnitOfWork oneUnitOfWork)
        {
            MakeSureTargetUnitOfWorkIsNotNull(oneUnitOfWork);
            unitOfWork = oneUnitOfWork;
        }

        private void MakeSureTargetUnitOfWorkIsNotNull(IUnitOfWork oneUnitOfWork)
        {
            if (oneUnitOfWork == null)
            {
                throw new ArgumentException();
            }
        }

        public int CreateProvider(Provider targetProvider)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProvider(int providerId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public IEnumerable<Provider> GetAllProviders()
        {
            return unitOfWork.ProviderRepository.Get(null, null, "");
        }

        public Provider GetSingleProvider(int providerId)
        {
            Provider foundProvider = unitOfWork.ProviderRepository.GetByID(providerId);
            ThrowArgumentExceptionIfProviderWasntFound(foundProvider);
            return foundProvider;
        }

        private void ThrowArgumentExceptionIfProviderWasntFound(Provider foundProvider)
        {
            if (foundProvider == null)
            {
                throw new ArgumentException();
            }
        }

        public bool UpdateProvider(int providerId, Provider targetProvider)
        {
            throw new NotImplementedException();
        }
    }
}
