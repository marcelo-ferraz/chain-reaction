using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using HearkenContainer.Model;
using HearkenContainer.Mixins.Model.Collections;

namespace HearkenContainer.Sources.Model
{
    public class AppConfigSourceInfo : SourceInfo
    {
        private AppConfig.CollectionOf.Triggers _triggers;
                

        public AppConfigSourceInfo(Type type, AppConfig.CollectionOf.Triggers triggers)
        {
            this.Type = type;
            this._triggers = triggers;
        }

        public AppConfigSourceInfo(SourceInfo src, AppConfig.CollectionOf.Triggers triggers)
        {
            this.Type = src.Type;

            this._triggers = triggers;
            foreach (var @event in src.Events)
            {
                var foundHere = this.Events.Foremost(
                    (i, item) => 
                        item.Name == @event.Name);

                if (foundHere.Value == null)
                { this.Events[foundHere.Key] = @event; }
            }
        }

        protected override IEnumerable<EventInfo> Extract()
        {
            var typeEvents = 
                Type.GetEvents(Flags);
            
            if (_triggers == null || _triggers.Count < 1)
            { return typeEvents; }

            var events = 
                ArrayMixins.Create<EventInfo>(0); 

            foreach (var trigger in _triggers)
            {   
                var typeEvent = typeEvents.Foremost(
                    (i, ev) =>                        
                        ev.Name == trigger.Name);

                if (typeEvent.Value != null)
                { events.Insert(typeEvent.Key, typeEvent.Value); }
            }
            return events;
        }
    }
}
