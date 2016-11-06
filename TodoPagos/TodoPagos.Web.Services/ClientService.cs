﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoPagos.Domain;
using TodoPagos.Domain.Repository;

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

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client> GetAllClients(string signedInUserEmail)
        {
            throw new NotImplementedException();
        }

        public Client GetSingleClient(int id, string signedInUserEmail)
        {
            throw new NotImplementedException();
        }

        public bool UpdateClient(int clientId, Client client, string signedInUserEmail)
        {
            throw new NotImplementedException();
        }
    }
}