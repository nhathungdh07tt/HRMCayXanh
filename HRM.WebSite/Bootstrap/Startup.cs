using System;
using System.Reflection;
using System.Web.Mvc;
using Microsoft.Owin;
using Owin;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using HRM.Services.AutoMapperService;
using HRM.WebSite.Binders;
using HRM.WebSite.Bootstrap;
using HRM.WebSite.IoC;
using StackExchange.Profiling.EntityFramework6;

[assembly: OwinStartup(typeof(HRM.WebSite.Bootstrap.Startup))]

namespace HRM.WebSite.Bootstrap
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AutoMapperConfiguration.Configure();
            IocConfig.ContainerBuilder.RegisterControllers(typeof(HRM.WebSite.Bootstrap.Startup).Assembly);
            IocConfig.ContainerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            IocConfig.ContainerBuilder.RegisterModule(new ContainerBuilderModule());
            var container = IocConfig.ContainerBuilder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            AuthorizeConfig.Configure(app);
            WebApiConfig.Register(app, container);
            AreaRegistration.RegisterAllAreas();
            MvcRouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(DateTime), new DateTimeBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new NullableDateTimeBinder());
            System.Diagnostics.Debug.WriteLine("Application Start");
        }
    }
}