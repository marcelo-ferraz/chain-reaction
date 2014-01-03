using System;

namespace HearkenContainer.Notations
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=true)]
    public class ActionAttribute: Attribute
    {
        public ActionAttribute()
        { }

        public ActionAttribute(string eventName)
        {
            EventName = eventName;
        }

        public string EventName { get; set; }
    }
}
