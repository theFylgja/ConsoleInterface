using System;
using System.Reflection;

namespace ConsoleInterface
{
    public class AAMainClass
    {
        public static void Hub()
        {
            Next.Debug("at Hub");
            Server.Initialize();
            Visualizer.Initialize();

            StackController controller = new StackController();

            //Next.Adv(new Command("ci print helloWorld").command[0]);

            try
            {
                Server.commandStack.Push(new Command(Next.Cmd()));
            }
            catch(Exception e)
            {
                Next.Err(e.Message);
            }
            Server.allowExecute = true;
            controller.Init();
        }

        public static void Setup()
        {
            Server.Initialize();
            return;
        }
    }
}
