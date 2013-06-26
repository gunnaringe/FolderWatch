using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace FolderWatch
{
    public class WindowsService : ServiceBase
    {
        public const string ServiceNameStr = "FolderWatch";

        public WindowsService()
        {
            this.ServiceName = ServiceNameStr;
            this.EventLog.Log = "Application";
            
            // These Flags set whether or not to handle that specific
            //  type of event. Set to true if you need it, false otherwise.
            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;        
            }
        


        private static void Main(string[] args)
        {
            WindowsService service = new WindowsService();

            if (Environment.UserInteractive)
            {
                service.OnStart(args);
                Console.WriteLine("Press any key to stop program");
                Console.Read();
                service.OnStop();
            }
            else
            {
                ServiceBase.Run(service);
            }

        }

        protected override void OnStart(string[] args)
        {
            Console.WriteLine("Started");
        }

        protected override void OnStop()
        {
            Console.WriteLine("Stopped");
        }

        //public void Run()
        //    {
        //        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //        //var feedRetrieverSection = (FeedRetrieverSection) ConfigurationManager.GetSection("feedRetriever");
        //        ConfigurationSection feedRetrieverSection = config.GetSection("feedRetriever") as FeedRetrieverSection;

        //        if (feedRetrieverSection == null)
        //        {
        //            Console.WriteLine("Null");
        //            Console.ReadLine();
        //            return;
        //        }

        //        foreach (FeedElement s in feedRetrieverSection.Feeds)
        //        {
        //            Console.WriteLine(s.Name);
        //        }
        //        Console.ReadLine();
        //    }
        //}
    }
}