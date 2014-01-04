using System.Configuration;

namespace HearkenContainer.AppConfig
{
    internal class HearkenContainerSection : ConfigurationSection
    {
        public static HearkenContainerSection Self
        {
            get
            {
                return (HearkenContainerSection)ConfigurationManager.GetSection("hearkenContainer");
            }
        }

        [ConfigurationProperty(CollectionOf.Groups.NodeName)]
        public CollectionOf.Groups Items
        {
            get { return ((CollectionOf.Groups)(base[CollectionOf.Groups.NodeName])); }
        }
    }
}
