using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using DataTestingProject.Base;
using KellermanSoftware.CompareNetObjects;
using StudentLibrary;

namespace DataTestingProject
{
    [TestClass]
    public class UnitTest1 : TestBase
    {
        /// <summary>
        /// Test reading a simple xml file into a list
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.ReadStudentsXml)]
        public void ReadEmployees()
        {
            var employees = EmployeeOperations.ParseXml(EmployeesXmlFile);
            var compareLogic = new CompareLogic();
            ComparisonResult result = compareLogic.Compare(employees, EmployeesExpected);
            Assert.IsTrue(result.AreEqual);

        }

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            TestResults = new List<TestContext>();
        }
        
        [TestInitialize]
        public void Init()
        {
            if (TestContext.TestName == "ReadEmployees")
            {
                Console.WriteLine("Do something for ReadEmployees test");
            }
        }
    }
}
