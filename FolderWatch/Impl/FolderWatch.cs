using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FolderWatch.Impl;
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


            // Get list of all hosts
            var hosts = new Dictionary<String, Host> { {"local", new LocalHost()} };

            foreach (FtpElement ftp in folderWatchSection.Sources.Ftps)
            {
                hosts[ftp.Name] = new FtpHost(ftp);
            }

            foreach (OtherElement e in folderWatchSection.Sources.Others)
            {
                hosts[e.Name] = new OtherHost(e);
            }
            
            foreach (string host in hosts.Keys)
            {
                Log.DebugFormat("Host name: {0}", host);
            }

            var flows = new List<Flow>();
            foreach (FlowElement flowSetting in folderWatchSection.Flows)
            {
                Flow flow = new Flow(hosts, flowSetting);
                flows.Add(flow);
            }

            timer = new Timer(Callback, flows, Timeout.Infinite, Timeout.Infinite);
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

        private void Callback(object flowsObj)
        {
            var flows = flowsObj as List<Flow>;
            if (flows == null)
            {
                Log.Error("Settings not set");
                return;
            }

            Log.Info("Callback invoked");

            foreach (Flow flow in flows)
            {
                Log.InfoFormat("Flow: {0}", flow);
                flow.Run();
            }

            Thread.Sleep(4000);
            timer.Change(1000, Timeout.Infinite);
        }
    }
}
