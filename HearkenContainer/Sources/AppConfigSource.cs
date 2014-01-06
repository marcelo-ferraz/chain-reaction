using System;
using System.Collections.Generic;
using HearkenContainer.Model;
using HearkenContainer.Notations;
using HearkenContainer.Sources.Model;
using HearkenContainer.Mixins.Model.Collections;
using HearkenContainer.AppConfig;

namespace HearkenContainer.Sources
{
    public class AppConfigSource : ISource
    {
        internal AppConfigSource() { }

        internal object Section;

        public void Save(IHearkenContainer container)
        {
            var groups = 
                ((HearkenContainerSection)(Section ?? HearkenContainerSection.Self)).Groups;
            
            foreach (var group in groups)
            {
                foreach (var src in group.Sources)
                {
                    var source = 
                        ParseNSave(container, src);

                    if (source.Triggers.Count < 1)
                    { SaveAllTriggers(container, source); }
                    else
                    { ParseNSave(container, source.Triggers); }
                }

                foreach (var act in group.Actions)
                {
                    var action =
                        ParseNSave(container, act);

                    if (action.Functions.Count < 1)
                    { SaveAllFunctions(container, action); }
                    else
                    { ParseNSave(container, action.Functions); }
                }
		    }            
        }

        private ActionInfo ParseNSave(IHearkenContainer container, Element.Action cfgAction)
        {
            throw new NotImplementedException();
        }

        private FunctionInfo ParseNSave(IHearkenContainer container, Element.Source cfgSource)
        {
            throw new NotImplementedException();
        }
    }
}