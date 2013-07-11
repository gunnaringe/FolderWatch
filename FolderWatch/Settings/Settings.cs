using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderWatch
{

    [ConfigurationCollection(typeof(FlowElement))]
    public class FlowElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FlowElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FlowElement)element).Name;
        }
    }

    public class FlowElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("sourceName", IsRequired = true)]
        public string SourceName
        {
            get { return (string) this["sourceName"]; }
            set { this["sourceName"] = value; }
        }

        [ConfigurationProperty("targetName", IsRequired = true)]
        public string TargetName
        {
            get { return (string) this["targetName"]; }
            set { this["targetName"] = value; }
        }

        [ConfigurationProperty("sourceFolder", IsRequired = true)]
        public string SourceFolder
        {
            get { return (string) this["sourceFolder"]; }
            set { this["sourceFolder"] = value; }
        }

        [ConfigurationProperty("targetFolder", IsRequired = true)]
        public string TargetFolder
        {
            get { return (string) this["targetFolder"]; }
            set { this["targetFolder"] = value; }
        }

        public override string ToString()
        {
            return String.Format("Name: {0} Source: {1}:{2} Target{3}:{4}", Name, SourceName, SourceFolder, TargetName, TargetFolder);
        }
    }
}
