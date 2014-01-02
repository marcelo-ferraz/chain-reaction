using System;
using System.Collections.Generic;
using System.Reflection;
using HearkenContainer.Mixins.Model.Collections;
using HearkenContainer.Model;
using HearkenContainer.Notations;

namespace HearkenContainer.Sources.Model
{
    public class NotedActionInfo: ActionInfo
    {
        protected override IEnumerable<FunctionInfo> Extract()
        {
            var methods = 
                this.Type.GetMethods();                
            
            if (methods.Foremost(e => e.IsDefined(typeof(ActionAttribute), true)) != null)
            {
                return Extract(methods.Which(e => 
                    e.IsDefined(typeof(ActionAttribute), true)), 
                    true);
            }
            
            return Extract(methods, false);            
        }

        private static IEnumerable<FunctionInfo> Extract(IEnumerable<MethodInfo> methods, bool isFullyDecorated)
        {   
            foreach(var method in methods)
            {
                yield return new FunctionInfo { 
                    Method = method,
                    EventName = GetMethodName(isFullyDecorated, method)
                };
            }            
        }

        private static string GetMethodName(bool hasAttr, MethodInfo method)
        {
            var attr = (ActionAttribute)Attribute.GetCustomAttribute(method, typeof(ActionAttribute));

            return hasAttr ?
                (string.IsNullOrEmpty(attr.EventName) ? method.Name : attr.EventName) :
                method.Name;
        }

        public override object Invoke(object eventSource, IEnumerable<EventInfo> events)
        {
            var result = 
                Activator.CreateInstance(this.Type);

            Attach(eventSource, events, result);

            return result;
        }

        public override void Attach(object eventSource, IEnumerable<EventInfo> events, object action)
        {
            var actionType =
                action.GetType();
            
            foreach (var function in Functions)
            {
                if (function.Method.DeclaringType == typeof(object)) { continue; }

                var @event =
                    events.Foremost(e => e.Name.Equals(function.EventName));

                var @delegate = Delegate
                    .CreateDelegate(@event.EventHandlerType, action, function.Method.Name);

                @event.AddEventHandler(eventSource, @delegate);
            }
        }

        protected override Type[] GetWhoShouldBeListened()
        {
            var attr = (ActionHolderAttribute)
                Type.GetCustomAttributes(true)
                .Foremost(a => a is ActionHolderAttribute);

            return attr.ListensTo;
        }
    }
}