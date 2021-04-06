using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KarenPayneService;

namespace TemplateService
{
    public partial class SampleService : ServiceBase
    {
        /// <summary>
        /// Used to trigger actions/operations in this service
        /// </summary>
        Timer _serviceTimer;
        
        /// <summary>
        /// Interval to run while in INTERVAL mode set in app.config
        /// </summary>
        int _executeTime = 0;

        /// <summary>
        /// Constructor for service to perform internal initialization
        /// </summary>
        public SampleService()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Entry point for service once the constructor above has finished.
        ///
        ///
        /// DEBUG gets set in project properties, uncheck for production, otherwise
        /// the service will throw a unhandled exception.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            
#if DEBUG
            Debugger.Launch();
#endif
            RequestAdditionalTime(10000);

            ScheduleService();
        }
        /// <summary>
        /// Called when the service stops or uninstalled
        /// </summary>
        protected override void OnStop()
        {
            _serviceTimer.Dispose();
        }

        /// <summary>
        /// Code to determine interval to run, DAILY or INTERVAL
        /// Currently for DAILY mode, 2 minutes before mid-night
        /// and INTERVAL mode (developer mode) is set to trigger <see cref="Dispatcher"/>
        /// which is in this class and is the point where to call your business logic code.
        /// </summary>
        public void ScheduleService()
        {
            var programStartupMode = "";
            var setupSchedule = new SetupSchedule();

            try
            {

                if (string.IsNullOrWhiteSpace(programStartupMode))
                {
                    programStartupMode = ConfigurationManager.AppSettings["Mode"].ToUpper();
                }

                _serviceTimer = new Timer(Dispatcher);

                if (_executeTime == 0)
                {
                    _executeTime = Convert.ToInt32(setupSchedule.ConfigureMode(programStartupMode));
                }

                EventLog.WriteEntry("Setting timer!!!");

                _serviceTimer.Change(_executeTime, Timeout.Infinite);

            }
            catch (Exception ex)
            {
                EventLog.WriteEntry($"Service Error on: {ex.Message + ex.StackTrace} ", EventLogEntryType.Error);

                using (var serviceController = new ServiceController("SampleService"))
                {
                    serviceController.Stop();
                }
            }

        }
        /// <summary>
        /// Code to run goes here
        /// </summary>
        /// <param name="e"></param>
        void Dispatcher(object e)
        {
            _serviceTimer.Dispose();
            
            
            /*
             * Place code to run in the service here
             */
            

            ScheduleService();
        }

    }
}
