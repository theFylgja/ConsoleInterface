using System;
using System.IO;
using BiomeLibrary;

namespace ConsoleInterface
{
    public class Next
    {
        //Important Paths
        
        //Colors
        public static ConsoleColor standard = ConsoleColor.White;
        public static ConsoleColor cmd = ConsoleColor.Blue;
        public static ConsoleColor arg = ConsoleColor.Blue;
        public static ConsoleColor title = ConsoleColor.DarkMagenta;
        public static ConsoleColor adv = ConsoleColor.Yellow;
        public static ConsoleColor err = ConsoleColor.Red;
        public static ConsoleColor listItem = ConsoleColor.Gray;
        public static ConsoleColor listEmptyError = ConsoleColor.Red;
        public static ConsoleColor debug = ConsoleColor.Cyan;

        public static string Cmd()
        {
            Console.WriteLine("");
            Console.ForegroundColor = cmd;
            Console.Write("cmd>    ");
            Console.ForegroundColor = standard;
            return Console.ReadLine();
        }
        public static string Arg()
        {
            Console.ForegroundColor = arg;
            Console.Write("arg>    ");
            Console.ForegroundColor = standard;
            return Console.ReadLine();
        }
        public static void Title(string text)
        {
            Console.ForegroundColor = title;
            Console.WriteLine(text);
            Console.WriteLine("");
            Console.ForegroundColor = standard;
        }
        public static void Adv(string text)
        {
            Console.ForegroundColor = adv;
            Console.WriteLine(text);
            Console.ForegroundColor = standard;
        }

        public static void Err(string msg)
        {
            Console.ForegroundColor = err;
            Console.WriteLine(msg);
            Console.ForegroundColor = standard;
        }

        public static void Debug(string msg)
        {
            Console.ForegroundColor = debug;
            Console.WriteLine(msg);
            Console.ForegroundColor = standard;
        }

        public static void List(string[] input)
        {
            Console.WriteLine();
            if (input != null)
            {
                Console.ForegroundColor = listItem;
                foreach (string i in input)
                {
                    Console.WriteLine(i);
                }
            }
            else
            {
                Console.ForegroundColor = listEmptyError;
                Console.WriteLine("none");
            }
            Console.ForegroundColor = standard;
            Console.WriteLine();
        }
    }

    public class BowlController
    {
        //matching string to enum
        public string MatchString(ConsoleColor color)
        {
            switch(color)
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
        public ConsoleColor MatchEnum(string input)
        {
            switch(input)
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
        public void LoadDefaults()
        {
            Next.standard = ConsoleColor.White;
            Next.cmd = ConsoleColor.Blue;
            Next.arg = ConsoleColor.Blue;
            Next.title = ConsoleColor.DarkMagenta;
            Next.adv = ConsoleColor.Yellow;
            Next.err = ConsoleColor.Red;
            Next.listItem = ConsoleColor.Gray;
            Next.listEmptyError = ConsoleColor.Red;
            Next.debug = ConsoleColor.Cyan;
        }
        public void LoadAllWhite()
        {
            Next.standard = ConsoleColor.White;
            Next.cmd = ConsoleColor.White;
            Next.arg = ConsoleColor.White;
            Next.title = ConsoleColor.White;
            Next.adv = ConsoleColor.White;
            Next.err = ConsoleColor.White;
            Next.listItem = ConsoleColor.White;
            Next.listEmptyError = ConsoleColor.White;
            Next.debug = ConsoleColor.White;
        }
    }
}
