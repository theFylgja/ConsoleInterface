using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace ConsoleInterface
{
    public class Handler
    {
        public static void CIHandle(Command cmd)
        {
            switch(cmd.command[1])
            {
                case "get":
                    if (cmd.command[2] == "dev")
                    {
                        Next.Text($"current version: {Server.VersionInfo.DevVersion}");
                    }
                    else if (cmd.command[2] == "public")
                    {
                        Next.Text($"current version: {Server.VersionInfo.PublicVersion}");
                    }
                    break;
                case "print":
                    Next.Adv(cmd.command[2]);
                    break;
                case "sett":
                    Settings.SettingCommandHandler(cmd);
                    break;
                case "ox":
                    IO.FileSystemHandler(new Command($"fs {Server.RootPath} opex"));
                    break;
                case "opex":
                    IO.FileSystemHandler(new Command($"fs {Server.RootPath} opex"));
                    break;
                case "dumpc":
                    ConsoleDumper.DumpConsoleContents(@"C:\WinTools\Files\CI\dump.txt");
                    Next.Adv(@"your dump file can now be found at: C:\WinTools\Files\CI\dump.txt");
                    Process.Start("explorer.exe", @"C:\WinTools\Files\CI");
                    break;
                default:
                    if (File.Exists(cmd.command[1]))
                    {
                        LoadScript(cmd);
                    }
                    break;
            }
        }
        
        public static void LoadScript(Command cmd)
        {
            if (cmd.command[2] != "lsc")
            {
                return;
            }
            try
            {
                StackController.CompileScript(cmd.command[1]);
            }
            catch
            {
                Next.Err("loading of script has failed.");
            }
        }

        public class IO
        {
            public static void MountDirectory(Command cmd)
            { 
                Server.RootPath = cmd.command[1].Length == 2 && cmd.command[1].Substring(1, 1) == ":" ? cmd.command[1] + @"\" : (Directory.Exists(cmd.command[1]) ? cmd.command[1] : Server.RootPath);
                Visualizer.Call(Server.RootPath);
            }

            public static void VarHandler(Command cmd)
            {
                try
                {
                    Server.Var.Set(cmd.command[1], cmd.command[3] ?? (string)Server.Settings.Get("homeDirectory"));
                    Next.Adv($"created variable {cmd.command[1]} and set the value");
                }
                catch
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
                        catch
                        {
                            Next.Err("starting of process failed");
                        }
                    }
                    else
                    {
                        Next.Adv("opening the file");
                        FileAccessProvider.HandleFileAccess(cmd);
                    }
                }
                switch(cmd.command[2])
                {
                    case "del":
                        try
                        {
                            File.Delete(cmd.command[1]);
                            Next.Adv("successfully deleted the file");
                        }
                        catch { Next.Err("the operation has failed"); }
                        try
                        {
                            Directory.Delete(cmd.command[1]);
                            Next.Adv("successfully deleted the directory");
                        }
                        catch { Next.Err("the operation has failed"); }
                        break;
                    case "cref":
                        if (!File.Exists(Server.RootPath + @"\" + cmd.command[1]))
                        {
                            try
                            {
                                File.Create(Server.RootPath + @"\" + cmd.command[1]);
                                Next.Adv("successfully created file");
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
                                Next.Adv("successfully created directory");
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
                            Process.Start("explorer.exe", cmd.command[1]);
                        }
                        catch { }
                        break;
                    case "unzip":
                        cmd.command[3] = cmd.command[3] ?? Server.RootPath;
                        try
                        {
                            ZipFile.ExtractToDirectory(cmd.command[1], cmd.command[3]);
                            Next.Adv("operation successful");
                        }
                        catch 
                        {
                            Next.Err("operation failed");
                        }
                        break;
                    case "zip":
                        cmd.command[3] = cmd.command[3] ?? Server.RootPath;
                        try
                        {
                            ZipFile.CreateFromDirectory(cmd.command[1], $@"{cmd.command[3]}\{new DirectoryInfo(cmd.command[1]).Name}.zip");
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
                    case "upd":
                        Updater(cmd);
                        break;
                    case "kraken":
                        Updater(cmd);
                        break;
                    default:
                        Next.Err("command not found");
                        break;
                }
            }

            public static void Updater(Command cmd)
            {
                switch(cmd.command[2])
                {
                    case "check":
                        CheckForNewVersion();
                        break;
                    case "getn":
                        if(!CheckForNewVersion())
                        {
                            Next.Adv("you are already up to date");
                            break;
                        }
                        GetNewVersion(); 
                        break;
                    default:
                        break;
                }
            }
            public static bool CheckForNewVersion()
            {
                try
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFile("https://raw.githubusercontent.com/theFylgja/ConsoleInterface/refs/heads/development/ConsoleInterface/bin/Release/version.txt", @"C:\\WinTools\Files\CI\Cache\checkVersion.txt");
                        if (File.ReadAllText(@"C:\WinTools\Files\CI\Cache\checkVersion.txt") != Server.VersionInfo.DevVersion)
                        {
                            Next.Adv($"a new version is available ({File.ReadAllText(@"C:\WinTools\Files\CI\Cache\checkVersion.txt")})");
                            File.Delete(@"C:\WinTools\Files\CI\Cache\checkVersion.txt");
                            return true;
                        }
                        else
                        {
                            Next.Adv("your files seem to be up to date");
                            File.Delete(@"C:\WinTools\\Files\CI\Cache\checkVersion.txt");
                            return false;
                        }
                    }
                }
                catch
                {
                    return false;
                }
            }
            public static void GetNewVersion()
            {
                try
                {
                    using (var client = new WebClient())
                    {
                        Next.Adv("downloading new contents...");
                        client.DownloadFile("https://github.com/theFylgja/ConsoleInterface/raw/refs/heads/development/ConsoleInterface/bin/Release/ConsoleInterface.dll", @"C:\WinTools\Files\CI\Cache\ConsoleInterface.dll");
                        Next.Adv("you'll have to stop the application and move the dll file into your application directory");
                        Process.Start("explorer.exe", @"C:\WinTools\Files\CI\Cache");
                    }
                }
                catch { }
            }

            public static void OpenWebLink(string url)
            {
                try
                {
                    url = Server.Var.Exists(url) ? (string)Server.Var.Get(url) : url;
                    if(!Server.Var.Exists(url) && url.Length < 12)
                    {
                        url = @"https://www." + url;

                        if (url.Substring(0, 12) != @"https://www.")
                        {
                            if (url.Substring(0, 4) != @"www.")
                            {
                                url = @"www." + url;
                            }
                            url = @"https://" + url;

                        }
                    }
                    Next.Debug(url);
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    });
                }
                catch{}
            }
        }

        public class Settings
        {
            public static void SettingCommandHandler(Command cmd)
            {
                switch(cmd.command[1])
                {
                    case "set":
                        try
                        {
                            Server.Settings.Set(cmd.command[2], cmd.command[3] ?? (string)Server.Settings.Get(cmd.command[2]));
                        }
                        catch
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
