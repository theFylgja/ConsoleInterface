using System;
using System.IO;
using BiomeLibrary;

namespace ConsoleInterface
{
    public class FileAccessProvider
    {
        public static void HandleFileAccess(Command cmd)
        {
            if (cmd.command[2] == "dpt")
            {
                //means "display plain text"
                PlainTextDisplay(cmd.command[1]);
            }
        }

        public static void PlainTextDisplay(string filePath)
        {
            string fileContent = File.ReadAllText(filePath) ?? "file is empty";
            Next.Title("Contents of File: " + filePath);
            Next.Text(fileContent);
        }
    }
}