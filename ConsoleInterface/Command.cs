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
            Next.Err(".");
            if(input.Substring(0, 2) == "//")
            {
                command = null;
                head = null;
                autoLoaded = false;
                skip = true;
                return;
            }
            Next.Err(".");
            string[] commandItems = new string[32];
            bool[] isPath = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
            int itemIndex = 0;
            bool wasOpened = false;
            char[] chars = input.ToCharArray();
            int last = 0;

            Next.Err(".");
            for (int i = 0; i < chars.Length; i++)
            {
                switch(chars[i])
                {
                    case ' ':
                        if (!wasOpened)
                        {
                            Next.Adv("at insert command");
                            commandItems[itemIndex] = input.Substring(last, i - last);
                            if (commandItems[itemIndex].Substring(0, 1) == "@")
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
                        isPath[itemIndex] = true;
                        break;
                    default:
                        break;

                }
            }
            Next.Err(".");
            commandItems[itemIndex] = input.Substring(last);
            //get physical paths
            for (int i = 0; i < 32; i++)
            {
                /*
                if (commandItems[i].Substring(0, 1) == '"'.ToString())
                {
                    commandItems[i] = commandItems[i].Substring(1, commandItems[i].Length - 2);
                }
                */
                if (isPath[i])
                {
                    commandItems[i] = GetPhysicalPath(commandItems[i].Substring(1));
                }
            }
            command = commandItems;
            head = command[0];
            autoLoaded = false;
            skip = false;
            Next.Err("done with that");
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
                return Server.Settings.Path;
            }
            return (string)Server.Settings.Get("home");
        }
    }
}
