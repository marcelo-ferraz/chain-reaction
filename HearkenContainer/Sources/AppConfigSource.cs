using System;
using HearkenContainer.AppConfig;
using HearkenContainer.Sources.Model;

namespace HearkenContainer.Sources
{
    public class AppConfigSource : DefaultSource
    {
        internal AppConfigSource() { }

        internal object Section;

        public override void Save(IHearkenContainer container)
        {
            var groups = 
                ((HearkenContainerSection)(Section ?? HearkenContainerSection.Self)).Groups;
            
            foreach (var configGroup in groups)
            {
                var group =
                    GetGroup(configGroup.Name, container);

                foreach (var source in configGroup.Sources)
                {
                    Type type = null;
                    try
                    { type = Type.GetType(source.Type); }
                    catch
                    { throw new SourceNotFoundException(source.Type); }

                    group.TryGetSource(
                        type,
                        () =>
                            new AppConfigSourceInfo(Type.GetType(source.Type), source.Triggers));
                }

                foreach (var action in configGroup.Actions)
                {
                    Type type = null;
                    try
                    { type = Type.GetType(action.Type); }
                    catch 
                    { throw new ActionNotFoundException(action.Type); }

                    group.TryGetAction(
                        type,
                        () =>
                            new AppConfigActionInfo(type, action.Functions));
                    
                }
		    }            
        }
    }
}