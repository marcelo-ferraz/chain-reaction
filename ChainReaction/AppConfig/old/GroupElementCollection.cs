using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace HearkenContainer.Configuration.AppConfig
{
    [ConfigurationCollection(typeof(ActionElement))]
    internal class GroupElementCollection : ConfigurationElementCollection
    {
        private T Get<T>(string propName)
        {
            return (T)base[propName];
        }

        public const string NodeName =  "groups"; 

        protected override ConfigurationElement CreateNewElement()
        {
            return new GroupElementCollection();
        }

        /// <summary>
        /// Group name
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((GroupElement)element).Name;
        }

        /// <summary>
        /// Returns each group
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public GroupElementCollection this[int index]
        {
            get { return (GroupElementCollection)BaseGet(index); }
        }
    }
}