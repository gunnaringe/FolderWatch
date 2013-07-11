using System;
using System.Configuration;

namespace FolderWatch
{
    public class OtherElement : AbstractSourceConfig
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


    [ConfigurationCollection(typeof(FtpElement))]
    public class OtherElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new OtherElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((OtherElement)element).Name;
        }
    }
}