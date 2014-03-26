using System;

namespace ChainReaction.Notations
{
    /// <summary>
    /// Marks a method to be bound to a given event. If no name is provided, it will try to bind to an event with the same name
    /// </summary>
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
