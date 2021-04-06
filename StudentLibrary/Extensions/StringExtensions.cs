using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentLibrary.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Determine if string is empty
        /// </summary>
        /// <param name="sender">String to test if null or whitespace</param>
        /// <returns>true if empty or false if not empty</returns>
        [DebuggerStepThrough]
        public static bool IsNullOrWhiteSpace(this string sender) => string.IsNullOrWhiteSpace(sender);
    }
}
