using System.Linq;
using System.Web.Mvc;

namespace uFPC.Core
{
    public class CustomViewEngine : RazorViewEngine
    {
        private static string[] ViewFormats = new[] {
            "~/Views/{1}/{0}.cshtml",
            "~/Views/{0}.cshtml",
            "~/Views/Partials/{1}/{0}.cshtml",
            "~/Views/Partials/{0}/{1}.cshtml",
            "~/Views/Partials/{0}.cshtml",
        };

        public CustomViewEngine()
        {
            base.ViewLocationFormats = base.ViewLocationFormats.Union(ViewFormats).ToArray();
            base.PartialViewLocationFormats = base.PartialViewLocationFormats.Union(ViewFormats).ToArray();
        }
    }
}