using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HearkenContainer.Model
{
    public abstract class ActionInfo: IHasTypedInfo
    {
        private IEnumerable<FunctionInfo> _methods;
        private Type[] _listensTo;

        public virtual Type Type { get; set; }

        public virtual Type[] ListensTo
        {
            get { return _listensTo ?? (_listensTo = GetWhoShouldBeListened()); } 
        }

        public virtual IEnumerable<FunctionInfo> Functions 
        {
            get { return _methods ?? (_methods = Extract()); }            
        }

        protected abstract IEnumerable<FunctionInfo> Extract();
        protected abstract Type[] GetWhoShouldBeListened();

        public abstract object Invoke(object eventSource, IEnumerable<EventInfo> events);
        public abstract void Attach(object eventSource, IEnumerable<EventInfo> events, object action);        
    }
}
