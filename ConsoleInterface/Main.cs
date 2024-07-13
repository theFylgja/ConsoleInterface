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
            Server.commandStack.Push(new Command(Next.Cmd()));
        }

        public static void Setup()
        {
            Server.Initialize();
            return;
        }
    }
}
