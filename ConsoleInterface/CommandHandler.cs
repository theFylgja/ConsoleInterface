using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
                case "sett":
                    Settings.SettingCommandHandler(cmd);
                    break;
                case "var":
                    IO.VarHandler(cmd);
                    break;
                default:
                    break;
            }
        }

        public class IO
        {
            public static void MountDirectory(Command cmd)
            {
                Server.RootPath = Directory.Exists(cmd.command[1]) ? cmd.command[1] : Server.RootPath;
            }

            public static void VarHandler(Command cmd)
            {
                try
                {
                    Server.Var.Set(cmd.command[1], cmd.command[3] ?? (string)Server.Settings.Get("homeDirectory"));
                }
                catch(Exception ex)
                {
                    Next.Err("invalid parameters");
                }
            }

            public static void FileSystemHandler(Command cmd)
            {
                Next.Debug("at Handler.FileSystemHandler");
                if (File.Exists(cmd.command[1]))
                {
                    Next.Debug("Existence confirmed");
                    Next.Debug(Server.GetFileExtension(cmd.command[1]));
                    if (Server.GetFileExtension(cmd.command[1]) == ".exe")
                    {
                        Next.Adv("starting application");
                        try
                        {
                            Process.Start(cmd.command[1]);
                        }
                        catch(Exception ex)
                        {
                            Next.Err("starting of process failed");
                        }
                    }
                    else
                    {
                        FileAccessProvider.HandleFileAccess(cmd);
                    }
                }
                switch(cmd.command[1])
                {
                    default:
                        break;
                }
            }
        }

        public class Settings
        {
            public static void SettingCommandHandler(Command cmd)
            {
                switch(cmd.command[2])
                {
                    case "set":
                        try
                        {
                            Server.Settings.Set(cmd.command[2], cmd.command[3] ?? (string)Server.Settings.Get(cmd.command[2]));
                        }
                        catch(Exception e)
                        {
                            Next.Err("can't set the setting");
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
