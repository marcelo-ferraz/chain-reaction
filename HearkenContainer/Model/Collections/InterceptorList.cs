using System;
using System.Collections.Generic;
using System.Text;

namespace HearkenContainer.Model.Collections
{
    /// <summary>
    /// Represents a list of interceptors, to be used in a group, or various groups
    /// </summary>
    public class InterceptorList : List<InterceptorInfo>
    {
        public InterceptorInfo this[string name]
        {
            get
            {
                return this.Find(i => i.Name.Equals(name));
            }
        }

        internal void Add(string itemName, Type interceptorType)
        {
            this.Add(new InterceptorInfo() { Name = itemName, Type = interceptorType });
        }
    }
}
