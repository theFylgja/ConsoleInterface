using BiomeLibrary;
using System;
using System.IO;
using System.Linq.Expressions;

namespace ConsoleInterface
{
    public class Visualizer
    {
        //Important Paths
        const string visualizerSettingsPath = @"C:\WinTools\Files\CI\BGDF\visualizer.bgdf";
        public static void Initialize()    
        {
            Settings.LoadDefaults();
        }
        public static bool Call(string rootPath)
        {
            if(Settings.autoLoadSettings)
            {
                Settings.Load();
            }
            if(!(bool)Server.Settings.Get("visualizer"))
            {
                return true;
            }
            if(Settings.printCurrentDirectory)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("-<");
                Console.ForegroundColor = Settings.currentDirColor;
                Console.Write(Server.RootPath);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(">-");
            }
            string[] subEntries = Directory.GetFileSystemEntries(rootPath);
            string[] postCut = new string[subEntries.Length];
            for(int i = 0; i < subEntries.Length; i++)  
            {
                postCut[i] = GetFileSystemEntryName(subEntries[i]);
                if (IsFile(subEntries[i])) 
                { 
                    if(Settings.visualizeFiles)
                    {
                        Console.ForegroundColor = Settings.symbolColor;
                        Console.Write("    " + (Settings.enableSymbols ? Settings.symbols[1] : " "));
                        Console.ForegroundColor = Settings.fileColor;
                        Console.WriteLine(postCut[i]);
                    }
                }
                else if(Settings.visualizeDirectories)
                {
                    Console.ForegroundColor = Settings.symbolColor;
                    Console.Write("    " + (Settings.enableSymbols ? Settings.symbols[1] : " "));
                    Console.ForegroundColor = Settings.directoryColor;
                    Console.WriteLine(postCut[i]);
                }
            }
            return true;

        }
        public static string GetFileSystemEntryName(string fullPath)
        {
            char[] asCharArray = fullPath.ToCharArray();
            int lastIndex = 0;
            for(int i = 0; i < asCharArray.Length; i++)
            {
                if (asCharArray[i] == '\\' || asCharArray[i] == @"\".ToCharArray()[0])
                {
                    lastIndex = i;
                }
            }
            return fullPath.Substring(lastIndex + 1);
        }
        public static bool IsFile(string fullPath)
        {
            return File.Exists(fullPath);
        }
        public class Settings
        {
            public static bool visualizeDirectories;
            public static bool visualizeSubDirectories;
            public static int subVisualizationDepth;
            public static bool visualizeFiles;
            public static int maxVisualizedItems;
            public static bool enableSymbols;
            //Directory-symbol, File-Symbol
            public static string[] symbols;
            public static bool autoLoadSettings;
            public static ConsoleColor fileColor;
            public static ConsoleColor directoryColor;
            public static ConsoleColor symbolColor;
            public static ConsoleColor currentDirColor;
            public static bool printCurrentDirectory;
            public static void LoadDefaults()
            {
                Server.VisualizerSettings.Set("visualizeDirectories", true);
                Server.VisualizerSettings.Set("visualizeSubDirectories", false);
                Server.VisualizerSettings.Set("subVisualizationDepth", 2);
                Server.VisualizerSettings.Set("visualizeFiles", true);
                Server.VisualizerSettings.Set("maxVisualizedItems", 10);
                Server.VisualizerSettings.Set("enableSymbols", false);
                Server.VisualizerSettings.Set("symbols", new string[] { "᨟", "᠅", });
                Server.VisualizerSettings.Set("autoLoadSettings", true);
                Server.VisualizerSettings.Set("fileColor", "darkGray");
                Server.VisualizerSettings.Set("directoryColor", "gray");
                Server.VisualizerSettings.Set("symbolColor", "white");
                Server.VisualizerSettings.Set("currentDirColor", "yellow");
                Server.VisualizerSettings.Set("printCurrentDirectory", true);
                Load();
            }
            public static void Load()
            {
                visualizeDirectories = (bool)Server.VisualizerSettings.Get("visualizeDirectories");
                visualizeSubDirectories = (bool)Server.VisualizerSettings.Get("visualizeSubDirectories");
                subVisualizationDepth = (int)Server.VisualizerSettings.Get("subVisualizationDepth");
                visualizeFiles = (bool)Server.VisualizerSettings.Get("visualizeFiles");
                maxVisualizedItems = (int)Server.VisualizerSettings.Get("maxVisualizedItems");
                enableSymbols = (bool)Server.VisualizerSettings.Get("enableSymbols");
                symbols = (string[])Server.VisualizerSettings.Get("symbols");
                autoLoadSettings = (bool)Server.VisualizerSettings.Get("autoLoadSettings");
                fileColor = Server.MatchEnum((string)Server.VisualizerSettings.Get("fileColor"));
                directoryColor = Server.MatchEnum((string)Server.VisualizerSettings.Get("directoryColor"));
                symbolColor = Server.MatchEnum((string)Server.VisualizerSettings.Get("symbolColor"));
                currentDirColor = Server.MatchEnum((string)Server.VisualizerSettings.Get("currentDirColor"));
                printCurrentDirectory = (bool)Server.VisualizerSettings.Get("printCurrentDirectory");
            }
        }
    }
}
