using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Domain.Entity;
using HRM.Repository.Repositories;
using HRM.Services.Base.Interfaces;
using HRM.ViewModels.Authenticate;
using HRM.ViewModels.Employee;
using HRM.ViewModels.System;

namespace HRM.Services
{
    public interface IAuthenticateService
    {
        UserViewModel Login(LoginViewModel model);
     
    }

    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUserRepository _userRepository;
        
        public AuthenticateService(IUserRepository userRepository  )
        {
            _userRepository = userRepository;
           
        }

        public UserViewModel Login(LoginViewModel model)
        {
            var user = _userRepository.FindByCondition(x =>
                x.UserName.Equals(model.Username) && x.Password.Equals(model.Password));

            if (user != null)
                return Mapper.Map<User, UserViewModel>(user);

            return null;
        }

       
   
    }
}
