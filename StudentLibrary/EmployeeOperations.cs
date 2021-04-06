using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using StudentLibrary.Containers;

namespace StudentLibrary
{
    /// <summary>
    /// Various employee operations
    /// </summary>
    public class EmployeeOperations
    {
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
