/*
Changelog
0.9 - Updated project dependencies, general fixes so it works on the userkit, can start developing again!
      Will start making the documentation for Medli, makes it easier to understand the source code for 
      what's going on with the internals, describing what methods do etc.
*/
using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.System.FileSystem.VFS;
using Sys = Cosmos.System;
using System.IO;
using Medli.System;
using Medli.Applications;

namespace Medli
{ 
    public class Kernel : Sys.Kernel
    {
        public static string current_dir = "0:\\";
        public static string root_dir = "0:\\";
        Sys.FileSystem.CosmosVFS fs;
        protected override void BeforeRun()
        {
            fs = new Sys.FileSystem.CosmosVFS();
            VFSManager.RegisterVFS(fs);
            fs.Initialize();
            Console.Clear();
            OSVars.ver();
            //This is just to identify the users machine, much later in Medli will this have any usage however
            //i.e. not until networking is set up, FS Permissions are working etc...
            //If it wasn't able to find a machinename file, then it will try and create one.
            //This is why initializing the filesystem is vital before executing this code.

            /*
            if (File.Exists(OSVars.reginfo))
            {
                string[] lines = File.ReadAllLines(OSVars.reginfo);
                foreach (string line in lines)
                {
                    OSVars.regname = line;
                }
                */

            PreInit();
            

            Console.Clear();
            OSVars.ver();
        }
        protected override void Run()
        {
            mshell.prompt();
        }
        public static void PreInit()
        {
            if (File.Exists(Kernel.current_dir + "pcinfo.sys"))
            {
                try
                {
                    string[] pcnames = File.ReadAllLines(OSVars.pcinfo);
                    foreach (string pcname in pcnames)
                    {
                        OSVars.pcname = pcname;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
            if (File.Exists(Kernel.current_dir + "usrinfo.sys"))
            {
                try
                {
                    string[] usernames = File.ReadAllLines(OSVars.usrinfo);
                    foreach (string username in usernames)
                    {
                        OSVars.username = username;
                        Console.WriteLine("Welcome back, " + OSVars.username + @"!");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey(true);
                    }
                }
                catch
                {

                }
            }
            else
            { 
                Installer.MInit();
            }
        }
    }
}