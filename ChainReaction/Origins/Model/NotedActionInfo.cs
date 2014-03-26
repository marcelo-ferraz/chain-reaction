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
    public class NotedActionInfo: ActionInfo
    {
        private ActionInfo _previousAction;
        internal NotedActionInfo() { }
        internal NotedActionInfo(ActionInfo previousAction)
        {
            _previousAction = previousAction;
        }

        protected override IEnumerable<FunctionInfo> Extract()
        {
            var methods = 
                this.Type.GetMethods();                
            
            if (methods.Foremost(e => e.IsDefined(typeof(FunctionAttribute), true)) != null)
            {
                return Extract(methods.Which(e => 
                    e.IsDefined(typeof(FunctionAttribute), true)), 
                    true);
            }

            FunctionInfo[] functions = 
                Extract(methods, false).ToArray();

            if (_previousAction == null ||
                _previousAction.Functions == null ||
                _previousAction.Functions.Length < 1)
            { return functions; }

            return functions.Union(
                _previousAction.Functions, 
                (t1, t2) => 
                    t1.Method.Name == t2.Method.Name);
        }

        private static IEnumerable<FunctionInfo> Extract(IEnumerable<MethodInfo> methods, bool isFullyDecorated)
        {
            foreach (var method in methods)
            {
                var attrs =
                    method.GetCustomAttributes(true).Which(e => e is FunctionAttribute);

                if (!isFullyDecorated)
                {
                    yield return new FunctionInfo
                    {
                        Method = method,
                        EventName = GetMethodName(isFullyDecorated, method)
                    };
                }
                else
                {
                    foreach (var attr in attrs)
                    {
                        yield return new FunctionInfo
                        {
                            Method = method,
                            EventName = GetMethodName(isFullyDecorated, method, (FunctionAttribute)attr)
                        };
                    }
                }
            }
        }

        private static string GetMethodName(bool hasAttr, MethodInfo method, FunctionAttribute actionAttr = null)
        {            
            return hasAttr ?
                (string.IsNullOrEmpty(actionAttr.EventName) ? method.Name : actionAttr.EventName) :
                method.Name;
        }

        protected override Type[] GetWhoShouldBeListened()
        {
            var attr = (ActionAttribute)
                Type.GetCustomAttributes(true)
                .Foremost(a => a is ActionAttribute);

            return attr.ListensTo;
        }
    }
}