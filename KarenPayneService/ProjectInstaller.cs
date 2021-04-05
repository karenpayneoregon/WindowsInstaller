using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace KarenPayneService
{
    /// <summary>
    /// 
    /// </summary>
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        private ServiceInstaller serviceInstaller;
        private ServiceProcessInstaller serviceProcessInstaller;
        
        /// <summary>
        /// 
        /// </summary>
        public ProjectInstaller()
        {
            InitializeComponent();
            
            //serviceInstaller = new ServiceInstaller
            //{
            //    StartType = ServiceStartMode.Automatic, 
            //    ServiceName = "KarenPayneService", 
            //    DisplayName = "DatabaseService", 
            //    Description = "Example service by Karen Payne"
            //};
            
            //serviceInstaller.StartType = ServiceStartMode.Automatic;
            //Installers.Add(serviceInstaller);

            //serviceProcessInstaller = new ServiceProcessInstaller {Account = ServiceAccount.User};
            //Installers.Add(serviceProcessInstaller);
            
            //AfterInstall += ProjectInstaller_AfterInstall;
            //BeforeInstall += OnBeforeInstall;
        }

        private void OnBeforeInstall(object sender, InstallEventArgs e)
        {
            
        }
 
        private void ProjectInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            //var service = new ServiceController(serviceInstaller1.ServiceName);
            //if (service.Status != ServiceControllerStatus.Running)
            //{
            //    service.Start();
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetContextParameter(string key)
        {
            string value = "";
            try
            {
                value = Context.Parameters[key];
            }
            catch
            {
                value = "";
            }
            
            return value;
        }



        //protected override void OnBeforeInstall(IDictionary savedState)
        //{
        //    base.OnBeforeInstall(savedState);

        //    string username = "OED/paynek";// GetContextParameter("user").Trim();
        //    string password = ""; // GetContextParameter("password").Trim();

        //    if (username != "")
        //    {
        //        serviceProcessInstaller.Username = username;
        //    }

        //    if (password != "")
        //    {
        //        serviceProcessInstaller.Password = password;
        //    }
        //}
    }
}
