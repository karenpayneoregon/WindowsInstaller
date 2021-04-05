using System;
using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using KarenPayneService.Classes.OracleOperations;

namespace KarenPayneService.Classes
{
    /// <summary>
    /// Windows service class main entry
    /// </summary>
    public partial class DatabaseService : ServiceBase
    {
        //private string _mServiceName = "DatabaseService";  
        Timer _serviceTimer; 
        int _executeTime = 0; 

        /// <summary>
        /// Used from Program.cs to start the service
        /// </summary>
        public DatabaseService()
        {
            InitializeComponent();
        }
        void ActionsInitialization()
        {
            EventLog.WriteEntry("Hello from ActionsInitialization()");
        }
        /// <summary>
        /// 
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
        /// 
        /// </summary>
        public void ScheduleService()
        {
            var programStartupMode = "";
            var setupSchedule = new SetupSchedule();

            try
            {
                ActionsInitialization();

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
                EventLog.WriteEntry($"Karen Payne Service Error on: {ex.Message + ex.StackTrace} " , EventLogEntryType.Error);

                using (var serviceController = new ServiceController("DatabaseService"))
                {
                    serviceController.Stop();
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        protected override void OnStop()
        {
            _serviceTimer.Dispose();
        }
        void Dispatcher(object e)
        {
            
            //ReadFileExample();
            //WriteRecordToSqlServerExample();
            
            WriteToOraclePartlyDoneExample();


            _serviceTimer.Dispose();

            ScheduleService();
        }

        private void WriteToOraclePartlyDoneExample()
        {
            var address = new CbrAddress() {PostalCode = "TODO"};

            int newIdentifier = -1;

            var exception = Operations.InsertRecord(address, ref newIdentifier);

            if (newIdentifier > -1)
            {
                // record inserted
            }
            else
            {
                EventLog.WriteEntry($"Karen Payne Service Error inserting record: {exception.Message}", EventLogEntryType.Error);
            }
        }
        /// <summary>
        /// Write a record to SQL-Server database KarensServiceDatabase, table MessagesFromService
        /// </summary>
        private void WriteRecordToSqlServerExample()
        {
            var ops = new SqlServerOperations();
            
            if (!ops.InsertMessage("Written in demo service"))
            {
                string errorMessage = ops.ExceptionMessage;
                EventLog.WriteEntry($"Karen Payne Service Error inserting record: {errorMessage}", EventLogEntryType.Error);
            }
        }

        private void ReadFileExample()
        {
            /*
             * Read an existing file
             *
             * If successful `exception` will be null and the file contents will be in `linesList`
             * while on failure `exception` will not be null
             *
             * NOTE: The return type is a 'named value tuple
             */
            var (exception, linesList) = IO.File.ReadNameFile();

            if (exception == null)
            {
            }
            else
            {
                EventLog.WriteEntry($"Karen Payne Service Error inserting record: {exception.Message}", EventLogEntryType.Error);
            }
        }
    }
}
