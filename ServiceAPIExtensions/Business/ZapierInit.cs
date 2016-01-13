using System;
using System.Linq;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Core;
using System.Net;
using ServiceAPIExtensions.Business;
using ServiceAPIExtensions.Controllers;
using ServiceAPIExtensions.Business.Configuration;

namespace ContentAPI.Zapier
{
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class ZapierInit : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var ZapierConfig = new ZapierConfiguration();
            
            //Add initialization logic, this method is called once after CMS has been initialized
            var events = ServiceLocator.Current.GetInstance<IContentEvents>();

            if (ZapierConfig.Configuration.OnPublishedContent)
                events.PublishedContent += events_PublishedContent;
            if (ZapierConfig.Configuration.OnCreatedContent)
                events.CreatedContent += events_CreatedContent;
            if (ZapierConfig.Configuration.OnDeletedContent)
                events.DeletedContent += events_DeletedContent;
            if (ZapierConfig.Configuration.OnSavedContent)
                events.SavedContent += events_SavedContent;
            //User events?

        }

        void events_SavedContent(object sender, EPiServer.ContentEventArgs e)
        {
            RestHook.InvokeRestHooks("content_saved", e);
        }

        void events_DeletedContent(object sender, EPiServer.DeleteContentEventArgs e)
        {
            RestHook.InvokeRestHooks("content_deleted", e);
        }

        void events_CreatedContent(object sender, EPiServer.ContentEventArgs e)
        {
            RestHook.InvokeRestHooks("content_created", e);
        }

        void events_PublishedContent(object sender, EPiServer.ContentEventArgs e)
        {
            RestHook.InvokeRestHooks("content_published", e);
        }

        public void Preload(string[] parameters) { }

        public void Uninitialize(InitializationEngine context)
        {
            //Add uninitialization logic
        }

        
    }
}