using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medli;
using System.IO;

namespace Medli.Installer
{
    public class Vars
    {
        public static string pcname;
        public static string username;
    }
    public class Install
    { 
        public static void PAKTC()
        {
            Console.CursorTop = 24;
            Console.ReadKey(true);
        }
        public static ConsoleColor defaultcol = ConsoleColor.Black;
        public static ConsoleColor color;
        public static string username

        public static void MInit()
        {
            Console.WriteLine("Medli was unable to find any info regarding your PC.");
            Console.WriteLine("The Medli installer will now run.");
            PAKTC();
            Console.Clear();
            InitScreen(defaultcol);
            Run();
            InitScreen(defaultcol);
            Console.WriteLine("Press any key and let's get started!");
            PAKTC();
            Console.WriteLine("Please enter a machine name:");
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
            Console.WriteLine("Awesome - you're all set!");
            Console.WriteLine("Press any key to start Medli!");
            PAKTC();
            Console.Clear();
        }

        public static void InitScreen(ConsoleColor color)
        {

            Console.BackgroundColor = color;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Medli Installer");
            Console.BackgroundColor = defaultcol;
            Console.CursorLeft = 10;
            Console.CursorTop = 7;
        }
        public static void Run()
        {
            InitScreen(defaultcol);
            Console.WriteLine("Welcome to the Medli installer.");
            Console.CursorLeft = 20;
            Console.WriteLine("Press any key to get started!");
            PAKTC();
            InitScreen(ConsoleColor.Black);
            Console.WriteLine("Choose a background colour to use with Medli:");

            Console.CursorLeft = 20; Console.BackgroundColor = ConsoleColor.Yellow; Console.Write("Yellow, ");
            Console.BackgroundColor = ConsoleColor.Cyan; Console.Write("Cyan,"); Console.BackgroundColor = defaultcol; Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.Green; Console.Write("Green,"); Console.BackgroundColor = defaultcol; Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.Blue; Console.Write("Blue,"); Console.BackgroundColor = defaultcol; Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.Red; Console.WriteLine("Red,"); Console.BackgroundColor = defaultcol; Console.Write(" ");
            Console.BackgroundColor = defaultcol; Console.Write("Black");
            Console.CursorTop = 24;
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
            Console.WriteLine("Enter a username for Medli:");
            Console.CursorTop = 24;
            Console.CursorLeft = 0;
            username = Console.ReadLine();
            InitScreen(color);
            try
            {
                Console.ForegroundColor = ConsoleColor.White; Console.Write("Creating user directory... "); Directory.CreateDirectory(Kernel.root_dir + "/" + username); Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("\t\tDone!");
                Console.CursorLeft = 20;
                Console.CursorTop = 8;
                Console.ForegroundColor = ConsoleColor.White; Console.Write("Creating users file...     "); File.Create(Kernel.current_dir + "usrinfo.sys"); Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("\t\tDone!");
                Console.CursorLeft = 20;
                Console.CursorTop = 9;
                Console.ForegroundColor = ConsoleColor.White; Console.Write("Writing username to file..."); File.WriteAllText(Kernel.current_dir + "usrinfo.sys", username); Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("\t\tDone!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch
            {
                ErrorHandler.BlueScreen.Init(5, @"The Installer was unable to create the user directory and other files. 
This may be due to an unformatted hard drive or some other error", "FAT Error");
            }
            Console.ForegroundColor = defaultcol;
            Console.CursorLeft = 20;
            Console.CursorTop = 10;
            Console.WriteLine("All set! Press any key to continue...");
            Console.CursorLeft = 0;
            PAKTC();
            Console.Clear();
        }
    }
}
