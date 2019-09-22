using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HRM.Repository.Base.Interfaces
{
    public interface IRepository<T> where T : class
    {
        // Marks an entity as new
        T Add(T entity);

        // Marks an entity as modified
        T Update(T entity);

        // Marks an entity to be removed
        T Delete(T entity);

        T Delete(long id);

        bool Exists(long id);

        //Delete multi records
        void DeleteMulti(Expression<Func<T, bool>> where);

        // Get an entity by int id
        T FindById(long id, params Expression<Func<T, object>>[] includeProperties);

        T FindByCondition(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> GetAllByCondition(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        //IQueryable<T> GetMultiPaging(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null);

    }
}