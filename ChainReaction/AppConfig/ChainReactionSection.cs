using System.Configuration;

namespace ChainReaction.AppConfig
{
    internal class ChainReactionSection : ConfigurationSection
    {
        /// <summary>
        /// Returns the instance of the container's configuration
        /// </summary>
        public static ChainReactionSection Self
        {
            get
            {
                return (ChainReactionSection)ConfigurationManager.GetSection("chainReaction");
            }
        }

        /// <summary>
        /// The groups, which holds the relation of events and actions and their visibility among themselves
        /// </summary>
        [ConfigurationProperty(CollectionOf.Groups.NodeName)]
        public CollectionOf.Groups Groups
        {
            get { return ((CollectionOf.Groups)(base[CollectionOf.Groups.NodeName])); }
        }
    }
}
