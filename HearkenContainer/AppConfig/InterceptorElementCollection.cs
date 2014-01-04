using System.Configuration;

namespace HearkenContainer.Configuration.AppConfig
{
    [ConfigurationCollection(typeof(InterceptorElement))]
    internal class InterceptorElementCollection : ConfigurationElementCollection
    {
        //private T Get<T>(string propName)
        //{
        //    return (T)base[propName];
        //}

        public static string NodeName
        {
            get { return "interceptors"; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new InterceptorElement();
        }

        //[ConfigurationProperty("interceptorType", DefaultValue = "", IsKey = false, IsRequired = true)]
        //public string Type
        //{
        //    get { return Get<string>("interceptorType"); }

        //    set { base["interceptorType"] = value; }
        //}

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((InterceptorElement)element).Name;
        }

        public InterceptorElement this[int index]
        {
            get { return (InterceptorElement)BaseGet(index); }
        }
    }
}
