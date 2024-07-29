using System;
using System.IO;

using BiomeLibrary;

namespace BGDFEditor
{
    //neccessary classes for Editor Plugin
    public class Main
    {
        public static void CallEditor(string fullPath)
        {

        }
    }
    public class Editor
    {
        Bowl currentFile;
        public void VisualizeFile(string fullPath)
        {
            currentFile = new Bowl(fullPath);
            string[] names = currentFile.GetAllNames();
            string[] allVars = new string[names.Length];
            object current;

            for (int i = 0; i < names.Length; i++)
            {
                current = currentFile.Get(names[i]);
                allVars[i] = "(" + current.GetType().Name + ")" + names[i] + " =  " + (string)current;
            }
        }
    }

    public class EditorInstaller
    {
        public void Install()
        {
            Console.WriteLine("installing the BGDF-Editor");
            Bowl bowl = new Bowl(@"C:\WinTools\Files\CI\BGDF\fileEditors.bgdf");
            bowl.Set(".bgdf", "BGDFEditor.dll");
            Console.WriteLine("install successful");
        }
    }
}
