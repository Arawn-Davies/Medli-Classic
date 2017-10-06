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
        public static void EnableACPI() { ACPI.Enable(); }
        public static uint GetRam() { return CPU.GetAmountOfRAM(); }
        public static void ShutDown()
        {
        ACPI.Shutdown();
        ACPI.Disable();
        Global.CPU.Halt();
        }
        public static void Reboot() { ACPI.Reboot(); }
    }
}
