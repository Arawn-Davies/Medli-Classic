/*
Changelog
0.10 -  Fixes/Changes   Worked on file system structure, installer and other kernel variables. 
                        An extra mkdir function is added just for the installer
        
        Removed:        unused 'version' double, only 'ver_no' is needed or used.
                        The ability to choose the installer background colour, not really needed
        
        Added:          Filesystem structure
                        New, better and cleaner installer
                        User management classes are there, just needs integrating.
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
        /// <summary>
        /// Sets the filesystems current directory to its initial value
        /// i.e. the root of the storage device, same initial value but keeps them separate
        /// </summary>
        public static string current_dir = "0:\\";
        /// <summary>
        /// Defines the root directory's value, same as current_dir's initial value but keeps them separate
        /// </summary>
        public static string root_dir = "0:\\";
        /// <summary>
        /// Creates a new instance of the virtual filesystem called fs
        /// </summary>
        Sys.FileSystem.CosmosVFS fs;
        /// <summary>
        /// Initial kernel method, overrides the built-in Cosmos BeforeRun method
        /// </summary>
        protected override void BeforeRun()
        {
            fs = new Sys.FileSystem.CosmosVFS();
            VFSManager.RegisterVFS(fs);
            fs.Initialize();
            Console.Clear();
            OSVars.Ver();
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
            OSVars.Ver();
        }
        /// <summary>
        /// Main kernel method that runs in a loop - Overrides the built-in Cosmos Run() method
        /// Calls the mshell.prompt() method that starts the user command line prompt.
        /// </summary>
        protected override void Run()
        {
            mshell.prompt();
        }
        /// <summary>
        /// Runs necessary checks to see if computer is running an existing installation
        /// </summary>
        public static void PreInit()
        {
            if (File.Exists(KernelVariables.sysdir + @"\" + "pcinfo.sys"))
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
            if (File.Exists(KernelVariables.sysdir + @"\" + "usrinfo.sys"))
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