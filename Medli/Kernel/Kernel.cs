/*
Changelog
0.11 -  Fixes/Changes   Fixed references to Medli.Hardware (Ring HAL) which is not allowed in the Kernel project (Ring User)
                        Version number is now stored as a string - easier to differentiate between versions 0.1 and 0.10
        
        Added:          Clock class with method in shell.

                        A small boolean added for development purposes, bypasses the installation 
                        procedure which is useful in cases where the testing of a feature is required.
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
        public static bool testing = true;
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