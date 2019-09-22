using HRM.Domain.Base;
using HRM.Repository.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using NMemory.Linq;

namespace HRM.Repository.Base
{
    public abstract class BaseRepository<T> : IRepository<T> 
        where T : Entity<long>
    {
        #region Properties

        private readonly IContext _dataContext;

        private readonly IDbSet<T> _dbSet;

        #endregion Properties

        protected BaseRepository(IContext dataContext)
        {
            _dataContext = dataContext;
            _dbSet = _dataContext.Set<T>();
        }

        #region Implementation

        public virtual T Add(T entity)
        {
            return _dbSet.Add(entity);
        }

        public virtual T Update(T entity)
        {
            _dbSet.AddOrUpdate(entity);
            return entity;
        }

        public virtual T Delete(T entity)
        {
            return _dbSet.Remove(entity);
        }

        public virtual T Delete(long id)
        {
            var entity = _dbSet.Find(id);
            return _dbSet.Remove(entity);
        }

        public bool Exists(long id)
        {
            return _dbSet.Any(e => e.Id == id);
        }

        public virtual void DeleteMulti(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                _dbSet.Remove(obj);
        }

        public T FindById(long id, params Expression<Func<T, object>>[] includeProperties)
        {
            return GetAllByCondition(e => e.Id == id, includeProperties).SingleOrDefault();
        }

        public T FindByCondition(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return GetAllByCondition(predicate, includeProperties).SingleOrDefault();
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = _dataContext.Set<T>();

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }

            return items;
        }

        public IQueryable<T> GetAllByCondition(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return GetAll(includeProperties).Where(predicate);
        }

        #endregion Implementation
    }
}