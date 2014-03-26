using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ChainReaction.Model;
using ChainReaction.Mixins.Model.Collections;

namespace ChainReaction.Origins.Model
{
    public class AppConfigSourceInfo : SourceInfo
    {
        private AppConfig.CollectionOf.Triggers _triggers;
        private SourceInfo _previousSource;                

        public AppConfigSourceInfo(Type type, AppConfig.CollectionOf.Triggers triggers)
        {
            this.Type = type;
            this._triggers = triggers;            
        }

        public AppConfigSourceInfo(SourceInfo src, AppConfig.CollectionOf.Triggers triggers)
        {
            this.Type = src.Type;
            this._previousSource = src;
            this._triggers = triggers;            
        }

        protected override IEnumerable<EventInfo> Extract()
        {
            var typeEvents = 
                Type.GetEvents(Flags);
            
            if (_triggers == null || _triggers.Count < 1)
            { return typeEvents; }

            var events = ArrayMixins.Create<EventInfo>()
                //to avoid repeated events
                .Union(typeEvents, (e1, e2) => e1.Name == e2.Name);

            if(_previousSource != null && 
                _previousSource.Events != null &&
                _previousSource.Events.Length > 0)
            {
                events = events.Union(
                    _previousSource.Events,
                    (e1, e2) =>
                        e1.Name == e2.Name);

                _previousSource = null;
            }            
            
            return events;
        }
    }
}
