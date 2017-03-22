namespace Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Models.Interfaces;

    public interface IRepository<T> where T : class, IModel
    {
        void InsertOrUpdate(T entity);

        void InsertOrUpdate(IEnumerable<T> entities);

        void Delete(T entity);

        T Find(int id);

        T Find(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetByPredicate(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAll();
    }
}