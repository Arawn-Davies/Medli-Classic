using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medli
{
    public class OSVars
    {
        public static string pcinfo = Kernel.root_dir + "pcinfo.sys";
        public static string usrinfo = Kernel.root_dir + "usrinfo.sys";
        //public static string reginfo = Kernel.root_dir + "reginfo.sys";
        public static string regname;
        public static string pcname;
        public static string username;
        public static double version;
        public static double ver_no = 0.9;
        public static string logo = @"
 /------\   /-------- /------\  ||        ---------- 
/|  ||  |\  ||        ||    ||  ||            ||     
||  ||  ||  ||        ||    ||  ||            ||     
||  ||  ||  |------   ||    ||  ||            ||     
||      ||  ||        ||    ||  ||            ||     
||      ||  ||        ||    ||  ||            ||     
||      ||  \-------- \------/  \-------- ----------";

        public static string wlcm1 = "Medli - Version " + OSVars.ver_no;
        public static string wlcm2 = "Maintained by Arawn Davies under the MIT License";
        //public static string wlcm3 = "This copy of Medli-core is registered to: ";

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
