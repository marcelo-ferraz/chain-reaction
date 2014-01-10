using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HearkenContainer.Model;
using HearkenContainer.Mixins.Model.Collections;

namespace HearkenContainer.Origins
{
    public abstract class DefaultOrigin : IOrigin
    {
        public abstract void Save(IHearkenContainer container);
        
        protected TAttr Get<TAttr>(Type type)
        {
            return (TAttr)type.GetCustomAttributes(true).Foremost(att => att is TAttr);
        }

        protected GroupInfo GetGroup(string groupName, IHearkenContainer container)
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
    }
}
