using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using HearkenContainer.AppConfig;

namespace HearkenContainer.Configuration.AppConfig
{
    [ConfigurationCollection(typeof(ActionElement))]
    internal class GroupElement : ConfigurationElement
    {
        private T Get<T>(string propName)
        {
            return (T)base[propName];
        }

        public static string NodeName
        {
            get { return "groupElements"; }
        }

        [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return Get<string>("name"); }

            set { base["name"] = value; }
        }

        [ConfigurationProperty("eventHolder", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Type
        {
            get { return Get<string>("eventHolder"); }

            set { base["eventHolder"] = value; }
        }

        [ConfigurationProperty(ActionElementCollection.NodeName, IsKey = false, IsRequired = true)]
        public ActionElementCollection Actions
        {
            get { return ((ActionElementCollection)(base[ActionElementCollection.NodeName])); }
        }

        [ConfigurationProperty(TriggerElementCollection.NodeName, DefaultValue = "", IsKey = false, IsRequired = true)]
        public TriggerElementCollection Triggers
        {
            get { return ((TriggerElementCollection)(base[TriggerElementCollection.NodeName])); }
        }
    }
}
