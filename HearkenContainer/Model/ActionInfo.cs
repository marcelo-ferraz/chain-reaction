using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using HearkenContainer.Mixins.Model.Collections;
namespace HearkenContainer.Model
{
    public abstract class ActionInfo: IHasTypedInfo
    {
        private FunctionInfo[] _methods;
        private Type[] _listensTo;
        public virtual BindingFlags Flags { get; set; }
        public virtual Type Type { get; set; }

        public virtual Type[] ListensTo
        {
            get { return _listensTo ?? (_listensTo = GetWhoShouldBeListened()); } 
        }

        public virtual FunctionInfo[] Functions 
        {
            get { return _methods ?? (_methods = Extract().ToArray()); }            
        }
        
        public ActionInfo()
        {
            Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        }

        protected abstract IEnumerable<FunctionInfo> Extract();
        protected abstract Type[] GetWhoShouldBeListened();

        public virtual object Invoke(object eventSource, IEnumerable<EventInfo> events)
        {
            var result =
                Activator.CreateInstance(this.Type);

            Attach(eventSource, events, result);

            return result;
        }

        public virtual void Attach(object eventSource, IEnumerable<EventInfo> events, object action)
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
    }
}
