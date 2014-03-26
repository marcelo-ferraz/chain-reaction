using System.Configuration;

namespace HearkenContainer.AppConfig
{
    public abstract class ConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = false)]
        public string Name
        {
            get { return Get<string>("name"); }

            set { base["name"] = value; }
        }

        protected T Get<T>(string propName)
        {
            return (T)base[propName];
        }
    }
}
