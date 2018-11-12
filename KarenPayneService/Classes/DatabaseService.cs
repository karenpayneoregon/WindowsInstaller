using System;
using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;

namespace KarenPayneService.Classes
{
    public partial class DatabaseService : ServiceBase
    {
        //private string _mServiceName = "DatabaseService";  
        Timer _serviceTimer; 
        int _executeTime = 0; 

        public DatabaseService()
        {
            InitializeComponent();
        }
        void ActionsInitiaization()
        {
            EventLog.WriteEntry("Hello from ActionsInitiaization()");
        }
        protected override void OnStart(string[] args)
        {

#if DEBUG
            Debugger.Launch();
#endif
            RequestAdditionalTime(10000);

            ScheduleService();

        }
        public void ScheduleService()
        {
            var programStartupMode = "";
            var setupSchedule = new SetupSchedule();

            try
            {
                ActionsInitiaization();

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
                EventLog.WriteEntry($"Karen Payne Service Error on: {0} " + 
                    ex.Message + ex.StackTrace, EventLogEntryType.Error);

                using (var serviceController = new ServiceController("DatabaseService"))
                {
                    serviceController.Stop();
                }
            }

        }
        protected override void OnStop()
        {
            _serviceTimer.Dispose();
        }
        void Dispatcher(object e)
        {
            var ops = new Operations();
            if (!ops.InsertMessage("Written in demo service"))
            {
                string errorMessage = ops.ExceptionMessage;
                EventLog.WriteEntry($"Karen Payne Service Error inserting record: {errorMessage}", EventLogEntryType.Error);
            }

            _serviceTimer.Dispose();

            ScheduleService();
        }

    }
}
