using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HearkenContainer.Model;
using HearkenContainer.Mixins.Model.Collections;

namespace HearkenContainer.Sources.Model
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

        public AppConfigActionInfo(ActionInfo src, AppConfig.Element.Action action)
        {
            this.Type = src.Type;

            this._cfgAction = action;
            this._previousAction = src;

            foreach (var function in src.Functions)
            {
                var foundHere = this.Functions.Foremost(
                    (i, item) =>
                        item.Method.Name == function.Method.Name);

                if (foundHere.Value == null)
                { this.Functions[foundHere.Key] = function; }
            }            
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
            return functions;
        }

        protected override Type[] GetWhoShouldBeListened()
        {
            var thisType = Type.GetType(_cfgAction.Source);
            
            if(_previousAction == null || _previousAction.ListensTo == null || _previousAction.ListensTo.Length < 1)
            { return new Type[] { thisType }; }

            var types = _previousAction.ListensTo;
            _previousAction = null;
            
            if (types.Foremost((i, src) => src == thisType).Value == null)
            {
                types.Insert(0, thisType);
            }

            return types;
        }
    }
}
