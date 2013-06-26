using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace FolderWatch
{
    [RunInstaller(true)]
    public class WindowsServiceInstaller : Installer
    {
        /// <summary>
        /// Public Constructor for WindowsServiceInstaller.
        /// - Put all of your Initialization code here.
        /// </summary>
        public WindowsServiceInstaller()
        {
            ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();
            ServiceInstaller serviceInstaller = new ServiceInstaller();

            //# Service Account Information
            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;

            //# Service Information
            serviceInstaller.DisplayName = WindowsService.DisplayName;
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.Description = WindowsService.Description;

            // Must be equal to value in WindowsService.cs
            serviceInstaller.ServiceName = WindowsService.ServiceNameStr;

            this.Installers.Add(serviceProcessInstaller);
            this.Installers.Add(serviceInstaller);
        }
    }
}