using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderWatch
{
    public class FeedElement : AbstractSourceConfig
    {
        [ConfigurationProperty("hostname", IsRequired = true)]
        //[RegexStringValidator(@"https?\://\S+")]
        public string Hostname
        {
            get { return (string)this["hostname"]; }
            set { this["hostname"] = value; }
        }

        public override string ToString()
        {
            return String.Format("{0} Hostname: {1}", base.ToString(), Hostname);
        }
    } 

    [ConfigurationCollection(typeof(FeedElement))]
    public class FeedElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FeedElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FeedElement)element).Name;
        }
    }
}
