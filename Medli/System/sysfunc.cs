using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Medli.System;
using Medli.Applications;

namespace Medli.SysInternal
{
    class Sysfunc
    {
        public static void shutdown()
        {
            CoreFunc.Shutdown();
        }
        public static void reboot() { CoreFunc.Reboot(); }
        public static void clearScreen() { Console.Clear(); }
        public static uint getram = CoreFunc.GetRam();
        public static void ram()
        {
            Console.WriteLine("");
            Console.WriteLine("Amount of RAM installed: " + getram + " megabytes");
        }
        //public static void enableACPI() { machineinfo.enableACPI(); }
    }
}
