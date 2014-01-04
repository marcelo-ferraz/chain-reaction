﻿using System.Configuration;

namespace HearkenContainer.AppConfig
{
    /// <summary>
    /// 
    /// </summary>
    public class Element
    {
        /// <summary>
        /// Provides a base of an configuration element
        /// </summary>
        public class OfConfiguration : ConfigurationElement
        {
            protected T Get<T>(string propName)
            { return (T)base[propName]; }
        }

        /// <summary>
        /// Provides a base of an configuration element, whose type is its key
        /// </summary>
        public class Typed : Element.OfConfiguration
        {
            [ConfigurationProperty("type", IsRequired = true)]
            public string Type
            {
                get { return Get<string>("type"); }
                set { base["type"] = value; }
            }
        }


        /// <summary>
        /// Provides a base of an configuration element, whose name is its key
        /// </summary>
        public class Named : Element.OfConfiguration
        {
            [ConfigurationProperty("name", IsRequired = true)]
            public string Name
            {
                get { return Get<string>("name"); }
                set { base["name"] = value; }
            }
        }

        /// <summary>
        /// Function configuration element 
        /// </summary>
        public class Function : Element.Named 
        {
            [ConfigurationProperty("event", IsKey = false)]
            public string Event
            {
                get { return Get<string>("event"); }
                set { base["event"] = value; }
            }
        }

        /// <summary>
        /// Trigger (event) configuration element 
        /// </summary>
        public class Trigger : Element.Named { }

        /// <summary>
        /// Action (Listener class) configuration element 
        /// </summary>
        public class Action : Element.Typed
        {
            [ConfigurationProperty("eventSource", IsKey = false, IsRequired = false)]
            public string EventSource
            {
                get { return Get<string>("eventSource"); }
                set { base["eventSource"] = value; }
            }

            [ConfigurationProperty(CollectionOf.Functions.NodeName, IsKey = false)]
            public CollectionOf.Functions Functions
            {
                get { return ((CollectionOf.Functions)(base[CollectionOf.Functions.NodeName])); }
            }
        }

        /// <summary>
        /// Trigger source (event holding class) configuration element 
        /// </summary>
        public class Source : Element.Typed 
        {
            [ConfigurationProperty(CollectionOf.Triggers.NodeName, IsKey = false)]
            public CollectionOf.Triggers Triggers
            {
                get { return ((CollectionOf.Triggers)(base[CollectionOf.Triggers.NodeName])); }
            }
        }

        /// <summary>
        /// Group configuration element 
        /// </summary>
        public class Group : Element.Named
        {
            [ConfigurationProperty(CollectionOf.Actions.NodeName, IsKey = false)]
            public CollectionOf.Actions Actions
            {
                get { return ((CollectionOf.Actions)(base[CollectionOf.Actions.NodeName])); }
            }

            [ConfigurationProperty(CollectionOf.Sources.NodeName, IsKey = false)]
            public CollectionOf.Sources Sources
            {
                get { return ((CollectionOf.Sources)(base[CollectionOf.Sources.NodeName])); }
            }
        }
    }
}