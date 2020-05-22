using System.Web.Mvc;
using uFPC.Attributes;
using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace uFPC.Controllers
{
    [uFPCCoreAttribute]
    public class uPFCCoreController : RenderMvcController
    {
        public override ActionResult Index(ContentModel model)
        {
            return View("~/App_Plugins/uFPC/cache/" + model.Content.GetTemplateAlias() + ".cshtml", model);
        }
    }

    public class FakeController : Controller { }
}