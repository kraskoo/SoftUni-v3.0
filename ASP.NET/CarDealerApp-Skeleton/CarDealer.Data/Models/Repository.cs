namespace CarDealer.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Linq.Expressions;
    using Interfaces;
    using IModel = CarDealer.Models.IModel;

    public abstract class Repository<T> : IRepository<T>
         where T : class, IModel, new()
    {
        protected Repository(DbContext context)
        {
            this.Table = context.Set<T>();
        }
        
        protected DbSet<T> Table { get; }

        public void InsertOrUpdate(T entity)
        {
            this.Table.AddOrUpdate(entity);
        }

        public void InsertOrUpdate(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                this.Table.AddOrUpdate(entity);
            }
        }

        public void Delete(T entity)
        {
            this.Table.Remove(entity);
        }

        public T Find(int id)
        {
            return this.Table.Find(id);
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return this.Table.Find(where);
        }

        public IQueryable<T> GetAll()
        {
            return this.Table;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> where)
        {
            return this.Table.Where(where);
        }
    }
}