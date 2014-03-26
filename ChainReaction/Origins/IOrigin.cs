using System.Collections.Generic;
using ChainReaction.Model;

namespace ChainReaction.Origins
{
    public interface IOrigin
    {
        void Save(IChainReactionContainer container);
    }
}
