using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using uFPC.Controllers;
using Umbraco.Core.Composing;
using Umbraco.Web;
using uFPC.IO;
using uFPC.Core;
using uFPC.Stream;
using uFPC.Helpers;
using uFPC.Cache;

namespace uFPC.Attributes
{
    public class uFPCCoreAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            uFPCCache.Create();
        }
    }
 }