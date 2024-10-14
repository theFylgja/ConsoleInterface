using System;
using System.Diagnostics;
using System.IO;
using BiomeLibrary;
using static ConsoleInterface.Handler;

namespace ConsoleInterface
{
    public class FileAccessProvider
    {
        public static void HandleFileAccess(Command cmd)
        {
            Process.Start("explorer.exe", cmd.command[1]);
        }

        public static void PlainTextDisplay(string filePath)
        {
            string fileContent = File.ReadAllText(filePath) ?? "file is empty";
            Next.Title("Contents of File: " + filePath);
            Next.Text(fileContent);
        }
    }
}