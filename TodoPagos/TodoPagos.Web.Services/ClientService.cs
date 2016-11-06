using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoPagos.Domain;
using TodoPagos.Domain.Repository;
using TodoPagos.UserAPI;

namespace TodoPagos.Web.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork unitOfWork;

        public ClientService(IUnitOfWork oneUnitOfWork)
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

        public int CreateClient(Client newClient, string signedInUserEmail)
        {
            throw new NotImplementedException();
        }

        public bool DeleteClient(int id, string signedInUserEmail)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client> GetAllClients(string signedInUserEmail)
        {
            MakeSureUserHasRequiredPrivilege(signedInUserEmail);
            return unitOfWork.ClientRepository.Get(null, null, "");
        }

        public Client GetSingleClient(int id, string signedInUserEmail)
        {
            MakeSureUserHasRequiredPrivilege(signedInUserEmail);
            Client foundClient = unitOfWork.ClientRepository.GetByID(id);
            ThrowArgumentExceptionIfUserWasntFound(foundClient);
            return foundClient;
        }

        private void ThrowArgumentExceptionIfUserWasntFound(Client foundClient)
        {
            if (foundClient == null)
            {
                throw new ArgumentException();
            }
        }

        public bool UpdateClient(int clientId, Client client, string signedInUserEmail)
        {
            throw new NotImplementedException();
        }

        private void MakeSureUserHasRequiredPrivilege(string signedInUserEmail)
        {
            if (!unitOfWork.CurrentSignedInUserHasRequiredPrivilege(signedInUserEmail, UserManagementPrivilege.GetInstance()))
            {
                throw new UnauthorizedAccessException();
            }
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
