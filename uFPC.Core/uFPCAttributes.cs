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

namespace uFPC.Attributes
{
    public class uFPCCoreAttribute : ActionFilterAttribute
    {
        private static RouteData routeData = new RouteData();
        private static HttpContextWrapper context = new HttpContextWrapper(HttpContext.Current);
        ControllerContext controllerContext = new ControllerContext(new RequestContext(context, routeData), new FakeController());

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var umbracoHelper = Umbraco.Web.Composing.Current.UmbracoHelper;
            var node = umbracoHelper.Content(umbracoHelper.AssignedContentItem.Id);
            var lastEditedDate = node.UpdateDate;
            var fileService = Current.Services.FileService;
            var nodeTemplatealias = node.GetTemplateAlias();
            var nodeTemplate = fileService.GetTemplate(nodeTemplatealias);
            string templateContent = nodeTemplate.Content;

            if (!uFPCio.PathExists(nodeTemplate, lastEditedDate))
            {
                ViewEngines.Engines.Add(new CustomViewEngine());

                if (!routeData.Values.ContainsValue("Fake"))
                {
                    routeData.Values.Add("Controller", "Fake");
                }

                templateContent = uFPCHelpers.ReplaceMasterLayoutPath(templateContent, nodeTemplate);

                uFPCio.WriteToCache(templateContent, nodeTemplate, lastEditedDate);

                templateContent = uFPCHelpers.GetRazorViewAsString(node, uFPCio.GetRelativePathFromCache(nodeTemplate, lastEditedDate), controllerContext);

                uFPCio.WriteToCache(templateContent, nodeTemplate, lastEditedDate);
            }

            var response = filterContext.HttpContext.Response;

            if (response.ContentType == "text/html")
            {
                response.Filter = new CustomStream(filterContext.HttpContext.Response.Filter, nodeTemplate, lastEditedDate);
            }
        }
    }
}