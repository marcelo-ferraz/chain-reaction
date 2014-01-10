using System;
using System.Collections.Generic;
using System.Text;
using HearkenContainer.Model;
using HearkenContainer.Mixins.Model.Collections;
using HearkenContainer.Notations;
using System.Reflection;

namespace HearkenContainer
{
    public class SimpleDispatchContainer: IHearkenContainer
    {
        GroupInfo[] _groups;

        public SimpleDispatchContainer()
        {
            _groups = (GroupInfo[])
                Array.CreateInstance(typeof(GroupInfo), 1);
        }

        public bool TryResolve(string groupName, out GroupInfo group)
        {
            return (group = _groups.BinaryFind(groupName)) != null;
        }

        public void Add(GroupInfo group)
        {
            int index = 0;

            if (_groups.Length < 2)
            {
                _groups[0] = group;
                return;
            }

            if (_groups.BinarySearch(group.Name, out index))
            { return; }

            _groups.Insert(~index, group);            
        }

        public T Invoke<T>(Action<object> afterLoadAction = null, string groupName = "")
        {
            var group = 
                _groups.Foremost(g => 
                    g.Name == groupName);

            var source = group.Sources.Foremost(
                tr => typeof(T).IsAssignableFrom(tr.Type));

            return InvokeNAttachAll<T>(group, source, afterLoadAction);
        }

        protected T InvokeNAttachAll<T>(GroupInfo group, SourceInfo trigger, Action<object> afterLoadAction)
        {
            var eventsSource =
                Activator.CreateInstance<T>();

            foreach (var action in group.Actions)
            {
                int i;

                //If is not found, it will not listen
                if (action.ListensTo != null && !action.ListensTo.BinarySearch(typeof(T), out i))
                { continue; }

                /*ELSE: If 
                 * is not set to any type, it will try with all from group, 
                 * or 
                 * it is focused on one type
                 */
                var listener =
                    Activator.CreateInstance(action.Type);
                                
                action.Attach(eventsSource, trigger.Events, listener);

                if (afterLoadAction != null)
                { afterLoadAction(listener); }
            }

            return eventsSource;
        }
    }
}
