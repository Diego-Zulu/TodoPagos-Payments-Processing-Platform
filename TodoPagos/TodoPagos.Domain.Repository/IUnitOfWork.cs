using Domain;
using System;
using TodoPagos.ProductImporterLogic;
using TodoPagos.UserAPI;

namespace TodoPagos.Domain.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> UserRepository { get; }

        IRepository<Provider> ProviderRepository { get; }

        IRepository<Payment> PaymentRepository { get; }

        IRepository<Receipt> ReceiptRepository { get; }

        IRepository<Role> RoleRepository { get; }

        IRepository<Privilege> PrivilegeRepository { get; }

        IRepository<LogEntry> EntriesRepository { get; }

        IRepository<PointsManager> PointsManagerRepository { get; }

        IRepository<Product> ProductsRepository { get; }

        void Save();

        bool CurrentSignedInUserHasRequiredPrivilege(string userEmail, Privilege somePrivilege);
    }
}
