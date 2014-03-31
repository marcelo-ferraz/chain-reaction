using ChainReaction.Origins;

namespace ChainReaction.Configuration
{
    public class WithExpression
    {
        public IChainReactionContainer Container { get; set; }
        
        public WithExpression With(IOrigin source)
        {
            source.Save(Container);

            return this;
        }
    }
}
