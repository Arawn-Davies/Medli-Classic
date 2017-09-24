using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medli
{
    public class OSVars
    {
        /// <summary>
        /// Defines where the PC information is stored as a file
        /// with it's location stored as a string
        /// </summary>
        public static string pcinfo = Kernel.root_dir + "pcinfo.sys";

        /// <summary>
        /// Defines where the user information is stored as a file,
        /// with it's location stored as a string
        /// </summary>
        public static string usrinfo = Kernel.root_dir + "usrinfo.sys";

        /// <summary>
        /// Won't be defined until registration is set up
        /// Left as null for now
        /// </summary>
        //public static string reginfo = Kernel.root_dir + "reginfo.sys";
        public static string regname;

        /// <summary>
        /// See 'Installer.MInit' for first declaration, shares the same value
        /// - Redefined for global usage in Medli
        /// </summary>
        public static string pcname;

        /// <summary>
        /// See 'Installer.username' - same string and value
        /// - Redefined for global usage in Medli
        /// </summary>
        public static string username;

        /// <summary>
        /// The Medli version is stored as a double
        /// </summary>
        public static double version;

        /// <summary>
        /// The Medli version number is stored as a double
        /// Internal memo - why two?
        /// </summary>
        public static double ver_no = 0.9;

        /// <summary>
        /// The Medli logo is stored as an escaped string,
        /// required for the use of backslashes and newlines
        /// </summary>
        public static string logo = @"
 /------\   /-------- /------\  ||        ---------- 
/|  ||  |\  ||        ||    ||  ||            ||     
||  ||  ||  ||        ||    ||  ||            ||     
||  ||  ||  |------   ||    ||  ||            ||     
||      ||  ||        ||    ||  ||            ||     
||      ||  ||        ||    ||  ||            ||     
||      ||  \-------- \------/  \-------- ----------";

        /// <summary>
        /// Welcome line one - 
        /// gets displayed on the welcome screen at first boot
        /// </summary>
        public static string wlcm1 = "Medli - Version " + OSVars.ver_no;
        /// <summary>
        /// Welcome line two -
        /// gets displayed after wlcm1 on the welcome screen at first boot
        /// </summary>
        public static string wlcm2 = "Maintained by Arawn Davies under the MIT License";
        //public static string wlcm3 = "This copy of Medli-core is registered to: ";

        /// <summary>
        /// The welcome lines one and two (and three at a later stage)
        /// are printed onto the screen with different foreground colours.
        /// </summary>
        public static void ver()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(wlcm1);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(wlcm2);
            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine(wlcm3);

            //Update 0.5 - Registration has been disabled because it's not production and in early stages,
            //plus this is an open-source project so having registration is a bit of a joke :P

            #region regsetup
            /*
            //Insert total asshattery here >>>
            if (File.Exists(OSVars.reginfo))
            {
                string[] lines = File.ReadAllLines(OSVars.reginfo);
                foreach (string line in lines)
                {
                    //     \/ Gotta be careful here hehehe \/
                    Console.Write(line + "\n");
                }
            }
            else
            {
                Console.Write("-=UNREGISTERED=-\n");
            }
            
            */
            #endregion

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(OSVars.logo);
            Console.WriteLine("Powered by the C# Open Source Managed Operating System");
            Console.ForegroundColor = ConsoleColor.White;


        }
    }
}
