using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using HearkenContainer.AppConfig;

namespace HearkenContainer.Configuration.AppConfig
{
    internal class ActionElement : ConfigElement
    {
        [ConfigurationProperty("type", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Type
        {
            get { return Get<string>("type"); }

            set { base["type"] = value; }
        }

        [ConfigurationProperty(FunctionElementCollection.NodeName, DefaultValue = "", IsKey = false, IsRequired = true)]
        public FunctionElementCollection functions
        {
            get { return ((FunctionElementCollection)(base[FunctionElementCollection.NodeName])); }
        }
    }
}
