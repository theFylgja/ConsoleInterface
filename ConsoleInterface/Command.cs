using System.Collections.Generic;

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

                }
            }
        }
    }
}
