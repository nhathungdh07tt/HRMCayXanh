using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using HRM.Domain.Entity;
using HRM.Repository;
using HRM.Repository.Repositories;
using HRM.Services.Base;
using HRM.Services.Base.Interfaces;
using HRM.ViewModels.Authenticate;

namespace HRM.Services
{
    public interface IRoleService : IService<Role>
    {
        List<RoleViewModel> GetRoles();
    }

    public class RoleService : BaseService<Role>, IRoleService
    {
        #region Properties
       
        private readonly IContext _context;
        private readonly IRoleRepository _repository;

        #endregion Properties

        #region Constructors

        public RoleService(IContext context, IRoleRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public List<RoleViewModel> GetRoles()
        {
            return _repository.GetAll().ProjectTo<RoleViewModel>().ToList();
        }

        #endregion Constructors

    }
}
