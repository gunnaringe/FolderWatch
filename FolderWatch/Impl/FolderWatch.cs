using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FolderWatch.Settings;
using Starksoft.Net.Ftp;
using log4net;
using log4net.Repository.Hierarchy;

namespace FolderWatch
{
    internal class FolderWatch
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Timer timer;
        private bool isStarted;

        private bool GetFlag(FolderWatchSection section, string key)
        {
            string value = section.Settings[key].Value;
            bool result;
            return Boolean.TryParse(value, out result) && result;
        }

        public FolderWatch(Configuration config)
        {
            var folderWatchSection = config.GetSection("folderwatch") as FolderWatchSection;
            if (folderWatchSection == null)
            {
                Log.Error("No config set");
                return;
            }

           if (GetFlag(folderWatchSection, "encrypt.section"))
           {
               Log.Debug("Section encryption is enabled");
               StringEncryption.EncryptConfigSection(config, folderWatchSection);     
           }

            if (GetFlag(folderWatchSection, "encrypt.passwords"))
            {
                Log.Debug("Password encryption is enabled");
                StringEncryption.EncryptPassword(config, folderWatchSection);
            }

            timer = new Timer(Callback, timer, Timeout.Infinite, Timeout.Infinite);
        }

        public void Start()
        {
            if (!isStarted)
            {
                lock (this)
                {
                    if (!isStarted)
                    {
                        isStarted = true;
                        timer.Change(1000, Timeout.Infinite);
                    }
                }
            }
        }

        public void Stop()
        {
            lock (this)
            {
                isStarted = false;
            }
        }

        private void Callback(object state)
        {
            Log.Info("Callback invoked");
            //FtpClient ftp = new FtpClient
            //                    {
            //                        Host = @"ftp.mozilla.org/pub/mozilla.org/",
            //Port = 22,
            //                    };
            //ftp.Open("anonymous", "gunnaringe@gmail.com");
            //Log.WarnFormat("Num files: {0}", ftp.GetDirList().Count);

            Thread.Sleep(4000);
            timer.Change(1000, Timeout.Infinite);
        }
    }
}
