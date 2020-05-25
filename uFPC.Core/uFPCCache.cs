using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using uFPC.Controllers;
using uFPC.Core;
using uFPC.Helpers;
using uFPC.IO;
using uFPC.Stream;
using Umbraco.Core.Composing;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace uFPC.Cache
{
    public static class uFPCCache
    {
        private static RouteData routeData = new RouteData();
        private static HttpContextWrapper context = new HttpContextWrapper(HttpContext.Current);

        public static void Create(int? nodeId = null)
        {
            ControllerContext controllerContext = new ControllerContext(new RequestContext(context, routeData), new FakeController());
            var umbracoHelper = Umbraco.Web.Composing.Current.UmbracoHelper;
            IPublishedContent node = null;

            if (nodeId != null && nodeId > 0)
            {
                node = umbracoHelper.Content(nodeId);    
            }
            else
            {
                node = umbracoHelper.Content(umbracoHelper.AssignedContentItem.Id);
            }

            var lastEditedDate = node.UpdateDate;
            var fileService = Current.Services.FileService;
            var nodeTemplatealias = node.GetTemplateAlias();
            var nodeTemplate = fileService.GetTemplate(nodeTemplatealias);

            if (nodeId != null && nodeId > 0)
            {
                uFPCio.RemoveFile(nodeTemplate);
            }

            if (!uFPCio.PathExists(nodeTemplate, lastEditedDate))
            {
                string templateContent = nodeTemplate.Content;
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
        }
    }
}
