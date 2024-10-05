using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

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
                Next.Adv("at cd"); 
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
                if (File.Exists(cmd.command[1]) && cmd.command[2] == null)
                {
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
                switch(cmd.command[2])
                {
                    case "del":
                        try
                        {
                            File.Delete(cmd.command[1]);
                        }
                        catch { }
                        try
                        {
                            Directory.Delete(cmd.command[1]);
                        }
                        catch { }
                        break;
                    case "cref":
                        if (!File.Exists(Server.RootPath + @"\" + cmd.command[1]))
                        {
                            try
                            {
                                File.Create(Server.RootPath + @"\" + cmd.command[1]);
                            }
                            catch 
                            {
                                Next.Err("could not create file");
                            }
                        }
                        break;
                    case "cred":
                        if (!Directory.Exists(Server.RootPath + @"\" + cmd.command[1]))
                        {
                            try
                            {
                                Directory.CreateDirectory(Server.RootPath + @"\" + cmd.command[1]);
                            }
                            catch
                            {
                                Next.Err("could not create directory");
                            }
                        }
                        break;
                    case "copyto":
                        try
                        {
                            if (File.Exists(cmd.command[1]))
                            {
                                File.Copy(cmd.command[1], cmd.command[3]);
                            }
                        }
                        catch { }
                        break;
                    case "moveto":
                        try
                        {
                            if (Directory.Exists(cmd.command[1]))
                            {
                                Directory.Move(cmd.command[1], cmd.command[3]);
                            }
                        }
                        catch { }
                        break;
                    case "opex":
                        try
                        {
                            Next.Debug("@" + cmd.command[1]);
                            Process.Start("explorer.exe", cmd.command[1]);
                        }
                        catch { }
                        break;
                    default:
                        break;
                } 
            }
        }

        public class Web
        {
            public static void WebHandler(Command cmd)
            {
                switch (cmd.command[1])
                {
                    case "url":
                        OpenWebLink(cmd.command[2]); 
                        break;
                    default:
                        Next.Err("command not found");
                        break;
                }
            }

            public static void OpenWebLink(string url)
            {
                try
                {
                    if(url.Length < 12)
                    {
                        url = @"https://www." + url;
                    }
                    if(url.Substring(0, 12) != @"https://www.")
                    {
                        if (url.Substring(0, 4) != @"www.")
                        {
                            url = @"www." + url;
                        }
                        url = @"https://" + url;
                        
                    }
                    Next.Debug(url);
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    });
                }
                catch{ Next.Debug("bro caught lackin"); }
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
