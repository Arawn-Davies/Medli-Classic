using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Cosmos.Debug;
using Medli.SysInternal;

namespace Medli
{
    /// <summary>
    /// Class for the Medli installer
    /// </summary>
    class Installer
    {
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
            InstallerWriteLine("Press any key to continue...");
            Console.ReadKey(true);    
        }
        /// <summary>
        /// The default colour for the console
        /// </summary>
        public static ConsoleColor defaultcol = ConsoleColor.Black;
        /// <summary>
        /// Defines the ConsoleColor color so it can be changed as a variable
        /// </summary>
        public static ConsoleColor color;
        /// <summary>
        /// Defines the username string but leaves it as NULL 
        /// until set by the user in the installer
        /// </summary>
        public static string username;
        /// <summary>
        /// Initializes the installer and allows the user to choose a machine name
        /// Sets the machine name as a variable and writes it to the disk
        /// </summary>
        public static void MInit()
        {
            InitScreen(defaultcol);
            InstallerWriteLine("Medli was unable to find any info regarding your PC.");
            InstallerWriteLine("The Medli installer will now run.");
            PAKTC();
            Console.Clear();
            InitScreen(defaultcol);
            Run();
        }
        /// <summary>
        /// Initializes the Medli installer console screen by 
        /// setting the default colour, the title and cursor position
        /// </summary>
        /// <param name="color"></param>
        public static void InitScreen(ConsoleColor color)
        {
            Console.BackgroundColor = color;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Medli Installer");
            Console.BackgroundColor = color;
            Console.CursorLeft = 7;
            Console.CursorTop = 7;
        }
        /// <summary>
        /// Main installer method, choose colour of installer, choose desired username and reports if a FAT error occurs
        /// </summary>
        public static void Run()
        {
            InitScreen(defaultcol);
            InstallerWriteLine("Welcome to the Medli installer.");
            PAKTC();
            InitScreen(defaultcol);
            Console.CursorTop = 24;
            Console.CursorLeft = 0;
            color = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.Blue;
            InitScreen(color);
            Mksysdir();
            InitScreen(color);
            InstallerWriteLine("Enter a username for Medli:");
            Console.CursorTop = 24;
            Console.CursorLeft = 0;
            username = Console.ReadLine();
            InitScreen(color);
            try
            {
                Console.CursorTop = 7;
                Console.ForegroundColor = ConsoleColor.White; InstallerWrite("Creating user directory... "); Directory.CreateDirectory(KernelVariables.homedir + @"\" + username); Console.ForegroundColor = ConsoleColor.Green; Console.Write("\t\tDone!");
                Console.CursorTop = 8;
                mDebugger = new Cosmos.Debug.Kernel.Debugger("User", "Kernel");
                mDebugger.Send(OSVars.usrinfo);
                //Not needed when using File.Append - creates file anyway if file doesn't exist.
                //Console.ForegroundColor = ConsoleColor.White; InstallerWrite("Creating users file...     "); File.Create(OSVars.usrinfo).Dispose(); Console.ForegroundColor = ConsoleColor.Green; Console.Write("\t\tDone!");
                Console.CursorTop = 9;
                Console.ForegroundColor = ConsoleColor.White; InstallerWrite("Writing username to file..."); File.AppendAllText(OSVars.usrinfo, username); Console.ForegroundColor = ConsoleColor.Green; Console.Write("\t\tDone!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch
            {
                Console.ReadKey(true);
                ErrorHandler.BlueScreen.Init(5, @"The Installer was unable to create the user directory and other files. 
This may be due to an unformatted hard drive or some other error", "FAT Error");
            }
            Console.CursorTop = 10;
            Console.ForegroundColor = ConsoleColor.White;
            InstallerWriteLine("All set! Press any key to continue...");
            Console.CursorLeft = 0;
            Console.CursorTop = 24;
            Console.ReadKey();
            OSVars.username = username;
            Console.BackgroundColor = color;
            Console.Clear();
            InitScreen(color);
            InstallerWriteLine("Please enter a machine name:");
            Console.CursorTop = 24;
            OSVars.pcname = Console.ReadLine();
            InitScreen(color);
            try
            {
                Console.ForegroundColor = ConsoleColor.White; InstallerWrite("Creating machineinfo file...  "); File.Create(OSVars.pcinfo).Dispose(); Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("\t\tDone!");
                Console.ForegroundColor = ConsoleColor.White; InstallerWrite("Writing machineinfo to file..."); File.WriteAllText(OSVars.pcinfo, OSVars.pcname); Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("\t\tDone!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch
            {
                Console.ReadKey(true);
                ErrorHandler.BlueScreen.Init(5, @"The Installer was unable to create the user directory and other files. 
This may be due to an unformatted hard drive or some other error", "FAT Error");
            }

            //Console.WriteLine("Excellent! Please enter who this copy of Medli is registered to:");
            //OSVars.regname = Console.ReadLine();
            //File.Create(Kernel.current_dir + "reginfo.sys");
            //File.WriteAllText(Kernel.current_dir + "reginfo.sys", OSVars.regname);

            Console.Clear();
            InitScreen(defaultcol);
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
            InitScreen(color);
            InstallerWriteLine("Creating system directories...");
            try
            {
                Fsfunc.mksysdir(KernelVariables.etcdir); InstallerWriteLine(@"\etc     done!");
                Fsfunc.mksysdir(KernelVariables.bindir); InstallerWriteLine(@"\bin     done!");
                Fsfunc.mksysdir(KernelVariables.sbindir); InstallerWriteLine(@"\sbin   done!");
                Fsfunc.mksysdir(KernelVariables.procdir); InstallerWriteLine(@"\proc   done!");
                Fsfunc.mksysdir(KernelVariables.usrdir); InstallerWriteLine(@"\usr     done!");
                Fsfunc.mksysdir(KernelVariables.homedir); InstallerWriteLine(@"\home   done!");
                Fsfunc.mksysdir(KernelVariables.rootdir); InstallerWriteLine(@"\root   done!");
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
