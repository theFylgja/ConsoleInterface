using System;
using System.Reflection;

namespace ConsoleInterface
{
    public class AAMainClass
    {
        public static void Hub()
        { 
            Server.Initialize();
            Visualizer.Initialize();

            StackController controller = new StackController();

            //Next.Adv(new Command("ci print helloWorld").command[0]);

            try
            {
                Next.Debug("in try thing");
                Server.commandStack.Push(new Command(Next.Cmd()));
                Next.Debug("pushed command");
            }
            catch(Exception e)
            {
                Next.Debug("at catch error(e)");
                Next.Err(e.Message);
                Next.Debug(e.Source);
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
