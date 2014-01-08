using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HearkenContainer.Model
{
    public abstract class SourceInfo: IHasTypedInfo
    {
        private EventInfo[] _events;
        public BindingFlags Flags { get; set; }
        public virtual Type Type { get; set; }

        public virtual EventInfo[] Events
        {
            get { return (_events ?? (_events = Extract().ToArray())); }            
        }

        public SourceInfo()
        {
            Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        }

        protected abstract IEnumerable<EventInfo> Extract();
    }
}
