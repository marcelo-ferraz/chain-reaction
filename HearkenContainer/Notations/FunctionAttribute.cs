using System;

namespace HearkenContainer.Notations
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=true)]
    public class FunctionAttribute: Attribute
    {
        public FunctionAttribute()
        { }

        public FunctionAttribute(string eventName)
        {
            EventName = eventName;
        }

        public string EventName { get; set; }
    }
}
