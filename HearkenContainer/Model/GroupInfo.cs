using System;
using System.Collections.Generic;
using HearkenContainer.Model.Collections;
using HearkenContainer.Mixins.Model.Collections;
using System.Runtime;

namespace HearkenContainer.Model
{

    public class GroupInfo: IComparable
    {
        internal GroupInfo() { }

        public GroupInfo(string name)
        {
            this.Name = name;
            Sources = new List<SourceInfo>();
            Actions = new List<ActionInfo>();
        }

        /// <summary>
        /// Key, responsible for naming and finding the container group
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<SourceInfo> Sources { get; set; }

        /// <summary>
        /// Listenters that will be executed as soon as necessary
        /// </summary>
        public IList<ActionInfo> Actions { get; set; }

        /// <summary>
        /// Interceptors, responsible for interfering in one or all actions
        /// </summary>
        public InterceptorList Interceptors { get; set; }

        [TargetedPatchingOptOut("Performance required")]
        private T TryGet<T>(Type type, IList<T> tees, Func<T> createT)
            where T : IHasTypedInfo
        {
            var tee = tees
                .Foremost(t => type == t.Type);

            if (tee == null)
            { tees.Add(tee = createT()); }

            return tee;
        }

        public ActionInfo TryGetAction(Type type, Func<ActionInfo> createAction)
        { 
            return TryGet<ActionInfo>(type, Actions, createAction);
        }

        public SourceInfo TryGetSource(Type type, Func<SourceInfo> createSource)
        {
            return TryGet<SourceInfo>(type, Sources, createSource);
        }

        public int CompareTo(object obj)
        {
            return (obj is string) ? this.Name.CompareTo(obj) :
                obj is GroupInfo ? this.Name.CompareTo(((GroupInfo)obj).Name) :
                -1;
        }

        
    }
}