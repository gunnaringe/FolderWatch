using System;
using System.Configuration;

namespace FolderWatch
{
    public class FtpElement : AbstractSourceConfig
    {
        private const string IsEncryptedLabel = "isEncrypted";
        private const string PasswordLabel = "password";
        private const string UserNameLabel = "username";

        [ConfigurationProperty("hostname", IsRequired = true)]
        //[RegexStringValidator(@"https?\://\S+")]
        public string Hostname
        {
            get { return (string)this["hostname"]; }
            set { this["hostname"] = value; }
        }

        [ConfigurationProperty(UserNameLabel, IsRequired = true)]
        public string UserName
        {
            get { return (string)this[UserNameLabel]; }
            set { this[UserNameLabel] = value; }
        }

        [ConfigurationProperty(PasswordLabel, IsRequired = true)]
        public string Password
        {
            get { return (string)this[PasswordLabel]; }
            set { this[PasswordLabel] = value; }
        }


        [ConfigurationProperty(IsEncryptedLabel, IsRequired = false, DefaultValue = false)]
        public bool IsEncrypted
        {
            get { return (bool)this[IsEncryptedLabel]; }
            set { this[IsEncryptedLabel] = value; }
        }

        public override string ToString()
        {
            return String.Format("{0} Hostname: {1}", base.ToString(), Hostname);
        }
    }


    [ConfigurationCollection(typeof(FtpElement))]
    public class FtpElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FtpElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FtpElement)element).Name;
        }
    }
}