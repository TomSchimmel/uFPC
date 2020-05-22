using uFPC.Controllers;
using Umbraco.Core.Composing;
using Umbraco.Web;

namespace uFPC.Composer
{
    public class uPFCCoreComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.SetDefaultRenderMvcController<uPFCCoreController>();
        }
    }
}