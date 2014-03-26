using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using HearkenContainer.AppConfig;

namespace HearkenContainer.Configuration.AppConfig
{
    [ConfigurationCollection(typeof(ActionElement))]
    public class ActionElementCollection : ConfigurationElementCollection
    {
        public const string NodeName = "actions"; 

        protected override ConfigurationElement CreateNewElement()
        {
            return new ActionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ActionElement)element).Name;
        }
    }



    
}
