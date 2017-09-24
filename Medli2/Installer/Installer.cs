using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Medli
{
    /// <summary>
    /// Class for the Medli installer
    /// </summary>
    class Installer
    {
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
            InitScreen(defaultcol);
            InstallerWriteLine("Press any key and let's get started!");
            Console.ReadKey(true);
            InstallerWriteLine("Please enter a machine name:");
            Console.CursorTop = 24;
            OSVars.pcname = Console.ReadLine();
            InitScreen(defaultcol);
            try
            {
                Console.ForegroundColor = ConsoleColor.White; Console.Write("Creating machineinfo file...  "); File.Create(Kernel.root_dir + "pcinfo.sys"); Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("\t\tDone!");
                Console.ForegroundColor = ConsoleColor.White; Console.Write("Writing machineinfo to file..."); File.WriteAllText(Kernel.root_dir + "pcinfo.sys", OSVars.pcname); Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("\t\tDone!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch
            {
                ErrorHandler.BlueScreen.Init(5, @"The Installer was unable to create the user directory and other files. 
This may be due to an unformatted hard drive or some other error", "FAT Error");
            }

            //Console.WriteLine("Excellent! Please enter who this copy of Medli is registered to:");
            //OSVars.regname = Console.ReadLine();
            //File.Create(Kernel.current_dir + "reginfo.sys");
            //File.WriteAllText(Kernel.current_dir + "reginfo.sys", OSVars.regname);

            Console.WriteLine("");
            Console.Clear();
            InitScreen(defaultcol);
            InstallerWriteLine("Awesome - you're all set!");
            InstallerWriteLine("Press any key to start Medli!");
            Console.ReadKey(true);
            Console.Clear();
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
            Console.BackgroundColor = defaultcol;
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
            InitScreen(ConsoleColor.Black);
            Console.WriteLine("Choose a background colour to use with Medli:");
            Console.BackgroundColor = ConsoleColor.Yellow; Console.Write("Yellow, ");
            Console.BackgroundColor = ConsoleColor.Cyan;  Console.Write("Cyan,"); Console.BackgroundColor = defaultcol; Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.Green; Console.Write("Green,"); Console.BackgroundColor = defaultcol; Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.Blue; Console.Write("Blue,"); Console.BackgroundColor = defaultcol; Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.Red; Console.WriteLine("Red,"); Console.BackgroundColor = defaultcol; Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.Black; Console.Write("Black");
            Console.CursorTop = 24;
            Console.CursorLeft = 0;
            string bgcolor = Console.ReadLine();
            if (bgcolor == "yellow")
            {
                color = ConsoleColor.Yellow;
                Console.BackgroundColor = ConsoleColor.Yellow;
            }
            else if (bgcolor == "cyan")
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
                color = ConsoleColor.Cyan;
            }
            else if (bgcolor == "green")
            {
                color = ConsoleColor.Green;
                Console.BackgroundColor = ConsoleColor.Green;
            }
            else if (bgcolor == "blue")
            {
                color = ConsoleColor.Blue;
                Console.BackgroundColor = ConsoleColor.Blue;
            }
            else if (bgcolor == "red")
            {
                color = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Red;

            }
            else
            {
                color = ConsoleColor.Black;
                Console.BackgroundColor = color;
            }
            InitScreen(color);
            InstallerWriteLine("Enter a username for Medli:");
            Console.CursorTop = 24;
            Console.CursorLeft = 0;
            username = Console.ReadLine();
            InitScreen(color);
            try
            {
                Console.CursorTop = 7;
                Console.ForegroundColor = ConsoleColor.White; InstallerWrite("Creating user directory... "); Directory.CreateDirectory(Kernel.root_dir + "/" + username); Console.ForegroundColor = ConsoleColor.Green; Console.Write("\t\tDone!");
                Console.CursorTop = 8;
                Console.ForegroundColor = ConsoleColor.White; InstallerWrite("Creating users file...     "); File.Create(Kernel.current_dir + "usrinfo.sys"); Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("\t\tDone!");
                Console.CursorTop = 9;
                Console.ForegroundColor = ConsoleColor.White; InstallerWrite("Writing username to file..."); File.WriteAllText(Kernel.current_dir + "usrinfo.sys", username); Console.ForegroundColor = ConsoleColor.Green; Console.Write("\t\tDone!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch
            {
                ErrorHandler.BlueScreen.Init(5, @"The Installer was unable to create the user directory and other files. 
This may be due to an unformatted hard drive or some other error", "FAT Error");
            }
            Console.ForegroundColor = ConsoleColor.White;
            InstallerWriteLine("All set! Press any key to continue...");
            Console.CursorLeft = 0;
            Console.ReadKey();
            OSVars.username = username;
            Console.Clear();
        }
    }
}
