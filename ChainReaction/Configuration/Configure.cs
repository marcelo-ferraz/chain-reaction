namespace ChainReaction.Configuration
{
    public static class Configure
    {
        public static ThisExpression This()
        {
            var instance = new ThisExpression();
            instance.Container = new SimpleChainReactionContainer();
            return instance;
        }

        public static ThisExpression This(IChainReactionContainer container)
        {
            var instance = new ThisExpression();
            instance.Container = container;
            return instance;
        }
    }
}