using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ServiceInstaller.Classes;

/// <summary>
/// MSDN System folder extensions
/// https://code.msdn.microsoft.com/Get-upper-folders-in-443e975a
/// </summary>
public static class DirectoryExtensions
{
    public static bool _hasException;
    public static Exception LastException;

    /// <summary>
    /// This would be what many developer would use, a batch file to remove their service and the reverse for
    /// installing thier service but the batch file would be created by hand which means if the source folder
    /// changes the batch file would need to be updated else it would fail or point to the original folder.
    /// </summary>
    /// <param name="pServiceProjectFolder"></param>
    /// <param name="pExecutableName"></param>
    /// <remarks>
    /// This method is not perfect as it only takes into account x86, slight modifications for using Framework x64
    /// See the following for path change for x64 https://docs.microsoft.com/en-us/dotnet/framework/windows-services/how-to-install-and-uninstall-services
    /// </remarks>
    public static void CreateManualUninstallBatchFile(string pServiceProjectFolder, string pExecutableName)
    {
        _hasException = false;

        var frameworkPath = "C:\\Windows\\Microsoft.NET\\Framework";
        var utilVersion = "";
        var utilNamne = "InstallUtil.exe";

        var result = Directory.GetFiles(frameworkPath, "installutil.exe", SearchOption.AllDirectories);

        try
        {
            utilVersion = result
                .Select(fileName => fileName.Replace(frameworkPath, "").Replace(utilNamne, "").Replace("\\", "").Replace("v", ""))
                .LastOrDefault();

            var executablePathName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.UpperFolder(4), pServiceProjectFolder, pExecutableName);
            var uninstallCommand = $"{frameworkPath}\\v{utilVersion}\\{utilNamne} /uninstall {executablePathName}";
            var sb = new StringBuilder();

            sb.AppendLine("@Echo off");
            sb.AppendLine("@cls");
            sb.AppendLine(uninstallCommand);
            sb.AppendLine("pause");

            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Uninstall.bat"),sb.ToString());

        }
        catch (Exception e)
        {
            _hasException = true;
            LastException = e;
        }
    }

    /// <summary>
    /// Given a folder name return all parents according to level
    /// </summary>
    /// <param name="pFolderName">Sub-folder name</param>
    /// <param name="level">Level to move up the folder chain</param>
    /// <returns>A physical folder path</returns>
    public static string UpperFolder(this string pFolderName, decimal level) => UpperFolder(pFolderName, Convert.ToInt32(level));
    /// <summary>
    /// Given a folder name return all parents according to level
    /// </summary>
    /// <param name="FolderName">Sub-folder name</param>
    /// <param name="level">Level to move up the folder chain</param>
    /// <returns>List of folders dependent on level parameter</returns>
    public static string UpperFolder(this string FolderName, int level)
    {
        var folderList = new List<string>();

        while (!string.IsNullOrWhiteSpace(FolderName))
        {
            var parentFolder = Directory.GetParent(FolderName);
            if (parentFolder == null)
            {
                break;
            }
            FolderName = Directory.GetParent(FolderName).FullName;
            folderList.Add(FolderName);
        }

        if (folderList.Count > 0 && level > 0)
        {
            if (level - 1 <= folderList.Count - 1)
            {
                return folderList[level - 1];
            }
            else
            {
                return FolderName;
            }
        }
        else
        {
            return FolderName;
        }
    }
    public static string CurrentProjectFolder(this string sender)
    {
        return sender.UpperFolder(3);
    }
    /// <summary>
    /// Get a list of all folders above 'FolderName'
    /// </summary>
    /// <param name="pFolderName">Folder to start at</param>
    /// <param name="pSort">True/False</param>
    /// <returns>List of folder names</returns>
    public static List<string> UpperFolderList(this string pFolderName, bool pSort)
    {
        var folderList = new List<string>();

        while (!string.IsNullOrWhiteSpace(pFolderName))
        {
            var parentFolder = Directory.GetParent(pFolderName);
            if (parentFolder == null)
            {
                break;
            }
            pFolderName = Directory.GetParent(pFolderName).FullName;
            folderList.Add(pFolderName);
        }

        if (pSort)
        {
            folderList.Sort();
        }

        return folderList;
    }
}