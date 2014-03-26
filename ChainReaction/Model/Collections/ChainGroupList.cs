using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ChainReaction.Mixins.Model.Collections;

namespace ChainReaction.Model.Collections
{
    /// <summary>
    /// Symbolizes a many listeners to one trigger, the trigger can be either a concrete class, an abstract one, or an interface.
    /// </summary>
    public abstract class ChainGroupList 
    {
        protected IEnumerable<GroupInfo> Groups;

        protected virtual BindingFlags DefaultFlags
        {
            get { return BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly; }
        }

        protected abstract void Attach(object source, Action<int, object> doAfterInstatiate, IList listeners, BindingFlags flags);

        protected virtual string GenerateName<T>()
        {
            return string.Concat(typeof(T).Name, "_group");
        }
                
        protected virtual void Attach(object source)
        {
            Attach(source, null);
        }
        
        protected virtual void Attach(object source, Action<int, object> doAfterInstatiate)
        {
            Attach(source, doAfterInstatiate, new ArrayList());
        }

        protected virtual void Attach(object source, Action<int, object> doAfterInstatiate, IList listeners)
        { 
            Attach(source, doAfterInstatiate, listeners, DefaultFlags);
        }
    }
}
