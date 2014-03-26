using System;
using System.Collections.Generic;
using ChainReaction.Model.Collections;
using ChainReaction.Mixins.Model.Collections;
using System.Runtime;

namespace ChainReaction.Model
{

    public class GroupInfo : IComparable
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
        private T UpdateOrCreate<T>(Type type, IList<T> tees, Func<int, T, T> update, Func<T> create)
            where T : IHasTypedInfo
        {
            int index = 0;
            var tee = tees
                .Foremost((i, t) =>
                {
                    index = i;
                    return type == t.Type;
                });

            T val = default(T);

            if (tee.Value == null)
            {
                tees.Add(val = create());
            }
            else
            {
                update(tee.Key, tee.Value);
            }

            return val;
        }

        public ActionInfo UpdateOrCreate(Type type, Func<int, ActionInfo, ActionInfo> update, Func<ActionInfo> create)
        {
            return UpdateOrCreate<ActionInfo>(type, Actions, update, create);
        }

        public SourceInfo UpdateOrCreate(Type type, Func<int, SourceInfo, SourceInfo> update, Func<SourceInfo> create)
        {
            return UpdateOrCreate<SourceInfo>(type, Sources, update, create);
        }

        public int CompareTo(object obj)
        {
            return (obj is string) ? this.Name.CompareTo(obj) :
                obj is GroupInfo ? this.Name.CompareTo(((GroupInfo)obj).Name) :
                -1;
        }
    }
}