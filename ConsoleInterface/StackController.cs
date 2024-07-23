using System.IO;
using System.Threading;
namespace ConsoleInterface
{
    public class StackController
    {
        public Command previous;
        public Command current = null;
        public void Init()
        {
            while(Server.allowExecute)
            {
                if (Server.commandStack.Count > 0 && Server.commandStack.Peek().skip)
                {
                    Next.Adv("at empty pop");
                    Server.commandStack.Pop();
                    break;
                }
                if(Server.commandStack.Count > 0)
                {
                    Next.Adv("at execute");
                    Next.Err(Server.commandStack.Peek().skip.ToString());
                    Execute();
                }
                Server.commandStack.Push(new Command(Next.Cmd()));
                Thread.Sleep(50);
            }
            Thread.Sleep(50);
            Init();
        }
        public void Execute()
        {
            //previous = current;
            current = Server.commandStack.Pop();
            if(current.command == null)
            {
                Next.Err("incorrect skip handling");
            }

            switch(current.command[0])
            {
                case "ci":
                    switch(current.command[1])
                    {
                        case "print":
                            Next.Adv(current.command[2]);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
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
