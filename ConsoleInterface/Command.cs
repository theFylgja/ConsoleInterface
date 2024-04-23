using System.IO;

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
            if(input.Substring(0, 2) == "//")
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

            for(int i = 0; i < chars.Length; i++)
            {
                switch(chars[i])
                {
                    case ' ':
                        if (!wasOpened)
                        {
                            commandItems[itemIndex] = input.Substring(last, i + 1);
                            if (commandItems[itemIndex].Substring(0, 1) == "@")
                            {
                                isPath[itemIndex] = true;
                            }
                            itemIndex++;
                            last = i + 1;
                        }
                        return;
                    case '"':
                        if (!wasOpened)
                        {
                            wasOpened = true;
                            break;
                        }
                        wasOpened = false;
                        break;
                    case '@':
                        isPath[itemIndex] = true;
                        break;
                    default:
                        break;

                }
            }
            //get physical paths
            for(int i = 0; i < 32; i++)
            {
                if (isPath[i])
                {
                    commandItems[i] = GetPhysicalPath(commandItems[i]);
                }
            }
        }
        public string GetPhysicalPath(string path)
        {
            if(Directory.Exists(path))
            {
                return path;
            }
            else if(Directory.Exists(Server.RootPath + @"\" + path))
            {
                return Server.RootPath + path;
            }
            else if(Server.Var.Exists(path))
            {
                return (string)Server.Var.Get(path);
            }
            return (string)Server.Settings.Get("home");
        }
    }
}
