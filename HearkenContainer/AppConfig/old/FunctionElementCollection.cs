using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace HearkenContainer.AppConfig
{
    [ConfigurationCollection(typeof(FunctionElement))]
    public class FunctionElementCollection : ConfigurationElementCollection
    {
        public const string NodeName = "functions";

        protected override ConfigurationElement CreateNewElement()
        {
            return new FunctionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FunctionElement)element).Name;
        }
    }
}
