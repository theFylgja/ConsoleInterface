using BiomeLibrary;
using System.IO;
using System.Linq.Expressions;

namespace ConsoleInterface
{
    public class Visualizer
    {
        //Important Paths
        const string visualizerSettingsPath = @"C:\WinTools\Files\CI\BGDF\visualizer.bgdf";
        public void Initialize() 
        {
            
        }
        static bool Call(string rootPath)
        {
            if(Settings.autoLoadSettings)
            {
                Settings.Load();
            }
            if(!(bool)Server.Settings.Get("visualizer"))
            {
                return true;
            }
            if(Settings.visualizeDirectories && Settings.visualizeFiles)
            {
                string[] subEntries = Directory.GetFileSystemEntries(rootPath);
                for(int i = 0; i < subEntries.Length; i++)
                {
                    if(Settings.enableSymbols)
                    {

                    }
                }

            }
        }
        public bool isFile(string fullPath)
        {
            if(File.Exists(fullPath))
            {
                return true;
            }
            return false;
            
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
            public void LoadDefaults()
            {
                Server.VisualizerSettings.Set("visualizeDirectories", true);
                Server.VisualizerSettings.Set("visualizeSubDirectories", false);
                Server.VisualizerSettings.Set("subVisualizationDepth", 2);
                Server.VisualizerSettings.Set("visualizeFiles", true);
                Server.VisualizerSettings.Set("maxVisualizedItems", 10);
                Server.VisualizerSettings.Set("enableSymbols", false);
                Server.VisualizerSettings.Set("symbols", new string[] { "᨟", "᠅", });
                Server.VisualizerSettings.Set("autoLoadSettings", false);
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
            }
        }
    }
}
