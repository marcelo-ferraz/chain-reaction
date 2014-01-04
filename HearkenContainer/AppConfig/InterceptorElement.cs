using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace HearkenContainer.Configuration.AppConfig
{
    internal class InterceptorElement : ConfigurationElement
    {
        private T Get<T>(string propName)
        {
            return (T)base[propName];
        }

        [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = false)]
        public string Name
        {
            get { return Get<string>("name"); }

            set { base["name"] = value; }
        }

        [ConfigurationProperty("type", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Type
        {
            get { return Get<string>("type"); }

            set { base["type"] = value; }
        }

        [ConfigurationProperty("intercepted", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string[] Intercepted
        {
            get { return Get<string>("intercepted").Split(';', ',', '|'); }

            set
            {
                if (value == null || value.Length < 1) { 
                    base["intercepted"] = string.Empty; 
                    return; 
                }
                
                StringBuilder sb = new StringBuilder();
                foreach (var name in value)
                {
                    sb.AppendFormat("{0};", name);
                }
                string intercepted = sb.ToString();
                base["intercepted"] = 
                    intercepted.Substring(0, intercepted.Length -1);
            }
        }
    }
}