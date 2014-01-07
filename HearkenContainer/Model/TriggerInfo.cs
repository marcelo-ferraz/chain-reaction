using System;
using System.Collections.Generic;
using System.Reflection;

namespace HearkenContainer.Model
{
    public abstract class SourceInfo: IHasTypedInfo
    {
        private IEnumerable<EventInfo> _events;
        public BindingFlags Flags { get; set; }
        public virtual Type Type { get; set; }

        public virtual IEnumerable<EventInfo> Events
        {
            get { return (_events ?? (_events = Extract())); }            
        }

        public SourceInfo()
        {
            Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        }

        protected abstract IEnumerable<EventInfo> Extract();
    }
}
