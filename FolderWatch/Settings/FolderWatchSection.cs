using System;
using System.Configuration;

namespace FolderWatch
{
    public class FolderWatchSection : ConfigurationSection
    {
        [ConfigurationProperty("sources")]
        public SourcesConfig Sources
        {
            get { return (SourcesConfig) this["sources"]; }
            set { this["sources"] = value; }
        }
    }
}