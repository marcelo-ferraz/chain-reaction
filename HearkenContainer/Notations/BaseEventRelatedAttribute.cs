using System;
using System.Collections.Generic;
using System.Text;

namespace HearkenContainer.Notations
{
    public abstract class EventRelatedAttribute: Attribute
    {
        public string Group { get; set; }
        public string Name { get; set; }
    }
}
