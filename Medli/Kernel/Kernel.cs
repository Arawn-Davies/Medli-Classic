/*
Changelog
0.1.5 -  Added:             Began creating GUI project and class.
                            Updated MIV to accept a filename as an argument for launching.
                            Introduced arguments for the commandline system.
                            Updated Cowsay - now features cow, tux and sodomized-sheep!
                            Introduced ColorChanger from Apollo (or was it Chocolate? Who knows - both made by Arawn-Davies)
                            Introducing the command database, easier management for a unified method of creating and launching Medli Applications.
        
        Fixes/Changes       Skipped a few versions, due to the vast amount of changes.
                            Switched to semantic versioning.

        What to see next:   Beginning user management, make proper use of home directories and start populating them.
  
 */
using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.System.FileSystem.VFS;
using Sys = Cosmos.System;
using System.IO;
using Medli.System;
using Medli.Applications;
using Cosmos.Debug;
using Medli.SysInternal;

namespace Medli
{ 
    public class Kernel : Sys.Kernel
    {
        public static bool testing = false;
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
        Cosmos.System.FileSystem.CosmosVFS fs;
        /// <summary>
        /// Initial kernel method, overrides the built-in Cosmos BeforeRun method
        /// </summary>
        protected override void BeforeRun()
        {
            fs = new Cosmos.System.FileSystem.CosmosVFS();
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
            Shell.prompt();
        }
        /// <summary>
        /// Runs necessary checks to see if computer is running an existing installation
        /// </summary>
        public static void PreInit()
        {
            if (testing == true)
            {
                OSVars.username = "test";
                OSVars.pcname = "testing";
            }
            else if (testing == false)
            {
                if (File.Exists(OSVars.pcinfo))
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
                if (File.Exists(OSVars.usrinfo))
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
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Installer.MInit();
                }
            }
        }
    }
}