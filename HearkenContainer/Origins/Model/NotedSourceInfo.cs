using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using HearkenContainer.Model;
using HearkenContainer.Notations;
using HearkenContainer.Mixins.Model.Collections;

namespace HearkenContainer.Origins.Model
{
    /// <summary>
    /// Implemented to work with trigger classes decoreted wuth attributes
    /// </summary>
    public class NotedSourceInfo: SourceInfo
    {
        public NotedSourceInfo() { }
        
        public NotedSourceInfo(SourceInfo src, Type type)
        {
            this._previousSource = src;
            this.Type = src.Type;            
        }

        public SourceInfo _previousSource { get; set; }
        
        /// <summary>
        /// Extracts the set of events from the type given
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<EventInfo> Extract()
        {
            if (!Type.IsDefined(typeof(SourceAttribute), true)) { return null; }
            
            var events = 
                Type.GetEvents(Flags);

            if(events.Count(e => e.IsDefined(typeof(TriggerAttribute), true)) > 0)
            {
                events = Type
                    .GetEvents(Flags)
                    .Where(e =>
                        e.IsDefined(typeof(TriggerAttribute), true)).ToArray();
            }
                
            if(_previousSource != null &&
                _previousSource.Events != null && 
                _previousSource.Events.Length > 0)
            {
                return events.Union(
                    _previousSource.Events, 
                    (one, other) => 
                        one.Name == other.Name);
            }

            return events;
        }
    }
}
