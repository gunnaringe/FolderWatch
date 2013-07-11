using System.Configuration;

namespace FolderWatch
{
    public class SourcesConfig : ConfigurationElement
    {
        private const string FTPLabel = "ftp";
        private const string OtherLabel = "other";

        [ConfigurationProperty(FTPLabel, IsDefaultCollection = false)]
        public FtpElementCollection Ftps
        {
            get { return (FtpElementCollection)this[FTPLabel]; }
            set { this[FTPLabel] = value; }
        }

        [ConfigurationProperty(OtherLabel, IsDefaultCollection = false)]
        public OtherElementCollection Others
        {
            get { return (OtherElementCollection) this[OtherLabel]; }
            set { this[OtherLabel] = value; }
        }
    }
}