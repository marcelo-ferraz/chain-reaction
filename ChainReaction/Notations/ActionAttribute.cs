using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChainReaction.Notations
{
    /// <summary>
    /// Marks a class as a set of functions that will be executed given the right event or set of events are fired
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class HandlerAttribute : EventRelatedAttribute
    {
        /// <summary>
        /// The interceptor, to be raised when the class is instantiated
        /// </summary>
        public Type Interceptor { get; set; }

        /// <summary>
        /// Which classes this action listens to
        /// </summary>
        public Type[] ListensTo { get; set; }
    }
}
