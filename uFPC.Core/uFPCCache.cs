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
            uFPCComponent._logger.Info(typeof(String), "UFPC.Cache");

            IPublishedContent node = null;
            var umbracoHelper = Umbraco.Web.Composing.Current.UmbracoHelper;
            var fileService = Current.Services.FileService;

            if (nodeId != null && nodeId > 0)
            {
                node = umbracoHelper.Content(nodeId);    
            }
            else
            {
                node = umbracoHelper.Content(umbracoHelper.AssignedContentItem.Id);
            }

            var nodeLastEditedDate = node.UpdateDate;
            var nodeTemplatealias = node.GetTemplateAlias();
            var nodeTemplate = fileService.GetTemplate(nodeTemplatealias);

            if (nodeId != null && nodeId > 0)
            {
                uFPCio.RemoveFile(nodeTemplate);
            }

            if (!uFPCio.PathExists(nodeTemplate, nodeLastEditedDate))
            {
                string templateContent = nodeTemplate.Content;
                ViewEngines.Engines.Add(new CustomViewEngine());

                if (!routeData.Values.ContainsValue("Fake"))
                {
                    routeData.Values.Add("Controller", "Fake");
                }

                templateContent = uFPCHelpers.ReplaceMasterLayoutPath(templateContent, nodeTemplate);

                uFPCio.WriteToCache(templateContent, nodeTemplate, nodeLastEditedDate);

                ControllerContext controllerContext = new ControllerContext(new RequestContext(context, routeData), new FakeController());
                templateContent = uFPCHelpers.GetRazorViewAsString(node, uFPCio.GetRelativePathFromCache(nodeTemplate, nodeLastEditedDate), controllerContext);

                uFPCio.WriteToCache(templateContent, nodeTemplate, nodeLastEditedDate);
            }
        }

        public static void Update()
        {
            var contentService = Current.Services.ContentService;

            foreach (var parent in contentService.GetRootContent())
            {
                long totalRecords = 0;
                foreach (var node in contentService.GetPagedDescendants(parent.Id, 0, Int32.MaxValue, out totalRecords))
                {
                    if (node != null)
                    {
                        uFPCCache.Create(node.Id);
                    }
                }
            }
        }
    }
}
