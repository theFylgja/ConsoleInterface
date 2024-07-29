using System;

using BiomeLibrary;

namespace ConsoleInterface
{
    public class BGDFEditor
    {

    }
    public class BVisualizer
    {
        Bowl currentFile;
        public void Visualize(string file)
        {
            currentFile = new Bowl(file);
            string[] names = currentFile.GetAllNames();
            string[] allVars = new string[names.Length];
            object current;

            for(int i = 0; i < names.Length; i++)
            {
                current = currentFile.Get(names[i]);
                allVars[i] = "(" + current.GetType().Name + ")" + names[i] + " =  " + (string)current;
            }
        }
    }
}
