using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using uFPC.Cache;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Events;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Core.Services.Implement;

namespace uFPC
{
    public class uFPCComponent : IComponent
    {
        public static ILogger _logger;

        public uFPCComponent(ILogger logger)
        {
            _logger = logger;
        }

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
            foreach(var entity in e.PublishedEntities)
            {
                uFPCCache.Create(entity.Id);
            }
        }

        public void Terminate()
        {
            uFPCComponent._logger.Info(typeof(String), "UFPC.Cache terminated" + HttpContext.Current.AllErrors.First().Message);
        }
    }
}