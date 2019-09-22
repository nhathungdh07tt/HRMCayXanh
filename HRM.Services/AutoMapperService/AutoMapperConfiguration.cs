using System.Linq;
using HRM.Domain.Entity;

namespace HRM.Services.AutoMapperService
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(cf =>
            {
                cf.AddProfile(new DomainToViewModelProfile());
                cf.AddProfile(new ViewModelToDomainProfile());
            });
        }
    }
}
