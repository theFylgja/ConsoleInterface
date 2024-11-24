using System;
using System.Diagnostics;
using System.IO;

namespace Updater
{
    internal class Program
    {
        static void Main(string[] args)
        {
            File.Delete(@".\ConsoleInterface.dll");
            File.Move(@"C:\WinTools\Files\CI\Cache\ConsoleInterface.dll", @".\ConsoleInterface.dll");
            Process.Start(@".\VirtualCI.exe");
            Environment.Exit(0);
        }
    }
}
