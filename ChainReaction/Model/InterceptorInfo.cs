using System;
using System.Collections.Generic;
using System.Text;

namespace ChainReaction.Model
{
    public class InterceptorInfo: IComparable
    {
        /// <summary>
        /// The name of the interceptor
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of the interceptor
        /// </summary>
        public Type Type { get; set; }

        [Obsolete("To be evaluated whether is viable or good")]
        public Type[] InterceptedTypes { get; set; }

        public int CompareTo(object obj)
        {
            return (obj == null) ? -1 :
                (obj is string) ? ((string)obj).CompareTo(Name) :
                (obj is InterceptorInfo) ? ((InterceptorInfo)obj).Name.CompareTo(Name) :            
                (obj is Type) ? ((Type)obj).Name.CompareTo(Type.Name) : -1; 
        }
    }
}
