using System;
using System.Configuration;

namespace FolderWatch
{
    public class FolderWatchSection : ConfigurationSection
    {
        [ConfigurationProperty("settings")]
        public NameValueConfigurationCollection Settings
        {
            get { return (NameValueConfigurationCollection) this["settings"]; }
            set { this["settings"] = value; }
        }

        [ConfigurationProperty("sources")]
        public SourcesConfig Sources
        {
            get { return (SourcesConfig) this["sources"]; }
            set { this["sources"] = value; }
        }
    }
}