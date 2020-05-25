using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Core.Services.Implement;

namespace uFPC
{
    public class uFPCComponent : IComponent
    {
        public void Initialize()
        {
            ContentService.Published += ContentService_Published;
            MediaService.Saved += MediaService_Published;
        }

        private void MediaService_Published(IMediaService sender, SaveEventArgs<IMedia> e)
        {
            throw new NotImplementedException();
        }

        private void ContentService_Published(IContentService sender, ContentPublishedEventArgs e)
        {
            //e.PublishedEntities.
        }

        public void Terminate()
        {
            throw new NotImplementedException();
        }
    }
}