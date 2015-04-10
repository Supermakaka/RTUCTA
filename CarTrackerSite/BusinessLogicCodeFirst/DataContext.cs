﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure;

namespace BusinessLogic.Models
{
    using BusinessLogic.Infrastructure;

    public class DataContext : DbContext, IDataContext, IDisposable
    {
        public DataContext() : base("name=DataContext") { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<BaseUser> Users { get; set; }
        public DbSet<UserOrder> UserOrders { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Location> Locations { get; set; }


        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public IDbSet<TEntity> SetEntity<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public interface IDataContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        DbSet<Company> Companies { get; set; }
        DbSet<BaseUser> Users { get; set; }
        DbSet<UserOrder> UserOrders { get; set; }
        DbSet<Car> Cars { get; set; }
        DbSet<Location> Locations { get; set; }

        int SaveChanges();
    }
}