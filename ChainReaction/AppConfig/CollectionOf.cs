using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using ChainReaction.Configuration.AppConfig;

namespace ChainReaction.AppConfig
{
    /// <summary>
    /// 
    /// </summary>
    public class CollectionOf
    {
        /// <summary>
        /// It provides a base for collections that will be got from app.config
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public abstract class Elements<T> : ConfigurationElementCollection
            where T : ConfigurationElement, new()
        {
            protected override ConfigurationElement CreateNewElement()
            {
                return new T();
            }

            public new IEnumerator<T> GetEnumerator()
            {
                foreach (var item in ((IEnumerable)this))
                {
                    yield return (T)item;
                }
            }
        }

        /// <summary>
        /// It provides a base for collections that will be got from app.config, and their elements have their type as key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class TypedElements<T> : Elements<T>
            where T : Element.Typed, new()
        {
            protected override object GetElementKey(ConfigurationElement element)
            {
                return ((Element.Typed)element).Type;
            }
        }

        /// <summary>
        /// It provides a base for collections that will be got from app.config, and their elements have a name as key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class NamedElements<T> : Elements<T>
            where T : Element.Named, new()
        {
            protected override object GetElementKey(ConfigurationElement element)
            {
                return ((Element.Named)element).Name;
            }
        }

        /// <summary>
        /// A set of functions
        /// </summary>
        [ConfigurationCollection(typeof(Element.Function))]
        public class Functions : CollectionOf.NamedElements<Element.Function>
        {
            public const string NodeName = "functions";
        }

        /// <summary>
        /// A set of triggers
        /// </summary>
        [ConfigurationCollection(typeof(Element.Trigger))]
        public class Triggers : CollectionOf.NamedElements<Element.Trigger>
        {
            public const string NodeName = "triggers";
        }

        /// <summary>
        /// A set of Actions
        /// </summary>
        [ConfigurationCollection(typeof(Element.Action))]
        public class Actions : CollectionOf.TypedElements<Element.Action>
        {
            public const string NodeName = "actions";
        }

        /// <summary>
        /// A set of sources of triggers
        /// </summary>
        [ConfigurationCollection(typeof(Element.Source))]
        public class Sources : CollectionOf.TypedElements<Element.Source>
        {
            public const string NodeName = "sources";
        }

        /// <summary>
        /// A set of groups
        /// </summary>
        [ConfigurationCollection(typeof(Element.Group))]
        public class Groups : CollectionOf.NamedElements<Element.Group>
        {
            public const string NodeName = "groups";
        }
    }
}
