using System.Configuration;

namespace ChainReaction.Configuration.AppConfig
{
    [ConfigurationCollection(typeof(InterceptorElement))]
    internal class InterceptorElementCollection : ConfigurationElementCollection
    {
        public static string NodeName
        {
            get { return "interceptors"; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new InterceptorElement();
        }

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
