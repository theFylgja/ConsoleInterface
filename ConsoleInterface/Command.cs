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
            List<string> commandItems = new List<string>();
            bool wasOpened = false;
            char[] chars = input.ToCharArray();

            for(int i = 0; i < chars.Length; i++)
            {
                switch(chars[i])
                {
                    case ' ':
                        if (!wasOpened)
                        {
                            commandItems.Add(input.Substring(0, i + 1));
                        } 

                }
            }
        }
    }
}
