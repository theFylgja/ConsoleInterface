using System;
using System.Threading;

using BiomeLibrary;
namespace Tester
{
    internal class Program
    {
        static void Main()
        {
            Bowl test = new Bowl(@"C:\WinTools\Files\CI\BGDF\settings.bgdf");
            Console.WriteLine();
            Console.WriteLine(test.Get("visualizer").GetType().Name);
            Thread.Sleep(50000);
        }
    }
}
