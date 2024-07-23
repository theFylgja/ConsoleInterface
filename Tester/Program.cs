using System;
using ConsoleInterface;
namespace Tester
{
    internal class Program
    {
        static void Main()
        {
            Command cmd = new Command("ci print helloWorld");
            Next.List(cmd.command);
            Next.Arg();
        }
    }
}
