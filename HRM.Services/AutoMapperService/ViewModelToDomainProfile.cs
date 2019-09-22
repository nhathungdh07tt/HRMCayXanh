using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Domain.Entity;
using HRM.ViewModels.Employee;
using HRM.ViewModels.System;

namespace HRM.Services.AutoMapperService
{
    public class ViewModelToDomainProfile : Profile
    {
        public ViewModelToDomainProfile()
        {
            CreateMap<UserViewModel, User>()
                .ForMember(m => m.Id, opt => opt.MapFrom(f => f.Id));

            CreateMap<RoleViewModel, Role>()
                .ForMember(m => m.Name, opt => opt.MapFrom(f => f.Name));

          
        }
    }
}
