using HRM.Repository.Base;
using HRM.Repository.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using HRM.Repository;

namespace HRM.Services.Base
{
    public abstract class BaseService<T> where T : class
    {
        private readonly IContext _context;
        private readonly IRepository<T> _repository;

        protected BaseService(IContext context, IRepository<T> repository)
        {
            _context = context;
            _repository = repository;
        }

        public virtual T Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var result = _repository.Add(entity);
            _context.SaveChanges();
            return result;
        }

        public virtual T Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            return _repository.Update(entity);
        }

        public virtual T Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            return _repository.Delete(entity);
        }

        public virtual T Delete(long id)
        {
            return _repository.Delete(id);
        }

        public virtual T Find(long id)
        {
            return _repository.FindById(id);
        }

        public virtual bool Exists(long id)
        {
            return _repository.Exists(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}