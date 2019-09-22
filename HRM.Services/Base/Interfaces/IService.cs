using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HRM.Services.Base.Interfaces
{
    public interface IService<T> where T : class
    {
        // Marks an entity as new
        T Add(T entity);

        // Marks an entity as modified
        T Update(T entity);

        // Marks an entity to be removed
        T Delete(T entity);

        T Delete(long id);

        T Find(long id);

        bool Exists(long id);

        void Save();

    }
}