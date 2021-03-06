﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Cosmos.Debug;
using Medli.SysInternal;
using AIC_Framework;

namespace Medli
{
    /// <summary>
    /// Class for the Medli installer
    /// </summary>
    class Installer
    {
        public static AConsole.ProgressBar installerProgress = new AConsole.ProgressBar(0, true);
        public static Cosmos.Debug.Kernel.Debugger mDebugger;
        /// <summary>
        /// Custom Write method for the installer console, sets the cursor position
        /// </summary>
        /// <param name="text"></param>
        public static void InstallerWrite(string text)
        {
            Console.CursorLeft = 7;
            Console.Write(text);
        }
        /// <summary>
        /// Custom WriteLine method for the installer console, sets the cursor position
        /// </summary>
        /// <param name="text"></param>
        public static void InstallerWriteLine(string text)
        {
            Console.CursorLeft = 7;
            Console.WriteLine(text);
        }
        /// <summary>
        /// Simple Press Any Key To Continue method.
        /// </summary>
        public static void PAKTC()
        {
            Console.CursorTop = 23;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);    
        }
        /// <summary>
        /// The default colour for the console
        /// </summary>
        public static ConsoleColor defaultcol = ConsoleColor.Blue;
        /// <summary>
        /// Defines the ConsoleColor color so it can be changed as a variable
        /// </summary>
        /// <summary>
        /// Defines the username string but leaves it as NULL 
        /// until set by the user in the installer
        /// </summary>
        public static string username;
        public static string userpass;
        public static string rootpass;
        /// <summary>
        /// Initializes the installer and allows the user to choose a machine name
        /// Sets the machine name as a variable and writes it to the disk
        /// </summary>
        public static void MInit()
        {
            InitScreen();
            InstallerWriteLine("Medli was unable to find any info regarding your PC.");
            InstallerWriteLine("The Medli installer will now run.");
            PAKTC();
            Console.Clear();
            InitScreen();
            Run();
        }
        /// <summary>
        /// Initializes the Medli installer console screen by 
        /// setting the default colour, the title and cursor position
        /// </summary>
        /// <param name="color"></param>
        public static void InitScreen()
        {
            
            Console.BackgroundColor = defaultcol;
            Console.Clear();
            //TODO
            installerProgress.Draw();
            installerProgress.Increment();
            installerProgress.Increment();
            installerProgress.Increment();
            installerProgress.Increment();
            installerProgress.Increment();
            installerProgress.Increment();
            installerProgress.Increment();
            installerProgress.Increment();
            installerProgress.Increment();
            installerProgress.Increment();
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Medli Installer ");
            Console.BackgroundColor = defaultcol;
            Console.ForegroundColor = ConsoleColor.White;

            Console.CursorLeft = 7;
            Console.CursorTop = 7;
        }
        /// <summary>
        /// Main installer method, choose colour of installer, choose desired username and reports if a FAT error occurs
        /// </summary>
        public static void Run()
        {
            InitScreen();
            InstallerWriteLine("Welcome to the Medli installer.");
            PAKTC();
            InitScreen();
            Mksysdir();
            InitScreen();
            
                InstallerWrite("Enter new account name: ");
                string usrname = Console.ReadLine();
                KernelVariables.username = usrname;
                InstallerWrite("Enter the new account password: ");
                string pass = Console.ReadLine();
                InstallerWrite("Enter the new account type (guest, normal, root): ");
                string user_type = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White; InstallerWriteLine("Creating user account...");
            try
            {
                Accounts.UserManagement.CreateUser(usrname, pass, user_type);
                Console.ForegroundColor = ConsoleColor.Green; Console.Write("\t\tDone!"); Console.ForegroundColor = ConsoleColor.White;
                InstallerWrite("Enter the root password: ");
                MEnvironment.rootpass = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White; InstallerWriteLine("Writing root password...");
                File.WriteAllText(MEnvironment.rpf, AIC_Framework.Crypto.MD5.hash(MEnvironment.rootpass));
                //MEnvironment.WriteRootPass();
                Console.ForegroundColor = ConsoleColor.Green; Console.Write("\t\tDone!"); Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception ex)
            {
                Bluescreen.Init(ex, true);
                Console.ReadKey(true);
            }
            Console.CursorTop = 10;
            Console.ForegroundColor = ConsoleColor.White;
            InstallerWriteLine("All set! Press any key to continue...");
            Console.CursorLeft = 0;
            Console.CursorTop = 24;
            Console.ReadKey();
            KernelVariables.username = username;
            Console.Clear();
            InitScreen();
            InstallerWriteLine("Please enter a machine name:");
            Console.CursorTop = 24;
            KernelVariables.pcname = Console.ReadLine();
            InitScreen();
            try
            {
                Console.ForegroundColor = ConsoleColor.White; InstallerWrite("Creating machineinfo file...  "); File.Create(KernelVariables.pcinfo).Dispose(); Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("\t\tDone!");
                Console.ForegroundColor = ConsoleColor.White; InstallerWrite("Writing machineinfo to file..."); File.WriteAllText(KernelVariables.pcinfo, KernelVariables.pcname); Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("\t\tDone!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch
            {
                Console.ReadKey(true);
                ErrorHandler.BlueScreen.Init(5, @"The Installer was unable to create the user directory and other files. 
This may be due to an unformatted hard drive or some other error", "FAT Error");
            }

            //Console.WriteLine("Excellent! Please enter who this copy of Medli is registered to:");
            //KernelVariables.regname = Console.ReadLine();
            //File.Create(Kernel.current_dir + "reginfo.sys");
            //File.WriteAllText(Kernel.current_dir + "reginfo.sys", KernelVariables.regname);

            Console.Clear();
            InitScreen();
            InstallerWriteLine("Awesome - you're all set!");
            InstallerWriteLine("Press any key to start Medli!");
            Console.CursorTop = 24;
            Console.ReadKey(true);
            Console.CursorTop = 0;
            Console.CursorLeft = 0;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
        }
        /// <summary>
        /// Creates the system directories required
        /// For now, a *nix file system is used
        /// </summary>
        public static void Mksysdir()
        {
            InitScreen();
            InstallerWriteLine("Creating system directories...");
            try
            {
                Fsfunc.mksysdir(KernelVariables.etcdir); InstallerWriteLine(@"\etc     done!");
                Fsfunc.mksysdir(KernelVariables.bindir); InstallerWriteLine(@"\bin     done!");
                Fsfunc.mksysdir(KernelVariables.sbindir); InstallerWriteLine(@"\sbin    done!");
                //Fsfunc.mksysdir(KernelVariables.procdir); InstallerWriteLine(@"\proc   done!");
                Fsfunc.mksysdir(KernelVariables.usrdir); InstallerWriteLine(@"\usr     done!");
                Fsfunc.mksysdir(KernelVariables.homedir); InstallerWriteLine(@"\home    done!");
                Fsfunc.mksysdir(KernelVariables.rootdir); InstallerWriteLine(@"\root    done!");
                Fsfunc.mksysdir(KernelVariables.tmpdir); InstallerWriteLine(@"\tmp     done!");
                Fsfunc.mksysdir(KernelVariables.vardir); InstallerWriteLine(@"\var     done!");
                Fsfunc.mksysdir(KernelVariables.sysdir); InstallerWriteLine(@"\sys     done!");
                Fsfunc.mksysdir(KernelVariables.libdir); InstallerWriteLine(@"\lib     done!");
                Fsfunc.mksysdir(KernelVariables.optdir); InstallerWriteLine(@"\opt     done!");
                Fsfunc.mksysdir(KernelVariables.devdir); InstallerWriteLine(@"\dev     done!");
                PAKTC();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
