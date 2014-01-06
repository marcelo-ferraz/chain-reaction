using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HearkenContainer.Notations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class ActionAttribute : EventRelatedAttribute
    {
        public Type Interceptor { get; set; }
        public Type[] ListensTo { get; set; }
    }
}
