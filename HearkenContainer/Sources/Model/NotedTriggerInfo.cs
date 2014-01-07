using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using HearkenContainer.Model;
using HearkenContainer.Notations;

namespace HearkenContainer.Sources.Model
{
    /// <summary>
    /// Implemented to work with trigger classes decoreted wuth attributes
    /// </summary>
    public class NotedTriggerInfo: SourceInfo
    {
        /// <summary>
        /// Extracts the set of events from the type given
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<EventInfo> Extract()
        {
            if (!Type.IsDefined(typeof(SourceAttribute), true)) { return null; }

            IEnumerable<EventInfo> events =
                Type.GetEvents(Flags);

            if (events.Count(e => e.IsDefined(typeof(TriggerAttribute), true)) > 0)
            {
                events = Type
                    .GetEvents(Flags)
                    .Where(e =>
                        e.IsDefined(typeof(TriggerAttribute), true));
            }

            return events;
        }
    }
}
