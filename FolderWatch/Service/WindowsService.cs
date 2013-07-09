using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using FolderWatch.Settings;
using log4net;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace FolderWatch
{
    public class WindowsService : ServiceBase
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        internal const string ServiceNameStr = "FolderWatch";
        internal const string DisplayName = "FolderWatch";
        internal const string Description = "Monitor an folder and copy to local folder";

        private readonly FolderWatch _folderWatch;

        private WindowsService(Configuration config)
        {
            Log.Info("New instance started");

            ServiceName = ServiceNameStr;
            EventLog.Log = "Application";

            CanHandlePowerEvent = false;
            CanHandleSessionChangeEvent = false;
            CanPauseAndContinue = false;
            CanShutdown = false;
            CanStop = true;

            _folderWatch = new FolderWatch(config);
        }

        public static void Main(string[] args)
        {
            Log.Debug("Main called");
            Configuration config = ConfigurationManager.OpenExeConfiguration("FolderWatch.exe");

            var folderWatchSection = config.GetSection("folderwatch") as FolderWatchSection;
            if (folderWatchSection == null)
            {
                Log.Error("No configuration has been set");
                return;
            }

            WindowsService service = new WindowsService(config);

            if (Environment.UserInteractive)
            {
                // Run as command line program
                service.OnStart(args);
                Console.WriteLine("Press any key to stop program");
                Console.Read();
                service.OnStop();
                Console.Read();
            }
            else
            {
                // Run as service
                Run(service);
            }

        }

        protected override void OnStart(string[] args)
        {
            Log.Info("Starting service");
            _folderWatch.Start();
        }

        protected override void OnStop()
        {
            Log.Info("Stopping service");
            _folderWatch.Stop();
        }
    }
}