namespace HearkenContainer.Configuration
{
    public static class Configure
    {
        public static ThisExpression This()
        {
            var instance = new ThisExpression();
            instance.Container = new SimpleDispatchContainer();
            return instance;
        }

        public static ThisExpression This(IHearkenContainer container)
        {
            var instance = new ThisExpression();
            instance.Container = container;
            return instance;
        }
    }
}