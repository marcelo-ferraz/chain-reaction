using System;
using System.Collections.Generic;
using System.Text;

namespace ChainReaction.Notations
{
    public abstract class EventRelatedAttribute: Attribute
    {
        public string Group { get; set; }
        public string Name { get; set; }
    }
}
