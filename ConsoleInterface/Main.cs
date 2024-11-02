using System;
using System.Reflection;

namespace ConsoleInterface
{
    public class Startup
    {
        public static void Execute()
        {
            AAMainClass.Hub();
        }
    }
    public class AAMainClass
    {
        public static void Hub()
        {
            Next.Debug("at Hub");
            Server.Initialize();
            Visualizer.Initialize();

            StackController controller = new StackController();


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
