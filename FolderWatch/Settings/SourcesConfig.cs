using System.Configuration;

namespace FolderWatch
{
    public class SourcesConfig : ConfigurationElement
    {
        private const string FTPLabel = "ftp";

        [ConfigurationProperty(FTPLabel, IsDefaultCollection = true)]
        public FeedElementCollection Feeds
        {
            get { return (FeedElementCollection)this[FTPLabel]; }
            set { this[FTPLabel] = value; }
        }
    }
}