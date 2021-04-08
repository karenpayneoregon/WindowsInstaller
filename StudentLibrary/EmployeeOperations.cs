using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using EmployeeLibrary.Containers;

namespace EmployeeLibrary
{
    /// <summary>
    /// Various employee operations
    /// </summary>
    public class EmployeeOperations
    {
        /// <summary>
        /// Simple read xml into a list of Employee
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<Employee> ParseXml(string fileName)
        {
            var xmlDoc = File.ReadAllText(fileName);

            XDocument xDoc;
            xDoc = XDocument.Parse(xmlDoc);

            var query = xDoc.Root.Descendants("Employee").Select(element => element);

            return query.Select(xElement => new Employee
            {
                Id = Convert.ToInt32(xElement.Element("Id").Value), 
                Name = xElement.Element("name").Value
            }).ToList();
        }
    }
}
