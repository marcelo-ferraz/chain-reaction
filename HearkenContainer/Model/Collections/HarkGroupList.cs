using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using HearkenContainer.Mixins.Model.Collections;

namespace HearkenContainer.Model.Collections
{
    //public class DispatchmentGroupList : List<DispatchmentGroupInfo>
    //{
    //    public DispatchmentGroupList() : base() { }
        
    //    public DispatchmentGroupList(DispatchmentGroupInfo entry) 
    //        : base() 
    //    { 
    //        this.Add(entry); 
    //    }

    //    public string Name { get; set; }

    //    public DispatchmentGroupList Add(string key, Type type, IDictionary<string, Type> listeners)
    //    {
    //        this.Add(new DispatchmentGroupInfo() { Key = key, Type = type, Listeners = listeners});
    //        return this;
    //    }

    //    public DispatchmentGroupList Add(string key, Type type, IDictionary<string, Type> listeners, InterceptorList interceptors)
    //    {
    //        this.Add(new DispatchmentGroupInfo() { Key = key, Type = type, Listeners = listeners, Interceptors = interceptors });
    //        return this;
    //    }

    //    private IEnumerable<DispatchmentGroupInfo> GetIEnumerable(Func<DispatchmentGroupInfo, bool> predicate)
    //    {
    //        for (int i = 0; i < this.Count; i++)
    //        {
    //            var entry = this[i];
    //            if (predicate(this[i])) { yield return entry; }
    //        }
    //    }

    //    private DispatchmentGroupInfo Get(string key)
    //    {
    //        return this.First(item => item.Key.Equals(key));
    //    }

    //    private IEnumerable<DispatchmentGroupInfo> Many<T>()
    //    {
    //        return this.Where(item => item.Type.Equals(typeof(T)));
    //    }

    //    private IEnumerable<DispatchmentGroupInfo> Many(Type type)
    //    {
    //        return this.Where(item => item.Type.Equals(type));
    //    }

    //    public DispatchmentGroupInfo First<T>()
    //    {
    //        return this.First(e => e is T);
    //    }

    //    public DispatchmentGroupInfo First(Type type)
    //    {
    //        return this.First(e => e.GetType() == type);
    //    }

    //    public object this[string key]
    //    {
    //        get { return Get(key); }
    //    }
    //}
    

    /// <summary>
    /// Symbolizes a many listeners to one trigger, the trigger can be either a concrete class, an abstract one, or an interface.
    /// </summary>
    public abstract class HarkGroupList 
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
