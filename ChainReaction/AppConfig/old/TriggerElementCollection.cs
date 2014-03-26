using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace HearkenContainer.AppConfig
{
    [ConfigurationCollection(typeof(TriggerElement))]
    internal class TriggerElementCollection: ConfigurationElement    
    {
        public const string NodeName = "triggers";

        protected override ConfigurationElement CreateNewElement()
        {
            return new TriggerElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TriggerElement)element).Name;
        }
    }
}



