using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        public FolderWatch(Configuration config)
        {
            var folderWatchSection = config.GetSection("folderwatch") as FolderWatchSection;
            if (folderWatchSection == null)
            {
                Log.Error("No config set");
                return;
            }

            var sourcesSection = folderWatchSection.Sources;
            if (sourcesSection == null)
            {
                Log.Info("Null");
                return;
            }

            foreach (FeedElement s in sourcesSection.Feeds)
            {
                Log.Info(s);
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
