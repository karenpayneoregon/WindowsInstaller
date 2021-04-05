using System.ServiceProcess;

namespace ServiceInstaller.Classes
{
    public class ServiceDetails
    {
        public string DisplayName { get; set; }
        public string ServiceName { get; set; }
        public ServiceControllerStatus Status { get; set; }
        public override string ToString() => DisplayName;

    }
}
