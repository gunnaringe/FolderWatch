using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace FolderWatch.Impl
{
    class Flow
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string _name;
        private readonly Host _sourceHost;
        private readonly Host _targetHost;
        private string _sourceFolder;
        private string _targetFolder;

        public Flow(Dictionary<string, Host> hosts, FlowElement flowSetting)
        {
            _name = flowSetting.Name;
            _sourceHost = hosts[flowSetting.SourceName];
            _targetHost = hosts[flowSetting.TargetName];
            _sourceFolder = flowSetting.SourceFolder;
            _targetFolder = flowSetting.TargetFolder;
        }

        public override string ToString()
        {
            return string.Format("Name: {0} Source: {1} Target: {2}", _name, _sourceHost.Name, _targetHost.Name);
        }

        public void Run()
        {
            int numberOfFiles;
            _sourceHost.Download(_sourceFolder, _targetFolder, out numberOfFiles);
            Log.InfoFormat("Number of files: {0}", numberOfFiles);
           
        }
    }
}
