using System;
using System.Windows.Forms;

namespace ServiceInstaller.Classes
{
    public static class WindowsFormsDialogs
    {
        public static bool Question(string pText) => (MessageBox.Show(pText, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes);

        /// <summary>
        /// Display error message with appropriate icon
        /// </summary>
        /// <param name="pText"></param>
        public static void ExceptionDialog(string pText)
        {
            MessageBox.Show(pText, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}