 using System.Collections.Generic;
using BiomeLibrary;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace ConsoleInterface
{
    //Hosts/provides variables needed by all other components
    public class Server
    {
        public static string RootPath;
        public static string crashPath = @"C:\WinTools\FIles\CI\crashLog.txt";
        public static Stack<Command> commandStack;
        public static Task executer;
        public static Bowl Settings;
        public static Bowl VisualizerSettings;
        public static Bowl NextSettings;
        public static Bowl Var;
        public static Bowl FileEditors;
        public static StreamWriter crashWriter;
        public static string SettingsPath;
        public static string VisualizerSettingsPath;
        public static string NextSettingsPath;
        public static string VarPath;
        public static string FileEditorsPath;
        public static bool allowExecute;
        public static string[] mountSymbols;
        public static string[] currentVisualizerContent;
        //maybe add cached settings later
        public static void Initialize()
        {
            string path1 = @"C:\WinTools\Files\CI\BGDF\settings.bgdf";
            string path2 = @"C:\WinTools\Files\CI\BGDF\visualizer.bgdf";
            string path3 = @"C:\WinTools\Files\CI\BGDF\designPreferences.bgdf";
            string path4 = @"C:\WinTools\Files\CI\BGDF\var.bgdf";
            string path5 = @"C:\WinTools\FIles\CI\BGDF\fileEditors.bgdf";

            SettingsPath = path1;
            VisualizerSettingsPath = path2;
            NextSettingsPath = path3;
            VarPath = path4;
            FileEditorsPath = path5;

            Settings = new Bowl(path1);
            VisualizerSettings = new Bowl(path2);
            NextSettings = new Bowl(path3);
            Var = new Bowl(path4);
            FileEditors = new Bowl(path5);

            commandStack = new Stack<Command>();

            AppDomain.CurrentDomain.UnhandledException += GlobalExceptionHandler;

            InitializeBowls();
            Server.RootPath = (string)Settings.Get("homeDirectory");
            Server.mountSymbols = (string[])Settings.Get("mountSymbols");
        }

        public static void InitializeBowls()
        {
            Settings.Set("mountSymbols", new string[] { "@", "-" });
        }
        //matching string to enum
        public static string MatchString(ConsoleColor color)
        {
            switch (color)
            {
                case ConsoleColor.Black:
                    return "black";
                case ConsoleColor.Blue:
                    return "blue";
                case ConsoleColor.Cyan:
                    return "cyan";
                case ConsoleColor.DarkBlue:
                    return "darkBlue";
                case ConsoleColor.Gray:
                    return "gray";
                case ConsoleColor.Green:
                    return "green";
                case ConsoleColor.Magenta:
                    return "magenta";
                case ConsoleColor.Red:
                    return "red";
                case ConsoleColor.White:
                    return "white";
                case ConsoleColor.Yellow:
                    return "yellow";
                case ConsoleColor.DarkCyan:
                    return "darkCyan";
                case ConsoleColor.DarkGreen:
                    return "darkGreen";
                case ConsoleColor.DarkYellow:
                    return "darkYellow";
                case ConsoleColor.DarkGray:
                    return "darkGray";
                case ConsoleColor.DarkRed:
                    return "darkRed";
                case ConsoleColor.DarkMagenta:
                    return "darkMagenta";
                default:
                    return "white";
            }
        }
        public static ConsoleColor MatchEnum(string input)
        {
            switch (input)
            {
                case "black":
                    return ConsoleColor.Black;
                case "blue":
                    return ConsoleColor.Blue;
                case "cyan":
                    return ConsoleColor.Cyan;
                case "darkBlue":
                    return ConsoleColor.DarkBlue;
                case "gray":
                    return ConsoleColor.Gray;
                case "green":
                    return ConsoleColor.Green;
                case "magenta":
                    return ConsoleColor.Magenta;
                case "red":
                    return ConsoleColor.Red;
                case "white":
                    return ConsoleColor.White;
                case "yellow":
                    return ConsoleColor.Yellow;
                case "darkCyan":
                    return ConsoleColor.DarkCyan;
                case "darkGreen":
                    return ConsoleColor.DarkGreen;
                case "darkYellow":
                    return ConsoleColor.DarkYellow;
                case "darkRed":
                    return ConsoleColor.DarkRed;
                case "darkGray":
                    return ConsoleColor.DarkGray;
                case "darkMagenta":
                    return ConsoleColor.DarkMagenta;
                default:
                    return ConsoleColor.White;
            }
        }

        public static string GetFileExtension(string fullPath)
        {
            FileInfo fileInfo = new FileInfo(fullPath);
            return fileInfo.Extension;
        }
        public static void GlobalExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            File.WriteAllText(crashPath, ConsoleDumper.DumpConsoleContents(crashPath)+  ex.Message + $"     Stacktrace: {ex.StackTrace}");
        }
    }
    public static class ConsoleDumper
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool ReadConsoleOutputCharacter(IntPtr hConsoleOutput, [Out] StringBuilder lpCharacter, uint nLength, COORD dwReadCoord, out uint lpNumberOfCharsRead);

        // Constants
        const int STD_OUTPUT_HANDLE = -11;

        // Struct representing a console coordinate
        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {
            public short X;
            public short Y;

            public COORD(short x, short y)
            {
                X = x;
                Y = y;
            }
        }

        public static string DumpConsoleContents(string filePath)
        {
            Next.Debug("dumping console");
            // Get the handle to the console output buffer
            IntPtr hConsole = GetStdHandle(STD_OUTPUT_HANDLE);

            if (hConsole == IntPtr.Zero)
            {
                throw new InvalidOperationException("Unable to get console handle.");
            }

            // Read the console buffer (arbitrary size)
            StringBuilder buffer = new StringBuilder(2000); // Adjust the size as needed
            COORD coord = new COORD(0, 0); // Start reading from the top-left corner of the console
            uint charsRead = 0;

            bool success = ReadConsoleOutputCharacter(hConsole, buffer, (uint)buffer.Capacity, coord, out charsRead);

            if (!success)
            {
                throw new InvalidOperationException("Unable to read console output.");
            }

            // Dump the console content to the file
            return buffer.ToString();
        }
    }
}
