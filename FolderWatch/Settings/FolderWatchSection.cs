using System;
using System.Configuration;

namespace FolderWatch
{
    public class FolderWatchSection : ConfigurationSection
    {
        private const string HostsLabel = "hosts";
        private const string SettingsLabel = "settings";
        private const string FlowLabel = "flows";

        [ConfigurationProperty(SettingsLabel)]
        public NameValueConfigurationCollection Settings
        {
            get { return this[SettingsLabel] as NameValueConfigurationCollection; }
            set { this[SettingsLabel] = value; }
        }

        [ConfigurationProperty(HostsLabel)]
        public SourcesConfig Sources
        {
            get { return (SourcesConfig) this[HostsLabel]; }
            set { this[HostsLabel] = value; }
        }

        [ConfigurationProperty(FlowLabel, IsDefaultCollection = true)]
        public FlowElementCollection Flows
        {
            get { return (FlowElementCollection) this[FlowLabel]; }
            set { this[FlowLabel] = value; }
        }
    }
}