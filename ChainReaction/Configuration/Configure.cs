using System;
namespace ChainReaction.Configuration
{
    public static class Configure
    {
        public static WithExpression This()
        {
            var cfgInstance = new WithExpression();
            cfgInstance.Container = new SimpleChainReactionContainer();
            return cfgInstance;
        }

        public static WithExpression This(IChainReactionContainer container)
        {
            var cfgInstance = new WithExpression();
            cfgInstance.Container = container;
            return cfgInstance;
        }

        public static WithExpression This<T>()
            where T : IChainReactionContainer 
        {
            var cfgInstance = new WithExpression();
            
            cfgInstance.Container = 
                Activator.CreateInstance<T>();
            
            return cfgInstance;
        }
    }
}