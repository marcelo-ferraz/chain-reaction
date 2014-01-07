using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using HearkenContainer.Model;

namespace HearkenContainer.Sources.Model
{
    public class AppConfigSourceInfo : SourceInfo
    {
        private AppConfig.CollectionOf.Triggers _triggers;

        public AppConfigSourceInfo(Type type, AppConfig.CollectionOf.Triggers triggers)
        {
            this.Type = type;
            this._triggers = triggers;
        }

        protected override IEnumerable<EventInfo> Extract()
        {
            throw new NotImplementedException();
        }
    }
}
