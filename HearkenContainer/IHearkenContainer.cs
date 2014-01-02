using System;
using HearkenContainer.Model;
namespace HearkenContainer
{
    public interface IHearkenContainer
    {
        bool TryResolve(string groupName, out GroupInfo group);

        void Add(GroupInfo group);

        T Get<T>(string groupName = "", Action<int, object> afterCreateAction = null);
    }
}