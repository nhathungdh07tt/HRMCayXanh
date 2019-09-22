using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HRM.Domain.Entity;
using HRM.Repository;
using HRM.Repository.Repositories;
using HRM.Services.Base;
using HRM.Services.Base.Interfaces;
using HRM.ViewModels.System;

namespace HRM.Services
{
    public interface IUserService : IService<User>
    {
        UserViewModel UserIncludeRole();
        UserViewModel GetUserById(long id);

        List<UserViewModel> GetUserTypes();       
        void Insert(UserViewModel model);
        void Update(UserViewModel model);
        void Delete(UserViewModel model);
    }

    public class UserService : BaseService<User>, IUserService
    {
        #region Properties

        private readonly IContext _context;
        private readonly IUserRepository _repository;


        #endregion Properties

        #region Constructors

        public UserService(IContext context, IUserRepository repository)
            : base(context, repository)
        {
            _repository = repository;
            _context = context;
        }

        public void Delete(UserViewModel model)
        {
            var item = _repository.FindById(model.Id);
            if (item != null)
                _repository.Delete(item);
        }

        public List<UserViewModel> GetUserTypes()
        {
            return _repository.GetAll().ProjectTo<UserViewModel>().ToList();
        }

     

        public void Insert(UserViewModel model)
        {
            var userType = AutoMapper.Mapper.Map<UserViewModel, User>(model);
            _repository.Add(userType);
        }

        public void Update(UserViewModel model)
        {
            var userType = AutoMapper.Mapper.Map<UserViewModel, User>(model);
            _repository.Update(userType);
        }

        public UserViewModel GetUserById(long id)
        {
            var user = _repository.FindById(id);

            if (user != null)
                return Mapper.Map<User, UserViewModel>(user);

            return null;
        }

        public UserViewModel UserIncludeRole()
        {
            
            return _repository
                .GetAll(x => x.Roles)
                .Where(x => x.Id == 1)
                .ProjectTo<UserViewModel>()
                .SingleOrDefault();
        }
        #endregion Constructors

    }

}
