using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HearkenContainer.Notations
{
    [AttributeUsage(AttributeTargets.Event, AllowMultiple=false)]
    public class TriggerAttribute : Attribute
    {
    }
}
