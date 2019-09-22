

using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Newtonsoft.Json.Serialization;
using Owin;

namespace HRM.WebSite.Bootstrap
{
    public static class WebApiConfig
    {
        public static void Register(IAppBuilder app, IContainer container)
        {
            HttpConfiguration config = new HttpConfiguration();
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
            //new DefaultContractResolver { IgnoreSerializableAttribute = true };

            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = RouteParameter.Optional }

            );

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            

            // Configure WebAPI / OWIN to suppress the Forms Authentication redirect when we send a 401 Unauthorized response
            // back from a web API. As we’re hosting out Web API inside an MVC project with Forms Auth enabled, without this,
            // the 401 Response would be captured by the Forms Auth processing and changed into a 302 redirect with a payload
            // for the Login Page. This code implements some OWIN middleware that explicitly prevents that from happening.
            //app.Use((context, next) =>
            //{
            //    HttpContext.Current.Response.SuppressFormsAuthenticationRedirect = true;
            //    return next.Invoke();
            //});

            // Configures container for WebAPI

            app.UseWebApi(config);

        }
    }
}