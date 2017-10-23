using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medli.SysInternal
{
    /// <summary>
    /// A class which holds the path of system directories in strings
    /// It also contains the method for creating these system directories.
    /// </summary>
    public class KernelVariables
    {
        public static string root = Environment.root_dir;

        #region systemdir
        
        /// <summary>
        /// etc system directory
        /// </summary>
        public static string etcdir = root + "etc";
        /// <summary>
        /// bin system directory
        /// </summary>
        public static string bindir = root + "bin";
        /// <summary>
        /// sbin system directory
        /// </summary>
        public static string sbindir = root  + @"sbin";
        /// <summary>
        /// usr system directory - not to be confused with /home user directorie
        /// </summary>
        public static string usrdir = root  + @"usr\";
        /// <summary>
        /// home system directory - holds user directories except from root
        /// </summary>
        public static string homedir = root + @"home\";
        /// <summary>
        /// root user directory
        /// </summary>
        public static string rootdir = root + @"root\";
        /// <summary>
        /// tmp system directory - stores tempory files and folders
        /// </summary>
        public static string tmpdir = root + @"tmp\";
        /// <summary>
        /// var system directory
        /// </summary>
        public static string vardir = root + @"var\";
        /// <summary>
        /// sys system directory
        /// </summary>
        public static string sysdir = root  + @"sy\s";
        /// <summary>
        /// lib system directory
        /// </summary>
        public static string libdir = root + @"lib\";
        /// <summary>
        /// opt system directory - for application libraries
        /// </summary>
        public static string optdir = root + @"opt\";
        /// <summary>
        /// dev system directory
        /// </summary>
        public static string devdir = root + @"dev\";
        #endregion
        #region KernelVariables
        /// <summary>
        /// Defines where the PC information is stored as a file
        /// with it's location stored as a string
        /// </summary>
        public static string pcinfo = KernelVariables.sysdir + @"\pcinfo.sys";

        /// <summary>
        /// Defines where the user information is stored as a file,
        /// with it's location stored as a string
        /// </summary>
        public static string usrinfo = KernelVariables.sysdir + @"\usrinfo.sys";

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
        /// The Medli version number is stored as a string
        /// </summary>
        public static string ver_no = "0.1.5";

        /// <summary>
        /// The Medli logo is stored as an escaped string,
        /// required for the use of backslashes and newlines
        /// </summary>
        public static string logo = @"
 _________   _______  ______            ____       ______    _____
/ __   __ \ / /_____ |  __  \ | |      |_  _|     | ____ |  | ____|
| | | | | | | |      | |  | | | |        ||       ||    ||  ||
| | | | | | | |_____ | |  | | | |        ||  ___  ||    ||  ||____
| | |_| | | |  _____ | |  | | | |        || |___| ||    ||  |____ |
| |     | | | |      | |  | | | |        ||       ||    ||       ||
| |     | | | |_____ | |__| | | |_____  _||_      ||____||   ____||
|_|     |_| \_______ |______/ \_______ |____|     |______|  |_____|";
        /// <summary>
        /// Welcome line one - 
        /// gets displayed on the welcome screen at first boot
        /// </summary>
        public static string wlcm1 = "Medli - Version " + KernelVariables.ver_no;
        /// <summary>
        /// Welcome line two -
        /// gets displayed after wlcm1 on the welcome screen at first boot
        /// </summary>
        public static string wlcm2 = "Maintained by Arawn Davies under the MIT License";
        //public static string wlcm3 = "This copy of Medli-core is registered to: ";
        #endregion
        /// <summary>
        /// The welcome lines one and two (and three at a later stage)
        /// are printed onto the screen with different foreground colours.
        /// </summary>
        public static void Ver()
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
            if (File.Exists(KernelVariables.reginfo))
            {
                string[] lines = File.ReadAllLines(KernelVariables.reginfo);
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
            Console.WriteLine(KernelVariables.logo);
            Console.WriteLine("Powered by the C# Open Source Managed Operating System");
            Console.ForegroundColor = ConsoleColor.White;


        }
    }
}
