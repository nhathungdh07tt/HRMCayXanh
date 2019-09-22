using System.Web;
using System.Web.Mvc;
using HRM.Common.Enum;

namespace HRM.WebSite.Attributes
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        private readonly UserRoles[] _roles;

        public AuthorizeUserAttribute(UserRoles[] roles)
        {
            _roles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            foreach (var role in _roles)
            {
                if (httpContext.User.IsInRole(role.ToString()))
                    return true;
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var urlHelper = new UrlHelper(filterContext.RequestContext);
            var currentUrl = filterContext.HttpContext.Request.RawUrl;
            filterContext.Result = new RedirectResult(urlHelper.Action("Login", "Account", new { returnUrl = currentUrl }));
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}