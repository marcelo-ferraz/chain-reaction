using System;
using ChainReaction.AppConfig;
using ChainReaction.Origins.Model;

namespace ChainReaction.Origins
{
    public class AppConfigOrigin : DefaultOrigin
    {
        internal AppConfigOrigin() 
        { }

        internal AppConfigOrigin(ChainReactionSection section) 
        {
            Section = section;
        }

        internal ChainReactionSection Section;

        public override void Save(IChainReactionContainer container)
        {
            var groups = 
                (Section ?? ChainReactionSection.Self).Groups;
            
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

                foreach (var action in configGroup.Handlers)
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