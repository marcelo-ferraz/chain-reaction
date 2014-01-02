using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HearkenContainer.Notations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class ActionHolderAttribute : EventRelatedAttribute
    {
        public Type Interceptor { get; set; }
        public Type[] ListensTo { get; set; }
    }
}
