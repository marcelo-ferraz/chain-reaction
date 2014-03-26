using System;
using ChainReaction.Mixins.Model.Collections;
using ChainReaction.Model;

namespace ChainReaction
{
    /// <summary>
    /// COntainer specialized in I
    /// </summary>
    public class SimpleChainReactionContainer: IChainReactionContainer
    {
        protected GroupInfo[] _groups;

        public SimpleChainReactionContainer()
        {
            _groups = (GroupInfo[])
                Array.CreateInstance(typeof(GroupInfo), 1);
        }
        
        private static void AttachAll<T>(Action<object> afterLoadAction, GroupInfo group, SourceInfo source, T eventsSource)
        {
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

                action.Attach(eventsSource, source.Events, listener);

                if (afterLoadAction != null)
                { afterLoadAction(listener); }
            }
        }

        protected virtual T SeekNInvoke<T>(string groupName, object[] arguments, out GroupInfo group, out SourceInfo source)
        {
            group =
                _groups.Foremost(g =>
                    g.Name == groupName);

            source = group.Sources.Foremost(
                tr => typeof(T).IsAssignableFrom(tr.Type));

            if (group == null)
            { throw new GroupNotFoundException("The Group named was not found", groupName); }

            if (source == null)
            { throw new SourceNotFoundException("The source of type {0} was not found in the group: '{1}'", typeof(T).Name, group.Name); }

            return (T)Activator.CreateInstance(typeof(T), arguments);
        }

        protected virtual T InvokeNAttachAll<T>(Action<object> afterLoadAction, string groupName, params object[] arguments)
        {
            GroupInfo group;
            SourceInfo source;
            
            T eventsSource = 
                SeekNInvoke<T>(groupName, arguments, out group, out source);

            AttachAll<T>(afterLoadAction, group, source, eventsSource);

            return eventsSource;
        }

        public virtual bool TryResolve(string groupName, out GroupInfo group)
        {
            return (group = _groups.BinaryFind(groupName)) != null;
        }

        public virtual void Add(GroupInfo group)
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

        public virtual T Invoke<T>(string groupName = "", Action<object> afterLoadAction = null)
        {
            return InvokeNAttachAll<T>(afterLoadAction, groupName);
        }

        public virtual T Invoke<T>(string groupName = "", Action<object> afterLoadAction = null, params object[] arguments)
        {
            return InvokeNAttachAll<T>(afterLoadAction, groupName, arguments);
        }
    }
}
