/*
Changelog
0.3.1 - Added:              Reworked usermanagement, created an IDE and new script system
        
        Fixes/Changes       Installer, worked on including AIC more, other stuff here and there

        What to see next:   
  
 */
//Default using directives
using System;
using System.Collections.Generic;
using System.Text;

//Cosmos-specific using directives
using Sys = Cosmos.System;
using System.IO;
using Cosmos.System.FileSystem.VFS;

// Medli-specific using directives
using Medli.System;
using Medli.Applications;
using AIC_Framework;
using Medli.Accounts;

namespace Medli
{ 
    public class Kernel : Sys.Kernel
    {
        public static bool testing = true;
        
        /// <summary>
        /// Creates a new instance of the virtual filesystem called fs
        /// </summary>
        public static Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
        /// <summary>
        /// Initial kernel method, overrides the built-in Cosmos BeforeRun method
        /// </summary>
        protected override void BeforeRun()
        {
            Serial.InitializeSerial();
            Serial.Write_serial('W');
            Serial.Write_serial('e');
            Serial.Write_serial('l');
            Serial.Write_serial('c');
            Serial.Write_serial('o');
            Serial.Write_serial('m');
            Serial.Write_serial('e');
			Serial.Write_serial('\n');
			Serial.Write_serial('\r');
			Serial.Write_serial('t');
			Serial.Write_serial('o');
			Serial.Write_serial('\n');
			Serial.Write_serial('\r');
			Serial.Write_serial('M');
            Serial.Write_serial('e');
            Serial.Write_serial('d');
            Serial.Write_serial('l');
            Serial.Write_serial('i');
			
            VFSManager.RegisterVFS(fs);
            fs.Initialize();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            KernelVariables.Ver();
            //Creates the default user account.
            Account.Accounts.Add(new Account("default", "default", UserType.Normal));

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
            ShellInfo.AdminShell.Run(ShellInfo.AdminShell.no_shell);
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