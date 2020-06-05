using System;
using uFPC.Cache;
using uFPC.Controllers;
using Umbraco.Core.Composing;
using Umbraco.Web;

namespace uFPC.Composer
{
    public class uPFCCoreComposer : ComponentComposer<uFPCComponent>, IUserComposer
    {
        public override void Compose(Composition composition)
        {
            composition.SetDefaultRenderMvcController<uPFCCoreController>();
            base.Compose(composition);
        }
    }

    public class LogWhenPublishedComposer : ComponentComposer<uFPCComponent>
    {
        // nothing needed to be done here!
    }
}