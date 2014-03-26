using System;

namespace ChainReaction.Notations
{
    /// <summary>
    /// Exposes an event to be able to be bound to an action
    /// </summary>
    [AttributeUsage(AttributeTargets.Event, AllowMultiple=false)]
    public class TriggerAttribute : Attribute
    {
    }
}
