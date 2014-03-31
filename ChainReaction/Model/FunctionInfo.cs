using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ChainReaction.Model
{
    public class ActionInfo
    {
        public MethodInfo Method { get; set; }
        public string EventName { get; set; }
    }
}
