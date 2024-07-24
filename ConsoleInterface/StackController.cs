using System;
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
                    Next.Debug("at empty pop");
                    Server.commandStack.Pop();
                    break;
                }
                if(Server.commandStack.Count > 0)
                {
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
            Next.Debug("at StackController.Execute");
            //previous = current;
            current = Server.commandStack.Pop();
            if(current.command == null)
            {
                Next.Err("incorrect skip handling");
            }

            switch(current.command[0])
            {
                case "ci":
                    Handler.CIHandle(current);
                    break;
                case "cd":
                    Handler.IO.MountDirectory(current);
                    break;
                default:
                    break;
            }
            try
            {
                Visualizer.Call(Server.RootPath);
            }
            catch(Exception e)
            {
                if(e is System.UnauthorizedAccessException)
                {
                    Next.Err("access to the Directory was denied by the OS");
                }
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
