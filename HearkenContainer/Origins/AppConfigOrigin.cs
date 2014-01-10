using System;
using HearkenContainer.AppConfig;
using HearkenContainer.Origins.Model;

namespace HearkenContainer.Origins
{
    public class AppConfigOrigin : DefaultOrigin
    {
        internal AppConfigOrigin() 
        { }

        internal AppConfigOrigin(HearkenContainerSection section) 
        {
            Section = section;
        }

        internal HearkenContainerSection Section;

        public override void Save(IHearkenContainer container)
        {
            var groups = 
                (Section ?? HearkenContainerSection.Self).Groups;
            
            foreach (var configGroup in groups)
            {
                var group =
                    GetGroup(configGroup.Name, container);

                foreach (var source in configGroup.Sources)
                {
                    Type type = null;
                    try
                    { type = Type.GetType(source.Type, true); }
                    catch(Exception e)
                    { throw new SourceNotFoundException(source.Type, e); }

                    group.UpdateOrCreate(
                        type,
                        update: (i, src) => new AppConfigSourceInfo(src, source.Triggers),
                        create: () => new AppConfigSourceInfo(Type.GetType(source.Type), source.Triggers));
                }

                foreach (var action in configGroup.Actions)
                {
                    Type type = null;
                    try
                    { type = Type.GetType(action.Type, true); }
                    catch 
                    { throw new ActionNotFoundException(action.Type); }

                    group.UpdateOrCreate(
                        type,
                        update: (i, act) => new AppConfigActionInfo(act, action),
                        create: () => new AppConfigActionInfo(type, action));
                    
                }
		    }            
        }
    }
}