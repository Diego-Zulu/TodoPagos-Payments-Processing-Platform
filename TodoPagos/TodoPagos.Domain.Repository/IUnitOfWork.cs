using System;
using TodoPagos.UserAPI;

namespace TodoPagos.Domain.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> UserRepository { get; }

        IRepository<Provider> ProviderRepository { get; }

        IRepository<Payment> PaymentRepository { get; }

        IRepository<Receipt> ReceiptRepository { get; }

        void Save();
    }
}
