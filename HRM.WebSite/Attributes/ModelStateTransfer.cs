using System.Web.Mvc;

namespace HRM.WebSite.Attributes
{
    public class ModelStateTransfer : ActionFilterAttribute
    {
        protected static readonly string Key = typeof(ModelStateTransfer).FullName;
    }
}