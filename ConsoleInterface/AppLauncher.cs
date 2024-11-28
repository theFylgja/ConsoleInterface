using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInterface
{
    public class AppLauncher
    {
        public static string RemoveSpaces(string input)
        {
            char[] chars = input.ToCharArray();
            string output = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (chars[i] == ' ' || chars[i] == '_' || chars[i] == '-' || chars[i] == '.')
                {

                }
            }
        }
        public static string[] Shift(string[] input) 
        {
            for (int i = 0; i < input.Length; i++) 
            {
                input[i] = input[i].ToLower();
                input
            }
            return input;
        }
        public static bool ContainsString(string input, string comparison)
        {
            return input.Contains(comparison);
        }
    }
}
