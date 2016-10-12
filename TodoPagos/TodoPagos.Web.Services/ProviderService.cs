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
            MakeSureTargetProviderIsCompleteAndActive(targetProvider);
            MakeSureTargetProvidersNameIsNotAlreadyOnAnotherActiveProviderInRepository(targetProvider);
        }

        private void MakeSureTargetProviderIsNotNull(Provider targetProvider)
        {
            if (targetProvider == null)
            {
                throw new ArgumentNullException();
            }
        }

        private void MakeSureTargetProviderIsCompleteAndActive(Provider targetProvider)
        {
            if (!targetProvider.IsComplete() || !targetProvider.Active)
            {
                throw new ArgumentException();
            }
        }

        public void MakeSureTargetProvidersNameIsNotAlreadyOnAnotherActiveProviderInRepository(Provider targetProvider)
        {
            IEnumerable<Provider> providersWithSameName = unitOfWork.ProviderRepository.Get(
                us => us.Name.Equals(targetProvider.Name) && us.Active, null, "");
            if (providersWithSameName.Count() > 0)
            {
                throw new InvalidOperationException();
            }
        }

        public bool MarkProviderAsDeleted(int id)
        {
            if (ExistsProvider(id))
            {
                MarkProviderAsDeletedAndUpdateOnlyIfItIsNotAlreadyDeleted(id);
                return true;
            }
            return false;
        }

        private void MarkProviderAsDeletedAndUpdateOnlyIfItIsNotAlreadyDeleted(int targetId)
        {
            Provider provider = unitOfWork.ProviderRepository.GetByID(targetId);
            if (provider.Active)
            {
                MarkProviderAsDeletedAndUpdate(provider);
            }
        }

        private void MarkProviderAsDeletedAndUpdate(Provider toBeMarkedAsDeletedProvider)
        {
            toBeMarkedAsDeletedProvider.MarkAsInactiveToShowItIsDeleted();
            unitOfWork.ProviderRepository.Update(toBeMarkedAsDeletedProvider);
            unitOfWork.Save();
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
                //providerToBeUpdated.UpdateInfoWithTargetProvidersInfo(oneProvider);
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
