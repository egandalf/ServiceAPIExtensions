using EPiServer;
using EPiServer.Data.Dynamic;
using ServiceAPIExtensions.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAPIExtensions.Business
{
    public class RestHook :IDynamicData
    {
        public EPiServer.Data.Identity Id
        {
            get;
            set;
        }

        public string Url { get; set; }

        public string EventName { get; set; }


        private static DynamicDataStore GetStore()
        {
            return DynamicDataStoreFactory.Instance.CreateStore(typeof(RestHook));
        }


        public static void InvokeRestHooks(string EventName, object Data)
        {
            HttpClient cli = new HttpClient();
            foreach (var r in GetStore().Items<RestHook>().Where(rh => rh.EventName == EventName))
            {
                //Post async json encoded object.
                var data=Newtonsoft.Json.JsonConvert.SerializeObject(Data);
                cli.PostAsJsonAsync(r.Url, Data);
            }
        }

        public static void InvokeRestHooks(string EventName, ContentEventArgs e)
        {
            if (e.Content != null)
            {
                object Data = ContentAPiController.ConstructExpandoObject(e.Content);
                InvokeRestHooks(EventName, Data);
            }
        }

        public static void InvokeRestHooks(string EventName, DeleteContentEventArgs e)
        {
            if (e.Content != null)
            {
                object Data = ContentAPiController.ConstructExpandoObject(e.Content);
                InvokeRestHooks(EventName, Data);
            }
        }

        public Guid SaveRestHook()
        {
            return GetStore().Save(this).ExternalId;
        }

        public static void DeleteRestHook(string ID)
        {
            GetStore().Delete(Guid.Parse(ID));
        }
    }
}
