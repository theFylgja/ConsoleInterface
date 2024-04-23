using System.Collections.Generic;
using System.IO;
namespace ConsoleInterface
{
    public class StackController
    {
        public Command previous;
        public Command current;
        public void Execute()
        {
            previous = current;
            current = Server.commandStack.Pop();

            switch(current.command[0])
            {
                case "ci":

            }
        }
        public void CompileScript(string path)
        {
            if(!Settings.enableScripts)
            {
                Next.Err("the execution of external scripts is disabled in the Settings.");
                return;
            }
            string[] fileContents = File.ReadAllLines(path);
            for(int i = fileContents.Length - 1; i >= 0 ; i--)
            {
                Server.commandStack.Push(new Command(fileContents[i]));
            }
        }
    }
}
