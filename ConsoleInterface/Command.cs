using System.IO;
using System;

namespace ConsoleInterface
{
    public class Command
    {
        public string[] command {  get; set; }
        public string head {  get; set; }
        public bool autoLoaded { get; set; }
        public bool skip { get; set; }

        public Command(string input)
        {
            if(input == null || input == String.Empty)
            {
                command = null;
                head = null;
                autoLoaded = false;
                skip = true;
                return;
            }
            else if(input.Substring(0, 2) == "//")
            {
                command = null;
                head = null;
                autoLoaded = false;
                skip = true;
                return;
            }
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
                            itemIndex++; 
                            last = i + 1;
                        }
                        break;
                    case '"':
                        if (!wasOpened)
                        {
                            wasOpened = true;
                            break;
                        }
                        wasOpened = false;
                        break;
                    case '@':
                        if(!wasOpened)
                        {
                            isPath[itemIndex] = true;
                        }
                        break;
                    case '-':
                        if (!wasOpened)
                        {
                            isPath[itemIndex] = true;
                        }
                        break;
                    default:
                        break;

                }
            }
            commandItems[itemIndex] = input.Substring(last);
            //get physical paths
            for (int i = 0; i < 32; i++)
            {
                if (commandItems[i]?.Substring(0, 1) == "@" || commandItems[i]?.Substring(0, 1) == "-")
                {
                    commandItems[i] = commandItems[i].Substring(1);
                }
                if (commandItems[i]?.Substring(0, 1) == '"'.ToString())
                {
                    commandItems[i] = commandItems[i].Substring(1, commandItems[i].Length - 2);
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
            if(path == ".")
            {
                return Server.RootPath;
            }
            else if(path == "..")
            {
                return new DirectoryInfo(Server.RootPath).Parent.FullName;
            }
            else if(Directory.Exists(path) || File.Exists(path))
            {
                return path;
            }
            else if(Directory.Exists(Server.RootPath + @"\" + path) || File.Exists(Server.RootPath + @"\" + path))
            {
                return Server.RootPath + @"\" + path;
            }
            else if(Server.Var.Exists(path))
            {
                return (string)Server.Var.Get(path);
            }
            else if(Convert.ToInt32(path) <= Server.currentVisualizerContent.Length && Convert.ToInt32(path) != 0)
            {
                return Server.currentVisualizerContent[Convert.ToInt32(path) - 1];
            }
            return "defavalidpathtrustme";
        }
    }
}
