using System;
using HearkenContainer.Model;
namespace HearkenContainer
{
    public interface IHearkenContainer
    {
        bool TryResolve(string groupName, out GroupInfo group);

        void Add(GroupInfo group);

        T Invoke<T>(Action<object> afterLoadListener = null, string groupName = "");
    }
}