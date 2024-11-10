using System.IO;
using System;

namespace ConsoleInterface
{
    public class Command : IDisposable
    {
        public string[] command {  get; set; }
        public string head {  get; set; }
        public string fullString { get; set; }
        public bool autoLoaded { get; set; }
        public bool skip { get; set; }

        public Command(string input)
        {
            if(input == null || input == String.Empty)
            {
                command = null;
                head = null;
                fullString = null;
                autoLoaded = false;
                skip = true;
                return;
            }
            else if(input.Substring(0, 2) == "//")
            {
                command = null;
                head = null;
                fullString = input;
                autoLoaded = false;
                skip = true;
                return;
            }
            fullString = input;
            string[] commandItems = new string[32];
            bool[] isPath = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
            int itemIndex = 0;
            bool wasOpened = false;
            char[] chars = input.ToCharArray();
            int last = 0;

            for (int i = 0; i < chars.Length; i++)
            {
                switch(chars[i])
                {
                    case ' ':
                        if (!wasOpened)
                        {
                            commandItems[itemIndex] = input.Substring(last, i - last);
                            if (commandItems[itemIndex].Substring(0, 1) == "@" || commandItems[itemIndex].Substring(0, 1) == "-")
                            {
                                isPath[itemIndex] = true;
                            }
                            commandItems[itemIndex] = commandItems[itemIndex].Substring(commandItems[itemIndex].Length - 1) == '"'.ToString() ? commandItems[itemIndex].Substring(0, commandItems[itemIndex].Length - 1) : commandItems[itemIndex];
                            itemIndex++; 
                            last = i + 1;
                        }
                        break;
                    case '-':
                        if(!wasOpened)
                        {
                            isPath[itemIndex] = true;
                        }
                        break;
                    case '"':
                        if (!wasOpened)
                        {
                            wasOpened = true;
                            last++;
                            break;
                        }
                        wasOpened = false;
                        break;
                    default:
                        break;

                }
            }
            commandItems[itemIndex] = input.Substring(last);
            commandItems[itemIndex] = commandItems[itemIndex].Substring(commandItems[itemIndex].Length - 1) == '"'.ToString() ? commandItems[itemIndex].Substring(0, commandItems[itemIndex].Length - 1) : commandItems[itemIndex];
            //get physical paths
            for (int i = 0; i < 32; i++)
            {
                if (commandItems[i]?.Substring(0, 1) == "@" || commandItems[i]?.Substring(0, 1) == "-")
                {
                    commandItems[i] = commandItems[i].Substring(1);
                }
                
                if (isPath[i])
                {
                    commandItems[i] = GetPhysicalPath(commandItems[i]);
                }
            }
            command = commandItems;
            head = command[0];
            autoLoaded = false;
            skip = false;
        }
        public string GetPhysicalPath(string path)
        {
            string output = "w";
            if(path == ".")
            {
                output = Server.RootPath;
            }
            else if(path == "..")
            {
                output = new DirectoryInfo(Server.RootPath).Parent.FullName;
            }
            else if(Directory.Exists(path) || File.Exists(path))
            {
                output = path;
            }
            else if(Directory.Exists(Server.RootPath + @"\" + path) || File.Exists(Server.RootPath + @"\" + path))
            {
                output = Server.RootPath + @"\" + path;
            }
            else if(Server.Var.Exists(path))
            {
                output = (string)Server.Var.Get(path);
            }
            else
            {
                try
                {
                    if(Convert.ToInt32(path) <= Server.currentVisualizerContent.Length && Convert.ToInt32(path) != 0)
                    {
                        output = Server.currentVisualizerContent[Convert.ToInt32(path) - 1];
                    }
                }
                catch 
                {
                    output = "invalidPath";
                }
            }
            return CleanPath(output);
        }
        public static string CleanPath(string input)
        {
            char[] chars = input.ToCharArray();
            string outputString = "";
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == '\u005c')
                {
                    while ((chars.Length >= i + 2 ? chars[i + 1] : 'a') == '\u005c')
                    {
                        i++;
                    }
                }
                outputString = outputString + chars[i];
            }
            return outputString;
        }

        public void Dispose()
        {
            return;
        }
    }
}
