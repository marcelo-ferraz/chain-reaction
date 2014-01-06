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
            
            if (methods.Foremost(e => e.IsDefined(typeof(FunctionAttribute), true)) != null)
            {
                return Extract(methods.Which(e => 
                    e.IsDefined(typeof(FunctionAttribute), true)), 
                    true);
            }
            
            return Extract(methods, false);            
        }

        private static IEnumerable<FunctionInfo> Extract(IEnumerable<MethodInfo> methods, bool isFullyDecorated)
        {
            foreach (var method in methods)
            {
                var attrs =
                    method.GetCustomAttributes(true).Which(e => e is FunctionAttribute);

                if (!isFullyDecorated)
                {
                    yield return new FunctionInfo
                    {
                        Method = method,
                        EventName = GetMethodName(isFullyDecorated, method)
                    };
                }
                else
                {
                    foreach (var attr in attrs)
                    {
                        yield return new FunctionInfo
                        {
                            Method = method,
                            EventName = GetMethodName(isFullyDecorated, method, (FunctionAttribute)attr)
                        };
                    }
                }
            }
        }

        private static string GetMethodName(bool hasAttr, MethodInfo method, FunctionAttribute actionAttr = null)
        {            
            return hasAttr ?
                (string.IsNullOrEmpty(actionAttr.EventName) ? method.Name : actionAttr.EventName) :
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

                if (@event == null) { continue; }
                //{ throw new EventNotFoundException(function.EventName, function.Method.Name, action.GetType()); }

                var @delegate = Delegate
                    .CreateDelegate(@event.EventHandlerType, action, function.Method.Name);

                @event.AddEventHandler(eventSource, @delegate);
            }
        }

        protected override Type[] GetWhoShouldBeListened()
        {
            var attr = (ActionAttribute)
                Type.GetCustomAttributes(true)
                .Foremost(a => a is ActionAttribute);

            return attr.ListensTo;
        }
    }
}