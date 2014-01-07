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
    public class NotationSource : DefaultSource
    {
        internal NotationSource()
        { Types = new List<Type>(); }

        internal List<Type> Types;


        /// <summary>
        /// Creates or updates a current set of groups
        /// </summary>
        public override void Save(IHearkenContainer container)
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

                    group.Sources.Add(new NotedTriggerInfo() { Type = type });
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