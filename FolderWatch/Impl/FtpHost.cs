using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using FolderWatch.Settings;
using Starksoft.Net.Ftp;
using log4net;

namespace FolderWatch.Impl
{
    class LocalHost : Host
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string _name = "local";

        public LocalHost()
        {
        }

        public override string Name
        {
            get { return _name; }
        }

        public override void Download(string sourceFolder, string targetFolder, out int numberOfFiles)
        {
            numberOfFiles = 0;
            Log.InfoFormat("Download called for LocalHost (Name: {0})", _name);
        }
    }

    class FtpHost : Host
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string _name;
        private string _hostname;
        private SecureString _password;
        private int _port;
        private string _username;

        public FtpHost(FtpElement hostSettings)
        {
            _name = hostSettings.Name;
            _hostname = hostSettings.Hostname;
            _username = hostSettings.UserName;
            _password = hostSettings.IsEncrypted ? StringEncryption.DecryptString(hostSettings.Password) : StringEncryption.ToSecureString(hostSettings.Password);
            _port = 21;
        }

        public override string Name
        {
            get { return _name; }
        }

        public override void Download(string sourceFolder, string targetFolder, out int numberOfFiles)
        {
            Log.DebugFormat("Download called for FtpHost (Name: {0})", _name);
            using (var c = new FtpClient(_hostname, _port, FtpSecurityProtocol.None))
            {
                c.AlwaysAcceptServerCertificate = true;
                c.Open(_username, StringEncryption.ToInsecureString(_password));
                c.ChangeDirectory(sourceFolder);

                var files = c.GetDirList().Where(ftpItem => ftpItem.ItemType == FtpItemType.File).ToList();
                Log.DebugFormat("Current directory: {0}", c.CurrentDirectory);
                foreach (var file in files)
                {
                    Log.DebugFormat("[File] Size: {1} Name: {0}", file.Name, file.Size);
                }

                foreach (var file in files)
                {
                    c.GetFile(file.FullPath, targetFolder + @"\" + file.Name, FileAction.Create);
                }

                numberOfFiles = files.Count;
            }
        }
    }

    class OtherHost : Host
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string _name;

        public OtherHost(OtherElement hostSettings)
        {
            _name = hostSettings.Name;
        }

        public override string Name {
            get { return _name; } 
        }

        public override void Download(string sourceFolder, string targetFolder, out int numberOfFiles)
        {
            numberOfFiles = 0;
            Log.InfoFormat("Download called for OtherHost (Name: {0})", _name);
        }
    }

    abstract class Host
    {
        abstract public string Name { get; }

        abstract public void Download(string sourceFolder, string targetFolder, out int numberOfFiles);
    }
}
