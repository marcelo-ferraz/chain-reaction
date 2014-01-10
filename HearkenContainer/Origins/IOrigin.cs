using System.Collections.Generic;
using HearkenContainer.Model;

namespace HearkenContainer.Origins
{
    public interface IOrigin
    {
        void Save(IHearkenContainer container);
    }
}
