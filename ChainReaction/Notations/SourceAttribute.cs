using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChainReaction.Notations
{
    /// <summary>
    /// Marks a class as a source of events. If no <see cref="ChainReaction.Notations.TriggerAttribute"/> is provided, in the events, it will expose all events.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SourceAttribute : EventRelatedAttribute
    {

    }
}
