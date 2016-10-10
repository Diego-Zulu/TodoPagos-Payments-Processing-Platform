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

        public int CreateProvider(Provider newProvider)
        {
            MakeSureTargetProviderIsReadyToBeCreated(newProvider);
            unitOfWork.ProviderRepository.Insert(newProvider);
            unitOfWork.Save();
            return newProvider.ID;
        }

        private void MakeSureTargetProviderIsReadyToBeCreated(Provider targetProvider)
        {
            MakeSureTargetProviderIsNotNull(targetProvider);
            MakeSureTargetProviderIsComplete(targetProvider);
        }

        private void MakeSureTargetProviderIsNotNull(Provider targetProvider)
        {
            if (targetProvider == null)
            {
                throw new ArgumentNullException();
            }
        }

        private void MakeSureTargetProviderIsComplete(Provider targetProvider)
        {
            if (!targetProvider.IsComplete())
            {
                throw new ArgumentException();
            }
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

        public bool UpdateProvider(int providerId, Provider oneProvider)
        {
            if (oneProvider != null && providerId == oneProvider.ID && ExistsProvider(providerId))
            {
                Provider providerToBeUpdated = unitOfWork.ProviderRepository.GetByID(providerId);
                providerToBeUpdated.UpdateInfoWithTargetProvidersInfo(oneProvider);
                unitOfWork.ProviderRepository.Update(providerToBeUpdated);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        private bool ExistsProvider(int providerId)
        {
            Provider provider = unitOfWork.ProviderRepository.GetByID(providerId);
            return provider != null;
        }
    }
}
