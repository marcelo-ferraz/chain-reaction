using System;
using ChainReaction.Model;
namespace ChainReaction
{
    public interface IChainReactionContainer
    {
        /// <summary>
        /// Tries to find a group
        /// </summary>
        /// <param name="groupName">the grou'sp name</param>
        /// <param name="group">the group itself</param>
        /// <returns></returns>
        bool TryResolveGroup(string groupName, out GroupInfo group);

        /// <summary>
        /// Adds one group to the container
        /// </summary>
        /// <param name="group">A set </param>
        void Add(GroupInfo group);

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
        T Invoke<T>(string group = "", Action<object> afterLoadHandler = null);

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
        T Invoke<T>(string group = "", Action<object> afterLoadHandler = null, params object[] arguments);

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
        T Invoke<T>(string group = "", params object[] handlers);

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
        T Invoke<T>(string group = "", object[] handlers = null, params object[] arguments);
    }
}