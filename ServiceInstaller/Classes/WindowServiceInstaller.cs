using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ServiceInstaller.Classes
{
    /// <summary>
    /// A generic window service installer for install or uninstalling.
    /// </summary>
    public class Serviceinstaller
    {
        /// <summary>
        /// Location of .NET Framework main folder
        /// </summary>
        public string FrameWorkDirectory => RuntimeEnvironment.GetRuntimeDirectory();

        /// <summary>
        /// Windows Service installer executable
        /// </summary>
        public string InstallerCommand => Path.Combine(FrameWorkDirectory, "InstallUtil.exe");

        private bool _CommandExeExists;
        public bool CommandExecutableExists { get { return _CommandExeExists; } }
        public string ServiceFolder { get; set; }
        /// <summary>
        /// Executable name of the ACED Notification Service { get; set; }
        /// </summary>
        public string ServiceExecutableName { get; set; }
        public bool ServiceExecutableExists { get; set; }
        public bool ProceedWithOperations { get; set; }
        /// <summary>
        /// This is the Service name in the ProjectInstaller class of the window service
        /// we are working with, in this case database Service.
        /// 
        /// Setup and determine if all is in place e.g.found the service executable, service installer executable
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Used to install a service
        /// </summary>
        /// <param name="pExecutableName">Service executable name with extension</param>
        /// <param name="pServiceKnownName">Name of the Windows service</param>
        /// <param name="pServiceProjectFolder">Visual Studio project folder</param>
        public Serviceinstaller(string pExecutableName, string pServiceKnownName, string pServiceProjectFolder)
        {
            ServiceExecutableName = pExecutableName;
            ServiceName = pServiceKnownName;

            _CommandExeExists = File.Exists(InstallerCommand);

            // assumes building for debug not release
            ServiceFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.UpperFolder(4), pServiceProjectFolder, "bin", "Debug");

            ServiceExecutableExists = File.Exists(Path.Combine(ServiceFolder, ServiceExecutableName));

            ProceedWithOperations = CommandExecutableExists && ServiceExecutableExists;
        }
        /// <summary>
        /// Used to uninstall a service.
        /// </summary>
        public void UninstallService()
        {

            var startInfo = new ProcessStartInfo(InstallerCommand) {WindowStyle = ProcessWindowStyle.Normal};

            var ops = new WindowsServices();
            var statusStates = new[] { "Running", "Stopped" };
            if (statusStates.Contains(ops.Status(ServiceName)))
            {
                startInfo.Arguments = $"/u {Path.Combine(ServiceFolder, ServiceExecutableName)}";
                var p = Process.Start(startInfo);
                p.WaitForExit();

                if (ops.Status(ServiceName) == "Not installed")
                {
                    MessageBox.Show("Service has been uninstalled");
                }
            }
            else
            {
                
            }
        }
        /// <summary>
        /// Start our service.
        /// First check to see if the service is running as attempting
        /// to install while running leads to doom and gloom :-)
        /// Install after the above check
        /// </summary>
        public void InstallService()
        {
            var startInfo = new ProcessStartInfo(InstallerCommand) {WindowStyle = ProcessWindowStyle.Normal};

            var ops = new WindowsServices();
            if (ops.Status(ServiceName) == "Running")
            {
                startInfo.Arguments = $"/u {Path.Combine(ServiceFolder, ServiceExecutableName)}";
                Process.Start(startInfo);
            }

            startInfo.Arguments = $"/i {Path.Combine(ServiceFolder, ServiceExecutableName)}";
            Process.Start(startInfo);
        }
    }
}
