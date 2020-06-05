using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using uFPC.Controllers;
using Umbraco.Core.Models;

namespace uFPC.Helpers
{
    public class uFPCHelpers
    {
        public static string ReplaceMasterLayoutPath(string templateContent, ITemplate nodeTemplate)
        {
            var alias = String.Empty;
            var regexLayout = new Regex("Layout\\s*=\\s*.*\"", RegexOptions.IgnoreCase);
            var layoutMatch = regexLayout.Match(templateContent);

            if (layoutMatch.Value.Contains("/"))
            {
                var forwardSlashRegex = new Regex(@"[^/]+$", RegexOptions.IgnoreCase);
                alias = forwardSlashRegex.Match(layoutMatch.Value.TrimEnd('/')).Value;
            }
            else if (layoutMatch.Value.Contains("\\"))
            {
                var backwardSlashRegex = new Regex(@"([^\\]+$)", RegexOptions.IgnoreCase);
                alias = backwardSlashRegex.Match(Regex.Unescape(layoutMatch.Value.TrimEnd('\\'))).Value;
            }
            else
            {
                var equalsRegex = new Regex(@"([^=]+$)", RegexOptions.IgnoreCase);
                alias = equalsRegex.Match(layoutMatch.Value).Value;
            }

            if (!String.IsNullOrEmpty(alias))
            {
                var view = ViewEngines.Engines.FindView(GetControllerContext(), alias.Replace(".cshtml", "").Replace('"', ' ').Trim(), "");

                if (view != null && view.View != null)
                {
                    var masterPath = (view.View as RazorView).ViewPath;

                    if (layoutMatch.Success)
                    {
                        templateContent = regexLayout.Replace(templateContent, "Layout = " + '"' + masterPath + '"');
                    }
                }
            }

            return templateContent;
        }

        public static string GetRazorViewAsString(object model, string filePath, ControllerContext controller)
        {
            StringWriter stringWriter = new StringWriter();
            HttpContextWrapper context = new HttpContextWrapper(HttpContext.Current);
            ControllerContext controllerContext = GetControllerContext();
            RazorView razor = new RazorView(controllerContext, filePath, null, false, null);
            razor.Render(new ViewContext(controllerContext, razor, new ViewDataDictionary(model), new TempDataDictionary(), stringWriter), stringWriter);
            return stringWriter.ToString();
        }

        public static ControllerContext GetControllerContext()
        {
            RouteData routeData = new RouteData();
            routeData.Values.Add("Controller", "Fake");
            return new ControllerContext(new RequestContext(new HttpContextWrapper(HttpContext.Current), routeData), new FakeController());
        }
    }
}