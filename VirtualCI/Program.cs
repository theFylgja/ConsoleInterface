using System;
using System.IO;
using ConsoleInterface;
using System.Threading;
using System.Runtime.Remoting;
using System.Diagnostics;

namespace VirtualCI
{
    public class Program
    {
        static Mutex mutex = null;
        static void Main(string[] args)
        {
            const string mutexName = "TheRealG";
            mutex = new Mutex(true, mutexName, out bool createdNew);

            if (!createdNew)
            {
                Environment.Exit(0);
            }
            if (args.Length > 0)
            {
                if (File.Exists(args[0]))
                {
                    Next.Adv("got path: " + args[0]);
                }
                else
                {
                    Next.Err("no path received");
                }
            }
            File.WriteAllText(@"C:\WinTools\Files\CI\Cache\reboot.txt", "0");
            ConsoleInterface.AAMainClass.Hub(args.Length > 0 ? $"ci {args[0]} lsc" : "ci print ConsoleInterface");
            
        }
    }
}
