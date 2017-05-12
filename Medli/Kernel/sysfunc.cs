using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Medli.System;
using Medli.Applications;

namespace Medli
{
    class Sysfunc
    {
        public static void shutdown() { machineinfo.shutdown(); }
        public static void reboot() { machineinfo.reboot(); }
        public static void clearScreen() { Console.Clear(); }
        public static uint getram = machineinfo.getRam();
        public static void ram()
        {
            Console.WriteLine("");
            Console.WriteLine("Amount of RAM installed: " + getram + " megabytes");
        }
        public static void enableACPI() { machineinfo.enableACPI(); }
    }
}
