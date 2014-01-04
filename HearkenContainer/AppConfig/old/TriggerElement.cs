using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace HearkenContainer.AppConfig
{
    public class TriggerElement : ConfigElement
    {
        [ConfigurationProperty("type", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Type
        {
            get { return Get<string>("type"); }

            set { base["type"] = value; }
        }
    }
}