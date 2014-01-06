using System;
using System.Collections.Generic;
using HearkenContainer.Model;
using HearkenContainer.Notations;
using HearkenContainer.Sources.Model;
using HearkenContainer.Mixins.Model.Collections;

namespace HearkenContainer.Sources
{
    /// <summary>
    /// Creates or updates a current set of groups
    /// </summary>
    public class NotationSource : ISource
    {
        internal NotationSource()
        { Types = new List<Type>(); }

        internal List<Type> Types;

        private TAttr Get<TAttr>(Type type)
        {
            return (TAttr)type.GetCustomAttributes(true).Foremost(att => att is TAttr);
        }

        private GroupInfo GetGroup(string groupName, IHearkenContainer container)
        {
            GroupInfo group = null;

            if (!container.TryResolve(groupName ?? string.Empty, out group))
            {
                group =
                    new GroupInfo(groupName ?? string.Empty);

                container.Add(group);
            }

            return group;
        }

        /// <summary>
        /// Creates or updates a current set of groups
        /// </summary>
        public void Save(IHearkenContainer container)
        {
            var group = new GroupInfo();

            for (int i = 0; i < Types.Count; i++)
            {
                var type = Types[i];

                if (type.IsDefined(typeof(SourceAttribute), true))
                {
                    var attr =
                        Get<SourceAttribute>(type);

                    group = GetGroup(attr.Group, container);

                    group.Triggers.Add(new NotedTriggerInfo() { Type = type });
                }

                if (type.IsDefined(typeof(ActionAttribute), true))
                {
                    var attr =
                        Get<ActionAttribute>(type);

                    group = GetGroup(attr.Group, container);

                    group.Actions.Add(new NotedActionInfo() { Type = type });
                }

                if (type.IsDefined(typeof(InterceptorAttribute), true))
                {
                    throw new NotImplementedException("still not implemented. Still thinking on the mechanism.");
                }
            }
        }
    }
}