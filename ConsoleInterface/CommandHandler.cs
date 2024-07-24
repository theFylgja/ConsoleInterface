using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization.Configuration;

namespace ConsoleInterface
{
    public class Handler
    {
        public static void CIHandle(Command cmd)
        {
            switch(cmd.command[1])
            {
                case "print":
                    Next.Adv(cmd.command[2]);
                    break;
                case "settings":

                    break;
                default:
                    break;
            }
        }

        public class IO
        {
            public static void MountDirectory(Command cmd)
            {
                Server.RootPath = cmd.command[1];
            }
        }
    }
}
