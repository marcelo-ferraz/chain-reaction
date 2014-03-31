using System;
using System.Collections.Generic;
using System.Linq;
using ChainReaction.Mixins.Model.Collections;
using ChainReaction.Model;

namespace ChainReaction.Origins.Model
{
    public class AppConfigActionInfo : HandlerInfo
    {
        private AppConfig.Element.Handler _cfgAction;
        private HandlerInfo _previousAction;

        public AppConfigActionInfo(Type type, AppConfig.Element.Handler action)
        {
            this.Type = type;
            this._cfgAction = action;
        }

        public AppConfigActionInfo(HandlerInfo previousAction, AppConfig.Element.Handler action)
        {
            this.Type = previousAction.Type;

            this._cfgAction = action;
            this._previousAction = previousAction;
        }

        protected override IEnumerable<ActionInfo> Extract()
        {
            var methods = 
                Type.GetMethods(Flags);

            if (_cfgAction.Actions == null || _cfgAction.Actions.Count < 1)
            { return methods.Select(m => new ActionInfo { Method = m, EventName = m.Name }); }

            var functions =
                ArrayMixins.Create<ActionInfo>(0);

            foreach (var action in _cfgAction.Actions)
            {
                var method = methods.Foremost(
                    (i, ev) =>
                        ev.Name == action.Name);

                if (method.Value != null)
                {
                    functions.Insert(method.Key, new ActionInfo() { 
                        Method = method.Value,
                        EventName = string.IsNullOrEmpty(action.Event) ? method.Value.Name : action.Event
                    }); 
                }
            }

            if (_previousAction == null ||
                _previousAction.ListensTo == null ||
                _previousAction.ListensTo.Length < 1)            
            { return functions; }

            return functions.Union(_previousAction.Actions, 
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
