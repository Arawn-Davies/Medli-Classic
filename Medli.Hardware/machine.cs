using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.Core;

namespace Medli.Hardware
{
    public class Machine
    {
        public static void EnableACPI() { Power.Enable(); }
        public static uint GetRam() { return CPU.GetAmountOfRAM(); }
        public static void ShutDown()
        {
            Power.Shutdown();
            Power.Disable();
            Global.CPU.Halt();
        }
        public static void Reboot() { Power.Reboot(); }
    }
}
