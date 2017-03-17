namespace Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Interfaces;
    using Models.Interfaces;

    public class Repository<T> : IRepository<T> where T : class, IModel
    {
        private readonly DbContext dbContext;
        private readonly IDbSet<T> entityTable;

        public Repository(DbContext context)
        {
            this.dbContext = context;
            this.entityTable = this.dbContext.Set<T>();
        }

        public void InsertOrUpdate(T entity)
        {
            this.entityTable.AddOrUpdate(entity);
        }

        public void InsertOrUpdate(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                this.entityTable.AddOrUpdate(entity);
            }
        }

        public void Delete(T entity)
        {
            this.entityTable.Remove(entity);
        }

        public T Find(int id)
        {
            return this.entityTable.Find(id);
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return this.entityTable.FirstOrDefault(predicate);
        }

        public IQueryable<T> GetByPredicate(Expression<Func<T, bool>> predicate)
        {
            return this.entityTable.Where(predicate).AsQueryable();
        }

        public IQueryable<T> GetAll()
        {
            return this.entityTable.AsQueryable();
        }
    }
}