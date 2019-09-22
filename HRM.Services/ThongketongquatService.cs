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
using HRM.ViewModels.Thongketongquat;

namespace HRM.Services
{
    public interface IThongketongquatService : IService<Employee>
    {
        List<ThongketongquatViewModel> GetThongketongquats();
        ThongketongquatViewModel GetInfo(long id);
        void Insert(ThongketongquatViewModel model);
        void Update(ThongketongquatViewModel model);
        void Delete(ThongketongquatViewModel model);
    }
}
   