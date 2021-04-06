using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataTestingProject.Base
{
    public class TestBase
    {
        protected const int TimeOutSeconds = 5;
        protected CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(TimeOutSeconds));
        protected static string ConnectionString = "Server=.\\SQLEXPRESS;Database=NorthWind2020;Integrated Security=true";

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
    }
}
