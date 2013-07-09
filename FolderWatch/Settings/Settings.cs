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
        private const string IsEncryptedLabel = "isEncrypted";
        private const string PasswordLabel = "password";

        [ConfigurationProperty("hostname", IsRequired = true)]
        //[RegexStringValidator(@"https?\://\S+")]
        public string Hostname
        {
            get { return (string)this["hostname"]; }
            set { this["hostname"] = value; }
        }

        [ConfigurationProperty(PasswordLabel, IsRequired = true)]
        public string Password
        {
            get { return (string) this[PasswordLabel]; }
            set { this[PasswordLabel] = value; }
        }

        [ConfigurationProperty(IsEncryptedLabel, IsRequired = false, DefaultValue = false)]
        public bool IsEncrypted
        {
            get { return (bool) this[IsEncryptedLabel]; }
            set { this[IsEncryptedLabel] = value;  }
        }

        public override string ToString()
        {
            return String.Format("{0} Hostname: {1} Password: {2}", base.ToString(), Hostname, Password);
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
