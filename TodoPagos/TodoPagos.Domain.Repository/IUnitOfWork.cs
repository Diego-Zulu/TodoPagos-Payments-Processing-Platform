using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoPagos.UserAPI;
using TodoPagos.Domain;

namespace TodoPagos.Domain.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> UserRepository { get; }

        IRepository<Provider> ProviderRepository { get; }

        void Save();
    }
}
