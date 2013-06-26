using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderWatch
{
    class FolderWatch
    {
        public FolderWatch()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            //var feedRetrieverSection = ConfigurationManager.GetSection("feedRetriever") as FeedRetrieverSection;

            //if (feedRetrieverSection == null)
            //{
            //    Console.WriteLine("Null");
            //    Console.ReadLine();
            //    return;
            //}

            //foreach (FeedElement s in feedRetrieverSection.Feeds)
            //{
            //    Console.WriteLine(s.Name);
            //}
            //Console.ReadLine();
        }

        public void Start()
        {
            Console.WriteLine("Start");
        }

        public void Stop()
        {
            Console.WriteLine("Stop");
        }
    }
}
