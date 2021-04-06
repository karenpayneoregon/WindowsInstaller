using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using DataTestingProject.Base;
using EmployeeLibrary;

/*
 * NuGet package, see readme.md
 */
using KellermanSoftware.CompareNetObjects;

namespace DataTestingProject
{
    [TestClass]
    public class XmlTest : TestBase
    {
        /// <summary>
        /// Test reading a simple xml file into a list
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.ReadStudentsXml)]
        public void ReadEmployees()
        {
            // arrange
            var compareLogic = new CompareLogic();
            
            // act
            var employees = EmployeeOperations.ParseXml(EmployeesXmlFile);
            
            // assert
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
