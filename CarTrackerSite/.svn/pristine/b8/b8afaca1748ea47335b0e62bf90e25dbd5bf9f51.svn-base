using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class InMemoryDataContext : IDataContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<BaseUser> Users { get; set; }
        public DbSet<UserOrder> UserOrders { get; set; }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            var property = this.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(IDbSet<TEntity>));

            if (property != null)
            {
                property.SetValue(this, new InMemoryDbSet<TEntity>());
                return property.GetValue(this) as InMemoryDbSet<TEntity>;
            }
      

            //TODO: Maybe better is throw exception to know that property doesn't exist.
            return null;
        }

        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException(); 
        }

        public int SaveChanges()
        {
            return 0;
        }
    }

    public class InMemoryDbSet<T> : DbSet<T> where T : class
    {
        ObservableCollection<T> data;
        IQueryable query;

        public InMemoryDbSet()
        {
            data = new ObservableCollection<T>();
            query = data.AsQueryable();
        }
    }
}
