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
            MakeSureUserHasRequiredPrivilege(signedInUserEmail);
            MakeSureTargetClientIsReadyToBeCreated(newClient);
            unitOfWork.ClientRepository.Insert(newClient);
            unitOfWork.Save();
            return newClient.ID;
        }

        private void MakeSureTargetClientIsReadyToBeCreated(Client targetClient)
        {
            MakeSureTargetClientIsNotNull(targetClient);
            MakeSureTargetIsNotAlreadyInRepository(targetClient);
            MakeSureClientIsComplete(targetClient);
        }

        private void MakeSureTargetClientIsNotNull(Client targetClient)
        {
            if (targetClient == null)
            {
                throw new ArgumentNullException("Un nuevo cliente no puede ser nulo");
            }
        }

        private void MakeSureClientIsComplete(Client targetClient)
        {
            if (!targetClient.IsComplete())
            {
                throw new InvalidOperationException();
            }
        }

        private void MakeSureTargetIsNotAlreadyInRepository(Client targetClient)
        {
            if (ExistsClient(targetClient))
            {
                throw new InvalidOperationException();
            }
        }

        private bool ExistsClient(Client targetClient)
        {
            IEnumerable<Client> clientsThatExists = unitOfWork.ClientRepository.Get(
             cli => cli.IDCard.Equals(targetClient.IDCard)
             || cli.ID.Equals(targetClient.ID), null, "");
            return clientsThatExists.Count() > 0;
        }

        public bool DeleteClient(int id, string signedInUserEmail)
        {
            MakeSureUserHasRequiredPrivilege(signedInUserEmail);
            if (ClientIdHasClientInRepository(id))
            {
                unitOfWork.ClientRepository.Delete(id);
                unitOfWork.Save();
                return true;
            }
            return false;
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
            MakeSureUserHasRequiredPrivilege(signedInUserEmail);
            if (client != null && clientId == client.ID && ClientIdHasClientInRepository(clientId) 
                && !AnotherDifferentUserAlreadyHasThisIDCard(client))
            {
                Client clientEntity = unitOfWork.ClientRepository.GetByID(clientId);
                clientEntity.UpdateClientWithCompletedInfoFromTargetClient(client);
                unitOfWork.ClientRepository.Update(clientEntity);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        private bool AnotherDifferentUserAlreadyHasThisIDCard(Client clientToBeChecked)
        {
            IEnumerable<Client> clientsThatExists = unitOfWork.ClientRepository.Get(
                cli => cli.ID != clientToBeChecked.ID && cli.IDCard.Equals(clientToBeChecked.IDCard), null, "");
            return clientsThatExists.Count() > 0;
        }

        private void MakeSureUserHasRequiredPrivilege(string signedInUserEmail)
        {
            if (!unitOfWork.CurrentSignedInUserHasRequiredPrivilege(signedInUserEmail, ClientManagementPrivilege.GetInstance()))
            {
                throw new UnauthorizedAccessException();
            }
        }

        private bool ClientIdHasClientInRepository(int clientId)
        {
            Client client = unitOfWork.ClientRepository.GetByID(clientId);
            return client != null;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
