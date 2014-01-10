using System;
using System.Collections.Generic;
using HearkenContainer.Model;
using HearkenContainer.Notations;
using HearkenContainer.Origins.Model;
using HearkenContainer.Mixins.Model.Collections;

namespace HearkenContainer.Origins
{
    /// <summary>
    /// Creates or updates a current set of groups
    /// </summary>
    public class NotationOrigin : DefaultOrigin
    {
        internal NotationOrigin()
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

                    group.UpdateOrCreate(type,
                        update: (j, previous) => new NotedSourceInfo(previous, type),
                        create: () => new NotedSourceInfo() { Type = type });

                }

                if (type.IsDefined(typeof(ActionAttribute), true))
                {
                    var attr =
                        Get<ActionAttribute>(type);

                    group = GetGroup(attr.Group, container);

                    group.UpdateOrCreate(type,
                        update: (j, previous) => new NotedActionInfo(previous) { Type = type },
                        create: () => new NotedActionInfo() { Type = type });

                }

                if (type.IsDefined(typeof(InterceptorAttribute), true))
                {
                    throw new NotImplementedException("still not implemented. Still thinking on the mechanism.");
                }
            }
        }
    }
}