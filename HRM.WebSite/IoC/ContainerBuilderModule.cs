using System.Runtime.Caching.Hosting;
using Autofac;
using HRM.Repository;
using HRM.Repository.Base;
using HRM.Services;
using HRM.Services.Base;
using HRM.Services.Cache;
using HRM.WebSite.Services.Cache;

namespace HRM.WebSite.IoC
{
    public class ContainerBuilderModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(HrmDataContext)).As(typeof(IContext)).InstancePerLifetimeScope();
            
            builder.RegisterAssemblyTypes(typeof(BaseRepository<>).Assembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(BaseService<>).Assembly)
               .Where(type => type.Name.EndsWith("Service"))
               .AsImplementedInterfaces();

            builder.RegisterType<AuthenticateService>().As<IAuthenticateService>();

            builder.RegisterType<MemoryCacheManager>().As<IStaticCacheManager>().SingleInstance();
            builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>().SingleInstance();

            base.Load(builder);
        }
    }
}