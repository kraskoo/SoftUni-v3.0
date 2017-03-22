namespace CarDealer.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using IModel = CarDealer.Models.IModel;

    public interface IRepository<T> where T : class, IModel, new()
    {
        void InsertOrUpdate(T entity);

        void InsertOrUpdate(IEnumerable<T> entity);

        void Delete(T entity);

        T Find(int id);

        T Find(Expression<Func<T, bool>> where);

        IQueryable<T> GetAll();

        IQueryable<T> GetAll(Expression<Func<T, bool>> where);
    }
}