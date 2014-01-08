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
        private T TryGet<T>(Type type, IList<T> tees, Func<int, T, T> doWhenFound, Func<T> createT)
            where T : IHasTypedInfo
        {
            int index = 0;
            var tee = tees
                .Foremost((i, t) => {
                    index = i;
                    return type == t.Type;
                });

            T val = default(T);

            if (tee.Value == null)
            {
                tees.Add(val = createT()); 
            }
            else 
            {
                doWhenFound(tee.Key, tee.Value);
            }

            return val;
        }

        public ActionInfo TryGetAction(Type type, Func<int, ActionInfo, ActionInfo> doWhenFound, Func<ActionInfo> createAction)
        {
            return TryGet<ActionInfo>(type, Actions, doWhenFound, createAction);
        }

        public SourceInfo TryGetSource(Type type, Func<int, SourceInfo, SourceInfo> doWhenFound, Func<SourceInfo> createSource)
        {
            return TryGet<SourceInfo>(type, Sources, doWhenFound, createSource);
        }

        public int CompareTo(object obj)
        {
            return (obj is string) ? this.Name.CompareTo(obj) :
                obj is GroupInfo ? this.Name.CompareTo(((GroupInfo)obj).Name) :
                -1;
        }

        
    }
}