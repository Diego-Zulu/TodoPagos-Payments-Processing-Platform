using Domain;
using System;
using System.Collections.Generic;
using TodoPagos.Domain.DataAccess;
using TodoPagos.ProductImporterLogic;
using TodoPagos.UserAPI;

namespace TodoPagos.Domain.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private TodoPagosContext context;
        private GenericRepository<User> userRepository;
        private GenericRepository<Receipt> receiptRepository;
        private GenericRepository<Provider> providerRepository;
        private GenericRepository<Payment> paymentRepository;
        private GenericRepository<Role> roleRepository;
        private GenericRepository<Privilege> privilegeRepository;
        private GenericRepository<LogEntry> entriesRepository;
        private GenericRepository<PointsManager> pointsManagerRepository;
        private GenericRepository<Product> productsRepository;

        public UnitOfWork(TodoPagosContext todoPagosContext)
        {
            CheckForNullTodoPagosContext(todoPagosContext);
            context = todoPagosContext;
        }

        private void CheckForNullTodoPagosContext(TodoPagosContext context)
        {
            if (context == null) throw new ArgumentException();
        }

        public IRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }

        public IRepository<Receipt> ReceiptRepository
        {
            get
            {

                if (this.receiptRepository == null)
                {
                    this.receiptRepository = new GenericRepository<Receipt>(context);
                }
                return receiptRepository;
            }
        }

        public IRepository<Provider> ProviderRepository
        {
            get
            {
                if (this.providerRepository == null)
                {
                    this.providerRepository = new GenericRepository<Provider>(context);
                }
                return providerRepository;
            }
        }

        public IRepository<Payment> PaymentRepository
        {
            get
            {
                if (this.paymentRepository == null)
                {
                    this.paymentRepository = new GenericRepository<Payment>(context);
                }
                return paymentRepository;
            }
        }

        public IRepository<Role> RoleRepository
        {
            get
            {
                if (this.roleRepository == null)
                {
                    this.roleRepository = new GenericRepository<Role>(context);
                }
                return roleRepository;
            }
        }

        public IRepository<Privilege> PrivilegeRepository
        {
            get
            {
                if (this.privilegeRepository == null)
                {
                    this.privilegeRepository = new GenericRepository<Privilege>(context);
                }
                return privilegeRepository;
            }
        }

        public IRepository<LogEntry> EntriesRepository
        {
            get
            {
                if (this.entriesRepository == null)
                {
                    this.entriesRepository = new GenericRepository<LogEntry>(context);
                }
                return entriesRepository;
            }
        }

        public IRepository<PointsManager> PointsManagerRepository
        {
            get
            {
                if (this.pointsManagerRepository == null)
                {
                    this.pointsManagerRepository = new GenericRepository<PointsManager>(context);
                }
                return pointsManagerRepository;
            }
        }

        public IRepository<Product> ProductsRepository
        {
            get
            {
                if (this.productsRepository == null)
                {
                    this.productsRepository = new GenericRepository<Product>(context);
                }
                return productsRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public bool CurrentSignedInUserHasRequiredPrivilege(string userEmail, Privilege somePrivilege)
        {
            IEnumerable<User> allUsers = UserRepository.Get(null, null, "");
            foreach(User user in allUsers)
            {
                if (user.Email.Equals(userEmail))
                {
                    return user.HasPrivilege(somePrivilege);
                }
            }
            return false;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}