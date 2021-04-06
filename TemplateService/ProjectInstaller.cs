using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace TemplateService
{
    /// <summary>
    /// 
    /// </summary>
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }


        private void ProjectInstaller_AfterInstall(object sender, InstallEventArgs e)
        {

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
    }
}
