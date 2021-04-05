using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileOps = System.IO.File;

namespace KarenPayneService.Classes.IO
{
    public class File
    {
        /// <summary>
        /// Example for reading a local file
        /// </summary>
        public static (Exception exception, List<string> linesList) ReadNameFile()
        {
            try
            {
                var fileName = "C:\\oed\\ServiceFiles\\Names.txt";
                if (FileOps.Exists(fileName))
                {
                    var lines = FileOps.ReadAllLines(fileName);
                    return (null, lines.ToList());
                }
            }
            catch (Exception e)
            {
                return (e,null);

            }

            return (null, null);
        }
    }
}


