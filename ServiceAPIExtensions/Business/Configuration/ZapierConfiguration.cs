using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ServiceAPIExtensions.Business.Configuration
{
    public class ZapierConfiguration : ConfigurationSection
    {
        private ZapierConfiguration _config;
        public ZapierConfiguration Configuration
        {
            get
            {
                return _config ?? (_config = (ZapierConfiguration)ConfigurationManager.GetSection("Zapier"));
            }
        }

        public ZapierConfiguration()
        {
            
        }

        [ConfigurationProperty("OnPublishedContent", DefaultValue = "false", IsRequired = false)]
        public bool OnPublishedContent
        {
            get
            {
                return (Boolean)this["OnPublishedContent"];
            }
            set
            {
                this["OnPublishedContent"] = value;
            }
        }

        [ConfigurationProperty("OnCreatedContent", DefaultValue = "false", IsRequired = false)]
        public bool OnCreatedContent
        {
            get
            {
                return (Boolean)this["OnCreatedContent"];
            }
            set
            {
                this["OnCreatedContent"] = value;
            }
        }

        [ConfigurationProperty("OnDeletedContent", DefaultValue = "false", IsRequired = false)]
        public bool OnDeletedContent
        {
            get
            {
                return (Boolean)this["OnDeletedContent"];
            }
            set
            {
                this["OnDeletedContent"] = value;
            }
        }

        [ConfigurationProperty("OnSavedContent", DefaultValue = "false", IsRequired = false)]
        public bool OnSavedContent
        {
            get
            {
                return (Boolean)this["OnSavedContent"];
            }
            set
            {
                this["OnSavedContent"] = value;
            }
        }
    }
}
