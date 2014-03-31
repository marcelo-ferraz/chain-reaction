using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using ChainReaction.Mixins.Model.Collections;
using ChainReaction.Model;
using ChainReaction.Notations;
using ChainReaction.Mixins.Model.Collections;

namespace ChainReaction.Origins.Model
{
    public class NotedActionInfo: HandlerInfo
    {
        private HandlerInfo _previousAction;
        internal NotedActionInfo() { }
        internal NotedActionInfo(HandlerInfo previousAction)
        {
            _previousAction = previousAction;
        }

        protected override IEnumerable<ActionInfo> Extract()
        {
            var methods = 
                this.Type.GetMethods();                
            
            if (methods.Foremost(e => e.IsDefined(typeof(ActionAttribute), true)) != null)
            {
                return Extract(methods.Which(e => 
                    e.IsDefined(typeof(ActionAttribute), true)), 
                    true);
            }

            ActionInfo[] functions = 
                Extract(methods, false).ToArray();

            if (_previousAction == null ||
                _previousAction.Actions == null ||
                _previousAction.Actions.Length < 1)
            { return functions; }

            return functions.Union(
                _previousAction.Actions, 
                (t1, t2) => 
                    t1.Method.Name == t2.Method.Name);
        }

        private static IEnumerable<ActionInfo> Extract(IEnumerable<MethodInfo> methods, bool isFullyDecorated)
        {
            foreach (var method in methods)
            {
                var attrs =
                    method.GetCustomAttributes(true).Which(e => e is ActionAttribute);

                if (!isFullyDecorated)
                {
                    yield return new ActionInfo
                    {
                        Method = method,
                        EventName = GetMethodName(isFullyDecorated, method)
                    };
                }
                else
                {
                    foreach (var attr in attrs)
                    {
                        yield return new ActionInfo
                        {
                            Method = method,
                            EventName = GetMethodName(isFullyDecorated, method, (ActionAttribute)attr)
                        };
                    }
                }
            }
        }

        private static string GetMethodName(bool hasAttr, MethodInfo method, ActionAttribute actionAttr = null)
        {            
            return hasAttr ?
                (string.IsNullOrEmpty(actionAttr.EventName) ? method.Name : actionAttr.EventName) :
                method.Name;
        }

        protected override Type[] GetWhoShouldBeListened()
        {
            var attr = (HandlerAttribute)
                Type.GetCustomAttributes(true)
                .Foremost(a => a is HandlerAttribute);

            return attr.ListensTo;
        }
    }
}