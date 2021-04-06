using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using DataTestingProject.Base;

namespace DataTestingProject
{
    [TestClass]
    public class UnitTest1 : TestBase
    {
        [TestMethod]
        public void TestMethod1()
        {

        }
        
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            TestResults = new List<TestContext>();
        }
        [TestInitialize]
        public void Init()
        {
            if (TestContext.TestName == "TODO")
            {

            }
        }
    }
}
