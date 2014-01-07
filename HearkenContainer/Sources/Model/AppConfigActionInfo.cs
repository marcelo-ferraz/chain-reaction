using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HearkenContainer.Model;

namespace HearkenContainer.Sources.Model
{
    public class AppConfigActionInfo : ActionInfo
    {
        private AppConfig.CollectionOf.Functions _functions;

        public AppConfigActionInfo(Type type, AppConfig.CollectionOf.Functions functions)
        {
            this.Type = type;
            this._functions = functions;
        }

        protected override IEnumerable<FunctionInfo> Extract()
        {
            throw new NotImplementedException();
        }

        protected override Type[] GetWhoShouldBeListened()
        {
            throw new NotImplementedException();
        }

        public override object Invoke(object eventSource, IEnumerable<System.Reflection.EventInfo> events)
        {
            throw new NotImplementedException();
        }

        public override void Attach(object eventSource, IEnumerable<System.Reflection.EventInfo> events, object action)
        {
            throw new NotImplementedException();
        }
    }
}
