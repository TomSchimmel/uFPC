using System.Text.RegularExpressions;
using System.Web.Mvc;
using uFPC.Attributes;
using Umbraco.Core.Composing;
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
            return View(uFPC.IO.uFPCio.FindView(Current.Services.FileService.GetTemplate(model.Content.GetTemplateAlias())).Replace(System.AppDomain.CurrentDomain.BaseDirectory, "~\\"));
        }
    }

    public class FakeController : Controller { }
}