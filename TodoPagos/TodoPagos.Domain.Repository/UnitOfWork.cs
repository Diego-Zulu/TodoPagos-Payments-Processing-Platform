﻿using System;
using System.Collections.Generic;
using TodoPagos.Domain.DataAccess;
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