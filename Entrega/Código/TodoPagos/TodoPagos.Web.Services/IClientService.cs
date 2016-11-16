using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoPagos.Domain;

namespace TodoPagos.Web.Services
{
    public interface IClientService
    {
        IEnumerable<Client> GetAllClients(string signedInUserEmail);

        Client GetSingleClient(int id, string signedInUserEmail);

        int CreateClient(Client newClient, string signedInUserEmail);

        bool UpdateClient(int clientId, Client client, string signedInUserEmail);

        bool DeleteClient(int id, string signedInUserEmail);

        void Dispose();
    }
}
