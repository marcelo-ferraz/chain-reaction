using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HearkenContainer.Sources.Model
{
    public class EventNotFoundException : Exception
    {
        private static string GetMsg(string eventName, string actionName, Type actionHolderType)
        {
            return string.Format("Failed to find the event named as '{0}', marked in the method '{1}' from the type '{2}'!", eventName, actionName, actionHolderType.FullName);
        }

        public EventNotFoundException(string eventName, string actionName, Type actionHolderType)
            : base(GetMsg(eventName, actionName, actionHolderType))
        {
            EventName = eventName;
            ActionName = actionName;
            ActionHolderType = actionHolderType;
        }

        public Type ActionHolderType { get; set; }

        public string ActionName { get; set; }

        public string EventName { get; set; }
    }
}
