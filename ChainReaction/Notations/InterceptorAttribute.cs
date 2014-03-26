using System;
using System.Collections.Generic;
using System.Text;

namespace ChainReaction.Notations
{
    [AttributeUsage(AttributeTargets.Class, Inherited=true)]
    public class InterceptorAttribute : EventRelatedAttribute
    {
        public Type[] ToBeIntercepted { get; set; }
    }
}
