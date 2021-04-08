using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using CommonLibrary.Containers;
using CommonLibrary.Generic.Helpers;
using DataTestingProject.Base;

namespace DataTestingProject
{
    [TestClass]
    public class GenericHelpersTest : TestBase
    {
        /// <summary>
        /// Test limit list to a specific Capacity
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.LimitList)]
        public void LimitListToSize()
        {
            // arrange
            int maxElements = 2;
            var list = new List<string>();
            list.Capacity = maxElements;

            // act
            while (list.Count <= maxElements - 1)
            {
                list.Add("X");
            }

            // assert
            Assert.AreEqual(list.Count, maxElements);

        }

        /// <summary>
        /// Limit list generically to a specific size.
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.LimitList)]
        public void LimitedPeople()
        {
            // arrange
            int maxItems = 3;

            var people = new List<Person>
            {
                new Person() {Id = 1, FirstName = "Karen", LastName = "Payne"},
                new Person() {Id = 2, FirstName = "Jim", LastName = "Gallagher"},
                new Person() {Id = 3, FirstName = "Anne", LastName = "Smith"},
                new Person() {Id = 4, FirstName = "Bob", LastName = "Lebow"},
                new Person() {Id = 5, FirstName = "Zack", LastName = "Anderson"}
            };

            // act
            LimitedList<Person> limitedList = new LimitedList<Person>(maxItems);
            foreach (var person in people)
            {
                limitedList.Add(person);
            }

            // assert
            List<Person> results = limitedList.ToList();
            Assert.IsTrue(results.Count == maxItems);
        }

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            TestResults = new List<TestContext>();
        }

        [TestInitialize]
        public void Init()
        {
            if (TestContext.TestName == "LimitedPeople" || TestContext.TestName == "LimitListToSize")
            {
                Console.WriteLine("Do something for limit testing");
            }
        }

    }
}
