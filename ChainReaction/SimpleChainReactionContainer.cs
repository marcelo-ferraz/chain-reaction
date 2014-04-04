using System;
using System.Runtime;
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

        [TargetedPatchingOptOut("Performance needed")]
        private static void AttachAll<T>(object[] handlers, GroupInfo group, SourceInfo source, T eventsSource)
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

                object listener = null;               
                
                for (int j = 0; j < handlers.Length; j++)
                {
                    if (object.Equals(handlers[j].GetType(), action.Type))
                    {
                        listener = handlers[j];
                        break;
                    }
                }

                listener = listener ??
                    Activator.CreateInstance(action.Type);

                action.Attach(eventsSource, source.Events, listener);
            }
        }

        [TargetedPatchingOptOut("Performance needed")]
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

        [TargetedPatchingOptOut("Performance needed")]
        protected virtual T SeekNInvoke<T>(string groupName, object[] arguments, out GroupInfo group, out SourceInfo source)
        {
            group = _groups
                .Foremost(g =>
                    g.Name == groupName);

            if (group == null)
            { throw new GroupNotFoundException("The Group named was not found", groupName); }

            source = group.Sources.Foremost(
                tr => typeof(T).IsAssignableFrom(tr.Type));

            if (source == null)
            { throw new SourceNotFoundException("The source of type {0} was not found in the group: '{1}'", typeof(T).Name, group.Name); }

            return (T)Activator.CreateInstance(typeof(T), arguments);
        }

        [TargetedPatchingOptOut("Performance needed")]
        protected virtual T InvokeNAttachAll<T>(Action<object> afterLoadAction, string groupName, params object[] arguments)
        {
            GroupInfo group;
            SourceInfo source;
            
            T eventsSource = 
                SeekNInvoke<T>(groupName, arguments, out group, out source);

            AttachAll<T>(afterLoadAction, group, source, eventsSource);

            return eventsSource;
        }

        [TargetedPatchingOptOut("Performance needed")]
        protected virtual T InvokeNAttachAll<T>(object[] handlers, string groupName, params object[] arguments)
        {
            GroupInfo group;
            SourceInfo source;

            T eventsSource =
                SeekNInvoke<T>(groupName, arguments, out group, out source);

            AttachAll<T>(handlers, group, source, eventsSource);

            return eventsSource;
        }

        /// <summary>
        /// Tries to find a group
        /// </summary>
        /// <param name="groupName">the grou'sp name</param>
        /// <param name="group">the group itself</param>
        /// <returns></returns>
        public virtual bool TryResolveGroup(string groupName, out GroupInfo group)
        {
            return (group = _groups.BinaryFind(groupName)) != null;
        }

        /// <summary>
        /// Adds one group to the container
        /// </summary>
        /// <param name="group">A set </param>
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

        /// <summary>
        /// Invokes the type, throughout a given group, with all exposed events bound, as the container configuration rules
        /// </summary>
        /// <remarks>
        /// In the case of a general group, is assumed that the group has no name, and none is requested
        /// </remarks>
        /// <typeparam name="T">The type to be invoked</typeparam>
        /// <param name="group">the name of the group</param>
        /// <param name="afterLoadHandler">funcion called when a handler is loaded</param>
        /// <returns>returns an instance of a given type</returns>
        public virtual T Invoke<T>(string group = "", Action<object> afterLoadHandler = null)
        {
            return InvokeNAttachAll<T>(afterLoadHandler, group);
        }

        /// <summary>
        /// Invokes the type, throughout a given group, with all exposed events bound, as the container configuration rules
        /// </summary>
        /// <remarks>
        /// In the case of a general group, is assumed that the group has no name, and none is requested
        /// </remarks>
        /// <typeparam name="T">The type to be invoked</typeparam>
        /// <param name="group">the name of the group</param>
        /// <param name="afterLoadHandler">funcion called when a handler is loaded</param>
        /// <param name="arguments">A list of arguments used to construct the object</param>
        /// <returns>returns an instance of a given type</returns>
        public virtual T Invoke<T>(string group = "", Action<object> afterLoadHandler = null, params object[] arguments)
        {
            return InvokeNAttachAll<T>(afterLoadHandler, group, arguments);
        }

        /// <summary>
        /// Invokes the type, throughout a given group, with all exposed events bound, as the container configuration rules. 
        /// And accept instances of handlers for those events, as long as they are previously mapped
        /// </summary>
        /// <remarks>
        /// In the case of a general group, is assumed that the group has no name, and none is requested
        /// </remarks>
        /// <typeparam name="T">The type to be invoked</typeparam>
        /// <param name="group">the name of the group</param>
        /// <param name="handlers">an array of handlers to be bound to the instance</param>
        /// <returns>returns an instance of a given type</returns>
        public virtual T Invoke<T>(string group = "", params object[] handlers)
        {
            return InvokeNAttachAll<T>(handlers, group, null);
        }

        /// <summary>
        /// Invokes the type, throughout a given group, with all exposed events bound, as the container configuration rules. 
        /// And accept instances of handlers for those events, as long as they are previously mapped
        /// </summary>
        /// <remarks>
        /// In the case of a general group, is assumed that the group has no name, and none is requested
        /// </remarks>
        /// <typeparam name="T">The type to be invoked</typeparam>
        /// <param name="group">the name of the group</param>
        /// <param name="handlers">an array of handlers to be bound to the instance</param>
        /// <param name="arguments">A list of arguments used to construct the object</param>
        /// <returns>returns an instance of a given type</returns>
        public virtual T Invoke<T>(string group = "", object[] handlers = null, params object[] arguments)
        {
            return InvokeNAttachAll<T>(handlers, group, arguments);
        }       
    }
}
