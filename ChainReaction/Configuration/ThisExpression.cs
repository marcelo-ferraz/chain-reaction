using ChainReaction.Origins;

namespace ChainReaction.Configuration
{
    public class ThisExpression
    {
        public IChainReactionContainer Container { get; set; }
        
        public ThisExpression Source(IOrigin source)
        {
            source.Save(Container);

            return this;
        }
    }
}
