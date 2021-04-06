using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EmployeeLibrary.Containers;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace DataTestingProject.Base
{
    /// <summary>
    /// used for keeping test method clean along with detection of which test is about to execute.
    /// </summary>
    public class TestBase
    {
        /// <summary>
        /// Seconds to wait for an async operation timeout
        /// </summary>
        protected const int TimeOutSeconds = 5;
        
        /// <summary>
        /// Used for cancellation of a long running async operations
        /// </summary>
        protected CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(TimeOutSeconds));
        
        /// <summary>
        /// Database connection
        /// </summary>
        protected static string ConnectionString = "TODO";

        protected TestContext TestContextInstance;
        public TestContext TestContext
        {
            get => TestContextInstance;
            set
            {
                TestContextInstance = value;
                TestResults.Add(TestContext);
            }
        }

        public static IList<TestContext> TestResults;

        /// <summary>
        /// Path and file name for xml student file
        /// </summary>
        protected static string EmployeesXmlFile => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "Employees.xml");

        /// <summary>
        /// Used to for comparisons of data read from <see cref="EmployeesXmlFile"/>
        /// </summary>
        protected static List<Employee> EmployeesExpected => new List<Employee>()
        {
            new Employee() {Id = 11, Name = "Karen"},
            new Employee() {Id = 12, Name = "Lisa"},
            new Employee() {Id = 13, Name = "Yelena"},
            new Employee() {Id = 14, Name = "Bill"}
        };

    }
}
