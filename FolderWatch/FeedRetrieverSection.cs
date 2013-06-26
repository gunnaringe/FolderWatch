using System.Configuration;

namespace FolderWatch
{
    public class FeedRetrieverSection : ConfigurationSection
    {
        [ConfigurationProperty("feeds", IsDefaultCollection = true)]
        public FeedElementCollection Feeds
        {
            get { return (FeedElementCollection)this["feeds"]; }
            set { this["feeds"] = value; }
        }
    }
}