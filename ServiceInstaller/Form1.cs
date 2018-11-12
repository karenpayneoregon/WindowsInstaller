using System;
using System.Linq;
using System.Windows.Forms;
using static ServiceInstaller.Classes.WindowsFormsDialogs;
using System.Configuration;
using ServiceInstaller.Classes;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ServiceInstaller
{
    public partial class MainForm : Form
    {
        Serviceinstaller servicesOperations;
        WindowsServices utilityOperations;

        public MainForm()
        {
            InitializeComponent();
            Shown += MainForm_Shown;
            Closing += MainForm_Closing;
            Text = $"Service: {ConfigurationManager.AppSettings["ServiceKnownName"].SplitCamelCase()}";
        }

        private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Determine if it's safe to run as in,
        /// is the service executable physically located
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Shown(object sender, EventArgs e)
        {
            
            if (Debugger.IsAttached)
            {
                startServiceButton.Enabled = false;
                chkStart.Enabled = false;
                MessageBox.Show("Start service disabled for running under the debugger\nStart this utility from Windows Explorer or create a shortcut in Visual Studio's IDE");
            }

            if (installServiceButton.Enabled == false)
            {
                ExceptionDialog("Incountered issues with finding the windows service");
            }
            else
            {
                timer1.Enabled = true;
            }           
        }
        /// <summary>
        /// Initialize service classes for working with our windows service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainForm_Load(object sender, EventArgs e)
        {

            servicesOperations = new Serviceinstaller(
                ConfigurationManager.AppSettings["ExecutableName"], 
                ConfigurationManager.AppSettings["ServiceKnownName"],
                ConfigurationManager.AppSettings["ServiceProjectFolder"]);


            var serviceName = ConfigurationManager.AppSettings["ServiceKnownName"] ?? throw new ArgumentNullException("ServiceKnownName not set");
            this.Text = $"{serviceName}: utility";
                      
            // create a manual uninstall batch file
            if (Environment.UserName != "Karens")
            {
                DirectoryExtensions.CreateManualUninstallBatchFile(
                    servicesOperations.ServiceFolder, 
                    servicesOperations.ServiceExecutableName);
            }

            utilityOperations =  new WindowsServices();

            if (servicesOperations.ProceedWithOperations == false)
            {
                // disable all buttons except for the close button as
                // we can not continue because either the install executable or
                // the service executable were not found.
                 Controls.OfType<Button>()
                    .Where(button => button.Name != "applicationCloseButton")
                    .ToList()
                    .ForEach(button => button.Enabled = false);              
            }
        }
        /// <summary>
        /// Install the service, prior to the install,
        /// if it's current status is "Running" then attempt to
        /// uninstall.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void installServiceButton_Click(object sender, EventArgs e)
        {
            if (utilityOperations.IsInstalled(servicesOperations.ServiceName))
            {
                ExceptionDialog($"{servicesOperations.ServiceName} currently installed, Use uninstall first.");
            }
            else
            {

                servicesOperations.InstallService();

                // wait for service status of Not installed then fire up the service
                if (chkStart.Checked)
                {                   
                    while (utilityOperations.Status(servicesOperations.ServiceName) == "Not installed")
                    {
                        await Task.Delay(25).ConfigureAwait(false);
                    }

                    utilityOperations.StartService(servicesOperations.ServiceName);

                }
            }
        }
        /// <summary>
        /// Uninstall service if presently installed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uninstallServiceButton_Click(object sender, EventArgs e)
        {
            if (utilityOperations.IsInstalled(servicesOperations.ServiceName))
            {
                if (Question("Uninstall service?"))
                {
                    servicesOperations.UninstallService();
                }               
            }
            else
            {
                ExceptionDialog($"{servicesOperations.ServiceName} currently not installed, aboring installation.");
            }          
        }
        /// <summary>
        /// For updating service status in statusbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            txtServiceStatus.Text =  utilityOperations.Status(servicesOperations.ServiceName);
        }
        /// <summary>
        /// Start the service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startServiceButton_Click(object sender, EventArgs e)
        {
            utilityOperations.StartService(servicesOperations.ServiceName);
        }
        /// <summary>
        /// Stop the running service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopServiceButton_Click(object sender, EventArgs e)
        {
            //utilityOperations.ServiceNames();
            utilityOperations.StopService(servicesOperations.ServiceName);
        }
        private void applicationCloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkBoxTopMost_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = !TopMost;
        }
    }
}
