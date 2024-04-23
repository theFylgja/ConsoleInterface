
using BiomeLibrary;
using System;

namespace ConsoleInterface
{
    //Hosts/provides variables needed by all other components
    public class Server
    {
        public static string RootPath;
        public static Bowl Settings;
        public static Bowl VisualizerSettings;
        public static Bowl NextSettings;
        public static Bowl Var;
        public static string SettingsPath;
        public static string VisualizerSettingsPath;
        public static string NextSettingsPath;
        public static string VarPath;
        //maybe add cached settings later
        public static void Initialize()
        {
            string path1 = @"C:\WinTools\Files\CI\BGDF\settings.bgdf";
            string path2 = @"C:\WinTools\Files\CI\BGDF\visualizer.bgdf";
            string path3 = @"C:\WinTools\Files\CI\BGDF\designPreferences.bgdf";
            string path4 = @"C:\WinTools\Files\CI\BGDF\var.bgdf";

            SettingsPath = path1;
            VisualizerSettingsPath = path2;
            NextSettingsPath = path3;
            VarPath = path4;

            Settings = new Bowl(path1);
            VisualizerSettings = new Bowl(path2);
            NextSettings = new Bowl(path3);
            Var = new Bowl(path4);
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
    }
}
