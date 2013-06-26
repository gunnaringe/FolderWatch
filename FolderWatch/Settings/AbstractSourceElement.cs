using System;
using System.Configuration;

namespace FolderWatch
{
    public class AbstractSourceConfig : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string) this["name"]; }
            set { this["name"] = value; }
        }

        public override string ToString()
        {
            return String.Format("Name: {0}", Name);
        }
    }
}