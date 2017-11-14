/*
Changelog
0.2.1 - Added:              Included the AIC Framework, will introduce more features for Medli
                            Introduced a class for environment methods, e.g. PressAnyKey();
        
        Fixes/Changes       Began user management, requires lists which aren't fully plugged (if list contains(string))
                            Will make proper use of home directories and start populating them.
                            Unified OSVars and KernelVariables class to have one single class of variables, strings and methods
                            Updated logo, made it slightly smaller

        What to see next:   
  
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
using AIC_Framework;

namespace Medli
{ 
    public class Kernel : Sys.Kernel
    {
        public static bool testing = false;
        
        /// <summary>
        /// Creates a new instance of the virtual filesystem called fs
        /// </summary>
        public static Sys.FileSystem.CosmosVFS fs = new Cosmos.System.FileSystem.CosmosVFS();
        /// <summary>
        /// Initial kernel method, overrides the built-in Cosmos BeforeRun method
        /// </summary>
        protected override void BeforeRun()
        {
            
            VFSManager.RegisterVFS(fs);
            fs.Initialize();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            KernelVariables.Ver();
            //This is just to identify the users machine, much later in Medli will this have any usage however
            //i.e. not until networking is set up, FS Permissions are working etc...
            //If it wasn't able to find a machinename file, then it will try and create one.
            //This is why initializing the filesystem is vital before executing this code.

            /*
            if (File.Exists(KernelVariables.reginfo))
            {
                string[] lines = File.ReadAllLines(KernelVariables.reginfo);
                foreach (string line in lines)
                {
                    KernelVariables.regname = line;
                }
                */

            PreInit();
            Console.Clear();
            KernelVariables.Ver();

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
        /// Runs necessary checks to see if computer has an existing installation
        /// </summary>
        public static void PreInit()
        {
            if (testing == true)
            {
                KernelVariables.username = "test";
                KernelVariables.pcname = "testing";
            }
            else if (testing == false)
            {
                if (File.Exists(KernelVariables.pcinfo))
                {
                    try
                    {
                        string[] pcnames = File.ReadAllLines(KernelVariables.pcinfo);
                        KernelVariables.pcname = pcnames[0];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
                if (File.Exists(KernelVariables.usrinfo))
                {
                    Console.Clear();
                    try
                    {
                        UserManagement.UserLogin();
                        AConsole.Fill(ConsoleColor.Black);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Welcome back, " + KernelVariables.username + @"!");
                        MEnvironment.PressAnyKey();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Medli encountered an exception during the pre-initialization stage.\nError: " + ex.Message);
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