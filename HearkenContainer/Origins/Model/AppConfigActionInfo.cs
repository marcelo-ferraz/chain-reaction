using System;
using System.Collections.Generic;
using System.Linq;
using HearkenContainer.Mixins.Model.Collections;
using HearkenContainer.Model;

namespace HearkenContainer.Origins.Model
{
    public class AppConfigActionInfo : ActionInfo
    {
        private AppConfig.Element.Action _cfgAction;
        private ActionInfo _previousAction;

        public AppConfigActionInfo(Type type, AppConfig.Element.Action action)
        {
            this.Type = type;
            this._cfgAction = action;
        }

        public AppConfigActionInfo(ActionInfo previousAction, AppConfig.Element.Action action)
        {
            this.Type = previousAction.Type;

            this._cfgAction = action;
            this._previousAction = previousAction;
        }

        protected override IEnumerable<FunctionInfo> Extract()
        {
            var methods = 
                Type.GetMethods(Flags);

            if (_cfgAction.Functions == null || _cfgAction.Functions.Count < 1)
            { return methods.Select(m => new FunctionInfo { Method = m, EventName = m.Name }); }

            var functions =
                ArrayMixins.Create<FunctionInfo>(0);

            foreach (var function in _cfgAction.Functions)
            {
                var method = methods.Foremost(
                    (i, ev) =>
                        ev.Name == function.Name);

                if (method.Value != null)
                {
                    functions.Insert(method.Key, new FunctionInfo() { 
                        Method = method.Value,
                        EventName = string.IsNullOrEmpty(function.Event) ? method.Value.Name : function.Event
                    }); 
                }
            }

            if (_previousAction == null ||
                _previousAction.ListensTo == null ||
                _previousAction.ListensTo.Length < 1)            
            { return functions; }

            return functions.Union(_previousAction.Functions, 
                (f1, f2) => 
                    f1.Method.Name == f2.Method.Name);
        }

        protected override Type[] GetWhoShouldBeListened()
        {
            Type[] listenedTypes = null;

            if(!string.IsNullOrEmpty(_cfgAction.Source))
            { listenedTypes = new []{ Type.GetType(_cfgAction.Source) }; }
            
            if(_previousAction == null || 
                _previousAction.ListensTo == null || 
                _previousAction.ListensTo.Length < 1)
            { return listenedTypes; }

            return listenedTypes.Union(_previousAction.ListensTo);
        }
    }
}
