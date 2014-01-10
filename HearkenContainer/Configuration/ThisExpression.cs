using HearkenContainer.Origins;

namespace HearkenContainer.Configuration
{
    public class ThisExpression
    {
        public IHearkenContainer Container { get; set; }
        
        public ThisExpression Source(IOrigin source)
        {
            source.Save(Container);

            return this;
        }
    }
}
