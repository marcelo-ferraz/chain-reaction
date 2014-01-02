using System.Collections.Generic;
using HearkenContainer.Model;

namespace HearkenContainer.Sources
{
    public interface ISource
    {
        void Save(IHearkenContainer container);
    }
}
