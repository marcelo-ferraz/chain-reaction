using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Text;
using ChainReaction.Mixins.Model.Collections;
namespace ChainReaction.Model
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
                
                if(HaveSameSignature(@delegate, @event))
                { @event.AddEventHandler(eventSource, @delegate); }
            }
        }     
   
        private static bool HaveSameSignature(Delegate @delegate, EventInfo @event)
        {
            if (@delegate.GetType().Equals(@event.EventHandlerType)) { return true; }

            /*
             * TODO: perhaps it might need to be cached. But before running into any battle, verify if
             * ever the delegate and event are diferent and still assignable.
             */

            var evHandlerInvoke = 
                @event.EventHandlerType.GetMethod("Invoke");

            if (!evHandlerInvoke.ReturnType.IsAssignableFrom(@delegate.Method.ReturnType))
            { return false; }

            var delParams = 
                @delegate.Method.GetParameters();

            var evParams =
                evHandlerInvoke.GetParameters();

            if (delParams.Length != evParams.Length) { return false; }

            for (int i = 0; i < delParams.Length; ++i)
            {
                bool areAssinable = delParams[i]
                    .GetType()
                    .IsAssignableFrom(evParams[i].GetType());
                
                if (!areAssinable) { return false; }
            }
            
            return true;
        }
    }
}
