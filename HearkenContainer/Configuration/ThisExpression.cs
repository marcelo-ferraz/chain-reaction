using HearkenContainer.Sources;

namespace HearkenContainer.Configuration
{
    public class ThisExpression
    {
        public IHearkenContainer Container { get; set; }
        
        public ThisExpression Source(ISource source)
        {
            source.Save(Container);

            return this;
        }
    }
}
