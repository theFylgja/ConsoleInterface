using System;
using System.IO;
using ConsoleInterface;
using System.Runtime.Remoting;

namespace VirtualCI
{
    public class Program
    {
        static void Main()
        {
            File.WriteAllText(@"C:\WinTools\Files\CI\Cache\reboot.txt", "0");
            AppDomain domain = AppDomain.CreateDomain("MainDomain");
            domain.Load("ConsoleInterface.dll");
            ObjectHandle main = domain.CreateInstance("ConsoleInterface", "AAMainClass");
            Startup obj = (Startup)main.Unwrap();
            
        }
    }
    public class Startup
    {
        public static void Execute()
        {
            AAMainClass.Hub();
        }
    }
}
