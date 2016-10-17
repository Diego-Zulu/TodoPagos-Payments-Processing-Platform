﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoPagos.UserAPI;

namespace TodoPagos.Domain.DataAccess
{
    public class TodoPagosContext : DbContext
    {
        public TodoPagosContext() : base("name=TodoPagosContext") { }

        public virtual DbSet<IField> Fields { get; set; }

        public virtual DbSet<Receipt> Receipts { get; set; }

        public virtual DbSet<Payment> Payments { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Privilege> Privileges { get; set; }

        public virtual DbSet<Provider> Providers { get; set; }

        public virtual DbSet<PayMethod> PayMethods { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                 .HasMany<Role>(u => u.Roles).WithOptional(
                r => r.userThatHasThisRole).WillCascadeOnDelete(true);

            modelBuilder.Entity<Role>().HasMany<Privilege>(
                r => r.Privileges).WithOptional(p => p.InRole).WillCascadeOnDelete(true);
        }
    }
}
