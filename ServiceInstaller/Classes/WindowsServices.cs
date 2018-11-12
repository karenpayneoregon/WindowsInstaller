using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;

namespace ServiceInstaller.Classes
{
    public class WindowsServices
    {
        /// <summary>
        /// Stop a Windows service service name
        /// </summary>
        /// <param name="pServiceName"></param>
        /// <remarks>
        /// A service does not stop instantly, so WaitForStatus method
        /// is used to 'wait' until the service has stopped. If the
        /// caller becomes unresponsive then there may be issues with
        /// the service stopping outside of code.
        /// </remarks>
        public void StopService(string pServiceName)
        {
            var sc = ServiceController.GetServices().FirstOrDefault(service => service.ServiceName == pServiceName);
            if (sc == null)
                return;

            if (sc.Status == ServiceControllerStatus.Running)
            {
                try
                {
                    sc.Stop();
                    sc.WaitForStatus(ServiceControllerStatus.Stopped);
                }
                catch (InvalidOperationException)
                {
                    // here for debug purposes
                }
            }
        }
        /// <summary>
        /// Start a Windows service by service name
        /// </summary>
        /// <param name="pServiceName"></param>
        public void StartService(string pServiceName)
        {
            var sc = ServiceController.GetServices().FirstOrDefault(service => service.ServiceName == pServiceName);
            if (sc == null)
                return;

            sc.ServiceName = pServiceName;
            if (sc.Status == ServiceControllerStatus.Stopped)
            {
                try
                {
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running);
                }
                catch (InvalidOperationException)
                {
                    // here for debug purposes
                }
            }
        }
        /// <summary>
        /// Determine if service is currently installed
        /// </summary>
        /// <param name="pServiceName"></param>
        /// <returns></returns>
        public bool IsInstalled(string pServiceName)
        {
            var sc = ServiceController.GetServices().FirstOrDefault(service => service.ServiceName == pServiceName);
            return (sc != null);
        }
        /// <summary>
        /// Get basic information on running services
        /// </summary>
        /// <returns></returns>
        public List<ServiceDetails> ServiceNames()
        {
            var detailList = new List<ServiceDetails>();
            var services = ServiceController.GetServices().OrderBy(x => x.DisplayName).ToList();
            foreach (var item in services)
            {
                detailList.Add(new ServiceDetails() { DisplayName = item.DisplayName, ServiceName = item.ServiceName, Status = item.Status });               
            }

            return detailList;
        }
        /// <summary>
        /// provides the service status by string
        /// </summary>
        /// <param name="pServiceName"></param>
        /// <returns></returns>
        /// <remarks>
        /// Example usage, set the text of a text box
        /// in a form statusbar.
        /// https://www.dotnetheaven.com/article/printer-settings-in-gdi-and-vb.net
        /// </remarks>
        public string Status(string pServiceName) 
        {
            var status = "Not installed";

            // Get our service, if not found in GetServices then it's not installed
            var sc = ServiceController.GetServices().FirstOrDefault(service => service.ServiceName == pServiceName);
            if (sc == null)
                return status;

            switch (sc.Status)
            {
                case ServiceControllerStatus.Running:
                    status = "Running";
                    break;
                case ServiceControllerStatus.Stopped:
                    status = "Stopped";
                    break;
                case ServiceControllerStatus.Paused:
                    status = "Paused";
                    break;
                case ServiceControllerStatus.StopPending:
                    status = "Stopping";
                    break;
                case ServiceControllerStatus.StartPending:
                    status = "Starting";
                    break;
                default:
                    status = "Status Changing";
                    break;
            }
            return status;
        }
    }
}
