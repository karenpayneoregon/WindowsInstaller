using System;
using System.Configuration;

namespace KarenPayneService
{
    /// <summary>
    /// 
    /// </summary>
    public class SetupSchedule
    {
        /// <summary>
        /// Determine startup mode, either daily or interval mode
        /// </summary>
        /// <param name="programStartupMode"></param>
        /// <returns></returns>
        public int ConfigureMode(string programStartupMode)
        {
            DateTime scheduledTime = DateTime.MinValue;

            // Get the difference in Minutes between the Scheduled and Current Time.
            if (programStartupMode == "DAILY")
            {
                scheduledTime = DateTime.Parse(ConfigurationManager.AppSettings["ScheduledTime"]);
                if (DateTime.Now > scheduledTime)
                {
                    // If Scheduled Time is passed set Schedule for the next day.
                    scheduledTime = scheduledTime.AddDays(1);
                }
            }

            if (programStartupMode.ToUpper() == "INTERVAL")
            {
                int intervalMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["IntervalMinutes"]);

                // Set the Scheduled Time by adding the Interval to Current Time.
                scheduledTime = DateTime.Now.AddMinutes(intervalMinutes);
                if (DateTime.Now > scheduledTime)
                {
                    // If Scheduled Time is passed set Schedule for the next Interval.
                    scheduledTime = scheduledTime.AddMinutes(intervalMinutes);
                }
            }
            
            return Convert.ToInt32(scheduledTime.Subtract(DateTime.Now).TotalMilliseconds);
        }
    }

}
