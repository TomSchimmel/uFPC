using System.Text.RegularExpressions;
using System.Web.Mvc;
using uFPC.Attributes;
using uFPC.Cache;
using Umbraco.Core.Composing;
using Umbraco.Web;
using Umbraco.Web.Editors;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace uFPC.Controllers
{
    [PluginController("uFPC")]
    public class ResourcesController : UmbracoAuthorizedJsonController
    {
        // E.G. /umbraco/ufpc/resources/update
        [System.Web.Http.HttpGet]
        public void Update()
        {
            uFPCCache.Update();
        }
    }
}